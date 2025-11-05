using ElevatorSystem.Interfaces;
using ElevatorSystem.Models;

namespace ElevatorSystem.States
{
    public class IdleState : IElevatorState
    {
        public void HandleRequest(ElevatorContext context, int floor)
        {
            if (floor != context.CurrentFloor)
            {
                context.SetState(new MovingState());
                context.MoveToFloor(floor);
            }
        }

        public void OpenDoors(ElevatorContext context)
        {
            context.SetState(new DoorOpeningState());
        }

        public void CloseDoors(ElevatorContext context)
        {
            if (context.DoorsOpen)
            {
                context.SetState(new DoorsClosingState());
            }
        }

        public string GetStateName()
        {
            return "IDLE";
        }
    }
}