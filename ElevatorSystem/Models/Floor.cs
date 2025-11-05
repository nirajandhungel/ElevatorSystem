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
        public bool IsServiced { get; set; }

        public Floor(int number)
        {
            Number = number;
            HasUpRequest = false;
            HasDownRequest = false;
            DisplayName = number == 0 ? "G" : number.ToString();
            RequestTimes = new List<DateTime>();
            IsServiced = true;
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

        public string GetWaitTimeDisplay()
        {
            var waitTime = GetWaitTime();
            if (waitTime.TotalSeconds < 60)
                return $"{waitTime.Seconds}s";
            else
                return $"{waitTime.Minutes}m {waitTime.Seconds}s";
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }

    public class FloorRequest
    {
        public int FloorNumber { get; }
        public string Direction { get; }
        public DateTime RequestTime { get; }
        public bool IsProcessed { get; set; }

        public FloorRequest(int floorNumber, string direction)
        {
            FloorNumber = floorNumber;
            Direction = direction;
            RequestTime = DateTime.Now;
            IsProcessed = false;
        }

        public TimeSpan GetWaitTime()
        {
            return DateTime.Now - RequestTime;
        }
    }
}