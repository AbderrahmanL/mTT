using MTT.Entities;

namespace MTT.Timetables
{
    public class ScheduleSlot
    {
        public Class Class { get; set; }
        public Subject Subject { get; set; }
        public Teacher Teacher { get; set; }
        public Classroom Room { get; set; }
        public TimeSlot TimeSlot { get; set; }

        public ScheduleSlot(Class classItem, Subject subject, Teacher teacher, Classroom room, TimeSlot timeSlot)
        {
            Class = classItem;
            Subject = subject;
            Teacher = teacher;
            Room = room;
            TimeSlot = timeSlot;
        }
    }
}
