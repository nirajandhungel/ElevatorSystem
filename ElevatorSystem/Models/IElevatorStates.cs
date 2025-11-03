using System;

namespace ElevatorSystem.Models
{
    // State Pattern Implementation
    public interface IElevatorState
    {
        void HandleRequest(ElevatorContext context, int targetFloor);
        void OpenDoors(ElevatorContext context);
        void CloseDoors(ElevatorContext context);
        string GetStateName();
    }

    // Idle State
    public class IdleState : IElevatorState
    {
        public void HandleRequest(ElevatorContext context, int targetFloor)
        {
            if (targetFloor != context.CurrentFloor)
            {
                context.SetState(new MovingState(targetFloor));
            }
            else
            {
                context.OpenDoors();
            }
        }

        public void OpenDoors(ElevatorContext context)
        {
            context.SetState(new DoorsOpenState());
        }

        public void CloseDoors(ElevatorContext context)
        {
            // Already closed in idle
        }

        public string GetStateName() => "IDLE";
    }

    // Moving State
    public class MovingState : IElevatorState
    {
        private readonly int targetFloor;

        public MovingState(int target)
        {
            targetFloor = target;
        }

        public void HandleRequest(ElevatorContext context, int targetFloor)
        {
            // Queue the request
            context.QueueRequest(targetFloor);
        }

        public void OpenDoors(ElevatorContext context)
        {
            // Cannot open doors while moving
        }

        public void CloseDoors(ElevatorContext context)
        {
            // Already closed
        }

        public string GetStateName() => "DOORS CLOSING";
    }

    // Doors Open State
    public class DoorsOpenState : IElevatorState
    {
        public void HandleRequest(ElevatorContext context, int targetFloor)
        {
            // Close doors first, then handle request
            context.SetState(new DoorsClosingState());
            context.QueueRequest(targetFloor);
        }

        public void OpenDoors(ElevatorContext context)
        {

            // Already open
        }

        public void CloseDoors(ElevatorContext context)
        {
            context.SetState(new DoorsClosingState());
        }

        public string GetStateName() => "DOORS OPEN";
    }

    // Doors Closing State
    public class DoorsClosingState : IElevatorState
    {
        public void HandleRequest(ElevatorContext context, int targetFloor)
        {
            // Wait for doors to close
            context.QueueRequest(targetFloor);
        }

        public void OpenDoors(ElevatorContext context)
        {
            context.SetState(new DoorsOpenState());
        }

        public void CloseDoors(ElevatorContext context)
        {
            // Already closing
        }

        public string GetStateName() => "DOORS CLOSING";
    }

    // Emergency State
    public class EmergencyState : IElevatorState
    {
        public void HandleRequest(ElevatorContext context, int targetFloor)
        {
            // Cannot process requests in emergency
        }

        public void OpenDoors(ElevatorContext context)
        {
            // Emergency - doors stay open for safety
            context.DoorsOpen = true;
        }

        public void CloseDoors(ElevatorContext context)
        {
            // Emergency - doors cannot be closed
        }

        public string GetStateName() => "EMERGENCY";
    }
}