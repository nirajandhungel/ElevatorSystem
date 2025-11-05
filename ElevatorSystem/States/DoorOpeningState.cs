using ElevatorSystem.Interfaces;
using ElevatorSystem.Models;

namespace ElevatorSystem.States
{
    public class DoorOpeningState : IElevatorState
    {
        public void HandleRequest(ElevatorContext context, int floor)
        {
            // Queue the request while doors are opening
            context.QueueRequest(floor);
        }

        public void OpenDoors(ElevatorContext context)
        {
            // Already opening
        }

        public void CloseDoors(ElevatorContext context)
        {
            // Cannot close while opening
        }

        public string GetStateName()
        {
            return "OPENING DOORS";
        }
    }
}