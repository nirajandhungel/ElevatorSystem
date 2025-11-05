using ElevatorSystem.Interfaces;
using ElevatorSystem.Models;

namespace ElevatorSystem.States
{
    public class DoorsOpenState : IElevatorState
    {
        public void HandleRequest(ElevatorContext context, int floor)
        {
            if (floor != context.CurrentFloor)
            {
                // Close doors first, then move
                context.SetState(new DoorsClosingState());
                context.QueueRequest(floor);
            }
        }

        public void OpenDoors(ElevatorContext context)
        {
            // Already open
        }

        public void CloseDoors(ElevatorContext context)
        {
            context.SetState(new DoorsClosingState());
        }

        public string GetStateName()
        {
            return "DOORS OPEN";
        }
    }
}