// MainForm.Designer.cs
namespace ElevatorSystem
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlMain = new Panel();
            pnlBuilding = new Panel();
            lblFloor1Indicator = new Label();
            lblFloor0Indicator = new Label();
            btnCallUp = new Button();
            btnCallDown = new Button();
            pbShaft = new PictureBox();
            lblFloorDisplay = new Label();
            pbElevatorCar = new PictureBox();
            pbDoorLeft = new PictureBox();
            pbDoorRight = new PictureBox();
            grpControlPanel = new GroupBox();
            lblControlPanelTitle = new Label();
            lblControlPanelDisplay = new Label();
            lblStatusDisplay = new Label();
            btnFloor1 = new Button();
            btnFloor0 = new Button();
            btnOpenDoor = new Button();
            btnCloseDoor = new Button();
            btnAlarm = new Button();
            btnEmergencyStop = new Button();
            grpLogs = new GroupBox();
            dgvLogs = new DataGridView();
            btnRefreshLogs = new Button();
            btnClearLogs = new Button();
            btnExportLogs = new Button();
            statusStrip = new StatusStrip();
            lblStatusBar = new ToolStripStatusLabel();
            progressBar = new ToolStripProgressBar();
            lblTitle = new Label();
            pnlMain.SuspendLayout();
            pnlBuilding.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbShaft).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbElevatorCar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbDoorLeft).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbDoorRight).BeginInit();
            grpControlPanel.SuspendLayout();
            grpLogs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).BeginInit();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(15, 23, 42);
            pnlMain.Controls.Add(pnlBuilding);
            pnlMain.Controls.Add(grpControlPanel);
            pnlMain.Controls.Add(grpLogs);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 96);
            pnlMain.Margin = new Padding(3, 4, 3, 4);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(1535, 893);
            pnlMain.TabIndex = 0;
            // 
            // pnlBuilding
            // 
            pnlBuilding.BackColor = Color.FromArgb(41, 45, 62);
            pnlBuilding.BorderStyle = BorderStyle.FixedSingle;
            pnlBuilding.Controls.Add(lblFloorDisplay);
            pnlBuilding.Controls.Add(pbDoorRight);
            pnlBuilding.Controls.Add(lblFloor1Indicator);
            pnlBuilding.Controls.Add(lblFloor0Indicator);
            pnlBuilding.Controls.Add(btnCallUp);
            pnlBuilding.Controls.Add(btnCallDown);
            pnlBuilding.Controls.Add(pbDoorLeft);
            pnlBuilding.Controls.Add(pbElevatorCar);
            pnlBuilding.Controls.Add(pbShaft);
            pnlBuilding.Location = new Point(360, 24);
            pnlBuilding.Margin = new Padding(3, 4, 3, 4);
            pnlBuilding.Name = "pnlBuilding";
            pnlBuilding.Size = new Size(499, 833);
            pnlBuilding.TabIndex = 0;
            // 
            // lblFloor1Indicator
            // 
            lblFloor1Indicator.AutoSize = true;
            lblFloor1Indicator.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFloor1Indicator.ForeColor = Color.FromArgb(203, 213, 225);
            lblFloor1Indicator.Location = new Point(440, 120);
            lblFloor1Indicator.Margin = new Padding(2, 0, 2, 0);
            lblFloor1Indicator.Name = "lblFloor1Indicator";
            lblFloor1Indicator.Size = new Size(24, 28);
            lblFloor1Indicator.TabIndex = 9;
            lblFloor1Indicator.Text = "1";
            // 
            // lblFloor0Indicator
            // 
            lblFloor0Indicator.AutoSize = true;
            lblFloor0Indicator.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFloor0Indicator.ForeColor = Color.FromArgb(203, 213, 225);
            lblFloor0Indicator.Location = new Point(440, 480);
            lblFloor0Indicator.Margin = new Padding(2, 0, 2, 0);
            lblFloor0Indicator.Name = "lblFloor0Indicator";
            lblFloor0Indicator.Size = new Size(26, 28);
            lblFloor0Indicator.TabIndex = 8;
            lblFloor0Indicator.Text = "G";
            // 
            // btnCallUp
            // 
            btnCallUp.BackColor = Color.FromArgb(59, 130, 246);
            btnCallUp.FlatAppearance.BorderSize = 0;
            btnCallUp.FlatAppearance.MouseDownBackColor = Color.FromArgb(37, 99, 235);
            btnCallUp.FlatAppearance.MouseOverBackColor = Color.FromArgb(96, 165, 250);
            btnCallUp.FlatStyle = FlatStyle.Flat;
            btnCallUp.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCallUp.ForeColor = Color.White;
            btnCallUp.Location = new Point(430, 152);
            btnCallUp.Margin = new Padding(3, 4, 3, 4);
            btnCallUp.Name = "btnCallUp";
            btnCallUp.Size = new Size(48, 48);
            btnCallUp.TabIndex = 7;
            btnCallUp.Text = "▲";
            btnCallUp.UseVisualStyleBackColor = false;
            // 
            // btnCallDown
            // 
            btnCallDown.BackColor = Color.FromArgb(59, 130, 246);
            btnCallDown.FlatAppearance.BorderSize = 0;
            btnCallDown.FlatAppearance.MouseDownBackColor = Color.FromArgb(37, 99, 235);
            btnCallDown.FlatAppearance.MouseOverBackColor = Color.FromArgb(96, 165, 250);
            btnCallDown.FlatStyle = FlatStyle.Flat;
            btnCallDown.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCallDown.ForeColor = Color.White;
            btnCallDown.Location = new Point(430, 432);
            btnCallDown.Margin = new Padding(3, 4, 3, 4);
            btnCallDown.Name = "btnCallDown";
            btnCallDown.Size = new Size(48, 48);
            btnCallDown.TabIndex = 6;
            btnCallDown.Text = "▼";
            btnCallDown.UseVisualStyleBackColor = false;
            // 
            // pbShaft
            // 
            pbShaft.BackColor = Color.FromArgb(30, 33, 45);
            pbShaft.Location = new Point(40, 36);
            pbShaft.Margin = new Padding(3, 4, 3, 4);
            pbShaft.Name = "pbShaft";
            pbShaft.Size = new Size(375, 674);
            pbShaft.TabIndex = 0;
            pbShaft.TabStop = false;
            // 
            // lblFloorDisplay
            // 
            lblFloorDisplay.BackColor = Color.Black;
            lblFloorDisplay.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFloorDisplay.ForeColor = Color.FromArgb(34, 197, 94);
            lblFloorDisplay.Location = new Point(219, 540);
            lblFloorDisplay.Margin = new Padding(2, 0, 2, 0);
            lblFloorDisplay.Name = "lblFloorDisplay";
            lblFloorDisplay.Size = new Size(64, 40);
            lblFloorDisplay.TabIndex = 4;
            lblFloorDisplay.Text = "G";
            lblFloorDisplay.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pbElevatorCar
            // 
            pbElevatorCar.BackColor = Color.FromArgb(220, 220, 220);
            pbElevatorCar.BackgroundImageLayout = ImageLayout.Center;
            pbElevatorCar.BorderStyle = BorderStyle.FixedSingle;
            pbElevatorCar.ErrorImage = null;
            pbElevatorCar.InitialImage = null;
            pbElevatorCar.Location = new Point(143, 540);
            pbElevatorCar.Margin = new Padding(3, 4, 3, 4);
            pbElevatorCar.Name = "pbElevatorCar";
            pbElevatorCar.Size = new Size(192, 144);
            pbElevatorCar.TabIndex = 1;
            pbElevatorCar.TabStop = false;
            // 
            // pbDoorLeft
            // 
            pbDoorLeft.BackColor = Color.FromArgb(255, 192, 128);
            pbDoorLeft.BorderStyle = BorderStyle.FixedSingle;
            pbDoorLeft.Location = new Point(143, 540);
            pbDoorLeft.Margin = new Padding(3, 4, 3, 4);
            pbDoorLeft.Name = "pbDoorLeft";
            pbDoorLeft.Size = new Size(96, 144);
            pbDoorLeft.TabIndex = 2;
            pbDoorLeft.TabStop = false;
            // 
            // pbDoorRight
            // 
            pbDoorRight.BackColor = Color.FromArgb(255, 224, 192);
            pbDoorRight.BorderStyle = BorderStyle.FixedSingle;
            pbDoorRight.Location = new Point(239, 540);
            pbDoorRight.Margin = new Padding(3, 4, 3, 4);
            pbDoorRight.Name = "pbDoorRight";
            pbDoorRight.Size = new Size(96, 144);
            pbDoorRight.TabIndex = 3;
            pbDoorRight.TabStop = false;
            // 
            // grpControlPanel
            // 
            grpControlPanel.BackColor = Color.FromArgb(30, 41, 59);
            grpControlPanel.Controls.Add(lblControlPanelTitle);
            grpControlPanel.Controls.Add(lblControlPanelDisplay);
            grpControlPanel.Controls.Add(lblStatusDisplay);
            grpControlPanel.Controls.Add(btnFloor1);
            grpControlPanel.Controls.Add(btnFloor0);
            grpControlPanel.Controls.Add(btnOpenDoor);
            grpControlPanel.Controls.Add(btnCloseDoor);
            grpControlPanel.Controls.Add(btnAlarm);
            grpControlPanel.Controls.Add(btnEmergencyStop);
            grpControlPanel.FlatStyle = FlatStyle.Flat;
            grpControlPanel.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            grpControlPanel.ForeColor = Color.FromArgb(203, 213, 225);
            grpControlPanel.Location = new Point(24, 24);
            grpControlPanel.Margin = new Padding(3, 4, 3, 4);
            grpControlPanel.Name = "grpControlPanel";
            grpControlPanel.Padding = new Padding(3, 4, 3, 4);
            grpControlPanel.Size = new Size(280, 560);
            grpControlPanel.TabIndex = 1;
            grpControlPanel.TabStop = false;
            grpControlPanel.Text = "Elevator Control Panel";
            // 
            // lblControlPanelTitle
            // 
            lblControlPanelTitle.BackColor = Color.FromArgb(59, 130, 246);
            lblControlPanelTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblControlPanelTitle.ForeColor = Color.White;
            lblControlPanelTitle.Location = new Point(0, 0);
            lblControlPanelTitle.Margin = new Padding(2, 0, 2, 0);
            lblControlPanelTitle.Name = "lblControlPanelTitle";
            lblControlPanelTitle.Size = new Size(280, 40);
            lblControlPanelTitle.TabIndex = 8;
            lblControlPanelTitle.Text = "BURJ KHALIFA";
            lblControlPanelTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblControlPanelDisplay
            // 
            lblControlPanelDisplay.BackColor = Color.Black;
            lblControlPanelDisplay.Font = new Font("Segoe UI", 28F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblControlPanelDisplay.ForeColor = Color.FromArgb(34, 197, 94);
            lblControlPanelDisplay.Location = new Point(60, 60);
            lblControlPanelDisplay.Margin = new Padding(2, 0, 2, 0);
            lblControlPanelDisplay.Name = "lblControlPanelDisplay";
            lblControlPanelDisplay.Size = new Size(160, 64);
            lblControlPanelDisplay.TabIndex = 0;
            lblControlPanelDisplay.Text = "G";
            lblControlPanelDisplay.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStatusDisplay
            // 
            lblStatusDisplay.BackColor = Color.FromArgb(51, 65, 85);
            lblStatusDisplay.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStatusDisplay.ForeColor = Color.FromArgb(34, 197, 94);
            lblStatusDisplay.Location = new Point(20, 136);
            lblStatusDisplay.Margin = new Padding(2, 0, 2, 0);
            lblStatusDisplay.Name = "lblStatusDisplay";
            lblStatusDisplay.Size = new Size(240, 32);
            lblStatusDisplay.TabIndex = 1;
            lblStatusDisplay.Text = "● READY";
            lblStatusDisplay.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnFloor1
            // 
            btnFloor1.BackColor = Color.FromArgb(59, 130, 246);
            btnFloor1.FlatAppearance.BorderSize = 0;
            btnFloor1.FlatAppearance.MouseDownBackColor = Color.FromArgb(37, 99, 235);
            btnFloor1.FlatAppearance.MouseOverBackColor = Color.FromArgb(96, 165, 250);
            btnFloor1.FlatStyle = FlatStyle.Flat;
            btnFloor1.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnFloor1.ForeColor = Color.White;
            btnFloor1.Location = new Point(160, 192);
            btnFloor1.Margin = new Padding(3, 4, 3, 4);
            btnFloor1.Name = "btnFloor1";
            btnFloor1.Size = new Size(64, 64);
            btnFloor1.TabIndex = 2;
            btnFloor1.Text = "1";
            btnFloor1.UseVisualStyleBackColor = false;
            // 
            // btnFloor0
            // 
            btnFloor0.BackColor = Color.FromArgb(59, 130, 246);
            btnFloor0.FlatAppearance.BorderSize = 0;
            btnFloor0.FlatAppearance.MouseDownBackColor = Color.FromArgb(37, 99, 235);
            btnFloor0.FlatAppearance.MouseOverBackColor = Color.FromArgb(96, 165, 250);
            btnFloor0.FlatStyle = FlatStyle.Flat;
            btnFloor0.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnFloor0.ForeColor = Color.White;
            btnFloor0.Location = new Point(60, 192);
            btnFloor0.Margin = new Padding(3, 4, 3, 4);
            btnFloor0.Name = "btnFloor0";
            btnFloor0.Size = new Size(64, 64);
            btnFloor0.TabIndex = 3;
            btnFloor0.Text = "G";
            btnFloor0.UseVisualStyleBackColor = false;
            // 
            // btnOpenDoor
            // 
            btnOpenDoor.BackColor = Color.FromArgb(34, 197, 94);
            btnOpenDoor.FlatAppearance.BorderSize = 0;
            btnOpenDoor.FlatAppearance.MouseDownBackColor = Color.FromArgb(21, 128, 61);
            btnOpenDoor.FlatAppearance.MouseOverBackColor = Color.FromArgb(74, 222, 128);
            btnOpenDoor.FlatStyle = FlatStyle.Flat;
            btnOpenDoor.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnOpenDoor.ForeColor = Color.White;
            btnOpenDoor.Location = new Point(36, 280);
            btnOpenDoor.Margin = new Padding(3, 4, 3, 4);
            btnOpenDoor.Name = "btnOpenDoor";
            btnOpenDoor.Size = new Size(96, 48);
            btnOpenDoor.TabIndex = 4;
            btnOpenDoor.Text = "◀▶ OPEN";
            btnOpenDoor.UseVisualStyleBackColor = false;
            // 
            // btnCloseDoor
            // 
            btnCloseDoor.BackColor = Color.FromArgb(239, 68, 68);
            btnCloseDoor.FlatAppearance.BorderSize = 0;
            btnCloseDoor.FlatAppearance.MouseDownBackColor = Color.FromArgb(185, 28, 28);
            btnCloseDoor.FlatAppearance.MouseOverBackColor = Color.FromArgb(248, 113, 113);
            btnCloseDoor.FlatStyle = FlatStyle.Flat;
            btnCloseDoor.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCloseDoor.ForeColor = Color.White;
            btnCloseDoor.Location = new Point(148, 280);
            btnCloseDoor.Margin = new Padding(3, 4, 3, 4);
            btnCloseDoor.Name = "btnCloseDoor";
            btnCloseDoor.Size = new Size(96, 48);
            btnCloseDoor.TabIndex = 5;
            btnCloseDoor.Text = "▶◀ CLOSE";
            btnCloseDoor.UseVisualStyleBackColor = false;
            // 
            // btnAlarm
            // 
            btnAlarm.BackColor = Color.FromArgb(245, 158, 11);
            btnAlarm.FlatAppearance.BorderSize = 0;
            btnAlarm.FlatAppearance.MouseDownBackColor = Color.FromArgb(180, 83, 9);
            btnAlarm.FlatAppearance.MouseOverBackColor = Color.FromArgb(251, 191, 36);
            btnAlarm.FlatStyle = FlatStyle.Flat;
            btnAlarm.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAlarm.ForeColor = Color.White;
            btnAlarm.Location = new Point(36, 344);
            btnAlarm.Margin = new Padding(3, 4, 3, 4);
            btnAlarm.Name = "btnAlarm";
            btnAlarm.Size = new Size(208, 48);
            btnAlarm.TabIndex = 6;
            btnAlarm.Text = "🔄 CALL HELP";
            btnAlarm.UseVisualStyleBackColor = false;
            // 
            // btnEmergencyStop
            // 
            btnEmergencyStop.BackColor = Color.FromArgb(239, 68, 68);
            btnEmergencyStop.FlatAppearance.BorderSize = 0;
            btnEmergencyStop.FlatAppearance.MouseDownBackColor = Color.FromArgb(185, 28, 28);
            btnEmergencyStop.FlatAppearance.MouseOverBackColor = Color.FromArgb(248, 113, 113);
            btnEmergencyStop.FlatStyle = FlatStyle.Flat;
            btnEmergencyStop.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEmergencyStop.ForeColor = Color.White;
            btnEmergencyStop.Location = new Point(36, 408);
            btnEmergencyStop.Margin = new Padding(3, 4, 3, 4);
            btnEmergencyStop.Name = "btnEmergencyStop";
            btnEmergencyStop.Size = new Size(208, 48);
            btnEmergencyStop.TabIndex = 7;
            btnEmergencyStop.Text = "\U0001f6d1 EMERGENCY";
            btnEmergencyStop.UseVisualStyleBackColor = false;
            // 
            // grpLogs
            // 
            grpLogs.BackColor = Color.FromArgb(30, 41, 59);
            grpLogs.Controls.Add(dgvLogs);
            grpLogs.Controls.Add(btnRefreshLogs);
            grpLogs.Controls.Add(btnClearLogs);
            grpLogs.Controls.Add(btnExportLogs);
            grpLogs.FlatStyle = FlatStyle.Flat;
            grpLogs.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            grpLogs.ForeColor = Color.FromArgb(203, 213, 225);
            grpLogs.Location = new Point(920, 24);
            grpLogs.Margin = new Padding(3, 4, 3, 4);
            grpLogs.Name = "grpLogs";
            grpLogs.Padding = new Padding(3, 4, 3, 4);
            grpLogs.Size = new Size(520, 792);
            grpLogs.TabIndex = 2;
            grpLogs.TabStop = false;
            grpLogs.Text = "System Activity Logs";
            // 
            // dgvLogs
            // 
            dgvLogs.AllowUserToAddRows = false;
            dgvLogs.AllowUserToDeleteRows = false;
            dgvLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLogs.BackgroundColor = Color.FromArgb(15, 23, 42);
            dgvLogs.BorderStyle = BorderStyle.None;
            dgvLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLogs.Location = new Point(20, 40);
            dgvLogs.Margin = new Padding(3, 4, 3, 4);
            dgvLogs.Name = "dgvLogs";
            dgvLogs.ReadOnly = true;
            dgvLogs.RowHeadersWidth = 51;
            dgvLogs.Size = new Size(480, 640);
            dgvLogs.TabIndex = 0;
            // 
            // btnRefreshLogs
            // 
            btnRefreshLogs.BackColor = Color.FromArgb(59, 130, 246);
            btnRefreshLogs.FlatAppearance.BorderSize = 0;
            btnRefreshLogs.FlatAppearance.MouseDownBackColor = Color.FromArgb(37, 99, 235);
            btnRefreshLogs.FlatAppearance.MouseOverBackColor = Color.FromArgb(96, 165, 250);
            btnRefreshLogs.FlatStyle = FlatStyle.Flat;
            btnRefreshLogs.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRefreshLogs.ForeColor = Color.White;
            btnRefreshLogs.Location = new Point(20, 696);
            btnRefreshLogs.Margin = new Padding(3, 4, 3, 4);
            btnRefreshLogs.Name = "btnRefreshLogs";
            btnRefreshLogs.Size = new Size(144, 48);
            btnRefreshLogs.TabIndex = 1;
            btnRefreshLogs.Text = "🔄 REFRESH";
            btnRefreshLogs.UseVisualStyleBackColor = false;
            // 
            // btnClearLogs
            // 
            btnClearLogs.BackColor = Color.FromArgb(245, 158, 11);
            btnClearLogs.FlatAppearance.BorderSize = 0;
            btnClearLogs.FlatAppearance.MouseDownBackColor = Color.FromArgb(180, 83, 9);
            btnClearLogs.FlatAppearance.MouseOverBackColor = Color.FromArgb(251, 191, 36);
            btnClearLogs.FlatStyle = FlatStyle.Flat;
            btnClearLogs.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnClearLogs.ForeColor = Color.White;
            btnClearLogs.Location = new Point(188, 696);
            btnClearLogs.Margin = new Padding(3, 4, 3, 4);
            btnClearLogs.Name = "btnClearLogs";
            btnClearLogs.Size = new Size(144, 48);
            btnClearLogs.TabIndex = 2;
            btnClearLogs.Text = "🗑️ CLEAR";
            btnClearLogs.UseVisualStyleBackColor = false;
            // 
            // btnExportLogs
            // 
            btnExportLogs.BackColor = Color.FromArgb(34, 197, 94);
            btnExportLogs.FlatAppearance.BorderSize = 0;
            btnExportLogs.FlatAppearance.MouseDownBackColor = Color.FromArgb(21, 128, 61);
            btnExportLogs.FlatAppearance.MouseOverBackColor = Color.FromArgb(74, 222, 128);
            btnExportLogs.FlatStyle = FlatStyle.Flat;
            btnExportLogs.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExportLogs.ForeColor = Color.White;
            btnExportLogs.Location = new Point(356, 696);
            btnExportLogs.Margin = new Padding(3, 4, 3, 4);
            btnExportLogs.Name = "btnExportLogs";
            btnExportLogs.Size = new Size(144, 48);
            btnExportLogs.TabIndex = 3;
            btnExportLogs.Text = "💾 EXPORT";
            btnExportLogs.UseVisualStyleBackColor = false;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(24, 24);
            statusStrip.Items.AddRange(new ToolStripItem[] { lblStatusBar, progressBar });
            statusStrip.Location = new Point(0, 989);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(2, 0, 11, 0);
            statusStrip.Size = new Size(1535, 35);
            statusStrip.TabIndex = 1;
            statusStrip.Text = "statusStrip1";
            // 
            // lblStatusBar
            // 
            lblStatusBar.Name = "lblStatusBar";
            lblStatusBar.Size = new Size(101, 29);
            lblStatusBar.Text = "System Ready";
            // 
            // progressBar
            // 
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(240, 27);
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.FromArgb(59, 130, 246);
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Margin = new Padding(2, 0, 2, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1535, 96);
            lblTitle.TabIndex = 2;
            lblTitle.Text = "🏢 BURJ KHALIFA ELEVATOR CONTROL SYSTEM";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1535, 1024);
            Controls.Add(pnlMain);
            Controls.Add(statusStrip);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Burj Khalifa Elevator Control System v4.0";
            WindowState = FormWindowState.Maximized;
            pnlMain.ResumeLayout(false);
            pnlBuilding.ResumeLayout(false);
            pnlBuilding.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbShaft).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbElevatorCar).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbDoorLeft).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbDoorRight).EndInit();
            grpControlPanel.ResumeLayout(false);
            grpLogs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLogs).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlBuilding;
        private System.Windows.Forms.PictureBox pbShaft;
        private System.Windows.Forms.PictureBox pbElevatorCar;
        private System.Windows.Forms.PictureBox pbDoorLeft;
        private System.Windows.Forms.PictureBox pbDoorRight;
        private System.Windows.Forms.Label lblFloorDisplay;
        private System.Windows.Forms.Button btnCallUp;
        private System.Windows.Forms.Button btnCallDown;
        private System.Windows.Forms.GroupBox grpControlPanel;
        private System.Windows.Forms.Label lblControlPanelDisplay;
        private System.Windows.Forms.Label lblStatusDisplay;
        private System.Windows.Forms.Button btnFloor1;
        private System.Windows.Forms.Button btnFloor0;
        private System.Windows.Forms.Button btnOpenDoor;
        private System.Windows.Forms.Button btnCloseDoor;
        private System.Windows.Forms.Button btnAlarm;
        private System.Windows.Forms.GroupBox grpLogs;
        private System.Windows.Forms.DataGridView dgvLogs;
        private System.Windows.Forms.Button btnRefreshLogs;
        private System.Windows.Forms.Button btnClearLogs;
        private System.Windows.Forms.Button btnExportLogs;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusBar;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblFloor1Indicator;
        private System.Windows.Forms.Label lblFloor0Indicator;
        private System.Windows.Forms.Label lblControlPanelTitle;
        private System.Windows.Forms.Button btnEmergencyStop;
    }
}