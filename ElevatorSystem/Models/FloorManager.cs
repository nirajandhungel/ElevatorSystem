using System;
using System.Collections.Generic;
using System.Linq;

namespace ElevatorSystem.Models
{
    public class FloorManager
    {
        private readonly Dictionary<int, Floor> floors;
        private readonly List<FloorRequest> pendingRequests;
        private readonly int minFloor;
        private readonly int maxFloor;

        public FloorManager(int minFloor = 0, int maxFloor = 1)
        {
            this.minFloor = minFloor;
            this.maxFloor = maxFloor;
            floors = new Dictionary<int, Floor>();
            pendingRequests = new List<FloorRequest>();

            InitializeFloors();
        }

        private void InitializeFloors()
        {
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
            ValidateFloorNumber(floorNumber);
            ValidateDirection(direction);

            Floor floor = GetFloor(floorNumber);

            if (direction == "UP")
            {
                floor.RequestUp();
                if (!HasPendingRequest(floorNumber, direction))
                {
                    pendingRequests.Add(new FloorRequest(floorNumber, direction));
                }
            }
            else if (direction == "DOWN")
            {
                floor.RequestDown();
                if (!HasPendingRequest(floorNumber, direction))
                {
                    pendingRequests.Add(new FloorRequest(floorNumber, direction));
                }
            }
        }

        public void ClearFloorRequests(int floorNumber)
        {
            ValidateFloorNumber(floorNumber);

            Floor floor = GetFloor(floorNumber);
            floor.ClearRequests();

            // Remove pending requests for this floor
            pendingRequests.RemoveAll(req => req.FloorNumber == floorNumber);
        }

        public void ClearDirectionRequest(int floorNumber, string direction)
        {
            ValidateFloorNumber(floorNumber);
            ValidateDirection(direction);

            Floor floor = GetFloor(floorNumber);

            if (direction == "UP")
            {
                floor.ClearUpRequest();
            }
            else if (direction == "DOWN")
            {
                floor.ClearDownRequest();
            }

            // Remove specific pending request
            var request = pendingRequests.FirstOrDefault(req =>
                req.FloorNumber == floorNumber && req.Direction == direction);
            if (request != null)
            {
                pendingRequests.Remove(request);
            }
        }

        public void MarkRequestProcessed(int floorNumber, string direction)
        {
            var request = pendingRequests.FirstOrDefault(req =>
                req.FloorNumber == floorNumber && req.Direction == direction);

            if (request != null)
            {
                request.IsProcessed = true;
                ClearDirectionRequest(floorNumber, direction);
            }
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

        public List<FloorRequest> GetPendingRequests()
        {
            return pendingRequests.Where(req => !req.IsProcessed).ToList();
        }

        public List<FloorRequest> GetPendingRequestsByPriority(int currentFloor, string currentDirection = null)
        {
            var pending = GetPendingRequests();

            if (string.IsNullOrEmpty(currentDirection))
            {
                // Simple distance-based priority when no direction
                return pending.OrderBy(req => Math.Abs(req.FloorNumber - currentFloor)).ToList();
            }

            // SCAN algorithm implementation
            var requestsInDirection = pending.Where(req =>
            {
                if (currentDirection == "UP")
                    return req.FloorNumber >= currentFloor;
                else
                    return req.FloorNumber <= currentFloor;
            }).ToList();

            var requestsOppositeDirection = pending.Except(requestsInDirection).ToList();

            if (currentDirection == "UP")
            {
                requestsInDirection = requestsInDirection.OrderBy(req => req.FloorNumber).ToList();
                requestsOppositeDirection = requestsOppositeDirection.OrderByDescending(req => req.FloorNumber).ToList();
            }
            else
            {
                requestsInDirection = requestsInDirection.OrderByDescending(req => req.FloorNumber).ToList();
                requestsOppositeDirection = requestsOppositeDirection.OrderBy(req => req.FloorNumber).ToList();
            }

            return requestsInDirection.Concat(requestsOppositeDirection).ToList();
        }

        public bool HasPendingRequests()
        {
            return pendingRequests.Any(req => !req.IsProcessed);
        }

        public bool HasPendingRequest(int floorNumber, string direction)
        {
            return pendingRequests.Any(req =>
                req.FloorNumber == floorNumber &&
                req.Direction == direction &&
                !req.IsProcessed);
        }

        public int GetNextRequestInDirection(int currentFloor, string direction)
        {
            var pending = GetPendingRequestsByPriority(currentFloor, direction);
            return pending.FirstOrDefault()?.FloorNumber ?? -1;
        }

        public Dictionary<string, int> GetRequestStatistics()
        {
            var stats = new Dictionary<string, int>
            {
                ["TotalFloors"] = floors.Count,
                ["FloorsWithRequests"] = GetFloorsWithRequests().Count,
                ["TotalRequests"] = pendingRequests.Count(req => !req.IsProcessed),
                ["UpRequests"] = pendingRequests.Count(req => req.Direction == "UP" && !req.IsProcessed),
                ["DownRequests"] = pendingRequests.Count(req => req.Direction == "DOWN" && !req.IsProcessed)
            };

            return stats;
        }

        public List<Floor> GetFloorsByWaitTime()
        {
            return GetFloorsWithRequests()
                .OrderByDescending(floor => floor.GetWaitTime())
                .ToList();
        }

        public string GetLongestWaitTime()
        {
            var floorsWithRequests = GetFloorsWithRequests();
            if (floorsWithRequests.Count == 0)
                return "0s";

            var longestWait = floorsWithRequests.Max(floor => floor.GetWaitTime());
            return FormatWaitTime(longestWait);
        }

        public string GetAverageWaitTime()
        {
            var floorsWithRequests = GetFloorsWithRequests();
            if (floorsWithRequests.Count == 0)
                return "0s";

            var averageWait = TimeSpan.FromSeconds(
                floorsWithRequests.Average(floor => floor.GetWaitTime().TotalSeconds));
            return FormatWaitTime(averageWait);
        }

        private string FormatWaitTime(TimeSpan waitTime)
        {
            if (waitTime.TotalSeconds < 60)
                return $"{waitTime.Seconds}s";
            else if (waitTime.TotalMinutes < 60)
                return $"{waitTime.Minutes}m {waitTime.Seconds}s";
            else
                return $"{waitTime.Hours}h {waitTime.Minutes}m";
        }

        public void EnableFloor(int floorNumber)
        {
            ValidateFloorNumber(floorNumber);
            GetFloor(floorNumber).IsServiced = true;
        }

        public void DisableFloor(int floorNumber)
        {
            ValidateFloorNumber(floorNumber);
            GetFloor(floorNumber).IsServiced = false;
            ClearFloorRequests(floorNumber);
        }

        public bool IsFloorEnabled(int floorNumber)
        {
            ValidateFloorNumber(floorNumber);
            return GetFloor(floorNumber).IsServiced;
        }

        public List<int> GetEnabledFloors()
        {
            return floors.Values
                .Where(floor => floor.IsServiced)
                .Select(floor => floor.Number)
                .ToList();
        }

        public List<int> GetDisabledFloors()
        {
            return floors.Values
                .Where(floor => !floor.IsServiced)
                .Select(floor => floor.Number)
                .ToList();
        }

        public int GetFloorCount()
        {
            return floors.Count;
        }

        public List<int> GetFloorNumbers()
        {
            return new List<int>(floors.Keys);
        }

        public List<Floor> GetAllFloors()
        {
            return new List<Floor>(floors.Values);
        }

        public void ResetAllRequests()
        {
            foreach (var floor in floors.Values)
            {
                floor.ClearRequests();
            }
            pendingRequests.Clear();
        }

        private void ValidateFloorNumber(int floorNumber)
        {
            if (!floors.ContainsKey(floorNumber))
                throw new ArgumentException($"Floor {floorNumber} does not exist. Valid floors: {minFloor}-{maxFloor}");
        }

        private void ValidateDirection(string direction)
        {
            if (direction != "UP" && direction != "DOWN")
                throw new ArgumentException("Direction must be 'UP' or 'DOWN'");
        }

        // Event for request changes
        public event EventHandler<FloorRequestEventArgs> RequestAdded;
        public event EventHandler<FloorRequestEventArgs> RequestRemoved;

        protected virtual void OnRequestAdded(FloorRequest request)
        {
            RequestAdded?.Invoke(this, new FloorRequestEventArgs(request));
        }

        protected virtual void OnRequestRemoved(FloorRequest request)
        {
            RequestRemoved?.Invoke(this, new FloorRequestEventArgs(request));
        }
    }

    public class FloorRequestEventArgs : EventArgs
    {
        public FloorRequest Request { get; }

        public FloorRequestEventArgs(FloorRequest request)
        {
            Request = request;
        }
    }
}