using System.Collections.Generic;
using System.Linq;
using MTT.Entities;
using MTT.Constraints;

namespace MTT.Timetables
{
    public class Timetable
    {
        private Dictionary<Class, List<TimeSlot>> classSchedules;
        private Dictionary<Teacher, List<TimeSlot>> teacherSchedules;
        private Dictionary<Classroom, List<TimeSlot>> roomSchedules;

        public Timetable()
        {
            classSchedules = new Dictionary<Class, List<TimeSlot>>();
            teacherSchedules = new Dictionary<Teacher, List<TimeSlot>>();
            roomSchedules = new Dictionary<Classroom, List<TimeSlot>>();
        }

        public void InitializeTeacherSchedule(Teacher teacher)
        {
            if (!teacherSchedules.ContainsKey(teacher))
            {
                teacherSchedules[teacher] = new List<TimeSlot>();
            }
        }

        public void InitializeRoomSchedule(Classroom room)
        {
            if (!roomSchedules.ContainsKey(room))
            {
                roomSchedules[room] = new List<TimeSlot>();
            }
        }

        public IEnumerable<TimeSlot> GetAvailableTimeSlotsForClass(Class classItem)
        {
            var teacherAvailability = teacherSchedules[classItem.teacher]
                .Where(slot => !teacherSchedules[classItem.teacher].Contains(slot));
            
            var roomAvailability = roomSchedules[classItem.room]
                .Where(slot => !roomSchedules[classItem.room].Contains(slot));
            
            return teacherAvailability.Intersect(roomAvailability);
        }

        public void AssignClassToTimeSlot(Class classItem, Teacher teacher, Classroom room, TimeSlot timeSlot)
        {
            var slot = new TimeSlot(timeSlot.Day, timeSlot.StartTime, timeSlot.EndTime);

            // Check or create list for class schedule slots
            if (!classSchedules.ContainsKey(classItem))
            {
                classSchedules[classItem] = new List<TimeSlot>();
            }
            classSchedules[classItem].Add(slot);

            // Update teacher and room schedules
            if (!teacherSchedules.ContainsKey(teacher))
            {
                teacherSchedules[teacher] = new List<TimeSlot>();
            }
            teacherSchedules[teacher].Add(timeSlot);

            if (!roomSchedules.ContainsKey(room))
            {
                roomSchedules[room] = new List<TimeSlot>();
            }
            roomSchedules[room].Add(timeSlot);
        }

        public void RevertLastAssignment()
        {
            // Logic to revert the last assigned timeslot for backtracking
            if (classSchedules.Count > 0)
            {
                var lastClass = classSchedules.LastOrDefault();
                if (lastClass.Value.Count > 0)
                {
                    var lastTimeSlot = lastClass.Value.Last();
                    classSchedules[lastClass.Key].Remove(lastTimeSlot);

                    // Revert changes for teacher and room as well
                    teacherSchedules[lastClass.Key.teacher].Remove(lastTimeSlot);
                    roomSchedules[lastClass.Key.room].Remove(lastTimeSlot);
                }
            }
        }

        public bool ValidateAllConstraints(List<Constraint> constraints)
        {
            foreach (var constraint in constraints)
            {
                foreach (var classSchedule in classSchedules)
                {
                    foreach (var timeSlot in classSchedule.Value)
                    {
                        if (!constraint.Validate(classSchedule.Key, timeSlot, this))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
