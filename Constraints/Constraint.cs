using MTT.Timetables;

namespace MTT.Constraints
{
    public abstract class Constraint
    {
        public abstract bool Validate(ScheduleSlot slot);
    }
}
