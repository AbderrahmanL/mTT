using MTT.Timetables;
using MTT.Entities;

namespace MTT.Constraints
{
    public class RoomCapacityConstraint : Constraint
    {
        public override bool Validate(ScheduleSlot slot)
        {
            return slot.Room.Capacity >= slot.Room.Capacity;
        }
    }
}
