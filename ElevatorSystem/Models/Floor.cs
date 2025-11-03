using System;
using System.Collections.Generic;

namespace ElevatorSystem.Models
{
    public class Floor
    {
        public int Number { get; }
        public bool HasUpRequest { get; set; }
        public bool HasDownRequest { get; set; }
        public string DisplayName { get; }
        public List<DateTime> RequestTimes { get; private set; }

        public Floor(int number)
        {
            Number = number;
            HasUpRequest = false;
            HasDownRequest = false;
            DisplayName = number == 0 ? "Ground (G)" : $"Floor {number}";
            RequestTimes = new List<DateTime>();
        }

        public bool HasRequest => HasUpRequest || HasDownRequest;

        public void RequestUp()
        {
            if (!HasUpRequest)
            {
                HasUpRequest = true;
                RequestTimes.Add(DateTime.Now);
            }
        }

        public void RequestDown()
        {
            if (!HasDownRequest)
            {
                HasDownRequest = true;
                RequestTimes.Add(DateTime.Now);
            }
        }

        public void ClearRequests()
        {
            HasUpRequest = false;
            HasDownRequest = false;
            RequestTimes.Clear();
        }

        public void ClearUpRequest()
        {
            HasUpRequest = false;
        }

        public void ClearDownRequest()
        {
            HasDownRequest = false;
        }

        public TimeSpan GetWaitTime()
        {
            if (RequestTimes.Count == 0)
                return TimeSpan.Zero;

            return DateTime.Now - RequestTimes[0];
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }

    public class FloorManager
    {
        private readonly Dictionary<int, Floor> floors;
        private readonly int minFloor;
        private readonly int maxFloor;

        public FloorManager(int minFloor, int maxFloor)
        {
            this.minFloor = minFloor;
            this.maxFloor = maxFloor;
            floors = new Dictionary<int, Floor>();

            for (int i = minFloor; i <= maxFloor; i++)
            {
                floors[i] = new Floor(i);
            }
        }

        public Floor GetFloor(int number)
        {
            if (floors.ContainsKey(number))
                return floors[number];

            throw new ArgumentException($"Floor {number} does not exist");
        }

        public void RequestElevator(int floorNumber, string direction)
        {
            Floor floor = GetFloor(floorNumber);

            if (direction == "UP")
                floor.RequestUp();
            else if (direction == "DOWN")
                floor.RequestDown();
        }

        public void ClearFloorRequests(int floorNumber)
        {
            GetFloor(floorNumber).ClearRequests();
        }

        public List<Floor> GetFloorsWithRequests()
        {
            List<Floor> requestedFloors = new List<Floor>();

            foreach (var floor in floors.Values)
            {
                if (floor.HasRequest)
                    requestedFloors.Add(floor);
            }

            return requestedFloors;
        }

        public int GetFloorCount()
        {
            return floors.Count;
        }

        public List<int> GetFloorNumbers()
        {
            return new List<int>(floors.Keys);
        }
    }
}