using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WeightingWhiteGlue
{
    public partial class MainForm : Form
    {
        //ww:Gross:毛重,wn:Net:净重,wt:Tare:皮重
        private SerialPort serialPort;
        private string currentWeight = "0.000";
        private string currentUnit = "kg";
        private string currentWeightType= "净重";
        private bool isStable = false;

        private bool isReadingData = false;
        private DateTime lastReadTime = DateTime.MinValue;
        private bool isBebinWeighing = false;
        private int? weighingId = 0;

        private SQLDBHelper SA = new SQLDBHelper();
        private OdbcHelper OA = new OdbcHelper();

        // 防抖相关字段
        private Timer debounceTimer = new Timer();
        private DateTime lastCommandTime = DateTime.MinValue;
        private const int DebounceInterval = 1000;
        
        private static readonly Regex WeightPattern = new Regex(@"(ww|wn|wt)\s*(-?\d*\.?\d+)(kg|g|lb)?", 
            RegexOptions.IgnoreCase | RegexOptions.Compiled);
        
        public MainForm()
        {
            InitializeComponent();
            InitShift();
            InitPlantMachine();
            InitializeSerialPort();
            lblPort.Text = "串口:" + Utils.GetParameterValue("Port");
            lblBaud.Text = "波特率:" + Utils.GetParameterValue("BaudRate");

            debounceTimer.Interval = DebounceInterval;
            debounceTimer.Tick += DebounceTimer_Tick;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateDGV();
        }

        private void UpdateDGV()
        {
            // 绑定DataGridView
            DataTable ds = SA.GetDataTable($@"SELECT TOP 1000 
[Id],[Plant],[MachineId],[Shift],[WeighingType],[WaterRate],[WeighingWeightBegin],[WeighingWeightEnd],[WeighingTimeBegin],[WeighingTimeEnd] 
FROM WeighingRecord Order By WeighingTimeBegin DESC", Utils.GetParameterValue("DBConnStr"));
            dgvRecords.DataSource = ds;
        }

        private void InitPlantMachine()
        {
            // 厂区初始化
            string plant = Utils.GetParameterValue("Plant") ?? "W6";
            List<string> plantList = Utils.GetParameterValue("Plants")?.Split('|').ToList();
            cmbPlant.Items.Clear();
            cmbPlant.Items.AddRange(plantList?.ToArray());
            cmbPlant.SelectedItem = plant;
            // 机台初始化
            List<string> machineList = Utils.GetParameterValue($"{plant}ConvertMachine")?.Split('|').ToList();
            cmbConvertMachine.Items.Clear();
            cmbConvertMachine.Items.AddRange(machineList?.ToArray());
        }

        private void InitShift()
        {
            // 班次初始化
            cmbShift.Items.Clear();
            List<ComboBoxItem> shiftList = new List<ComboBoxItem>
            {
                new ComboBoxItem("忠班", "1"),
                new ComboBoxItem("义班", "2")
            };
            cmbShift.DataSource = shiftList;
            cmbShift.SelectedIndex = 0;
        }

        private void InitializeSerialPort()
        {
            serialPort = new SerialPort
            {
                DataBits = 8,
                StopBits = StopBits.One,
                Parity = Parity.None,
                Encoding = Encoding.ASCII,
                ReadTimeout = 1000,
                WriteTimeout = 1000
            };
            serialPort.DataReceived += SerialPort_DataReceived;
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbPlant.Text) || string.IsNullOrEmpty(cmbConvertMachine.Text)) 
                { 
                    MessageBox.Show("请先选择厂区和机台！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                serialPort.PortName = Utils.GetParameterValue("Port") ?? "com2";
                serialPort.BaudRate = Utils.GetParameterValue("BaudRate") != null ? Convert.ToInt32(Utils.GetParameterValue("BaudRate")) : 1200;
                serialPort.Open();

                cmbPlant.Enabled = false;
                cmbConvertMachine.Enabled = false;
                cmbShift.Enabled = false;
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                btnZero.Enabled = true;
                btnTare.Enabled = true;
                btnRead.Enabled = true;
                chkAutoRead.Enabled = true;
                numWaterRate.Enabled = true;

                lblStatus.Text = $"状态: 已连接 - {serialPort.PortName} ({serialPort.BaudRate})";
                lblStatus.ForeColor = Color.Green;

                Log($"串口连接成功: {serialPort.PortName} - {serialPort.BaudRate}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"连接失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log($"串口连接失败: {ex.Message}");
            }
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    autoReadTimer.Stop();
                    chkAutoRead.Checked = false;
                    serialPort.Close();
                }

                cmbPlant.Enabled = true;
                cmbConvertMachine.Enabled = true;
                cmbShift.Enabled = true;
                btnConnect.Enabled = true;
                btnDisconnect.Enabled = false;
                btnZero.Enabled = false;
                btnTare.Enabled = false;
                btnRead.Enabled = false;
                chkAutoRead.Enabled = false;
                numWaterRate.Enabled = false;

                lblStatus.Text = "状态: 未连接";
                lblStatus.ForeColor = Color.Black;

                Log("串口已断开");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"断开失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log($"串口断开失败: {ex.Message}");
            }
        }

        private void BtnZero_Click(object sender, EventArgs e)
        {
            SendCommand("Z");
            lblStatus.Text = "状态: 已发送Z置零命令";
        }

        private void BtnTare_Click(object sender, EventArgs e)
        {
            SendCommand("T");
            lblStatus.Text = "状态: 已发送T去皮命令";
        }

        /// <summary>
        /// 开始称重
        /// </summary>
        private void BtnRead_Click(object sender, EventArgs e)
        {
            // 计算与上次发送命令的时间间隔
            TimeSpan timeSinceLastCommand = DateTime.Now - lastCommandTime;
            // 如果间隔大于等于防抖间隔，立即发送命令
            if (timeSinceLastCommand >= TimeSpan.FromMilliseconds(DebounceInterval))
            {
                if (!isReadingData)
                {
                    // 开始称重的标记
                    isBebinWeighing = true;
                    btnRead.Enabled = false;
                    btnReadEnd.Enabled = true;
                    // 开始读取：设置读取状态，准备接收数据
                    isReadingData = true;
                    lastReadTime = DateTime.MinValue;
                    SendCommand("R");
                    lblStatus.Text = "状态: 已发送R读取命令，正在接收数据...";
                }
                else
                {
                    // 停止读取：重置读取状态
                    isReadingData = false;
                }
            }            
        }
        /// <summary>
        /// 结束称重
        /// </summary>
        private void btnReadEnd_Click(object sender, EventArgs e)
        {
            // 计算与上次发送命令的时间间隔
            TimeSpan timeSinceLastCommand = DateTime.Now - lastCommandTime;
            // 如果间隔大于等于防抖间隔，立即发送命令
            if (timeSinceLastCommand >= TimeSpan.FromMilliseconds(DebounceInterval))
            {
                if (!isReadingData)
                {
                    // 开始称重的标记
                    weighingId = 0;
                    isBebinWeighing = false;
                    btnRead.Enabled = true;
                    btnReadEnd.Enabled = false;
                    // 开始读取：设置读取状态，准备接收数据
                    isReadingData = true;
                    lastReadTime = DateTime.MinValue;
                    SendCommand("R");
                    lblStatus.Text = "状态: 已发送R读取命令，正在接收数据...";
                }
                else
                {
                    // 停止读取：重置读取状态
                    isReadingData = false;
                }
            }
        }
        private void SendCommand(string command)
        {
            try
            {
                // 计算与上次发送命令的时间间隔
                TimeSpan timeSinceLastCommand = DateTime.Now - lastCommandTime;
                
                // 如果间隔大于等于防抖间隔，立即发送命令
                if (timeSinceLastCommand >= TimeSpan.FromMilliseconds(DebounceInterval))
                {
                    if (serialPort.IsOpen)
                    {
                        serialPort.Write(command);
                        lastCommandTime = DateTime.Now;
                        Log($"发送命令: {command}");
                    }
                    else
                    {
                        Log($"尝试在串口未打开时发送命令: {command}");
                    }
                }
                else
                {
                    Log($"命令 {command} 触发防抖，距离上次发送仅 {timeSinceLastCommand.TotalMilliseconds:F0}ms，已忽略");
                    Log($"isReadingData={isReadingData}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发送命令失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log($"发送命令失败: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 防抖计时器触发事件
        /// </summary>
        private void DebounceTimer_Tick(object sender, EventArgs e)
        {
            debounceTimer.Stop();
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                // 只有在发送了读取命令后才处理数据
                if (!isReadingData)
                {
                    return;
                }

                // 使用ReadLine()读取一行数据
                try
                {
                    string data = serialPort.ReadLine();
                    if (string.IsNullOrEmpty(data)) 
                        return;

                    Log($"[ReadLine接收]: {data.Replace("\r", "\\r").Replace("\n", "\\n")}");
                    
                    // 直接处理单行数据，不需要缓冲区
                    ProcessSingleLineData(data);
                }
                catch (TimeoutException)
                {
                    Log($"[超时]: 等待换行符超时");
                    // 超时后可以尝试使用ReadExisting()作为备选方案
                    string data = serialPort.ReadExisting();
                    if (!string.IsNullOrEmpty(data))
                    {
                        Log($"[备选方案接收]: {data}");
                        ProcessReceivedData(data);
                    }
                }
            }
            catch (Exception ex)
            {
                isReadingData = false;
                Log($"数据接收异常: {ex.Message}");
                UpdateUI(() =>
                {
                    lblStatus.Text = $"接收数据错误: {ex.Message}";
                    lblStatus.ForeColor = Color.Red;
                });
            }
        }

        private void ProcessSingleLineData(string lineData)
        {
            try
            {
                // 使用静态正则表达式提取有效重量数据，查找字符串中任意位置的匹配
                Match match = WeightPattern.Match(lineData);
                
                if (match.Success)
                {
                    ProcessMatchResult(match, "[ReadLine读取完成]");
                }
                else
                {
                    // 尝试查找所有匹配，处理包含噪声的数据
                    MatchCollection matches = WeightPattern.Matches(lineData);
                    if (matches.Count > 0)
                    {
                        ProcessMatchResult(matches[0], "[ReadLine读取完成]");
                    }
                    else
                    {
                        Log($"[ReadLine警告]: 未找到有效重量数据: {lineData}");
                        isReadingData = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"单行数据处理异常: {ex.Message}");
            }
        }

        private void ProcessReceivedData(string buffer)
        {
            try
            {
                // 使用静态正则表达式提取有效重量数据
                MatchCollection matches = WeightPattern.Matches(buffer);
                
                if (matches.Count > 0)
                {
                    // 只处理第一条匹配到的重量数据
                    ProcessMatchResult(matches[0], "[单次读取完成]");
                    return;
                }
                else if (buffer.Length > 1000) // 防止缓冲区无限增长
                {
                    Log($"缓冲区过大，清空: {buffer.Length} 字节");
                }
            }
            catch (Exception ex)
            {
                Log($"数据处理异常: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 处理匹配到的重量数据
        /// </summary>
        /// <param name="match">正则表达式匹配结果</param>
        /// <param name="logPrefix">日志前缀</param>
        private void ProcessMatchResult(Match match, string logPrefix)
        {
            if (match.Success)
            {
                string typeCode = match.Groups[1].Value.ToLower();
                string weightStr = match.Groups[2].Value;
                string unit = match.Groups[3].Success ? match.Groups[3].Value : "kg";
                
                ProcessWeightData(typeCode, weightStr, unit);
                
                // 读取到一条有效数据后，自动停止读取
                isReadingData = false;
                
                // 更新UI状态
                UpdateUI(() =>
                {
                    lblStatus.Text = "状态: 已读取一条数据，自动停止";
                });
                
                Log($"{logPrefix}: 已读取一条数据并自动停止");
            }
        }

        private void ProcessWeightData(string typeCode, string weightStr, string unit)
        {
            try
            {
                // 验证重量是否为有效数字
                if (!decimal.TryParse(weightStr, out decimal weight))
                {
                    Log($"[警告]: 无效的重量值: {weightStr}");
                    return;
                }

                // 验证类型代码
                if (typeCode != "ww" && typeCode != "wn" && typeCode != "wt")
                {
                    Log($"[警告]: 无效的类型代码: {typeCode}");
                    return;
                }

                currentWeightType = typeCode;
                currentWeight = weight.ToString();
                currentUnit = unit;

                string typeName = string.Empty;
                switch (typeCode)
                {
                    case "ww":
                        typeName = "毛重";
                        break;
                    case "wn":
                        typeName = "净重";
                        break;
                    case "wt":
                        typeName = "皮重";
                        break;
                }

                // 检查是否是新数据（避免短时间内重复处理）
                if (DateTime.Now - lastReadTime > TimeSpan.FromSeconds(1))
                {
                    // 线程安全的UI更新
                    UpdateWeightDisplaySafe(typeName);
                    lastReadTime = DateTime.Now;

                    // 保存到DataGridView
                    UpdateUI(() => SaveToDGV());
                    Log($"[解析成功]: {typeName} = {weightStr}{unit}");
                }
            }
            catch (Exception ex)
            {
                Log($"[解析错误]: {ex.Message} - 类型: {typeCode}, 重量: {weightStr}, 单位: {unit}");
                UpdateUI(() =>
                {
                    lblStatus.Text = $"解析数据错误: {ex.Message}";
                    lblStatus.ForeColor = Color.Red;
                });
            }
        }

        /// <summary>
        /// 通用的UI更新方法，线程安全
        /// </summary>
        /// <param name="action">要执行的UI更新操作</param>
        private void UpdateUI(Action action)
        {
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }
        
        /// <summary>
        /// 线程安全的UI更新方法
        /// </summary>
        private void UpdateWeightDisplaySafe(string typeName)
        {
            UpdateUI(() => UpdateWeightDisplay(typeName));
        }

        private void UpdateWeightDisplay(string typeName)
        {
            lblWeight.Text = currentWeight;
            lblUnit.Text = currentUnit;
            lblWeightType.Text = typeName;

            // 判断是否稳定（简单判断：非零且不变化）
            isStable = !currentWeight.Trim().StartsWith("0.000");
            pnlIndicator.BackColor = isStable ? Color.Lime : Color.Gray;

            lblStatus.Text = $"状态: 接收成功 - {typeName}: {currentWeight}{currentUnit}";
            lblStatus.ForeColor = Color.Green;
        }

        private void ChkAutoRead_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoRead.Checked)
            {
                autoReadTimer.Start();
                lblStatus.Text = "状态: 自动读取已启动";
                Log("自动读取已启动");
            }
            else
            {
                autoReadTimer.Stop();
                lblStatus.Text = "状态: 自动读取已停止";
                Log("自动读取已停止");
            }
        }

        private void AutoReadTimer_Tick(object sender, EventArgs e)
        {
            // 设置读取状态，准备接收数据
            isReadingData = true;
            SendCommand("R");
        }

        private void SaveToDGV()
        {
            if (string.IsNullOrEmpty(currentWeight) || currentWeight.Trim() == "0.000")
            {
                MessageBox.Show("当前重量为零，无需保存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //lblStatus.Text = $"状态: 记录已保存 - 共 {weightRecords.Count} 条";
            //Log($"记录已保存: {record.Weight}{record.Unit} ({record.Status})");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Log("程序关闭");
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }
            base.OnFormClosing(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // 释放串口资源
                serialPort?.Close();
                serialPort?.Dispose();
                
                // 释放计时器资源
                autoReadTimer?.Dispose();
                debounceTimer?.Dispose();
            }
            base.Dispose(disposing);
        }

        private static void Log(string data)
        {
            Utils.AppendToFile(Utils.SystemLogFile, data, true);
        }

    }

    public class ComboBoxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public ComboBoxItem(string text, string value)
        {
            Text = text;
            Value = value;
        }
        public override string ToString()
        {
            return Text;
        }
    }

}