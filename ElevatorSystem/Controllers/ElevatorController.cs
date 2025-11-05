using ElevatorSystem.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ElevatorSystem.Controllers
{
    public class ElevatorController
    {
        private ElevatorContext elevatorContext;
        private PictureBox elevatorCar;
        private Panel doorLeft;
        private Panel doorRight;
        private Label floorDisplay;
        private Label controlPanelDisplay;
        private Label statusDisplay;
        private LogManager logManager;
        private Action<string, Color> updateStatusBar;

        public event EventHandler DoorsClosed;
        public event EventHandler DoorsOpened;
        public event EventHandler<int> MovementRequested; // New event for movement requests

        // Door animation properties
        private int doorLeftInitialWidth;
        private int doorRightInitialWidth;
        private int doorRightInitialLeft;
        private int doorLeftInitialLeft;

        private bool doorsOpen = false;
        private bool isMoving = false;
        private int? pendingMovementTarget = null; // Track pending movement

        private readonly System.Windows.Forms.Timer doorTimer;
        private float doorProgressFloat = 0f;
        private DoorAction currentDoorAction = DoorAction.None;
        private readonly int doorStep = 3;
        private readonly int openOffset;

        public bool IsEmergency => elevatorContext?.IsEmergency ?? false;

        private enum DoorAction
        {
            None,
            Opening,
            Closing
        }

        public ElevatorController(PictureBox car, Panel leftDoor, Panel rightDoor,
                                Label floorLabel, Label controlPanelLabel, Label statusLabel,
                                LogManager logMgr, Action<string, Color> statusUpdater)
        {
            elevatorCar = car;
            doorLeft = leftDoor;
            doorRight = rightDoor;
            floorDisplay = floorLabel;
            controlPanelDisplay = controlPanelLabel;
            statusDisplay = statusLabel;
            logManager = logMgr;
            updateStatusBar = statusUpdater;

            doorLeftInitialWidth = doorLeft.Width;
            doorRightInitialWidth = doorRight.Width;
            doorLeftInitialLeft = doorLeft.Left;
            doorRightInitialLeft = doorRight.Left;

            openOffset = Math.Min(doorLeftInitialWidth, 60);

            doorTimer = new System.Windows.Forms.Timer();
            doorTimer.Interval = 16;
            doorTimer.Tick += DoorTimer_Tick;

            InitializeElevator();
        }

        private void InitializeElevator()
        {
            elevatorContext = new ElevatorContext();

            elevatorContext.FloorChanged += OnFloorChanged;
            elevatorContext.StateChanged += OnStateChanged;
            elevatorContext.MovementStarted += OnMovementStarted;
            elevatorContext.MovementCompleted += OnMovementCompleted;
            elevatorContext.EmergencyActivated += OnEmergencyActivated;
            elevatorContext.EmergencyReset += OnEmergencyReset;

            doorsOpen = false;
            doorProgressFloat = 0f;
            ApplyDoorProgress(doorProgressFloat);

            UpdateDisplays();
            logManager.LogActivity("Elevator controller initialized", "INFO");
        }

        public bool AreDoorsOpen()
        {
            return doorsOpen;
        }

        public void CompleteDoorOperation(bool doorsOpened)
        {
            doorsOpen = doorsOpened;
            if (doorsOpened)
            {
                statusDisplay.Text = "● DOORS OPEN";
                statusDisplay.ForeColor = Color.FromArgb(34, 197, 94);
                updateStatusBar("Doors fully opened", Color.FromArgb(34, 197, 94));

                // Raise DoorsOpened event
                DoorsOpened?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                statusDisplay.Text = "● DOORS CLOSED";
                statusDisplay.ForeColor = Color.FromArgb(59, 130, 246);
                updateStatusBar("Doors fully closed", Color.FromArgb(59, 130, 246));

                // Raise DoorsClosed event
                DoorsClosed?.Invoke(this, EventArgs.Empty);

                // If there's a pending movement, start it now that doors are closed
                if (pendingMovementTarget.HasValue)
                {
                    BeginMoveToFloor(pendingMovementTarget.Value);
                    pendingMovementTarget = null;
                }
            }
        }

        public void OpenDoors()
        {
            if (isMoving)
            {
                MessageBox.Show("Cannot open doors while the elevator is moving!", "Operation Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (IsEmergency)
            {
                MessageBox.Show("Cannot open doors in emergency!", "Operation Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (doorsOpen || currentDoorAction == DoorAction.Opening)
                return;

            elevatorContext.OpenDoors();
            StartDoorAnimation(DoorAction.Opening);
            logManager.LogActivity("Door open requested", "ACTION");
        }

        public void CloseDoors()
        {
            if (isMoving)
            {
                MessageBox.Show("Cannot close doors while the elevator is moving!", "Operation Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (IsEmergency)
            {
                MessageBox.Show("Cannot close doors -- emergency state!", "Operation Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!doorsOpen || currentDoorAction == DoorAction.Closing)
                return;

            elevatorContext.CloseDoors();
            StartDoorAnimation(DoorAction.Closing);
            logManager.LogActivity("Door close requested", "ACTION");
        }

        private void StartDoorAnimation(DoorAction action)
        {
            currentDoorAction = action;
            doorProgressFloat = doorsOpen ? 100f : 0f;

            if (!doorTimer.Enabled)
                doorTimer.Start();

            if (action == DoorAction.Opening)
            {
                statusDisplay.Text = "● OPENING DOORS";
                statusDisplay.ForeColor = Color.FromArgb(245, 158, 11);
            }
            else if (action == DoorAction.Closing)
            {
                statusDisplay.Text = "● CLOSING DOORS";
                statusDisplay.ForeColor = Color.FromArgb(245, 158, 11);
            }
        }

        private void DoorTimer_Tick(object sender, EventArgs e)
        {
            if (currentDoorAction == DoorAction.None)
            {
                doorTimer.Stop();
                return;
            }

            if (currentDoorAction == DoorAction.Opening)
            {
                doorProgressFloat += doorStep;
                if (doorProgressFloat >= 100f)
                {
                    FinishDoorAnimation(true);
                    return;
                }
            }
            else if (currentDoorAction == DoorAction.Closing)
            {
                doorProgressFloat -= doorStep;
                if (doorProgressFloat <= 0f)
                {
                    FinishDoorAnimation(false);
                    return;
                }
            }

            float easedProgress = EaseInOut(doorProgressFloat / 100f);
            ApplyDoorProgress(easedProgress * 100f);
        }

        private float EaseInOut(float t)
        {
            return (float)(t < 0.5 ? 4 * t * t * t : 1 - Math.Pow(-2 * t + 2, 3) / 2);
        }

        private void FinishDoorAnimation(bool opened)
        {
            doorProgressFloat = opened ? 100f : 0f;
            ApplyDoorProgress(doorProgressFloat);
            doorTimer.Stop();
            currentDoorAction = DoorAction.None;
            doorsOpen = opened;

            elevatorContext.CompleteDoorOperation(opened);
            CompleteDoorOperation(opened);
        }

        private void ApplyDoorProgress(float progress)
        {
            if (doorLeft.InvokeRequired)
            {
                doorLeft.Invoke(new Action<float>(ApplyDoorProgress), progress);
                return;
            }

            progress = Math.Max(0, Math.Min(100, progress));

            int leftClosed = doorLeftInitialLeft;
            int leftOpen = doorLeftInitialLeft - openOffset;
            int rightClosed = doorRightInitialLeft;
            int rightOpen = doorRightInitialLeft + openOffset;

            float t = progress / 100f;
            doorLeft.Left = (int)Math.Round(leftClosed + (leftOpen - leftClosed) * t);
            doorRight.Left = (int)Math.Round(rightClosed + (rightOpen - rightClosed) * t);
        }

        public void StartMovement(int targetFloor)
        {
            // If doors are open or opening, wait for them to close completely before moving
            if (doorsOpen || currentDoorAction == DoorAction.Opening)
            {
                // Store the target floor for when doors are closed
                pendingMovementTarget = targetFloor;

                // Start closing doors if they're open
                if (doorsOpen && currentDoorAction != DoorAction.Closing)
                {
                    CloseDoors();
                }

                logManager.LogActivity($"Movement queued - waiting for doors to close before moving to floor {(targetFloor == 0 ? "G" : "1")}", "QUEUE");
                return;
            }

            // If doors are already closed, start movement immediately
            BeginMoveToFloor(targetFloor);
        }

        private void BeginMoveToFloor(int targetFloor)
        {
            isMoving = true;
            elevatorContext.MoveToFloor(targetFloor);
            logManager.LogActivity($"Movement started to floor {(targetFloor == 0 ? "G" : "1")}", "MOVEMENT");

            // Notify that movement is starting
            MovementRequested?.Invoke(this, targetFloor);
        }

        public void UpdateMovementProgress(int progress)
        {
            if (progress == 50)
            {
                statusDisplay.Text = "● MOVING...";
                statusDisplay.ForeColor = Color.FromArgb(59, 130, 246);
            }
        }

        public void CompleteMovement(int targetFloor)
        {
            elevatorContext.ArriveAtFloor(targetFloor);
            isMoving = false;

            // Open doors after arrival with a small delay
            System.Threading.Thread.Sleep(300); // Small delay before opening doors
            OpenDoors();

            logManager.LogActivity($"Movement completed to floor {(targetFloor == 0 ? "G" : "1")}", "MOVEMENT");
        }

        public void RequestFloor(int floor, string source)
        {
            elevatorContext.RequestFloor(floor);
        }

        public void QueueRequest(int floor)
        {
            elevatorContext.QueueRequest(floor);
        }

        public int? GetNextRequest()
        {
            return elevatorContext.GetNextRequest();
        }

        public bool CanMove()
        {
            return elevatorContext.CanMove() && !IsEmergency && !doorsOpen && currentDoorAction == DoorAction.None;
        }

        public void ActivateEmergency()
        {
            elevatorContext.ActivateEmergency();
            // Clear any pending movements in emergency
            pendingMovementTarget = null;
        }

        public void ResetEmergency()
        {
            elevatorContext.ResetEmergency();
        }

        private void OnFloorChanged(object sender, FloorChangedEventArgs e)
        {
            UpdateDisplays();
            logManager.LogActivity($"Now at floor {(e.Floor == 0 ? "G" : "1")}", "INFO");
        }

        private void OnStateChanged(object sender, StateChangedEventArgs e)
        {
            logManager.LogActivity($"State changed: {e.OldState} → {e.NewState}", "STATE");
        }

        private void OnMovementStarted(object sender, MovementEventArgs e)
        {
            isMoving = true;
            statusDisplay.Text = "● MOVING";
            statusDisplay.ForeColor = Color.FromArgb(59, 130, 246);
        }

        private void OnMovementCompleted(object sender, EventArgs e)
        {
            isMoving = false;
        }

        private void OnEmergencyActivated(object sender, EventArgs e)
        {
            statusDisplay.Text = "● EMERGENCY";
            statusDisplay.ForeColor = Color.FromArgb(239, 68, 68);
            updateStatusBar("EMERGENCY ACTIVATED", Color.FromArgb(239, 68, 68));
        }

        private void OnEmergencyReset(object sender, EventArgs e)
        {
            statusDisplay.Text = "● READY";
            statusDisplay.ForeColor = Color.FromArgb(34, 197, 94);
            updateStatusBar("Emergency reset - System normal", Color.FromArgb(34, 197, 94));
        }

        private void UpdateDisplays()
        {
            if (floorDisplay.InvokeRequired)
            {
                floorDisplay.Invoke(new Action(UpdateDisplays));
                return;
            }

            string floorText = elevatorContext.GetFloorDisplay();
            floorDisplay.Text = floorText;
            controlPanelDisplay.Text = floorText;

            if (IsEmergency)
            {
                floorDisplay.ForeColor = Color.FromArgb(239, 68, 68);
                controlPanelDisplay.ForeColor = Color.FromArgb(239, 68, 68);
            }
            else if (isMoving)
            {
                floorDisplay.ForeColor = Color.FromArgb(59, 130, 246);
                controlPanelDisplay.ForeColor = Color.FromArgb(59, 130, 246);
            }
            else
            {
                floorDisplay.ForeColor = Color.FromArgb(34, 197, 94);
                controlPanelDisplay.ForeColor = Color.FromArgb(34, 197, 94);
            }
        }

        public string GetCurrentState()
        {
            return elevatorContext?.GetCurrentStateName() ?? "UNKNOWN";
        }

        public int GetCurrentFloor()
        {
            return elevatorContext?.CurrentFloor ?? 0;
        }

        public int GetRequestCount()
        {
            return elevatorContext?.GetRequestCount() ?? 0;
        }

        public bool HasPendingMovement()
        {
            return pendingMovementTarget.HasValue;
        }

        public void CancelPendingMovement()
        {
            pendingMovementTarget = null;
        }
    }
}