using System;

namespace MTT.Timetables
{
    public class TimeSlot
    {
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public TimeSlot(DayOfWeek day, TimeSpan startTime, TimeSpan endTime)
        {
            Day = day;
            StartTime = startTime;
            EndTime = endTime;
        }

        public bool OverlapsWith(TimeSlot other)
        {
            return Day == other.Day && StartTime < other.EndTime && EndTime > other.StartTime;
        }

        public override bool Equals(object obj)
        {
            if (obj is TimeSlot other)
            {
                return Day == other.Day && StartTime == other.StartTime && EndTime == other.EndTime;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Day, StartTime, EndTime);
        }

        public override string ToString()
        {
            return $"{Day} {StartTime} - {EndTime}";
        }
    }
}
