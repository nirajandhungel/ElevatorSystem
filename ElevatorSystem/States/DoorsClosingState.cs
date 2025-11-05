using ElevatorSystem.Interfaces;
using ElevatorSystem.Models;

namespace ElevatorSystem.States
{
    public class DoorsClosingState : IElevatorState
    {
        public void HandleRequest(ElevatorContext context, int floor)
        {
            // Queue the request while doors are closing
            context.QueueRequest(floor);
        }

        public void OpenDoors(ElevatorContext context)
        {
            // Can reopen while closing
            context.SetState(new DoorOpeningState());
        }

        public void CloseDoors(ElevatorContext context)
        {
            // Already closing
        }

        public string GetStateName()
        {
            return "CLOSING DOORS";
        }
    }
}