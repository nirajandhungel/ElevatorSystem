using ElevatorSystem.Models;
using System;
using System.Drawing;
using System.Security.Claims;
using System.Windows.Forms;

namespace ElevatorSystem.Controllers
{
    public class ElevatorController
    {
        private ElevatorContext elevatorContext;
        private PictureBox elevatorCar;
        private PictureBox doorLeft;
        private PictureBox doorRight;
        private Label floorDisplay;
        private Label controlPanelDisplay;
        private Label statusDisplay;
        private LogManager logManager;
        private Action<string, Color> updateStatusBar;
        // Add this public event
        public event EventHandler DoorsClosed;

        // Add this public event
        public event EventHandler DoorsOpened;

        // Door animation properties (initial positions)
        private int doorLeftInitialWidth;
        private int doorRightInitialWidth;
        private int doorRightInitialLeft;
        private int doorLeftInitialLeft;

        private bool doorsOpen = false;
        private bool isMoving = false;

        // Animation timer & state
        private readonly System.Windows.Forms.Timer doorTimer;
        private float doorProgressFloat = 0f; // 0 = fully closed, 100 = fully open
        private DoorAction currentDoorAction = DoorAction.None;
        private readonly int doorStep = 3; // progress step per tick
        private readonly int openOffset; // how far each door moves when opening

        public bool IsEmergency => elevatorContext?.IsEmergency ?? false;

        private enum DoorAction
        {
            None,
            Opening,
            Closing
        }

        public ElevatorController(PictureBox car, PictureBox leftDoor, PictureBox rightDoor,
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

            // Store initial door geometry for animation (from Designer)
            doorLeftInitialWidth = doorLeft.Width;
            doorRightInitialWidth = doorRight.Width;
            doorLeftInitialLeft = doorLeft.Left;
            doorRightInitialLeft = doorRight.Left;

            // Determine a sane open offset: don't move more than width, clamp to 60 px
            openOffset = Math.Min(doorLeftInitialWidth, 60);

            // Door timer setup
            doorTimer = new System.Windows.Forms.Timer { };
            doorTimer.Interval = 16; // ms
            doorTimer.Tick += DoorTimer_Tick;

            InitializeElevator();
        }

        private void InitializeElevator()
        {
            elevatorContext = new ElevatorContext();

            // Subscribe to events
            elevatorContext.FloorChanged += OnFloorChanged;
            elevatorContext.StateChanged += OnStateChanged;
            //elevatorContext.DoorsOpened += OnDoorsOpened;
            //elevatorContext.DoorsClosed += OnDoorsClosed;
            elevatorContext.MovementStarted += OnMovementStarted;
            elevatorContext.MovementCompleted += OnMovementCompleted;
            elevatorContext.EmergencyActivated += OnEmergencyActivated;
            elevatorContext.EmergencyReset += OnEmergencyReset;

            // Start with doors closed and position values matching Designer
            doorsOpen = false;
            doorProgressFloat = 0f;
            ApplyDoorProgress(doorProgressFloat);

            UpdateDisplays();
            logManager.LogActivity("Elevator controller initialized", "INFO");
        }

        // NEW METHOD: Check if doors are open
        public bool AreDoorsOpen()
        {
            return doorsOpen;
        }

        // NEW METHOD: Complete door operation (keeps controller and UI consistent)
        public void CompleteDoorOperation(bool doorsOpened)
        {
            doorsOpen = doorsOpened;
            if (doorsOpened)
            {
                statusDisplay.Text = "● DOORS OPEN";
                statusDisplay.ForeColor = Color.FromArgb(34, 197, 94);
                updateStatusBar("Doors fully opened", Color.FromArgb(34, 197, 94));
            }
            else
            {
                statusDisplay.Text = "● DOORS CLOSED";
                statusDisplay.ForeColor = Color.FromArgb(59, 130, 246);
                updateStatusBar("Doors fully closed", Color.FromArgb(59, 130, 246));
            }
        }

        private void OnDoorsClosed(object sender, EventArgs e)
        {
            doorsOpen = false;
            statusDisplay.Text = "● DOORS CLOSED";
            statusDisplay.ForeColor = Color.FromArgb(59, 130, 246);
            updateStatusBar("Doors closed", Color.FromArgb(59, 130, 246));

            // Raise the public event
            DoorsClosed?.Invoke(this, EventArgs.Empty);
        }


        private void OnDoorsOpened(object sender, EventArgs e)
        {
            doorsOpen = false;
            statusDisplay.Text = "● DOORS Opened";
            statusDisplay.ForeColor = Color.FromArgb(59, 130, 246);
            updateStatusBar("Doors opened", Color.FromArgb(59, 130, 246));

            // Raise the public event
            DoorsOpened?.Invoke(this, EventArgs.Empty);
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

            // If already open or opening, ignore
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

            // If already closed or closing, ignore
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

            // Update status immediately when starting animation
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



        private void DoorTimer_Tick(object? sender, EventArgs e)
        {
            if (currentDoorAction == DoorAction.None)
            {
                doorTimer.Stop();
                return;
            }

            // Calculate progress with easing
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

            // Apply smooth easing
            float easedProgress = EaseInOut(doorProgressFloat / 100f);
            ApplyDoorProgress(easedProgress * 100f);
        }


        private float EaseInOut(float t)
        {
            // Smooth ease-in-out function
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

            // Clamp progress
            progress = Math.Max(0, Math.Min(100, progress));

            // Calculate positions
            int leftClosed = doorLeftInitialLeft;
            int leftOpen = doorLeftInitialLeft - openOffset;
            int rightClosed = doorRightInitialLeft;
            int rightOpen = doorRightInitialLeft + openOffset;

            // Smooth interpolation
            float t = progress / 100f;
            doorLeft.Left = (int)Math.Round(leftClosed + (leftOpen - leftClosed) * t);
            doorRight.Left = (int)Math.Round(rightClosed + (rightOpen - rightClosed) * t);
        }


        public void StartMovement(int targetFloor)
        {
            // If doors are open, close them first and start movement after closed
            if (doorsOpen)
            {
                // one-shot handler to begin movement when doors closed
                void AfterClose(object? s, EventArgs args)
                {
                    elevatorContext.DoorsClosed -= AfterClose;
                    // now perform move
                    BeginMoveToFloor(targetFloor);
                }

                elevatorContext.DoorsClosed += AfterClose;
                CloseDoors();
                return;
            }

            BeginMoveToFloor(targetFloor);
        }

        private void BeginMoveToFloor(int targetFloor)
        {
            isMoving = true;
            elevatorContext.MoveToFloor(targetFloor);
            logManager.LogActivity($"Movement started to floor {(targetFloor == 0 ? "G" : "1")}", "MOVEMENT");
        }

        public void UpdateMovementProgress(int progress)
        {
            // Optional: update status at mid-point
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
            // Open doors automatically after arrival
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
            return elevatorContext.CanMove() && !IsEmergency;
        }

        public void ActivateEmergency()
        {
            elevatorContext.ActivateEmergency();
        }

        public void ResetEmergency()
        {
            elevatorContext.ResetEmergency();
        }

        // Event handlers
        private void OnFloorChanged(object sender, FloorChangedEventArgs e)
        {
            UpdateDisplays();
            logManager.LogActivity($"Now at floor {(e.Floor == 0 ? "G" : "1")}", "INFO");
        }

        private void OnStateChanged(object sender, StateChangedEventArgs e)
        {
            logManager.LogActivity($"State changed: {e.OldState} → {e.NewState}", "STATE");
        }

        //private void OnDoorsOpened(object sender, EventArgs e)
        //{
        //    doorsOpen = true;
        //    statusDisplay.Text = "● DOORS OPEN";
        //    statusDisplay.ForeColor = Color.FromArgb(34, 197, 94);
        //    updateStatusBar("Doors opened", Color.FromArgb(34, 197, 94));
        //}

        //private void OnDoorsClosed(object sender, EventArgs e)
        //{
        //    doorsOpen = false;
        //    statusDisplay.Text = "● DOORS CLOSED";
        //    statusDisplay.ForeColor = Color.FromArgb(59, 130, 246);
        //    updateStatusBar("Doors closed", Color.FromArgb(59, 130, 246));
        //}

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

            // Update floor display color based on state
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

        // Additional helper methods
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
    }
}