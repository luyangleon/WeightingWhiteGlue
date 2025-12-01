using System;
using System.Drawing;
using System.Windows.Forms;

namespace WeightingWhiteGlue
{
    partial class SettingsForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            // 控件声明
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabComm = new System.Windows.Forms.TabPage();
            this.tabDisplay = new System.Windows.Forms.TabPage();
            this.tabData = new System.Windows.Forms.TabPage();

            // 通讯设置控件
            this.lblCommMode = new System.Windows.Forms.Label();
            this.cmbCommMode = new System.Windows.Forms.ComboBox();
            this.lblInterval = new System.Windows.Forms.Label();
            this.numReadInterval = new System.Windows.Forms.NumericUpDown();
            this.chkAutoConnect = new System.Windows.Forms.CheckBox();
            this.lblCommNote = new System.Windows.Forms.Label();

            // 显示设置控件
            this.lblDecimal = new System.Windows.Forms.Label();
            this.cmbDecimalPlaces = new System.Windows.Forms.ComboBox();
            this.chkShowStableIndicator = new System.Windows.Forms.CheckBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.cmbWeightColor = new System.Windows.Forms.ComboBox();

            // 数据设置控件
            this.chkAutoSave = new System.Windows.Forms.CheckBox();
            this.lblMaxRecords = new System.Windows.Forms.Label();
            this.numMaxRecords = new System.Windows.Forms.NumericUpDown();
            this.chkExportOnClose = new System.Windows.Forms.CheckBox();
            this.lblPath = new System.Windows.Forms.Label();
            this.txtExportPath = new System.Windows.Forms.TextBox();
            this.btnBrowseExport = new System.Windows.Forms.Button();

            // 底部按钮
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            this.tabControl.SuspendLayout();
            this.tabComm.SuspendLayout();
            this.tabDisplay.SuspendLayout();
            this.tabData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numReadInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxRecords)).BeginInit();
            this.SuspendLayout();

            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabComm);
            this.tabControl.Controls.Add(this.tabDisplay);
            this.tabControl.Controls.Add(this.tabData);
            this.tabControl.Location = new System.Drawing.Point(10, 10);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(470, 300);
            this.tabControl.TabIndex = 0;

            // 
            // tabComm
            // 
            this.tabComm.Controls.Add(this.lblCommMode);
            this.tabComm.Controls.Add(this.cmbCommMode);
            this.tabComm.Controls.Add(this.lblInterval);
            this.tabComm.Controls.Add(this.numReadInterval);
            this.tabComm.Controls.Add(this.chkAutoConnect);
            this.tabComm.Controls.Add(this.lblCommNote);
            this.tabComm.Location = new System.Drawing.Point(4, 22);
            this.tabComm.Name = "tabComm";
            this.tabComm.Padding = new System.Windows.Forms.Padding(3);
            this.tabComm.Size = new System.Drawing.Size(462, 274);
            this.tabComm.TabIndex = 0;
            this.tabComm.Text = "通讯设置";
            this.tabComm.UseVisualStyleBackColor = true;

            // 
            // lblCommMode
            // 
            this.lblCommMode.AutoSize = true;
            this.lblCommMode.Location = new System.Drawing.Point(20, 20);
            this.lblCommMode.Name = "lblCommMode";
            this.lblCommMode.Size = new System.Drawing.Size(65, 12);
            this.lblCommMode.TabIndex = 0;
            this.lblCommMode.Text = "通讯模式:";

            // 
            // cmbCommMode
            // 
            this.cmbCommMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCommMode.FormattingEnabled = true;
            this.cmbCommMode.Items.AddRange(new object[] {
            "指令方式",
            "连续方式",
            "稳定时连续发送"});
            this.cmbCommMode.Location = new System.Drawing.Point(120, 17);
            this.cmbCommMode.Name = "cmbCommMode";
            this.cmbCommMode.Size = new System.Drawing.Size(200, 20);
            this.cmbCommMode.TabIndex = 1;
            this.cmbCommMode.SelectedIndex = 0;

            // 
            // lblInterval
            // 
            this.lblInterval.AutoSize = true;
            this.lblInterval.Location = new System.Drawing.Point(20, 60);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(89, 12);
            this.lblInterval.TabIndex = 2;
            this.lblInterval.Text = "读取间隔(秒):";

            // 
            // numReadInterval
            // 
            this.numReadInterval.Location = new System.Drawing.Point(120, 57);
            this.numReadInterval.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numReadInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numReadInterval.Name = "numReadInterval";
            this.numReadInterval.Size = new System.Drawing.Size(100, 21);
            this.numReadInterval.TabIndex = 3;
            this.numReadInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});

            // 
            // chkAutoConnect
            // 
            this.chkAutoConnect.AutoSize = true;
            this.chkAutoConnect.Location = new System.Drawing.Point(20, 100);
            this.chkAutoConnect.Name = "chkAutoConnect";
            this.chkAutoConnect.Size = new System.Drawing.Size(204, 16);
            this.chkAutoConnect.TabIndex = 4;
            this.chkAutoConnect.Text = "启动时自动连接上次使用的串口";
            this.chkAutoConnect.UseVisualStyleBackColor = true;

            // 
            // lblCommNote
            // 
            this.lblCommNote.ForeColor = System.Drawing.Color.Blue;
            this.lblCommNote.Location = new System.Drawing.Point(20, 140);
            this.lblCommNote.Name = "lblCommNote";
            this.lblCommNote.Size = new System.Drawing.Size(400, 40);
            this.lblCommNote.TabIndex = 5;
            this.lblCommNote.Text = "注意: 指令方式适用于按需读取\r\n连续方式适用于实时监控";

            // 
            // tabDisplay
            // 
            this.tabDisplay.Controls.Add(this.lblDecimal);
            this.tabDisplay.Controls.Add(this.cmbDecimalPlaces);
            this.tabDisplay.Controls.Add(this.chkShowStableIndicator);
            this.tabDisplay.Controls.Add(this.lblColor);
            this.tabDisplay.Controls.Add(this.cmbWeightColor);
            this.tabDisplay.Location = new System.Drawing.Point(4, 22);
            this.tabDisplay.Name = "tabDisplay";
            this.tabDisplay.Padding = new System.Windows.Forms.Padding(3);
            this.tabDisplay.Size = new System.Drawing.Size(462, 274);
            this.tabDisplay.TabIndex = 1;
            this.tabDisplay.Text = "显示设置";
            this.tabDisplay.UseVisualStyleBackColor = true;

            // 
            // lblDecimal
            // 
            this.lblDecimal.AutoSize = true;
            this.lblDecimal.Location = new System.Drawing.Point(20, 20);
            this.lblDecimal.Name = "lblDecimal";
            this.lblDecimal.Size = new System.Drawing.Size(65, 12);
            this.lblDecimal.TabIndex = 0;
            this.lblDecimal.Text = "小数位数:";

            // 
            // cmbDecimalPlaces
            // 
            this.cmbDecimalPlaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDecimalPlaces.FormattingEnabled = true;
            this.cmbDecimalPlaces.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.cmbDecimalPlaces.Location = new System.Drawing.Point(120, 17);
            this.cmbDecimalPlaces.Name = "cmbDecimalPlaces";
            this.cmbDecimalPlaces.Size = new System.Drawing.Size(100, 20);
            this.cmbDecimalPlaces.TabIndex = 1;
            this.cmbDecimalPlaces.SelectedIndex = 3;

            // 
            // chkShowStableIndicator
            // 
            this.chkShowStableIndicator.AutoSize = true;
            this.chkShowStableIndicator.Checked = true;
            this.chkShowStableIndicator.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowStableIndicator.Location = new System.Drawing.Point(20, 60);
            this.chkShowStableIndicator.Name = "chkShowStableIndicator";
            this.chkShowStableIndicator.Size = new System.Drawing.Size(108, 16);
            this.chkShowStableIndicator.TabIndex = 2;
            this.chkShowStableIndicator.Text = "显示稳定指示灯";
            this.chkShowStableIndicator.UseVisualStyleBackColor = true;

            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(20, 100);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(65, 12);
            this.lblColor.TabIndex = 3;
            this.lblColor.Text = "重量颜色:";

            // 
            // cmbWeightColor
            // 
            this.cmbWeightColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWeightColor.FormattingEnabled = true;
            this.cmbWeightColor.Items.AddRange(new object[] {
            "绿色",
            "蓝色",
            "黑色",
            "红色"});
            this.cmbWeightColor.Location = new System.Drawing.Point(120, 97);
            this.cmbWeightColor.Name = "cmbWeightColor";
            this.cmbWeightColor.Size = new System.Drawing.Size(100, 20);
            this.cmbWeightColor.TabIndex = 4;
            this.cmbWeightColor.SelectedIndex = 0;

            // 
            // tabData
            // 
            this.tabData.Controls.Add(this.chkAutoSave);
            this.tabData.Controls.Add(this.lblMaxRecords);
            this.tabData.Controls.Add(this.numMaxRecords);
            this.tabData.Controls.Add(this.chkExportOnClose);
            this.tabData.Controls.Add(this.lblPath);
            this.tabData.Controls.Add(this.txtExportPath);
            this.tabData.Controls.Add(this.btnBrowseExport);
            this.tabData.Location = new System.Drawing.Point(4, 22);
            this.tabData.Name = "tabData";
            this.tabData.Size = new System.Drawing.Size(462, 274);
            this.tabData.TabIndex = 2;
            this.tabData.Text = "数据设置";
            this.tabData.UseVisualStyleBackColor = true;

            // 
            // chkAutoSave
            // 
            this.chkAutoSave.AutoSize = true;
            this.chkAutoSave.Location = new System.Drawing.Point(20, 20);
            this.chkAutoSave.Name = "chkAutoSave";
            this.chkAutoSave.Size = new System.Drawing.Size(120, 16);
            this.chkAutoSave.TabIndex = 0;
            this.chkAutoSave.Text = "自动保存称重记录";
            this.chkAutoSave.UseVisualStyleBackColor = true;

            // 
            // lblMaxRecords
            // 
            this.lblMaxRecords.AutoSize = true;
            this.lblMaxRecords.Location = new System.Drawing.Point(20, 60);
            this.lblMaxRecords.Name = "lblMaxRecords";
            this.lblMaxRecords.Size = new System.Drawing.Size(77, 12);
            this.lblMaxRecords.TabIndex = 1;
            this.lblMaxRecords.Text = "最大记录数:";

            // 
            // numMaxRecords
            // 
            this.numMaxRecords.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMaxRecords.Location = new System.Drawing.Point(120, 57);
            this.numMaxRecords.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMaxRecords.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMaxRecords.Name = "numMaxRecords";
            this.numMaxRecords.Size = new System.Drawing.Size(100, 21);
            this.numMaxRecords.TabIndex = 2;
            this.numMaxRecords.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});

            // 
            // chkExportOnClose
            // 
            this.chkExportOnClose.AutoSize = true;
            this.chkExportOnClose.Location = new System.Drawing.Point(20, 100);
            this.chkExportOnClose.Name = "chkExportOnClose";
            this.chkExportOnClose.Size = new System.Drawing.Size(132, 16);
            this.chkExportOnClose.TabIndex = 3;
            this.chkExportOnClose.Text = "关闭时自动导出记录";
            this.chkExportOnClose.UseVisualStyleBackColor = true;

            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(20, 140);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(65, 12);
            this.lblPath.TabIndex = 4;
            this.lblPath.Text = "导出路径:";

            // 
            // txtExportPath
            // 
            this.txtExportPath.Location = new System.Drawing.Point(120, 137);
            this.txtExportPath.Name = "txtExportPath";
            this.txtExportPath.Size = new System.Drawing.Size(250, 21);
            this.txtExportPath.TabIndex = 5;
            this.txtExportPath.Text = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);

            // 
            // btnBrowseExport
            // 
            this.btnBrowseExport.Location = new System.Drawing.Point(380, 136);
            this.btnBrowseExport.Name = "btnBrowseExport";
            this.btnBrowseExport.Size = new System.Drawing.Size(60, 25);
            this.btnBrowseExport.TabIndex = 6;
            this.btnBrowseExport.Text = "浏览...";
            this.btnBrowseExport.UseVisualStyleBackColor = true;
            this.btnBrowseExport.Click += new System.EventHandler(this.BtnBrowseExport_Click);

            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(300, 320);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);

            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(390, 320);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);

            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(490, 370);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "系统设置";
            this.tabControl.ResumeLayout(false);
            this.tabComm.ResumeLayout(false);
            this.tabComm.PerformLayout();
            this.tabDisplay.ResumeLayout(false);
            this.tabDisplay.PerformLayout();
            this.tabData.ResumeLayout(false);
            this.tabData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numReadInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxRecords)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabComm;
        private System.Windows.Forms.TabPage tabDisplay;
        private System.Windows.Forms.TabPage tabData;

        // 通讯设置控件
        private System.Windows.Forms.Label lblCommMode;
        private System.Windows.Forms.ComboBox cmbCommMode;
        private System.Windows.Forms.Label lblInterval;
        private System.Windows.Forms.NumericUpDown numReadInterval;
        private System.Windows.Forms.CheckBox chkAutoConnect;
        private System.Windows.Forms.Label lblCommNote;

        // 显示设置控件
        private System.Windows.Forms.Label lblDecimal;
        private System.Windows.Forms.ComboBox cmbDecimalPlaces;
        private System.Windows.Forms.CheckBox chkShowStableIndicator;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.ComboBox cmbWeightColor;

        // 数据设置控件
        private System.Windows.Forms.CheckBox chkAutoSave;
        private System.Windows.Forms.Label lblMaxRecords;
        private System.Windows.Forms.NumericUpDown numMaxRecords;
        private System.Windows.Forms.CheckBox chkExportOnClose;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.TextBox txtExportPath;
        private System.Windows.Forms.Button btnBrowseExport;

        // 底部按钮
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}