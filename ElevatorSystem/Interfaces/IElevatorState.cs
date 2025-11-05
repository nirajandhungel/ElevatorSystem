using ElevatorSystem.Models;

namespace ElevatorSystem.Interfaces
{
    public interface IElevatorState
    {
        void HandleRequest(ElevatorContext context, int floor);
        void OpenDoors(ElevatorContext context);
        void CloseDoors(ElevatorContext context);
        string GetStateName();
    }
}