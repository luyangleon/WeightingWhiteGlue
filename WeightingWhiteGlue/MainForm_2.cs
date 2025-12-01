using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WeightingWhiteGlue
{
    public partial class MainForm2 : Form
    {
        private SerialPort serialPort;
        private StringBuilder receiveBuffer;
        private List<WeightRecord> weightRecords;
        private string currentWeight = "0.000";
        private string currentUnit = "kg";
        private bool isStable = false;
        private WeightType currentWeightType = WeightType.Gross;

        // 数据接收统计
        private int totalReceived = 0;
        private int validData = 0;
        private int invalidData = 0;

        // 定义分隔符和数据格式
        private static readonly string[] LineSeparators = { "\r\n", "\n", "\r" };
        
        // 正则表达式验证数据格式：(ww|wn|wt)[-+]?\d+\.\d+kg
        private static readonly Regex WeightDataPattern = new Regex(
            @"^(ww|wn|wt)([\s\-\+]?\d{1,6}\.\d{3})(kg|lb)$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled
        );

        public MainForm2()
        {
            InitializeComponent();
            InitializeSerialPort();
            receiveBuffer = new StringBuilder(4096); // 设置初始容量
            weightRecords = new List<WeightRecord>();
            LoadAvailablePorts();
            
            Utils.CleanOldLogs(7);
            Utils.WriteLog("程序启动", LogLevel.Info);
        }

        private void InitializeSerialPort()
        {
            serialPort = new SerialPort
            {
                DataBits = 8,
                StopBits = StopBits.One,
                Parity = Parity.None,
                Encoding = Encoding.ASCII,  // 使用ASCII编码
                ReadTimeout = 1000,
                WriteTimeout = 1000,
                ReceivedBytesThreshold = 1  // 接收到1个字节就触发事件
            };
            serialPort.DataReceived += SerialPort_DataReceived;
            serialPort.ErrorReceived += SerialPort_ErrorReceived;  // 添加错误处理
        }

        /// <summary>
        /// 串口错误处理
        /// </summary>
        private void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            Utils.WriteLog($"串口错误: {e.EventType}", LogLevel.Error);
        }

        private void LoadAvailablePorts()
        {
            cmbPorts.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            if (ports.Length > 0)
            {
                cmbPorts.Items.AddRange(ports);
                cmbPorts.SelectedIndex = 0;
                Utils.WriteLog($"检测到 {ports.Length} 个串口: {string.Join(", ", ports)}", LogLevel.Info);
            }
            else
            {
                MessageBox.Show("未检测到可用串口！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Utils.WriteLog("未检测到可用串口", LogLevel.Warning);
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

                string portName = cmbPorts.SelectedItem.ToString();
                int baudRate = int.Parse(cmbBaudRate.SelectedItem.ToString());

                // 重置统计
                totalReceived = 0;
                validData = 0;
                invalidData = 0;
                receiveBuffer.Clear();

                serialPort.PortName = portName;
                serialPort.BaudRate = baudRate;
                serialPort.Open();

                // 清空串口缓冲区
                serialPort.DiscardInBuffer();
                serialPort.DiscardOutBuffer();

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

                lblStatus.Text = $"状态: 已连接 - {portName} ({baudRate})";
                lblStatus.ForeColor = Color.Green;
                
                Utils.WriteLog($"串口连接成功: {portName} - {baudRate}", LogLevel.Info);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"连接失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utils.WriteLog($"串口连接失败: {ex.Message}", LogLevel.Error);
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
                
                // 显示统计信息
                Utils.WriteLog($"串口已断开 - 统计: 总接收={totalReceived}, 有效={validData}, 无效={invalidData}", LogLevel.Info);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"断开失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utils.WriteLog($"串口断开失败: {ex.Message}", LogLevel.Error);
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
            SendCommand("R");
            lblStatus.Text = "状态: 已发送读取命令";
        }

        private void SendCommand(string command)
        {
            try
            {
                if (serialPort != null && serialPort.IsOpen)
                {
                    serialPort.Write(command);
                    Utils.WriteLog($"发送命令: {command}", LogLevel.Debug);
                }
                else
                {
                    Utils.WriteLog("尝试在串口未打开时发送命令", LogLevel.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发送命令失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utils.WriteLog($"发送命令失败: {ex.Message}", LogLevel.Error);
            }
        }

        /// <summary>
        /// 数据接收处理 - 增强版，防止乱码
        /// </summary>
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                // 检查串口状态
                if (serialPort == null || !serialPort.IsOpen)
                {
                    return;
                }

                // 读取所有可用数据
                int bytesToRead = serialPort.BytesToRead;
                if (bytesToRead == 0) return;

                byte[] buffer = new byte[bytesToRead];
                int bytesRead = serialPort.Read(buffer, 0, bytesToRead);

                // 清理数据：移除非ASCII字符和控制字符（保留\r\n）
                string data = CleanReceivedData(buffer, bytesRead);
                
                if (string.IsNullOrEmpty(data))
                {
                    Utils.WriteLog("接收到空数据或全是无效字符", LogLevel.Warning);
                    return;
                }

                totalReceived++;
                receiveBuffer.Append(data);
                
                // 记录原始数据（用于调试）
                string logData = data.Replace("\r", "\\r").Replace("\n", "\\n");
                Utils.WriteLog($"接收[{totalReceived}]: {logData} (长度={data.Length}, 字节={bytesRead})", LogLevel.Debug);

                // 检查缓冲区大小，防止内存溢出
                if (receiveBuffer.Length > 4096)
                {
                    Utils.WriteLog($"⚠️ 缓冲区过大: {receiveBuffer.Length} 字节，强制清空", LogLevel.Warning);
                    receiveBuffer.Clear();
                    return;
                }

                string bufferContent = receiveBuffer.ToString();
                
                // 检查是否包含换行符
                if (bufferContent.IndexOfAny(new[] { '\r', '\n' }) == -1)
                {
                    // 没有完整数据，继续等待
                    return;
                }

                // 分割并处理完整的数据行
                ProcessCompleteLines(bufferContent);
            }
            catch (TimeoutException)
            {
                Utils.WriteLog("串口读取超时", LogLevel.Warning);
            }
            catch (Exception ex)
            {
                Utils.WriteLog($"❌ 数据接收异常: {ex.Message}\n堆栈: {ex.StackTrace}", LogLevel.Error);
                
                // 清空缓冲区，防止错误累积
                receiveBuffer.Clear();
                
                UpdateStatusSafe($"接收数据错误: {ex.Message}", Color.Red);
            }
        }

        /// <summary>
        /// 清理接收的数据，移除非法字符
        /// </summary>
        private string CleanReceivedData(byte[] buffer, int length)
        {
            StringBuilder cleaned = new StringBuilder(length);
            
            for (int i = 0; i < length; i++)
            {
                byte b = buffer[i];
                
                // 只保留可打印ASCII字符 (32-126) 和换行符 (10, 13)
                if ((b >= 32 && b <= 126) || b == 10 || b == 13)
                {
                    cleaned.Append((char)b);
                }
                else
                {
                    // 记录被过滤的字符
                    Utils.WriteLog($"过滤非法字符: 0x{b:X2} (位置={i})", LogLevel.Debug);
                }
            }
            
            return cleaned.ToString();
        }

        /// <summary>
        /// 处理完整的数据行
        /// </summary>
        private void ProcessCompleteLines(string bufferContent)
        {
            // 分割数据行
            var lines = bufferContent.Split(LineSeparators, StringSplitOptions.None);
            
            // 最后一行可能不完整，保留在缓冲区
            bool hasIncomplete = !bufferContent.EndsWith("\r") && !bufferContent.EndsWith("\n");
            int linesToProcess = hasIncomplete ? lines.Length - 1 : lines.Length;
            
            // 处理完整的行
            for (int i = 0; i < linesToProcess; i++)
            {
                string line = lines[i].Trim();
                
                if (string.IsNullOrEmpty(line))
                {
                    continue; // 跳过空行
                }
                
                // 验证并处理数据
                if (ValidateAndProcessWeightData(line))
                {
                    validData++;
                }
                else
                {
                    invalidData++;
                    Utils.WriteLog($"❌ 无效数据[{invalidData}]: {line}", LogLevel.Warning);
                }
            }
            
            // 清空缓冲区，如果有不完整的行则保留
            receiveBuffer.Clear();
            if (hasIncomplete && lines.Length > 0)
            {
                receiveBuffer.Append(lines[lines.Length - 1]);
                Utils.WriteLog($"保留不完整数据: {lines[lines.Length - 1]}", LogLevel.Debug);
            }
        }

        /// <summary>
        /// 验证并处理称重数据 - 使用正则表达式
        /// </summary>
        private bool ValidateAndProcessWeightData(string data)
        {
            try
            {
                // 移除所有空白字符
                string cleanData = data.Replace(" ", "").Replace("\t", "");
                
                // 数据长度检查
                if (cleanData.Length < 12 || cleanData.Length > 20)
                {
                    Utils.WriteLog($"数据长度异常: {cleanData.Length} - {cleanData}", LogLevel.Warning);
                    return false;
                }

                // 正则表达式验证
                Match match = WeightDataPattern.Match(cleanData);
                if (!match.Success)
                {
                    Utils.WriteLog($"格式不匹配: {cleanData}", LogLevel.Warning);
                    return false;
                }

                // 提取各部分
                string typeCode = match.Groups[1].Value.ToLower();
                string weightStr = match.Groups[2].Value.Trim();
                string unit = match.Groups[3].Value.ToLower();

                // 二次验证：解析重量值
                if (!decimal.TryParse(weightStr, out decimal weight))
                {
                    Utils.WriteLog($"重量值解析失败: {weightStr}", LogLevel.Warning);
                    return false;
                }

                // 合理性检查：重量范围
                if (weight < -9999.999m || weight > 9999.999m)
                {
                    Utils.WriteLog($"重量值超出合理范围: {weight}", LogLevel.Warning);
                    return false;
                }

                // 确定重量类型
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

                // 更新当前数据
                currentWeight = weightStr;
                currentUnit = unit;
                currentWeightType = type;

                // 线程安全的UI更新
                UpdateWeightDisplaySafe(typeName);
                
                Utils.WriteLog($"✓ 解析成功[{validData + 1}]: {typeName}={weightStr}{unit}", LogLevel.Info);
                return true;
            }
            catch (Exception ex)
            {
                Utils.WriteLog($"❌ 验证异常: {ex.Message} - 数据: {data}", LogLevel.Error);
                return false;
            }
        }

        /// <summary>
        /// 线程安全的UI更新
        /// </summary>
        private void UpdateWeightDisplaySafe(string typeName)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(UpdateWeightDisplay), typeName);
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

            // 判断是否稳定
            isStable = !currentWeight.Trim().TrimStart('-', '+').StartsWith("0.000");
            pnlIndicator.BackColor = isStable ? Color.Lime : Color.Gray;

            // 更新状态栏（包含统计信息）
            lblStatus.Text = $"状态: {typeName} {currentWeight}{currentUnit} | 有效:{validData} 无效:{invalidData}";
            lblStatus.ForeColor = Color.Green;
        }

        /// <summary>
        /// 线程安全的状态更新
        /// </summary>
        private void UpdateStatusSafe(string message, Color color)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<string, Color>(UpdateStatus), message, color);
            }
            else
            {
                UpdateStatus(message, color);
            }
        }

        private void UpdateStatus(string message, Color color)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = color;
        }

        private void ChkAutoRead_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoRead.Checked)
            {
                autoReadTimer.Start();
                lblStatus.Text = "状态: 自动读取已启动";
                Utils.WriteLog("自动读取已启动", LogLevel.Info);
            }
            else
            {
                autoReadTimer.Stop();
                lblStatus.Text = "状态: 自动读取已停止";
                Utils.WriteLog("自动读取已停止", LogLevel.Info);
            }
        }

        private void AutoReadTimer_Tick(object sender, EventArgs e)
        {
            SendCommand("R");
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentWeight) || currentWeight.Trim().TrimStart('-', '+') == "0.000")
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
            Utils.WriteLog($"记录已保存: {record.Weight}{record.Unit} ({record.Status})", LogLevel.Info);
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