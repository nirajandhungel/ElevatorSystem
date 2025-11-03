using ElevatorSystem.Controllers;
using ElevatorSystem.Database;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ElevatorSystem
{
    public partial class MainForm : Form
    {
        private ElevatorController elevatorController;
        private DatabaseManager dbManager;
        private LogManager logManager;
        private BackgroundWorker movementWorker;
        private bool isEmergencyStopActive = false;

        public MainForm()
        {
            InitializeComponent();
            // Set window to maximize

            // Enable double buffering to reduce flicker
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            this.WindowState = FormWindowState.Maximized;
            InitializeSystem();
            SetupBackgroundWorkers();
            ApplyModernStyling();
        }

        private void InitializeSystem()
        {
            try
            {
                // Initialize database
                dbManager = new DatabaseManager();
                dbManager.InitializeDatabase();

                // Initialize log manager
                logManager = new LogManager(dbManager, dgvLogs);

                // Initialize elevator controller
                elevatorController = new ElevatorController(
                    pbElevatorCar,
                    pbDoorLeft,
                    pbDoorRight,
                    lblFloorDisplay,
                    lblControlPanelDisplay,
                    lblStatusDisplay,
                    logManager,
                    UpdateStatusBar
                );

                // Set up event handlers
                SetupEventHandlers();

                // Initialize elevator position - REMOVED the SetInitialPosition call since it's handled in controller

                logManager.LogActivity("Burj Khalifa Elevator System initialized successfully", "INFO");
                UpdateStatusBar("System Ready - Burj Khalifa Elevator Control Active", Color.FromArgb(34, 197, 94));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Initialization Error: {ex.Message}", "System Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyModernStyling()
        {
            // Make sure elevator components are visible
            pbElevatorCar.BringToFront();
            pbDoorLeft.BringToFront();
            pbDoorRight.BringToFront();
            lblFloorDisplay.BringToFront();

    
        }

        private void SetupEventHandlers()
        {
            // Control Panel Buttons
            btnFloor0.Click += (s, e) => RequestFloor(0, "Control Panel");
            btnFloor1.Click += (s, e) => RequestFloor(1, "Control Panel");
            btnOpenDoor.Click += (s, e) => elevatorController.OpenDoors();
            btnCloseDoor.Click += (s, e) => elevatorController.CloseDoors();
            btnAlarm.Click += BtnAlarm_Click;
            btnEmergencyStop.Click += BtnEmergencyStop_Click;

            // Floor Request Buttons
            btnCallUp.Click += (s, e) => RequestFloor(1, "Floor 1 Call");
            btnCallDown.Click += (s, e) => RequestFloor(0, "Ground Call");

            // Log Management
            btnRefreshLogs.Click += (s, e) => RefreshLogs();
            btnClearLogs.Click += BtnClearLogs_Click;
            btnExportLogs.Click += BtnExportLogs_Click;

            // Form closing event
            this.FormClosing += MainForm_FormClosing;
        }

        private void SetupBackgroundWorkers()
        {
            movementWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            movementWorker.DoWork += MovementWorker_DoWork;
            movementWorker.ProgressChanged += MovementWorker_ProgressChanged;
            movementWorker.RunWorkerCompleted += MovementWorker_RunWorkerCompleted;

        }
        private void RequestFloor(int floor, string source)
        {
            if (isEmergencyStopActive)
            {
                MessageBox.Show("Emergency stop active! Reset emergency to continue.", "Emergency Stop",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!elevatorController.CanMove())
            {
                MessageBox.Show("Elevator is currently unavailable. Please wait...", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string floorName = floor == 0 ? "G" : "1";
            logManager.LogActivity($"Floor {floorName} requested from {source}", "REQUEST");

            if (!movementWorker.IsBusy)
            {
                // Close doors first if they are open, then start movement
                if (elevatorController.AreDoorsOpen())
                {
                    // Subscribe to doors closed event
                    void OnDoorsClosed(object s, EventArgs e)
                    {
                        elevatorController.DoorsClosed -= OnDoorsClosed; // Unsubscribe
                        movementWorker.RunWorkerAsync(floor);
                    }

                    elevatorController.DoorsClosed += OnDoorsClosed;
                    elevatorController.CloseDoors();
                    //movementWorker.RunWorkerAsync(floor);

                }
                else
                {
                    // Doors already closed, start movement immediately
                    movementWorker.RunWorkerAsync(floor);
                }
            }
            else
            {
                elevatorController.QueueRequest(floor);
                logManager.LogActivity($"Floor {floorName} added to queue", "QUEUE");
                UpdateStatusBar($"Floor {floorName} queued - Processing requests", Color.FromArgb(245, 158, 11));
            }
        }
        private void MovementWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int targetFloor = (int)e.Argument;
            BackgroundWorker worker = sender as BackgroundWorker;

            // Simulate movement with progress updates
            for (int i = 0; i <= 100; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                System.Threading.Thread.Sleep(20); // Faster movement for modern elevator
                worker.ReportProgress(i, targetFloor);
            }

            e.Result = targetFloor;
        }

        private void MovementWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            int targetFloor = (int)e.UserState;

            // Update elevator movement in controller
            if (e.ProgressPercentage == 0)
            {
                elevatorController.StartMovement(targetFloor);

                UpdateStatusBar($"Moving to Floor {(targetFloor == 0 ? "G" : "1")}...", Color.FromArgb(59, 130, 246));
            }

            elevatorController.UpdateMovementProgress(e.ProgressPercentage);
            UpdateElevatorCarPosition(e.ProgressPercentage, targetFloor);
        }

        private void MovementWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                UpdateStatusBar("Movement cancelled", Color.FromArgb(245, 158, 11));
            }
            else if (e.Error != null)
            {
                UpdateStatusBar($"Movement error: {e.Error.Message}", Color.FromArgb(239, 68, 68));
                logManager.LogActivity($"Movement error: {e.Error.Message}", "ERROR");
            }
            else
            {
                int targetFloor = (int)e.Result;
                elevatorController.CompleteMovement(targetFloor);

                // Open doors after arrival
                elevatorController.OpenDoors();

                UpdateStatusBar($"Arrived at Floor {(targetFloor == 0 ? "G" : "1")}", Color.FromArgb(34, 197, 94));

                // Process next request in queue
                ProcessNextRequest();
            }
        }





        private void UpdateElevatorCarPosition(int progress, int targetFloor = 0)
        {
            if (pbElevatorCar.InvokeRequired)
            {
                pbElevatorCar.Invoke(new Action<int, int>(UpdateElevatorCarPosition), progress, targetFloor);
                return;
            }

            // Calculate position based on floor and progress
            int groundFloorPosition = 400; // Position for Ground floor
            int firstFloorPosition = 96;  // Position for 1st floor

            if (targetFloor == 1)
            {
                // Moving to first floor
                int newPosition = groundFloorPosition - (int)((groundFloorPosition - firstFloorPosition) * (progress / 100f));
                pbElevatorCar.Top = newPosition;
                pbDoorLeft.Top = newPosition;
                pbDoorRight.Top = newPosition;
                lblFloorDisplay.Top = newPosition + 20;
            }
            else
            {
                // Moving to ground floor
                int newPosition = firstFloorPosition + (int)((groundFloorPosition - firstFloorPosition) * (progress / 100f));
                pbElevatorCar.Top = newPosition;
                pbDoorLeft.Top = newPosition;
                pbDoorRight.Top = newPosition;
                lblFloorDisplay.Top = newPosition + 20;
            }
        }

        private void ProcessNextRequest()
        {
            int? nextFloor = elevatorController.GetNextRequest();
            if (nextFloor.HasValue && !movementWorker.IsBusy)
            {
                System.Threading.Thread.Sleep(1500); // Wait before processing next request
                movementWorker.RunWorkerAsync(nextFloor.Value);
            }
        }

        private void BtnAlarm_Click(object sender, EventArgs e)
        {
            if (elevatorController.IsEmergency)
            {
                elevatorController.ResetEmergency();
                btnAlarm.BackColor = Color.FromArgb(245, 158, 11);
                btnAlarm.Text = "🔄 CALL HELP";
                UpdateStatusBar("Emergency reset - System normal", Color.FromArgb(34, 197, 94));
                logManager.LogActivity("Emergency alarm reset by user", "INFO");
            }
            else
            {
                elevatorController.ActivateEmergency();
                btnAlarm.BackColor = Color.FromArgb(180, 83, 9);
                btnAlarm.Text = "RESET ALARM";
                logManager.LogActivity("Emergency alarm activated by user", "EMERGENCY");

                // Flash the alarm button
                FlashButton(btnAlarm, Color.FromArgb(245, 158, 11), Color.FromArgb(180, 83, 9), 3);

                MessageBox.Show("EMERGENCY ALARM ACTIVATED!\n\nSecurity and maintenance have been notified.\nPlease remain calm.", "EMERGENCY ALERT",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnEmergencyStop_Click(object sender, EventArgs e)
        {
            if (isEmergencyStopActive)
            {
                // Reset emergency stop
                isEmergencyStopActive = false;
                elevatorController.ResetEmergency();
                btnEmergencyStop.BackColor = Color.FromArgb(239, 68, 68);
                btnEmergencyStop.Text = "🛑 EMERGENCY";
                UpdateEmergencyState(false);
                UpdateStatusBar("Emergency stop reset - System operational", Color.FromArgb(34, 197, 94));
                logManager.LogActivity("Emergency stop reset by user", "INFO");
            }
            else
            {
                // Activate emergency stop
                var result = MessageBox.Show(
                    "ACTIVATE EMERGENCY STOP?\n\nThis will immediately halt all elevator operations.\nOnly use in case of real emergency!",
                    "CONFIRM EMERGENCY STOP",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    isEmergencyStopActive = true;
                    elevatorController.ActivateEmergency();
                    movementWorker.CancelAsync();

                    btnEmergencyStop.BackColor = Color.FromArgb(185, 28, 28);
                    btnEmergencyStop.Text = "RESET STOP";
                    UpdateEmergencyState(true);

                    logManager.LogActivity("EMERGENCY STOP ACTIVATED BY USER", "CRITICAL");
                    UpdateStatusBar("EMERGENCY STOP ACTIVE - ALL OPERATIONS HALTED", Color.FromArgb(239, 68, 68));

                    // Flash the emergency stop button
                    FlashButton(btnEmergencyStop, Color.FromArgb(239, 68, 68), Color.FromArgb(185, 28, 28), 5);
                }
            }
        }

        private void UpdateEmergencyState(bool isEmergency)
        {
            Color backgroundColor = isEmergency ? Color.FromArgb(127, 29, 29) : Color.FromArgb(30, 41, 59);
            Color textColor = isEmergency ? Color.FromArgb(254, 226, 226) : Color.FromArgb(203, 213, 225);

            // Update main panel
            pnlMain.BackColor = backgroundColor;

            // Update building panel
            pnlBuilding.BackColor = isEmergency ? Color.FromArgb(69, 10, 10) : Color.FromArgb(30, 41, 59);

            // Update control panel
            grpControlPanel.BackColor = isEmergency ? Color.FromArgb(69, 10, 10) : Color.FromArgb(30, 41, 59);
            grpControlPanel.ForeColor = textColor;

            // Update logs panel
            grpLogs.BackColor = isEmergency ? Color.FromArgb(69, 10, 10) : Color.FromArgb(30, 41, 59);
            grpLogs.ForeColor = textColor;

            if (isEmergency)
            {
                lblStatusDisplay.Text = "● EMERGENCY STOP";
                lblStatusDisplay.ForeColor = Color.FromArgb(239, 68, 68);
            }
        }

        private async void FlashButton(Button button, Color color1, Color color2, int flashes)
        {
            for (int i = 0; i < flashes; i++)
            {
                button.BackColor = color1;
                await System.Threading.Tasks.Task.Delay(200);
                button.BackColor = color2;
                await System.Threading.Tasks.Task.Delay(200);
            }
        }

        private void RefreshLogs()
        {
            logManager.RefreshLogs();
            UpdateStatusBar("Activity logs refreshed", Color.FromArgb(34, 197, 94));
        }

        private void BtnClearLogs_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to clear all activity logs?\nThis action cannot be undone.",
                "Confirm Clear Logs",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    dbManager.ClearLogs();
                    RefreshLogs();
                    logManager.LogActivity("All activity logs cleared by user", "INFO");
                    UpdateStatusBar("All logs cleared", Color.FromArgb(245, 158, 11));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error clearing logs: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnExportLogs_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV Files (*.csv)|*.csv|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                sfd.FileName = $"BurjKhalifa_Elevator_Logs_{DateTime.Now:yyyyMMdd_HHmmss}";
                sfd.Title = "Export Elevator Activity Logs";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logManager.ExportLogs(sfd.FileName);
                        logManager.LogActivity($"Activity logs exported to {sfd.FileName}", "INFO");
                        MessageBox.Show("Activity logs exported successfully!", "Export Complete",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Export failed: {ex.Message}", "Export Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void UpdateStatusBar(string message, Color color)
        {
            if (lblStatusBar.GetCurrentParent().InvokeRequired)
            {
                lblStatusBar.GetCurrentParent().Invoke(new Action(() => UpdateStatusBar(message, color)));
                return;
            }

            lblStatusBar.Text = $"{DateTime.Now:HH:mm:ss} - {message}";
            lblStatusBar.ForeColor = color;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isEmergencyStopActive)
            {
                MessageBox.Show("Cannot exit while emergency stop is active. Please reset emergency stop first.",
                    "Emergency Stop Active",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }

            var result = MessageBox.Show(
                "Are you sure you want to exit the Burj Khalifa Elevator Control System?",
                "Confirm System Shutdown",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            // Cleanup resources
            if (movementWorker != null && movementWorker.IsBusy)
            {
                movementWorker.CancelAsync();
            }

            // Log shutdown
            logManager?.LogActivity("Burj Khalifa Elevator Control System shutdown initiated", "INFO");
        }

        public void UpdateProgressBar(int value)
        {
            if (progressBar.GetCurrentParent().InvokeRequired)
            {
                progressBar.GetCurrentParent().Invoke(new Action<int>(UpdateProgressBar), value);
                return;
            }
            progressBar.Value = value;
        }

       
    }
}