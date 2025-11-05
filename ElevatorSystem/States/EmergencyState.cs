using ElevatorSystem.Interfaces;
using ElevatorSystem.Models;

namespace ElevatorSystem.States
{
    public class EmergencyState : IElevatorState
    {
        public void HandleRequest(ElevatorContext context, int floor)
        {
            // No operations allowed in emergency
        }

        public void OpenDoors(ElevatorContext context)
        {
            // No operations allowed in emergency
        }

        public void CloseDoors(ElevatorContext context)
        {
            // No operations allowed in emergency
        }

        public string GetStateName()
        {
            return "EMERGENCY";
        }
    }
}