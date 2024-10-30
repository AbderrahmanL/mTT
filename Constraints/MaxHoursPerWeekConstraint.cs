using MTT.Timetables;

namespace MTT.Constraints
{
    public class MaxHoursPerWeekConstraint : Constraint
    {
        private int maxHours;

        public MaxHoursPerWeekConstraint(int maxHours)
        {
            this.maxHours = maxHours;
        }

        public override bool Validate(ScheduleSlot slot)
        {
            return slot.Subject.HoursPerWeek <= maxHours;
        }
    }
}
