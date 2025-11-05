using ElevatorSystem.Interfaces;
using ElevatorSystem.Models;

namespace ElevatorSystem.States
{
    public class MovingState : IElevatorState
    {
        public void HandleRequest(ElevatorContext context, int floor)
        {
            // Queue the request while moving
            context.QueueRequest(floor);
        }

        public void OpenDoors(ElevatorContext context)
        {
            // Cannot open doors while moving
        }

        public void CloseDoors(ElevatorContext context)
        {
            // Cannot close doors while moving
        }

        public string GetStateName()
        {
            return "MOVING";
        }
    }
}