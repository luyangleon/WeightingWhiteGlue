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
            this.gbConnection = new System.Windows.Forms.GroupBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblBaud = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.gbDisplay = new System.Windows.Forms.GroupBox();
            this.lblWeight = new System.Windows.Forms.Label();
            this.lblUnit = new System.Windows.Forms.Label();
            this.lblWeightType = new System.Windows.Forms.Label();
            this.pnlIndicator = new System.Windows.Forms.Panel();
            this.gbOperation = new System.Windows.Forms.GroupBox();
            this.btnZero = new System.Windows.Forms.Button();
            this.btnTare = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.chkAutoRead = new System.Windows.Forms.CheckBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.gbRecords = new System.Windows.Forms.GroupBox();
            this.dgvRecords = new System.Windows.Forms.DataGridView();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.autoReadTimer = new System.Windows.Forms.Timer(this.components);
            this.cmbPlant = new System.Windows.Forms.ComboBox();
            this.lblPlant = new System.Windows.Forms.Label();
            this.cmbConvertMachine = new System.Windows.Forms.ComboBox();
            this.lblConvertMachine = new System.Windows.Forms.Label();
            this.lblWaterRate = new System.Windows.Forms.Label();
            this.numWaterRate = new System.Windows.Forms.NumericUpDown();
            this.gbConnection.SuspendLayout();
            this.gbDisplay.SuspendLayout();
            this.gbOperation.SuspendLayout();
            this.gbRecords.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaterRate)).BeginInit();
            this.SuspendLayout();
            // 
            // gbConnection
            // 
            this.gbConnection.Controls.Add(this.lblConvertMachine);
            this.gbConnection.Controls.Add(this.lblPlant);
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
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(18, 139);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(59, 12);
            this.lblPort.TabIndex = 0;
            this.lblPort.Text = "串口:com2";
            // 
            // lblBaud
            // 
            this.lblBaud.AutoSize = true;
            this.lblBaud.Location = new System.Drawing.Point(82, 139);
            this.lblBaud.Name = "lblBaud";
            this.lblBaud.Size = new System.Drawing.Size(71, 12);
            this.lblBaud.TabIndex = 2;
            this.lblBaud.Text = "波特率:1200";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(41, 65);
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
            this.btnDisconnect.Location = new System.Drawing.Point(136, 65);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(85, 25);
            this.btnDisconnect.TabIndex = 5;
            this.btnDisconnect.Text = "断开";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.BtnDisconnect_Click);
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
            this.lblWeightType.Text = "毛重";
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
            // btnZero
            // 
            this.btnZero.Enabled = false;
            this.btnZero.Location = new System.Drawing.Point(10, 84);
            this.btnZero.Name = "btnZero";
            this.btnZero.Size = new System.Drawing.Size(85, 30);
            this.btnZero.TabIndex = 0;
            this.btnZero.Text = "置零 (Z)";
            this.btnZero.UseVisualStyleBackColor = true;
            this.btnZero.Visible = false;
            this.btnZero.Click += new System.EventHandler(this.BtnZero_Click);
            // 
            // btnTare
            // 
            this.btnTare.Enabled = false;
            this.btnTare.Location = new System.Drawing.Point(101, 84);
            this.btnTare.Name = "btnTare";
            this.btnTare.Size = new System.Drawing.Size(85, 30);
            this.btnTare.TabIndex = 1;
            this.btnTare.Text = "去皮 (T)";
            this.btnTare.UseVisualStyleBackColor = true;
            this.btnTare.Visible = false;
            this.btnTare.Click += new System.EventHandler(this.BtnTare_Click);
            // 
            // btnRead
            // 
            this.btnRead.Enabled = false;
            this.btnRead.Location = new System.Drawing.Point(149, 20);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(138, 37);
            this.btnRead.TabIndex = 2;
            this.btnRead.Text = "读取 (R)";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.BtnRead_Click);
            // 
            // chkAutoRead
            // 
            this.chkAutoRead.AutoSize = true;
            this.chkAutoRead.Enabled = false;
            this.chkAutoRead.Location = new System.Drawing.Point(10, 65);
            this.chkAutoRead.Name = "chkAutoRead";
            this.chkAutoRead.Size = new System.Drawing.Size(72, 16);
            this.chkAutoRead.TabIndex = 3;
            this.chkAutoRead.Text = "自动读取";
            this.chkAutoRead.UseVisualStyleBackColor = true;
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
            this.colTime,
            this.colWeight,
            this.colUnit,
            this.colType,
            this.colStatus});
            this.dgvRecords.Location = new System.Drawing.Point(10, 20);
            this.dgvRecords.Name = "dgvRecords";
            this.dgvRecords.ReadOnly = true;
            this.dgvRecords.RowTemplate.Height = 23;
            this.dgvRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecords.Size = new System.Drawing.Size(950, 450);
            this.dgvRecords.TabIndex = 0;
            // 
            // colTime
            // 
            this.colTime.HeaderText = "时间";
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            // 
            // colWeight
            // 
            this.colWeight.HeaderText = "重量";
            this.colWeight.Name = "colWeight";
            this.colWeight.ReadOnly = true;
            // 
            // colUnit
            // 
            this.colUnit.HeaderText = "单位";
            this.colUnit.Name = "colUnit";
            this.colUnit.ReadOnly = true;
            // 
            // colType
            // 
            this.colType.HeaderText = "类型";
            this.colType.Name = "colType";
            this.colType.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "状态";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // autoReadTimer
            // 
            this.autoReadTimer.Interval = 1000;
            this.autoReadTimer.Tick += new System.EventHandler(this.AutoReadTimer_Tick);
            // 
            // cmbPlant
            // 
            this.cmbPlant.FormattingEnabled = true;
            this.cmbPlant.Location = new System.Drawing.Point(65, 25);
            this.cmbPlant.Name = "cmbPlant";
            this.cmbPlant.Size = new System.Drawing.Size(49, 20);
            this.cmbPlant.TabIndex = 6;
            // 
            // lblPlant
            // 
            this.lblPlant.AutoSize = true;
            this.lblPlant.Location = new System.Drawing.Point(24, 28);
            this.lblPlant.Name = "lblPlant";
            this.lblPlant.Size = new System.Drawing.Size(35, 12);
            this.lblPlant.TabIndex = 7;
            this.lblPlant.Text = "厂区:";
            // 
            // cmbConvertMachine
            // 
            this.cmbConvertMachine.FormattingEnabled = true;
            this.cmbConvertMachine.Location = new System.Drawing.Point(172, 25);
            this.cmbConvertMachine.Name = "cmbConvertMachine";
            this.cmbConvertMachine.Size = new System.Drawing.Size(49, 20);
            this.cmbConvertMachine.TabIndex = 6;
            // 
            // lblConvertMachine
            // 
            this.lblConvertMachine.AutoSize = true;
            this.lblConvertMachine.Location = new System.Drawing.Point(131, 28);
            this.lblConvertMachine.Name = "lblConvertMachine";
            this.lblConvertMachine.Size = new System.Drawing.Size(35, 12);
            this.lblConvertMachine.TabIndex = 7;
            this.lblConvertMachine.Text = "机台:";
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
            // numWaterRate
            // 
            this.numWaterRate.DecimalPlaces = 3;
            this.numWaterRate.Enabled = false;
            this.numWaterRate.Location = new System.Drawing.Point(73, 24);
            this.numWaterRate.Name = "numWaterRate";
            this.numWaterRate.Size = new System.Drawing.Size(70, 21);
            this.numWaterRate.TabIndex = 6;
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
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "白糊称重";
            this.gbConnection.ResumeLayout(false);
            this.gbConnection.PerformLayout();
            this.gbDisplay.ResumeLayout(false);
            this.gbDisplay.PerformLayout();
            this.gbOperation.ResumeLayout(false);
            this.gbOperation.PerformLayout();
            this.gbRecords.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaterRate)).EndInit();
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
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;

        private System.Windows.Forms.Timer autoReadTimer;
        private Label lblPlant;
        private ComboBox cmbPlant;
        private Label lblConvertMachine;
        private ComboBox cmbConvertMachine;
        private Label lblWaterRate;
        private NumericUpDown numWaterRate;
    }
}