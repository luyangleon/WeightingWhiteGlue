using System;
using System.Drawing;
using System.Windows.Forms;

namespace WeightingWhiteGlue
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.gbConnection = new System.Windows.Forms.GroupBox();
            this.lblSite = new System.Windows.Forms.Label();
            this.lblShift = new System.Windows.Forms.Label();
            this.lblConvertMachine = new System.Windows.Forms.Label();
            this.lblPlant = new System.Windows.Forms.Label();
            this.cmbSite = new System.Windows.Forms.ComboBox();
            this.cmbShift = new System.Windows.Forms.ComboBox();
            this.cmbConvertMachine = new System.Windows.Forms.ComboBox();
            this.cmbPlant = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblBaud = new System.Windows.Forms.Label();
            this.gbDisplay = new System.Windows.Forms.GroupBox();
            this.lblWeight = new System.Windows.Forms.Label();
            this.lblUnit = new System.Windows.Forms.Label();
            this.lblWeightType = new System.Windows.Forms.Label();
            this.pnlIndicator = new System.Windows.Forms.Panel();
            this.gbOperation = new System.Windows.Forms.GroupBox();
            this.btnReadEnd = new System.Windows.Forms.Button();
            this.numWaterRate = new System.Windows.Forms.NumericUpDown();
            this.lblWaterRate = new System.Windows.Forms.Label();
            this.btnZero = new System.Windows.Forms.Button();
            this.btnTare = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.chkAutoRead = new System.Windows.Forms.CheckBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.gbRecords = new System.Windows.Forms.GroupBox();
            this.dgvRecords = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Plant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MachineId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Shift = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WeighingType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WaterRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WeighingWeightBegin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WeighingWeightEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WeighingTimeBegin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WeighingTimeEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Site = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.autoReadTimer = new System.Windows.Forms.Timer(this.components);
            this.gbConnection.SuspendLayout();
            this.gbDisplay.SuspendLayout();
            this.gbOperation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWaterRate)).BeginInit();
            this.gbRecords.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).BeginInit();
            this.SuspendLayout();
            // 
            // gbConnection
            // 
            this.gbConnection.Controls.Add(this.lblSite);
            this.gbConnection.Controls.Add(this.lblShift);
            this.gbConnection.Controls.Add(this.lblConvertMachine);
            this.gbConnection.Controls.Add(this.lblPlant);
            this.gbConnection.Controls.Add(this.cmbSite);
            this.gbConnection.Controls.Add(this.cmbShift);
            this.gbConnection.Controls.Add(this.cmbConvertMachine);
            this.gbConnection.Controls.Add(this.cmbPlant);
            this.gbConnection.Controls.Add(this.btnConnect);
            this.gbConnection.Controls.Add(this.btnDisconnect);
            this.gbConnection.Location = new System.Drawing.Point(10, 10);
            this.gbConnection.Name = "gbConnection";
            this.gbConnection.Size = new System.Drawing.Size(300, 120);
            this.gbConnection.TabIndex = 0;
            this.gbConnection.TabStop = false;
            this.gbConnection.Text = "连接设置";
            // 
            // lblSite
            // 
            this.lblSite.AutoSize = true;
            this.lblSite.Location = new System.Drawing.Point(8, 53);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(47, 12);
            this.lblSite.TabIndex = 7;
            this.lblSite.Text = "加糊点:";
            // 
            // lblShift
            // 
            this.lblShift.AutoSize = true;
            this.lblShift.Location = new System.Drawing.Point(202, 28);
            this.lblShift.Name = "lblShift";
            this.lblShift.Size = new System.Drawing.Size(35, 12);
            this.lblShift.TabIndex = 7;
            this.lblShift.Text = "班别:";
            // 
            // lblConvertMachine
            // 
            this.lblConvertMachine.AutoSize = true;
            this.lblConvertMachine.Location = new System.Drawing.Point(111, 27);
            this.lblConvertMachine.Name = "lblConvertMachine";
            this.lblConvertMachine.Size = new System.Drawing.Size(35, 12);
            this.lblConvertMachine.TabIndex = 7;
            this.lblConvertMachine.Text = "机台:";
            // 
            // lblPlant
            // 
            this.lblPlant.AutoSize = true;
            this.lblPlant.Location = new System.Drawing.Point(21, 27);
            this.lblPlant.Name = "lblPlant";
            this.lblPlant.Size = new System.Drawing.Size(35, 12);
            this.lblPlant.TabIndex = 7;
            this.lblPlant.Text = "厂区:";
            // 
            // cmbSite
            // 
            this.cmbSite.FormattingEnabled = true;
            this.cmbSite.Location = new System.Drawing.Point(56, 50);
            this.cmbSite.Name = "cmbSite";
            this.cmbSite.Size = new System.Drawing.Size(49, 20);
            this.cmbSite.TabIndex = 6;
            // 
            // cmbShift
            // 
            this.cmbShift.FormattingEnabled = true;
            this.cmbShift.Location = new System.Drawing.Point(238, 23);
            this.cmbShift.Name = "cmbShift";
            this.cmbShift.Size = new System.Drawing.Size(49, 20);
            this.cmbShift.TabIndex = 6;
            // 
            // cmbConvertMachine
            // 
            this.cmbConvertMachine.FormattingEnabled = true;
            this.cmbConvertMachine.Location = new System.Drawing.Point(147, 23);
            this.cmbConvertMachine.Name = "cmbConvertMachine";
            this.cmbConvertMachine.Size = new System.Drawing.Size(49, 20);
            this.cmbConvertMachine.TabIndex = 6;
            // 
            // cmbPlant
            // 
            this.cmbPlant.FormattingEnabled = true;
            this.cmbPlant.Location = new System.Drawing.Point(56, 23);
            this.cmbPlant.Name = "cmbPlant";
            this.cmbPlant.Size = new System.Drawing.Size(49, 20);
            this.cmbPlant.TabIndex = 6;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(55, 84);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(85, 25);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(150, 84);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(85, 25);
            this.btnDisconnect.TabIndex = 5;
            this.btnDisconnect.Text = "断开";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.BtnDisconnect_Click);
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(18, 139);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(59, 12);
            this.lblPort.TabIndex = 0;
            this.lblPort.Text = "串口:0000";
            // 
            // lblBaud
            // 
            this.lblBaud.AutoSize = true;
            this.lblBaud.Location = new System.Drawing.Point(82, 139);
            this.lblBaud.Name = "lblBaud";
            this.lblBaud.Size = new System.Drawing.Size(71, 12);
            this.lblBaud.TabIndex = 2;
            this.lblBaud.Text = "波特率:0000";
            // 
            // gbDisplay
            // 
            this.gbDisplay.Controls.Add(this.lblWeight);
            this.gbDisplay.Controls.Add(this.lblUnit);
            this.gbDisplay.Controls.Add(this.lblWeightType);
            this.gbDisplay.Controls.Add(this.pnlIndicator);
            this.gbDisplay.Location = new System.Drawing.Point(320, 10);
            this.gbDisplay.Name = "gbDisplay";
            this.gbDisplay.Size = new System.Drawing.Size(350, 120);
            this.gbDisplay.TabIndex = 1;
            this.gbDisplay.TabStop = false;
            this.gbDisplay.Text = "重量显示";
            // 
            // lblWeight
            // 
            this.lblWeight.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeight.ForeColor = System.Drawing.Color.Green;
            this.lblWeight.Location = new System.Drawing.Point(20, 30);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(250, 60);
            this.lblWeight.TabIndex = 0;
            this.lblWeight.Text = "0.000";
            this.lblWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUnit
            // 
            this.lblUnit.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnit.ForeColor = System.Drawing.Color.Green;
            this.lblUnit.Location = new System.Drawing.Point(275, 50);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(60, 40);
            this.lblUnit.TabIndex = 1;
            this.lblUnit.Text = "kg";
            // 
            // lblWeightType
            // 
            this.lblWeightType.AutoSize = true;
            this.lblWeightType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWeightType.ForeColor = System.Drawing.Color.Blue;
            this.lblWeightType.Location = new System.Drawing.Point(20, 95);
            this.lblWeightType.Name = "lblWeightType";
            this.lblWeightType.Size = new System.Drawing.Size(32, 17);
            this.lblWeightType.TabIndex = 2;
            this.lblWeightType.Text = "净重";
            // 
            // pnlIndicator
            // 
            this.pnlIndicator.BackColor = System.Drawing.Color.Gray;
            this.pnlIndicator.Location = new System.Drawing.Point(280, 30);
            this.pnlIndicator.Name = "pnlIndicator";
            this.pnlIndicator.Size = new System.Drawing.Size(15, 15);
            this.pnlIndicator.TabIndex = 3;
            // 
            // gbOperation
            // 
            this.gbOperation.Controls.Add(this.btnReadEnd);
            this.gbOperation.Controls.Add(this.numWaterRate);
            this.gbOperation.Controls.Add(this.lblWaterRate);
            this.gbOperation.Controls.Add(this.btnZero);
            this.gbOperation.Controls.Add(this.btnTare);
            this.gbOperation.Controls.Add(this.btnRead);
            this.gbOperation.Controls.Add(this.chkAutoRead);
            this.gbOperation.Location = new System.Drawing.Point(680, 10);
            this.gbOperation.Name = "gbOperation";
            this.gbOperation.Size = new System.Drawing.Size(300, 120);
            this.gbOperation.TabIndex = 2;
            this.gbOperation.TabStop = false;
            this.gbOperation.Text = "操作";
            // 
            // btnReadEnd
            // 
            this.btnReadEnd.Enabled = false;
            this.btnReadEnd.Location = new System.Drawing.Point(152, 55);
            this.btnReadEnd.Name = "btnReadEnd";
            this.btnReadEnd.Size = new System.Drawing.Size(138, 37);
            this.btnReadEnd.TabIndex = 7;
            this.btnReadEnd.Text = "结束称重 (R)";
            this.btnReadEnd.UseVisualStyleBackColor = true;
            this.btnReadEnd.Click += new System.EventHandler(this.btnReadEnd_Click);
            // 
            // numWaterRate
            // 
            this.numWaterRate.DecimalPlaces = 3;
            this.numWaterRate.Enabled = false;
            this.numWaterRate.Location = new System.Drawing.Point(73, 24);
            this.numWaterRate.Name = "numWaterRate";
            this.numWaterRate.Size = new System.Drawing.Size(70, 21);
            this.numWaterRate.TabIndex = 6;
            // 
            // lblWaterRate
            // 
            this.lblWaterRate.AutoSize = true;
            this.lblWaterRate.Location = new System.Drawing.Point(8, 28);
            this.lblWaterRate.Name = "lblWaterRate";
            this.lblWaterRate.Size = new System.Drawing.Size(59, 12);
            this.lblWaterRate.TabIndex = 4;
            this.lblWaterRate.Text = "注水量kg:";
            // 
            // btnZero
            // 
            this.btnZero.Enabled = false;
            this.btnZero.Location = new System.Drawing.Point(152, 20);
            this.btnZero.Name = "btnZero";
            this.btnZero.Size = new System.Drawing.Size(68, 25);
            this.btnZero.TabIndex = 0;
            this.btnZero.Text = "置零 (Z)";
            this.btnZero.UseVisualStyleBackColor = true;
            this.btnZero.Visible = false;
            this.btnZero.Click += new System.EventHandler(this.BtnZero_Click);
            // 
            // btnTare
            // 
            this.btnTare.Enabled = false;
            this.btnTare.Location = new System.Drawing.Point(226, 20);
            this.btnTare.Name = "btnTare";
            this.btnTare.Size = new System.Drawing.Size(68, 25);
            this.btnTare.TabIndex = 1;
            this.btnTare.Text = "去皮 (T)";
            this.btnTare.UseVisualStyleBackColor = true;
            this.btnTare.Visible = false;
            this.btnTare.Click += new System.EventHandler(this.BtnTare_Click);
            // 
            // btnRead
            // 
            this.btnRead.Enabled = false;
            this.btnRead.Location = new System.Drawing.Point(10, 55);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(138, 37);
            this.btnRead.TabIndex = 2;
            this.btnRead.Text = "开始称重 (R)";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.BtnRead_Click);
            // 
            // chkAutoRead
            // 
            this.chkAutoRead.AutoSize = true;
            this.chkAutoRead.Enabled = false;
            this.chkAutoRead.Location = new System.Drawing.Point(10, 98);
            this.chkAutoRead.Name = "chkAutoRead";
            this.chkAutoRead.Size = new System.Drawing.Size(72, 16);
            this.chkAutoRead.TabIndex = 3;
            this.chkAutoRead.Text = "自动读取";
            this.chkAutoRead.UseVisualStyleBackColor = true;
            this.chkAutoRead.Visible = false;
            this.chkAutoRead.CheckedChanged += new System.EventHandler(this.ChkAutoRead_CheckedChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatus.Location = new System.Drawing.Point(159, 135);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(821, 20);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "状态: 未连接";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbRecords
            // 
            this.gbRecords.Controls.Add(this.dgvRecords);
            this.gbRecords.Location = new System.Drawing.Point(10, 160);
            this.gbRecords.Name = "gbRecords";
            this.gbRecords.Size = new System.Drawing.Size(970, 480);
            this.gbRecords.TabIndex = 4;
            this.gbRecords.TabStop = false;
            this.gbRecords.Text = "称重记录";
            // 
            // dgvRecords
            // 
            this.dgvRecords.AllowUserToAddRows = false;
            this.dgvRecords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecords.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Plant,
            this.MachineId,
            this.Shift,
            this.WeighingType,
            this.WaterRate,
            this.WeighingWeightBegin,
            this.WeighingWeightEnd,
            this.WeighingTimeBegin,
            this.WeighingTimeEnd,
            this.Site});
            this.dgvRecords.Location = new System.Drawing.Point(10, 20);
            this.dgvRecords.Name = "dgvRecords";
            this.dgvRecords.ReadOnly = true;
            this.dgvRecords.RowTemplate.Height = 23;
            this.dgvRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecords.Size = new System.Drawing.Size(950, 450);
            this.dgvRecords.TabIndex = 0;
            this.dgvRecords.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvRecords_CellFormatting);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.FillWeight = 60F;
            this.Id.HeaderText = "编号";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Plant
            // 
            this.Plant.DataPropertyName = "Plant";
            this.Plant.FillWeight = 60F;
            this.Plant.HeaderText = "厂区";
            this.Plant.Name = "Plant";
            this.Plant.ReadOnly = true;
            this.Plant.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MachineId
            // 
            this.MachineId.DataPropertyName = "MachineId";
            this.MachineId.FillWeight = 50F;
            this.MachineId.HeaderText = "机台";
            this.MachineId.Name = "MachineId";
            this.MachineId.ReadOnly = true;
            this.MachineId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Shift
            // 
            this.Shift.DataPropertyName = "Shift";
            this.Shift.FillWeight = 60F;
            this.Shift.HeaderText = "班别";
            this.Shift.Name = "Shift";
            this.Shift.ReadOnly = true;
            this.Shift.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // WeighingType
            // 
            this.WeighingType.DataPropertyName = "WeighingType";
            this.WeighingType.FillWeight = 70F;
            this.WeighingType.HeaderText = "称重类型";
            this.WeighingType.Name = "WeighingType";
            this.WeighingType.ReadOnly = true;
            this.WeighingType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // WaterRate
            // 
            this.WaterRate.DataPropertyName = "WaterRate";
            this.WaterRate.FillWeight = 90F;
            this.WaterRate.HeaderText = "注水量";
            this.WaterRate.Name = "WaterRate";
            this.WaterRate.ReadOnly = true;
            this.WaterRate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // WeighingWeightBegin
            // 
            this.WeighingWeightBegin.DataPropertyName = "WeighingWeightBegin";
            this.WeighingWeightBegin.FillWeight = 90F;
            this.WeighingWeightBegin.HeaderText = "开始称重重量";
            this.WeighingWeightBegin.Name = "WeighingWeightBegin";
            this.WeighingWeightBegin.ReadOnly = true;
            this.WeighingWeightBegin.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // WeighingWeightEnd
            // 
            this.WeighingWeightEnd.DataPropertyName = "WeighingWeightEnd";
            this.WeighingWeightEnd.FillWeight = 90F;
            this.WeighingWeightEnd.HeaderText = "结束称重重量";
            this.WeighingWeightEnd.Name = "WeighingWeightEnd";
            this.WeighingWeightEnd.ReadOnly = true;
            this.WeighingWeightEnd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // WeighingTimeBegin
            // 
            this.WeighingTimeBegin.DataPropertyName = "WeighingTimeBegin";
            this.WeighingTimeBegin.HeaderText = "开始称重时间";
            this.WeighingTimeBegin.Name = "WeighingTimeBegin";
            this.WeighingTimeBegin.ReadOnly = true;
            this.WeighingTimeBegin.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // WeighingTimeEnd
            // 
            this.WeighingTimeEnd.DataPropertyName = "WeighingTimeEnd";
            this.WeighingTimeEnd.HeaderText = "结束称重时间";
            this.WeighingTimeEnd.Name = "WeighingTimeEnd";
            this.WeighingTimeEnd.ReadOnly = true;
            this.WeighingTimeEnd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Site
            // 
            this.Site.DataPropertyName = "Site";
            this.Site.FillWeight = 60F;
            this.Site.HeaderText = "加糊点";
            this.Site.Name = "Site";
            this.Site.ReadOnly = true;
            this.Site.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // autoReadTimer
            // 
            this.autoReadTimer.Interval = 1000;
            this.autoReadTimer.Tick += new System.EventHandler(this.AutoReadTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.lblBaud);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.gbRecords);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.gbOperation);
            this.Controls.Add(this.gbDisplay);
            this.Controls.Add(this.gbConnection);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "白糊称重";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.gbConnection.ResumeLayout(false);
            this.gbConnection.PerformLayout();
            this.gbDisplay.ResumeLayout(false);
            this.gbDisplay.PerformLayout();
            this.gbOperation.ResumeLayout(false);
            this.gbOperation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWaterRate)).EndInit();
            this.gbRecords.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbConnection;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblBaud;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;

        private System.Windows.Forms.GroupBox gbDisplay;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.Label lblWeightType;
        private System.Windows.Forms.Panel pnlIndicator;

        private System.Windows.Forms.GroupBox gbOperation;
        private System.Windows.Forms.Button btnZero;
        private System.Windows.Forms.Button btnTare;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.CheckBox chkAutoRead;

        private System.Windows.Forms.Label lblStatus;

        private System.Windows.Forms.GroupBox gbRecords;
        private System.Windows.Forms.DataGridView dgvRecords;

        private System.Windows.Forms.Timer autoReadTimer;
        private Label lblPlant;
        private ComboBox cmbPlant;
        private Label lblConvertMachine;
        private ComboBox cmbConvertMachine;
        private Label lblWaterRate;
        private NumericUpDown numWaterRate;
        private Button btnReadEnd;
        private Label lblShift;
        private ComboBox cmbShift;
        private Label lblSite;
        private ComboBox cmbSite;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn Plant;
        private DataGridViewTextBoxColumn MachineId;
        private DataGridViewTextBoxColumn Shift;
        private DataGridViewTextBoxColumn WeighingType;
        private DataGridViewTextBoxColumn WaterRate;
        private DataGridViewTextBoxColumn WeighingWeightBegin;
        private DataGridViewTextBoxColumn WeighingWeightEnd;
        private DataGridViewTextBoxColumn WeighingTimeBegin;
        private DataGridViewTextBoxColumn WeighingTimeEnd;
        private DataGridViewTextBoxColumn Site;
    }
}