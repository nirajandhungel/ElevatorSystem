namespace ElevatorSystem
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        // Main panels
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlBuilding;
        private System.Windows.Forms.Panel panelShaft;

        // Elevator components (renamed to match MainForm.cs)
        private System.Windows.Forms.PictureBox pbElevatorCar;
        private System.Windows.Forms.Panel pbDoorLeft;
        private System.Windows.Forms.Panel pbDoorRight;
        private System.Windows.Forms.Panel panelLight;

        // Floor indicators
        private System.Windows.Forms.Label labelFirstFloor;
        private System.Windows.Forms.Label labelGroundFloor;

        // Call buttons
        private System.Windows.Forms.Button btnCallUp;
        private System.Windows.Forms.Button btnCallDown;

        // Control Panel (renamed to match MainForm.cs)
        private System.Windows.Forms.GroupBox grpControlPanel;
        private System.Windows.Forms.Button btnFloor0;  // Was btnControlG
        private System.Windows.Forms.Button btnFloor1;  // Was btnControl1
        private System.Windows.Forms.Button btnOpenDoor;  // Was btnControlOpen
        private System.Windows.Forms.Button btnCloseDoor;  // Was btnControlClose
        private System.Windows.Forms.Label labelControlPanel;

        // Display Components (renamed to match MainForm.cs)
        private System.Windows.Forms.Panel panelDisplay;
        private System.Windows.Forms.Label lblFloorDisplay;  // Was displayFloor
        private System.Windows.Forms.Label lblControlPanelDisplay;  // Was displayLabel
        private System.Windows.Forms.Label lblStatusDisplay;  // Was displayStatus

        // Emergency buttons (renamed to match MainForm.cs)
        private System.Windows.Forms.Button btnAlarm;  // Was btnEmergency
        private System.Windows.Forms.Button btnEmergencyStop;  // NEW - was missing

        // Status and info
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelFloor;
        private System.Windows.Forms.Label lblStatusBar;  // NEW - was missing

        // Log management (renamed to match MainForm.cs)
        private System.Windows.Forms.GroupBox grpLogs;  // NEW
        private System.Windows.Forms.Button btnRefreshLogs;  // Was btnViewLogs
        private System.Windows.Forms.Button btnClearLogs;
        private System.Windows.Forms.Button btnExportLogs;  // NEW - was missing
        private System.Windows.Forms.DataGridView dgvLogs;  // Was dataGridViewLogs

        // Progress bar (NEW - was missing)
        private System.Windows.Forms.ProgressBar progressBar;

        // Sound toggle
        private System.Windows.Forms.Button btnSoundToggle;
        private System.Windows.Forms.StatusStrip statusStrip1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            pnlMain = new Panel();
            pnlBuilding = new Panel();
            panelShaft = new Panel();
            pbDoorRight = new Panel();
            pbDoorLeft = new Panel();
            pbElevatorCar = new PictureBox();
            labelFirstFloor = new Label();
            labelGroundFloor = new Label();
            btnCallUp = new Button();
            btnCallDown = new Button();
            grpControlPanel = new GroupBox();
            labelControlPanel = new Label();
            btnFloor0 = new Button();
            btnFloor1 = new Button();
            btnOpenDoor = new Button();
            btnCloseDoor = new Button();
            btnAlarm = new Button();
            btnEmergencyStop = new Button();
            panelDisplay = new Panel();
            lblControlPanelDisplay = new Label();
            lblFloorDisplay = new Label();
            lblStatusDisplay = new Label();
            grpLogs = new GroupBox();
            btnRefreshLogs = new Button();
            btnClearLogs = new Button();
            btnExportLogs = new Button();
            dgvLogs = new DataGridView();
            progressBar = new ProgressBar();
            labelStatus = new Label();
            labelFloor = new Label();
            btnSoundToggle = new Button();
            statusStrip1 = new StatusStrip();
            lblStatusBar = new Label();
            pnlMain.SuspendLayout();
            pnlBuilding.SuspendLayout();
            panelShaft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbElevatorCar).BeginInit();
            grpControlPanel.SuspendLayout();
            panelDisplay.SuspendLayout();
            grpLogs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).BeginInit();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(30, 41, 59);
            pnlMain.Controls.Add(pnlBuilding);
            pnlMain.Controls.Add(grpControlPanel);
            pnlMain.Controls.Add(panelDisplay);
            pnlMain.Controls.Add(grpLogs);
            pnlMain.Controls.Add(progressBar);
            pnlMain.Controls.Add(labelStatus);
            pnlMain.Controls.Add(labelFloor);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Margin = new Padding(4, 5, 4, 5);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(1600, 1055);
            pnlMain.TabIndex = 0;
            pnlMain.Paint += pnlMain_Paint;
            // 
            // pnlBuilding
            // 
            pnlBuilding.BackColor = Color.FromArgb(0, 54, 30);
            pnlBuilding.BorderStyle = BorderStyle.Fixed3D;
            pnlBuilding.Controls.Add(panelShaft);
            pnlBuilding.Controls.Add(labelFirstFloor);
            pnlBuilding.Controls.Add(labelGroundFloor);
            pnlBuilding.Controls.Add(btnCallUp);
            pnlBuilding.Controls.Add(btnCallDown);
            pnlBuilding.Location = new Point(40, 46);
            pnlBuilding.Margin = new Padding(4, 5, 4, 5);
            pnlBuilding.Name = "pnlBuilding";
            pnlBuilding.Size = new Size(417, 878);
            pnlBuilding.TabIndex = 0;
            pnlBuilding.Paint += PanelBuilding_Paint;
            // 
            // panelShaft
            // 
            panelShaft.BackColor = Color.Black;
            panelShaft.Controls.Add(pbDoorRight);
            panelShaft.Controls.Add(pbDoorLeft);
            panelShaft.Controls.Add(pbElevatorCar);
            panelShaft.Location = new Point(107, 75);
            panelShaft.Margin = new Padding(4, 5, 4, 5);
            panelShaft.Name = "panelShaft";
            panelShaft.Size = new Size(187, 738);
            panelShaft.TabIndex = 0;
            // 
            // pbDoorRight
            // 
            pbDoorRight.BackColor = Color.FromArgb(160, 160, 160);
            pbDoorRight.Location = new Point(93, 499);
            pbDoorRight.Margin = new Padding(4, 5, 4, 5);
            pbDoorRight.Name = "pbDoorRight";
            pbDoorRight.Size = new Size(93, 181);
            pbDoorRight.TabIndex = 1;
            // 
            // pbDoorLeft
            // 
            pbDoorLeft.BackColor = Color.FromArgb(160, 160, 160);
            pbDoorLeft.Location = new Point(0, 499);
            pbDoorLeft.Margin = new Padding(4, 5, 4, 5);
            pbDoorLeft.Name = "pbDoorLeft";
            pbDoorLeft.Size = new Size(93, 181);
            pbDoorLeft.TabIndex = 0;
            // 
            // pbElevatorCar
            // 
            pbElevatorCar.BackColor = Color.FromArgb(200, 200, 200);
            pbElevatorCar.BackgroundImage = (Image)resources.GetObject("pbElevatorCar.BackgroundImage");
            pbElevatorCar.BackgroundImageLayout = ImageLayout.Stretch;
            pbElevatorCar.BorderStyle = BorderStyle.FixedSingle;
            pbElevatorCar.Location = new Point(0, 499);
            pbElevatorCar.Margin = new Padding(4, 5, 4, 5);
            pbElevatorCar.Name = "pbElevatorCar";
            pbElevatorCar.Size = new Size(186, 181);
            pbElevatorCar.TabIndex = 0;
            pbElevatorCar.TabStop = false;
            // 
            // labelFirstFloor
            // 
            labelFirstFloor.BackColor = Color.Transparent;
            labelFirstFloor.Font = new Font("Arial", 10F, FontStyle.Bold);
            labelFirstFloor.ForeColor = Color.FromArgb(203, 213, 225);
            labelFirstFloor.Location = new Point(302, 138);
            labelFirstFloor.Margin = new Padding(4, 0, 4, 0);
            labelFirstFloor.Name = "labelFirstFloor";
            labelFirstFloor.Size = new Size(123, 24);
            labelFirstFloor.TabIndex = 1;
            labelFirstFloor.Text = "FLOOR 1";
            // 
            // labelGroundFloor
            // 
            labelGroundFloor.BackColor = Color.Transparent;
            labelGroundFloor.Font = new Font("Arial", 10F, FontStyle.Bold);
            labelGroundFloor.ForeColor = Color.FromArgb(203, 213, 225);
            labelGroundFloor.Location = new Point(307, 713);
            labelGroundFloor.Margin = new Padding(4, 0, 4, 0);
            labelGroundFloor.Name = "labelGroundFloor";
            labelGroundFloor.Size = new Size(123, 42);
            labelGroundFloor.TabIndex = 2;
            labelGroundFloor.Text = "GROUND";
            // 
            // btnCallUp
            // 
            btnCallUp.BackColor = Color.FromArgb(34, 197, 94);
            btnCallUp.FlatStyle = FlatStyle.Flat;
            btnCallUp.Font = new Font("Arial", 16F, FontStyle.Bold);
            btnCallUp.ForeColor = Color.White;
            btnCallUp.Location = new Point(307, 169);
            btnCallUp.Margin = new Padding(4, 5, 4, 5);
            btnCallUp.Name = "btnCallUp";
            btnCallUp.Size = new Size(67, 77);
            btnCallUp.TabIndex = 3;
            btnCallUp.Text = "▲";
            btnCallUp.UseVisualStyleBackColor = false;
            // 
            // btnCallDown
            // 
            btnCallDown.BackColor = Color.FromArgb(34, 197, 94);
            btnCallDown.FlatStyle = FlatStyle.Flat;
            btnCallDown.Font = new Font("Arial", 16F, FontStyle.Bold);
            btnCallDown.ForeColor = Color.White;
            btnCallDown.Location = new Point(307, 631);
            btnCallDown.Margin = new Padding(4, 5, 4, 5);
            btnCallDown.Name = "btnCallDown";
            btnCallDown.Size = new Size(67, 77);
            btnCallDown.TabIndex = 4;
            btnCallDown.Text = "▼";
            btnCallDown.UseVisualStyleBackColor = false;
            // 
            // grpControlPanel
            // 
            grpControlPanel.BackColor = Color.FromArgb(30, 41, 59);
            grpControlPanel.Controls.Add(labelControlPanel);
            grpControlPanel.Controls.Add(btnFloor0);
            grpControlPanel.Controls.Add(btnFloor1);
            grpControlPanel.Controls.Add(btnOpenDoor);
            grpControlPanel.Controls.Add(btnCloseDoor);
            grpControlPanel.Controls.Add(btnAlarm);
            grpControlPanel.Controls.Add(btnEmergencyStop);
            grpControlPanel.Font = new Font("Arial", 10F, FontStyle.Bold);
            grpControlPanel.ForeColor = Color.FromArgb(203, 213, 225);
            grpControlPanel.Location = new Point(493, 369);
            grpControlPanel.Margin = new Padding(4, 5, 4, 5);
            grpControlPanel.Name = "grpControlPanel";
            grpControlPanel.Padding = new Padding(4, 5, 4, 5);
            grpControlPanel.Size = new Size(373, 554);
            grpControlPanel.TabIndex = 1;
            grpControlPanel.TabStop = false;
            grpControlPanel.Text = "CONTROL PANEL";
            grpControlPanel.Paint += PanelControl_Paint;
            // 
            // labelControlPanel
            // 
            labelControlPanel.Font = new Font("Arial", 9F);
            labelControlPanel.Location = new Point(27, 38);
            labelControlPanel.Margin = new Padding(4, 0, 4, 0);
            labelControlPanel.Name = "labelControlPanel";
            labelControlPanel.Size = new Size(320, 31);
            labelControlPanel.TabIndex = 0;
            labelControlPanel.Text = "Select Floor";
            labelControlPanel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnFloor0
            // 
            btnFloor0.BackColor = Color.FromArgb(59, 130, 246);
            btnFloor0.FlatStyle = FlatStyle.Flat;
            btnFloor0.Font = new Font("Arial", 18F, FontStyle.Bold);
            btnFloor0.ForeColor = Color.White;
            btnFloor0.Location = new Point(40, 92);
            btnFloor0.Margin = new Padding(4, 5, 4, 5);
            btnFloor0.Name = "btnFloor0";
            btnFloor0.Size = new Size(133, 92);
            btnFloor0.TabIndex = 1;
            btnFloor0.Text = "G";
            btnFloor0.UseVisualStyleBackColor = false;
            // 
            // btnFloor1
            // 
            btnFloor1.BackColor = Color.FromArgb(59, 130, 246);
            btnFloor1.FlatStyle = FlatStyle.Flat;
            btnFloor1.Font = new Font("Arial", 18F, FontStyle.Bold);
            btnFloor1.ForeColor = Color.White;
            btnFloor1.Location = new Point(200, 92);
            btnFloor1.Margin = new Padding(4, 5, 4, 5);
            btnFloor1.Name = "btnFloor1";
            btnFloor1.Size = new Size(133, 92);
            btnFloor1.TabIndex = 2;
            btnFloor1.Text = "1";
            btnFloor1.UseVisualStyleBackColor = false;
            // 
            // btnOpenDoor
            // 
            btnOpenDoor.BackColor = Color.FromArgb(34, 197, 94);
            btnOpenDoor.FlatStyle = FlatStyle.Flat;
            btnOpenDoor.Font = new Font("Arial", 10F, FontStyle.Bold);
            btnOpenDoor.ForeColor = Color.White;
            btnOpenDoor.Location = new Point(40, 215);
            btnOpenDoor.Margin = new Padding(4, 5, 4, 5);
            btnOpenDoor.Name = "btnOpenDoor";
            btnOpenDoor.Size = new Size(133, 77);
            btnOpenDoor.TabIndex = 3;
            btnOpenDoor.Text = "◄ ► OPEN";
            btnOpenDoor.UseVisualStyleBackColor = false;
            // 
            // btnCloseDoor
            // 
            btnCloseDoor.BackColor = Color.FromArgb(239, 68, 68);
            btnCloseDoor.FlatStyle = FlatStyle.Flat;
            btnCloseDoor.Font = new Font("Arial", 10F, FontStyle.Bold);
            btnCloseDoor.ForeColor = Color.White;
            btnCloseDoor.Location = new Point(200, 215);
            btnCloseDoor.Margin = new Padding(4, 5, 4, 5);
            btnCloseDoor.Name = "btnCloseDoor";
            btnCloseDoor.Size = new Size(133, 77);
            btnCloseDoor.TabIndex = 4;
            btnCloseDoor.Text = "► ◄ CLOSE";
            btnCloseDoor.UseVisualStyleBackColor = false;
            // 
            // btnAlarm
            // 
            btnAlarm.BackColor = Color.FromArgb(245, 158, 11);
            btnAlarm.FlatStyle = FlatStyle.Flat;
            btnAlarm.Font = new Font("Arial", 10F, FontStyle.Bold);
            btnAlarm.ForeColor = Color.White;
            btnAlarm.Location = new Point(40, 323);
            btnAlarm.Margin = new Padding(4, 5, 4, 5);
            btnAlarm.Name = "btnAlarm";
            btnAlarm.Size = new Size(293, 92);
            btnAlarm.TabIndex = 5;
            btnAlarm.Text = "🔔 CALL HELP";
            btnAlarm.UseVisualStyleBackColor = false;
            // 
            // btnEmergencyStop
            // 
            btnEmergencyStop.BackColor = Color.FromArgb(239, 68, 68);
            btnEmergencyStop.FlatStyle = FlatStyle.Flat;
            btnEmergencyStop.Font = new Font("Arial", 11F, FontStyle.Bold);
            btnEmergencyStop.ForeColor = Color.White;
            btnEmergencyStop.Location = new Point(40, 438);
            btnEmergencyStop.Margin = new Padding(4, 5, 4, 5);
            btnEmergencyStop.Name = "btnEmergencyStop";
            btnEmergencyStop.Size = new Size(293, 92);
            btnEmergencyStop.TabIndex = 6;
            btnEmergencyStop.Text = "\U0001f6d1 EMERGENCY";
            btnEmergencyStop.UseVisualStyleBackColor = false;
            // 
            // panelDisplay
            // 
            panelDisplay.BackColor = Color.Black;
            panelDisplay.BorderStyle = BorderStyle.FixedSingle;
            panelDisplay.Controls.Add(lblControlPanelDisplay);
            panelDisplay.Controls.Add(lblFloorDisplay);
            panelDisplay.Controls.Add(lblStatusDisplay);
            panelDisplay.Location = new Point(493, 108);
            panelDisplay.Margin = new Padding(4, 5, 4, 5);
            panelDisplay.Name = "panelDisplay";
            panelDisplay.Size = new Size(373, 214);
            panelDisplay.TabIndex = 2;
            // 
            // lblControlPanelDisplay
            // 
            lblControlPanelDisplay.Font = new Font("Arial", 12F, FontStyle.Bold);
            lblControlPanelDisplay.ForeColor = Color.Yellow;
            lblControlPanelDisplay.Location = new Point(13, 23);
            lblControlPanelDisplay.Margin = new Padding(4, 0, 4, 0);
            lblControlPanelDisplay.Name = "lblControlPanelDisplay";
            lblControlPanelDisplay.Size = new Size(347, 46);
            lblControlPanelDisplay.TabIndex = 0;
            lblControlPanelDisplay.Text = "ELEVATOR SYSTEM";
            lblControlPanelDisplay.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblFloorDisplay
            // 
            lblFloorDisplay.Font = new Font("Arial", 24F, FontStyle.Bold);
            lblFloorDisplay.ForeColor = Color.Lime;
            lblFloorDisplay.Location = new Point(13, 77);
            lblFloorDisplay.Margin = new Padding(4, 0, 4, 0);
            lblFloorDisplay.Name = "lblFloorDisplay";
            lblFloorDisplay.Size = new Size(347, 62);
            lblFloorDisplay.TabIndex = 1;
            lblFloorDisplay.Text = "G";
            lblFloorDisplay.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStatusDisplay
            // 
            lblStatusDisplay.Font = new Font("Arial", 10F, FontStyle.Bold);
            lblStatusDisplay.ForeColor = Color.Cyan;
            lblStatusDisplay.Location = new Point(13, 154);
            lblStatusDisplay.Margin = new Padding(4, 0, 4, 0);
            lblStatusDisplay.Name = "lblStatusDisplay";
            lblStatusDisplay.Size = new Size(347, 38);
            lblStatusDisplay.TabIndex = 2;
            lblStatusDisplay.Text = "● IDLE";
            lblStatusDisplay.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // grpLogs
            // 
            grpLogs.BackColor = Color.FromArgb(30, 41, 59);
            grpLogs.Controls.Add(btnRefreshLogs);
            grpLogs.Controls.Add(btnClearLogs);
            grpLogs.Controls.Add(btnExportLogs);
            grpLogs.Controls.Add(dgvLogs);
            grpLogs.Font = new Font("Arial", 10F, FontStyle.Bold);
            grpLogs.ForeColor = Color.FromArgb(203, 213, 225);
            grpLogs.Location = new Point(907, 46);
            grpLogs.Margin = new Padding(4, 5, 4, 5);
            grpLogs.Name = "grpLogs";
            grpLogs.Padding = new Padding(4, 5, 4, 5);
            grpLogs.Size = new Size(653, 877);
            grpLogs.TabIndex = 5;
            grpLogs.TabStop = false;
            grpLogs.Text = "ACTIVITY LOGS";
            // 
            // btnRefreshLogs
            // 
            btnRefreshLogs.BackColor = Color.FromArgb(59, 130, 246);
            btnRefreshLogs.FlatStyle = FlatStyle.Flat;
            btnRefreshLogs.Font = new Font("Arial", 9F);
            btnRefreshLogs.ForeColor = Color.White;
            btnRefreshLogs.Location = new Point(27, 46);
            btnRefreshLogs.Margin = new Padding(4, 5, 4, 5);
            btnRefreshLogs.Name = "btnRefreshLogs";
            btnRefreshLogs.Size = new Size(133, 54);
            btnRefreshLogs.TabIndex = 0;
            btnRefreshLogs.Text = "🔄 Refresh";
            btnRefreshLogs.UseVisualStyleBackColor = false;
            // 
            // btnClearLogs
            // 
            btnClearLogs.BackColor = Color.FromArgb(245, 158, 11);
            btnClearLogs.FlatStyle = FlatStyle.Flat;
            btnClearLogs.Font = new Font("Arial", 9F);
            btnClearLogs.ForeColor = Color.White;
            btnClearLogs.Location = new Point(173, 46);
            btnClearLogs.Margin = new Padding(4, 5, 4, 5);
            btnClearLogs.Name = "btnClearLogs";
            btnClearLogs.Size = new Size(133, 54);
            btnClearLogs.TabIndex = 1;
            btnClearLogs.Text = "🗑️ Clear";
            btnClearLogs.UseVisualStyleBackColor = false;
            // 
            // btnExportLogs
            // 
            btnExportLogs.BackColor = Color.FromArgb(34, 197, 94);
            btnExportLogs.FlatStyle = FlatStyle.Flat;
            btnExportLogs.Font = new Font("Arial", 9F);
            btnExportLogs.ForeColor = Color.White;
            btnExportLogs.Location = new Point(320, 46);
            btnExportLogs.Margin = new Padding(4, 5, 4, 5);
            btnExportLogs.Name = "btnExportLogs";
            btnExportLogs.Size = new Size(133, 54);
            btnExportLogs.TabIndex = 2;
            btnExportLogs.Text = "📥 Export";
            btnExportLogs.UseVisualStyleBackColor = false;
            // 
            // dgvLogs
            // 
            dgvLogs.AllowUserToAddRows = false;
            dgvLogs.AllowUserToDeleteRows = false;
            dgvLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLogs.BackgroundColor = Color.FromArgb(51, 65, 85);
            dgvLogs.BorderStyle = BorderStyle.None;
            dgvLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLogs.Location = new Point(27, 115);
            dgvLogs.Margin = new Padding(4, 5, 4, 5);
            dgvLogs.Name = "dgvLogs";
            dgvLogs.ReadOnly = true;
            dgvLogs.RowHeadersVisible = false;
            dgvLogs.RowHeadersWidth = 82;
            dgvLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLogs.Size = new Size(600, 731);
            dgvLogs.TabIndex = 3;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(493, 338);
            progressBar.Margin = new Padding(4, 5, 4, 5);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(373, 15);
            progressBar.TabIndex = 6;
            // 
            // labelStatus
            // 
            labelStatus.Font = new Font("Arial", 10F);
            labelStatus.ForeColor = Color.FromArgb(203, 213, 225);
            labelStatus.Location = new Point(493, 46);
            labelStatus.Margin = new Padding(4, 0, 4, 0);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(373, 31);
            labelStatus.TabIndex = 3;
            labelStatus.Text = "Status: System Ready";
            // 
            // labelFloor
            // 
            labelFloor.Font = new Font("Arial", 10F);
            labelFloor.ForeColor = Color.FromArgb(203, 213, 225);
            labelFloor.Location = new Point(40, 938);
            labelFloor.Margin = new Padding(4, 0, 4, 0);
            labelFloor.Name = "labelFloor";
            labelFloor.Size = new Size(267, 31);
            labelFloor.TabIndex = 4;
            labelFloor.Text = "Current Floor: G";
            // 
            // btnSoundToggle
            // 
            btnSoundToggle.BackColor = Color.FromArgb(34, 197, 94);
            btnSoundToggle.FlatStyle = FlatStyle.Flat;
            btnSoundToggle.Font = new Font("Arial", 8F);
            btnSoundToggle.ForeColor = Color.White;
            btnSoundToggle.Location = new Point(1427, 938);
            btnSoundToggle.Margin = new Padding(4, 5, 4, 5);
            btnSoundToggle.Name = "btnSoundToggle";
            btnSoundToggle.Size = new Size(133, 46);
            btnSoundToggle.TabIndex = 7;
            btnSoundToggle.Text = "🔊 Sounds ON";
            btnSoundToggle.UseVisualStyleBackColor = false;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.FromArgb(15, 23, 42);
            statusStrip1.ImageScalingSize = new Size(32, 32);
            statusStrip1.Location = new Point(0, 1033);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 21, 0);
            statusStrip1.Size = new Size(1600, 22);
            statusStrip1.TabIndex = 8;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatusBar
            // 
            lblStatusBar.BackColor = Color.FromArgb(15, 23, 42);
            lblStatusBar.Dock = DockStyle.Bottom;
            lblStatusBar.Font = new Font("Arial", 9F);
            lblStatusBar.ForeColor = Color.FromArgb(34, 197, 94);
            lblStatusBar.Location = new Point(0, 990);
            lblStatusBar.Margin = new Padding(4, 0, 4, 0);
            lblStatusBar.Name = "lblStatusBar";
            lblStatusBar.Padding = new Padding(13, 8, 13, 8);
            lblStatusBar.Size = new Size(1600, 43);
            lblStatusBar.TabIndex = 9;
            lblStatusBar.Text = "System Ready - Burj Khalifa Elevator Control Active";
            lblStatusBar.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1600, 1055);
            Controls.Add(lblStatusBar);
            Controls.Add(btnSoundToggle);
            Controls.Add(statusStrip1);
            Controls.Add(pnlMain);
            Margin = new Padding(4, 5, 4, 5);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Burj Khalifa Elevator Control System";
            Load += MainForm_Load;
            pnlMain.ResumeLayout(false);
            pnlBuilding.ResumeLayout(false);
            panelShaft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbElevatorCar).EndInit();
            grpControlPanel.ResumeLayout(false);
            panelDisplay.ResumeLayout(false);
            grpLogs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLogs).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        // Event handler placeholders
        private void PanelBuilding_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Custom paint logic for building panel can go here
        }

        private void PanelControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Custom paint logic for control panel can go here
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            // Form load logic is handled in MainForm.cs constructor
        }
    }
}