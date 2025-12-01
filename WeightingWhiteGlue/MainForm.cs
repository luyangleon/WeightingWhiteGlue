using System;
using System.Collections.Generic;
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
        private SerialPort serialPort;
        private List<WeightRecord> weightRecords;
        private string currentWeight = "0.000";
        private string currentUnit = "kg";
        private bool isStable = false;
        private WeightType currentWeightType = WeightType.Gross;

        private bool isReadingData = false;
        private DateTime lastReadTime = DateTime.MinValue;

        // 防抖相关字段
        private Timer debounceTimer = new Timer();
        private DateTime lastCommandTime = DateTime.MinValue;
        private const int DebounceInterval = 1000;
        
        private static readonly Regex WeightPattern = new Regex(@"(ww|wn|wt)\s*([0-9.]+)(kg|g|lb)?", 
            RegexOptions.IgnoreCase | RegexOptions.Compiled);
        
        // 重量类型到中文名称的映射
        private static readonly Dictionary<string, string> WeightTypeNames = new Dictionary<string, string>
        {
            { "Gross", "毛重" },
            { "Net", "净重" },
            { "Tare", "皮重" }
        };
        
        // 类型代码到WeightType的映射
        private static readonly Dictionary<string, WeightType> TypeCodeMap = new Dictionary<string, WeightType>
        {
            { "ww", WeightType.Gross },
            { "wn", WeightType.Net },
            { "wt", WeightType.Tare }
        };

        public MainForm()
        {
            InitializeComponent();
            InitializeSerialPort();
            weightRecords = new List<WeightRecord>();
            LoadAvailablePorts();
            this.cmbBaudRate.SelectedIndex = 0;
            
            debounceTimer.Interval = DebounceInterval;
            debounceTimer.Tick += DebounceTimer_Tick;
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

        private void LoadAvailablePorts()
        {
            cmbPorts.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            if (ports.Length > 0)
            {
                cmbPorts.Items.AddRange(ports);
                cmbPorts.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("未检测到可用串口！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbPorts.SelectedItem == null)
                {
                    MessageBox.Show("请选择串口！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                serialPort.PortName = cmbPorts.SelectedItem.ToString();
                serialPort.BaudRate = int.Parse(cmbBaudRate.SelectedItem.ToString());
                serialPort.Open();

                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                btnZero.Enabled = true;
                btnTare.Enabled = true;
                btnRead.Enabled = true;
                btnExport.Enabled = true;
                chkAutoRead.Enabled = true;
                cmbPorts.Enabled = false;
                cmbBaudRate.Enabled = false;

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

                btnConnect.Enabled = true;
                btnDisconnect.Enabled = false;
                btnZero.Enabled = false;
                btnTare.Enabled = false;
                btnRead.Enabled = false;
                chkAutoRead.Enabled = false;
                cmbPorts.Enabled = true;
                cmbBaudRate.Enabled = true;

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

        private void BtnRead_Click(object sender, EventArgs e)
        {
            // 计算与上次发送命令的时间间隔
            TimeSpan timeSinceLastCommand = DateTime.Now - lastCommandTime;
            // 如果间隔大于等于防抖间隔，立即发送命令
            if (timeSinceLastCommand >= TimeSpan.FromMilliseconds(DebounceInterval))
            {
                if (!isReadingData)
                {
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

                // 使用字典获取重量类型和中文名称
                WeightType type = TypeCodeMap.TryGetValue(typeCode, out WeightType mappedType) ? mappedType : WeightType.Gross;
                string typeName = WeightTypeNames.TryGetValue(type.ToString(), out string name) ? name : "毛重";

                currentWeight = weight.ToString();
                currentUnit = unit;
                currentWeightType = type;

                // 检查是否是新数据（避免短时间内重复处理）
                if (DateTime.Now - lastReadTime > TimeSpan.FromSeconds(1))
                {
                    // 线程安全的UI更新
                    UpdateWeightDisplaySafe(typeName);
                    lastReadTime = DateTime.Now;

                    // 保存到DataGridView
                    SaveToDGV();
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

            WeightRecord record = new WeightRecord
            {
                Time = DateTime.Now,
                Weight = currentWeight,
                Unit = currentUnit,
                Type = currentWeightType.ToString(),
                Status = isStable ? "稳定" : "不稳定"
            };

            weightRecords.Add(record);

            dgvRecords.Rows.Add(
                record.Time.ToString("yyyy-MM-dd HH:mm:ss"),
                record.Weight,
                record.Unit,
                GetWeightTypeName(record.Type),
                record.Status
            );

            lblStatus.Text = $"状态: 记录已保存 - 共 {weightRecords.Count} 条";
            Log($"记录已保存: {record.Weight}{record.Unit} ({record.Status})");
        }

        private string GetWeightTypeName(string type)
        {
            return WeightTypeNames.TryGetValue(type, out string name) ? name : type;
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (weightRecords.Count == 0)
            {
                MessageBox.Show("没有可导出的记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "CSV文件|*.csv|文本文件|*.txt",
                FileName = $"称重记录_{DateTime.Now:yyyyMMddHHmmss}"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                    {
                        sw.WriteLine("时间,重量,单位,类型,状态");
                        foreach (WeightRecord record in weightRecords)
                        {
                            sw.WriteLine($"{record.Time:yyyy-MM-dd HH:mm:ss},{record.Weight},{record.Unit},{GetWeightTypeName(record.Type)},{record.Status}");
                        }
                    }
                    MessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblStatus.Text = $"状态: 已导出 {weightRecords.Count} 条记录到 {sfd.FileName}";
                    Log($"导出记录成功: {weightRecords.Count} 条 -> {sfd.FileName}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"导出失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Log($"导出记录失败: {ex.Message}");
                }
            }
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

    public enum WeightType
    {
        Gross,  // 毛重
        Net,    // 净重
        Tare    // 皮重
    }

    public class WeightRecord
    {
        public DateTime Time { get; set; }
        public string Weight { get; set; }
        public string Unit { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    }
}