using System.Collections.Generic;
using System.Linq;
using MTT.Entities;
using MTT.Constraints;

namespace MTT.Timetables
{
    public class TimetableGenerator
    {
        private List<Class> classes;
        private List<Teacher> teachers;
        private List<Classroom> rooms;
        private List<Constraint> constraints;

        public TimetableGenerator(InputData inputData, List<Constraint> constraints)
        {
            this.classes = inputData.Classes;
            this.teachers = inputData.Teachers;
            this.rooms = inputData.Classrooms;
            this.constraints = constraints;
        }

        public Timetable Generate()
        {
            Timetable timetable = new Timetable();
            InitializeSchedule(timetable);

            foreach (var classItem in classes)
            {
                if (!AssignClass(classItem, timetable))
                {
                    Backtrack(timetable);
                }
            }

            if (!ValidateTimetable(timetable))
            {
                throw new System.Exception("Unable to generate a valid timetable.");
            }

            return timetable;
        }

        private void InitializeSchedule(Timetable timetable)
        {
            // Initialize schedules for rooms, teachers, and students
            foreach (var teacher in teachers)
            {
                timetable.InitializeTeacherSchedule(teacher);
            }

            foreach (var room in rooms)
            {
                timetable.InitializeRoomSchedule(room);
            }

            foreach (var classItem in classes)
            {
                foreach (var student in classItem.Students)
                {
                    timetable.InitializeStudentSchedule(student);
                }
            }
        }

        private bool AssignClass(Class classItem, Timetable timetable)
        {
            foreach (var timeSlot in GetAvailableTimeSlots(classItem, timetable))
            {
                if (CheckConstraints(classItem, timeSlot, timetable))
                {
                    ScheduleClass(classItem, timeSlot, timetable);
                    return true;
                }
            }
            return false;
        }

        private IEnumerable<TimeSlot> GetAvailableTimeSlots(Class classItem, Timetable timetable)
        {
            // Return available timeslots based on teacher and room availability
            return timetable.GetAvailableTimeSlotsForClass(classItem);
        }

        private bool CheckConstraints(Class classItem, TimeSlot timeSlot, Timetable timetable)
        {
            foreach (var constraint in constraints)
            {
                if (!constraint.IsSatisfied(classItem, timeSlot, timetable))
                {
                    return false;
                }
            }
            return true;
        }

        private void ScheduleClass(Class classItem, TimeSlot timeSlot, Timetable timetable)
        {
            // Update schedules for teacher, room, and students for the timeslot
            timetable.AssignClassToTimeSlot(classItem, timeSlot);
        }

        private void Backtrack(Timetable timetable)
        {
            // Logic to revert last assignments and attempt alternative configurations
            timetable.RevertLastAssignment();
        }

        private bool ValidateTimetable(Timetable timetable)
        {
            // Ensure the final timetable meets all constraints
            return timetable.ValidateAllConstraints(constraints);
        }
    }
}
