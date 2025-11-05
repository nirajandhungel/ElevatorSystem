using ElevatorSystem.Interfaces;
using ElevatorSystem.States;
using System;
using System.Collections.Generic;

namespace ElevatorSystem.Models
{
    public class ElevatorContext
    {
        private IElevatorState currentState;
        private Queue<int> requestQueue;

        public int CurrentFloor { get; set; }
        public int MinFloor { get; } = 0;
        public int MaxFloor { get; } = 1;
        public bool DoorsOpen { get; set; }
        public bool IsEmergency { get; private set; }
        public bool IsCallHelp { get; private set; }


        // Events - marked as nullable to fix warnings
        public event EventHandler<FloorChangedEventArgs>? FloorChanged;
        public event EventHandler<StateChangedEventArgs>? StateChanged;
        public event EventHandler? DoorsOpened;
        public event EventHandler? DoorsClosed;
        public event EventHandler<MovementEventArgs>? MovementStarted;
        public event EventHandler? MovementCompleted;
        public event EventHandler? EmergencyActivated;
        public event EventHandler? EmergencyReset;
        public event EventHandler? AlarmActivated;
        public event EventHandler? AlarmReset;

        public ElevatorContext()
        {
            currentState = new IdleState();
            requestQueue = new Queue<int>();
            CurrentFloor = 0;
            DoorsOpen = true;
            IsEmergency = false;
            IsCallHelp = false;

        }

        public void SetState(IElevatorState newState)
        {
            var oldStateName = currentState?.GetStateName() ?? "UNKNOWN";
            currentState = newState;
            var newStateName = currentState.GetStateName();

            StateChanged?.Invoke(this, new StateChangedEventArgs(oldStateName, newStateName));

            // Handle state transitions
            if (newState is DoorsOpenState)
            {
                DoorsOpen = true;
                DoorsOpened?.Invoke(this, EventArgs.Empty);
            }
            else if (newState is DoorsClosingState)
            {
                DoorsOpen = false;
                DoorsClosed?.Invoke(this, EventArgs.Empty);
            }
            else if (newState is IdleState)
            {
                MovementCompleted?.Invoke(this, EventArgs.Empty);
            }
        }

        public void CompleteDoorOperation(bool doorsOpened)
        {
            if (doorsOpened)
            {
                SetState(new DoorsOpenState());
            }
            else
            {
                SetState(new IdleState());
            }
        }

        public void RequestFloor(int floor)
        {
            if (floor < MinFloor || floor > MaxFloor)
            {
                throw new ArgumentException($"Floor must be between {MinFloor} and {MaxFloor}");
            }
            if (CurrentFloor == floor)
            {
                throw new ArgumentException($"Already at floor {floor}");
            }

            if (!IsEmergency)
            {
                currentState.HandleRequest(this, floor);
            }
        }

        public void OpenDoors()
        {
            if (!IsEmergency)
            {
                currentState.OpenDoors(this);
            }
        }

        public void CloseDoors()
        {
            if (!IsEmergency)
            {
                currentState.CloseDoors(this);
            }
        }

        public void QueueRequest(int floor)
        {
            if (!requestQueue.Contains(floor) && floor != CurrentFloor && !IsEmergency)
            {
                requestQueue.Enqueue(floor);
            }
        }

        public int? GetNextRequest()
        {
            if (requestQueue.Count > 0)
                return requestQueue.Dequeue();
            return null;
        }

        public void MoveToFloor(int targetFloor)
        {
            if (targetFloor == CurrentFloor || IsEmergency)
                return;

            MovementStarted?.Invoke(this, new MovementEventArgs(CurrentFloor, targetFloor));
        }

        public void ArriveAtFloor(int floor)
        {
            CurrentFloor = floor;
            FloorChanged?.Invoke(this, new FloorChangedEventArgs(floor));
            SetState(new IdleState());
            SetState(new DoorsOpenState());
        }

        public void ActivateAlarm()
        {
            IsCallHelp = true;
            SetState(new HelpState());
            requestQueue.Clear();
            AlarmActivated?.Invoke(this, EventArgs.Empty);
        }

        public void ResetAlarm()
        {
            IsCallHelp = false;
            SetState(new IdleState());
            DoorsOpen = true;
            AlarmReset?.Invoke(this, EventArgs.Empty);
        }

        public void ActivateEmergency()
        {
            IsEmergency = true;
            SetState(new EmergencyState());
            requestQueue.Clear();
            EmergencyActivated?.Invoke(this, EventArgs.Empty);
        }

        public void ResetEmergency()
        {
            IsEmergency = false;
            SetState(new IdleState());
            DoorsOpen = true;
            EmergencyReset?.Invoke(this, EventArgs.Empty);
        }

        public string GetCurrentStateName()
        {
            return currentState.GetStateName();
        }

        public bool CanMove()
        {
            return !IsEmergency && !(currentState is MovingState);
        }

        public string GetFloorDisplay()
        {
            return CurrentFloor == 0 ? "G" : CurrentFloor.ToString();
        }

        public int GetRequestCount()
        {
            return requestQueue.Count;
        }

        public void ClearRequests()
        {
            requestQueue.Clear();
        }
    }

    // Event Arguments
    public class FloorChangedEventArgs : EventArgs
    {
        public int Floor { get; }
        public DateTime Timestamp { get; }

        public FloorChangedEventArgs(int floor)
        {
            Floor = floor;
            Timestamp = DateTime.Now;
        }
    }

    public class StateChangedEventArgs : EventArgs
    {
        public string OldState { get; }
        public string NewState { get; }
        public DateTime Timestamp { get; }

        public StateChangedEventArgs(string oldState, string newState)
        {
            OldState = oldState;
            NewState = newState;
            Timestamp = DateTime.Now;
        }
    }

    public class MovementEventArgs : EventArgs
    {
        public int FromFloor { get; }
        public int ToFloor { get; }
        public string Direction { get; }
        public DateTime Timestamp { get; }

        public MovementEventArgs(int from, int to)
        {
            FromFloor = from;
            ToFloor = to;
            Direction = to > from ? "UP" : "DOWN";
            Timestamp = DateTime.Now;
        }
    }
}