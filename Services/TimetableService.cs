using MTT.Constraints;
using MTT.Timetables;
using System.Text.Json;

namespace MTT.Services
{
    public class TimetableService
    {
        private List<Constraint> constraints;

        public TimetableService()
        {
            constraints = new List<Constraint>
            {
                new MaxHoursPerWeekConstraint(30),
                new RoomCapacityConstraint()
            };
        }

        public Timetable GenerateTimetableFromJson(string jsonInput)
        {
            InputData inputData = JsonSerializer.Deserialize<InputData>(jsonInput);
            TimetableGenerator generator = new TimetableGenerator(inputData, constraints);
            return generator.Generate();
        }
    }
}
