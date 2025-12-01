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
    public partial class MainForm_bak : Form
    {
        private SerialPort serialPort;
        private StringBuilder receiveBuffer;
        private delegate void UpdateUIDelegate(string data);
        private List<WeightRecord> weightRecords;
        private string currentWeight = "0.000";
        private string currentUnit = "kg";
        private bool isStable = false;
        private WeightType currentWeightType = WeightType.Gross;

        public MainForm_bak()
        {
            InitializeComponent();
            InitializeSerialPort();
            receiveBuffer = new StringBuilder();
            weightRecords = new List<WeightRecord>();
            LoadAvailablePorts();
            this.cmbBaudRate.SelectedIndex = 0;
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
                btnSave.Enabled = true;
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
            lblStatus.Text = "状态: 已发送置零命令";
        }

        private void BtnTare_Click(object sender, EventArgs e)
        {
            SendCommand("T");
            lblStatus.Text = "状态: 已发送去皮命令";
        }

        private void BtnRead_Click(object sender, EventArgs e)
        {
            if (!isReadingData)
            {
                // 开始读取：设置读取状态，准备接收数据
                isReadingData = true;
                lastReadTime = DateTime.MinValue;
                receiveBuffer.Clear(); // 清空缓冲区
                SendCommand("R");
                lblStatus.Text = "状态: 已发送读取命令，正在接收数据...";
                btnRead.Text = "停止读取";
            }
            else
            {
                // 停止读取：重置读取状态
                isReadingData = false;
                receiveBuffer.Clear();
                lblStatus.Text = "状态: 已停止读取数据";
                btnRead.Text = "读取";
            }
        }

        private void SendCommand(string command)
        {
            try
            {
                if (serialPort != null && serialPort.IsOpen)
                {
                    serialPort.Write(command);
                    Log($"发送命令: {command}");
                }
                else
                {
                    Log($"尝试在串口未打开时发送命令: {command}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发送命令失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log($"发送命令失败: {ex.Message}");
            }
        }

        // 定义分隔符为常量
        private static readonly string[] LineSeparators = { "\r\n", "\n", "\r" };
        // 标记是否正在读取数据
        private bool isReadingData = false;
        // 上次读取时间
        private DateTime lastReadTime = DateTime.MinValue; 
        
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                // 只有在发送了读取命令后才处理数据
                if (!isReadingData)
                {
                    // 清空缓冲区，避免积累无用数据
                    receiveBuffer.Clear();
                    return;
                }

                string data = serialPort.ReadExisting();
                if (string.IsNullOrEmpty(data)) 
                    return;
                //string data = serialPort.ReadLine();
                receiveBuffer.Append(data);

                string buffer = receiveBuffer.ToString();
                //Log($"buffer={buffer}");

                ProcessReceivedData(buffer);
            }
            catch (Exception ex)
            {
                isReadingData = false;
                Log($"数据接收异常: {ex.Message}");
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        lblStatus.Text = $"接收数据错误: {ex.Message}";
                        lblStatus.ForeColor = Color.Red;
                    }));
                }
                else
                {
                    lblStatus.Text = $"接收数据错误: {ex.Message}";
                    lblStatus.ForeColor = Color.Red;
                }
            }
        }

        private void ProcessReceivedData(string buffer)
        {
            try
            {
                // 使用正则表达式提取有效重量数据
                Regex weightPattern = new Regex(@"(ww|wn|wt)\s*([0-9.]+)(kg|g|lb)?", 
                    RegexOptions.IgnoreCase);
                
                MatchCollection matches = weightPattern.Matches(buffer);
                Log($"matches.count={matches.Count}");
                
                if (matches.Count > 0)
                {
                    // 只处理第一条匹配到的重量数据
                    Match match = matches[0];
                    if (match.Success)
                    {
                        string typeCode = match.Groups[1].Value.ToLower();
                        string weightStr = match.Groups[2].Value;
                        string unit = match.Groups[3].Success ? match.Groups[3].Value : "kg";
                        
                        ProcessWeightData(typeCode, weightStr, unit);
                        
                        // 读取到一条有效数据后，自动停止读取
                        isReadingData = false;
                        receiveBuffer.Clear();
                        
                        // 更新UI状态
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new Action(() =>
                            {
                                btnRead.Text = "读取";
                                lblStatus.Text = "状态: 已读取一条数据，自动停止";
                            }));
                        }
                        else
                        {
                            btnRead.Text = "读取";
                            lblStatus.Text = "状态: 已读取一条数据，自动停止";
                        }
                        
                        Log($"[单次读取完成]: 已读取一条数据并自动停止");
                        return;
                    }
                }
                else if (buffer.Length > 1000) // 防止缓冲区无限增长
                {
                    Log($"缓冲区过大，清空: {buffer.Length} 字节");
                    receiveBuffer.Clear();
                }
            }
            catch (Exception ex)
            {
                Log($"数据处理异常: {ex.Message}");
                receiveBuffer.Clear();
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

                WeightType type = WeightType.Gross;
                string typeName = "毛重";

                switch (typeCode)
                {
                    case "wn":
                        type = WeightType.Net;
                        typeName = "净重";
                        break;
                    case "wt":
                        type = WeightType.Tare;
                        typeName = "皮重";
                        break;
                }

                currentWeight = weight.ToString();
                currentUnit = unit;
                currentWeightType = type;

                // 检查是否是新数据（避免短时间内重复处理）
                if (DateTime.Now - lastReadTime > TimeSpan.FromSeconds(1))
                {
                    Log($"DateTime.Now={DateTime.Now}, lastReadTime={lastReadTime}，{TimeSpan.FromSeconds(1)}");
                    
                    // 线程安全的UI更新
                    UpdateWeightDisplaySafe(typeName);
                    lastReadTime = DateTime.Now;
                    
                    Log($"[解析成功]: {typeName} = {weightStr}{unit}");
                }
            }
            catch (Exception ex)
            {
                Log($"[解析错误]: {ex.Message} - 类型: {typeCode}, 重量: {weightStr}, 单位: {unit}");
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        lblStatus.Text = $"解析数据错误: {ex.Message}";
                        lblStatus.ForeColor = Color.Red;
                    }));
                }
                else
                {
                    lblStatus.Text = $"解析数据错误: {ex.Message}";
                    lblStatus.ForeColor = Color.Red;
                }
            }
        }

        /// <summary>
        /// 线程安全的UI更新方法
        /// </summary>
        private void UpdateWeightDisplaySafe(string typeName)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new UpdateUIDelegate(UpdateWeightDisplay), new object[] { typeName });
            }
            else
            {
                UpdateWeightDisplay(typeName);
            }
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
            receiveBuffer.Clear(); // 清空缓冲区
            SendCommand("R");
        }

        private void BtnSave_Click(object sender, EventArgs e)
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
            switch (type)
            {
                case "Gross": return "毛重";
                case "Net": return "净重";
                case "Tare": return "皮重";
                default: return type;
            }
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
                if (serialPort != null)
                {
                    if (serialPort.IsOpen)
                    {
                        serialPort.Close();
                    }
                    serialPort.Dispose();
                }

                if (autoReadTimer != null)
                {
                    autoReadTimer.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private static void Log(string data)
        {
            Utils.AppendToFile(Utils.SystemLogFile, data, true);
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            try
            {
                // 如果串口已连接，先断开
                bool wasConnected = false;
                string currentPort = "";
                int currentBaudRate = 9600;

                if (serialPort != null && serialPort.IsOpen)
                {
                    wasConnected = true;
                    currentPort = serialPort.PortName;
                    currentBaudRate = serialPort.BaudRate;

                    var result = MessageBox.Show(
                        "串口调试工具需要独占串口。\n是否断开当前连接并打开调试工具？",
                        "提示",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.No)
                    {
                        return;
                    }

                    // 断开当前连接
                    BtnDisconnect_Click(null, null);
                }

                // 打开调试窗口
                using (DebugForm debugForm = new DebugForm())
                {
                    debugForm.ShowDialog();
                }

                // 如果之前是连接状态，询问是否重新连接
                if (wasConnected)
                {
                    var result = MessageBox.Show(
                        $"是否重新连接到 {currentPort}?",
                        "提示",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.Yes)
                    {
                        // 恢复连接
                        if (cmbPorts.Items.Contains(currentPort))
                        {
                            cmbPorts.SelectedItem = currentPort;
                            cmbBaudRate.SelectedItem = currentBaudRate.ToString();
                            BtnConnect_Click(null, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打开调试工具失败: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log($"打开调试工具失败: {ex.Message}");
            }
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