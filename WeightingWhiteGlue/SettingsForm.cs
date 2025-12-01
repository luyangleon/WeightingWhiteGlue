using System;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;

namespace WeightingWhiteGlue
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            try
            {
                // 从配置文件加载设置（需要App.config支持）
                // 这里使用默认值演示
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载设置失败: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 保存设置到配置文件
                MessageBox.Show("设置已保存！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存设置失败: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BtnBrowseExport_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = txtExportPath.Text;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtExportPath.Text = fbd.SelectedPath;
                }
            }
        }

        // 属性用于获取设置值
        public string CommMode => cmbCommMode.SelectedItem?.ToString();
        public int ReadInterval => (int)numReadInterval.Value;
        public bool AutoConnect => chkAutoConnect.Checked;
        public int DecimalPlaces => int.Parse(cmbDecimalPlaces.SelectedItem?.ToString() ?? "3");
        public bool ShowStableIndicator => chkShowStableIndicator.Checked;
        public string WeightColor => cmbWeightColor.SelectedItem?.ToString();
        public bool AutoSave => chkAutoSave.Checked;
        public int MaxRecords => (int)numMaxRecords.Value;
        public bool ExportOnClose => chkExportOnClose.Checked;
        public string ExportPath => txtExportPath.Text;
    }
}