using System.Collections.Generic;

namespace Travels
{
    public class Schedule
    {
        private readonly List<ScheduleEntry> _schedules = new List<ScheduleEntry>();

        public List<ScheduleEntry> GetAllSchedules()
        {
            return _schedules;
        }

        public void SetNextSchedule(string destination, string departureDate, Train train)
        {
            _schedules.Add(new ScheduleEntry
            {
                Destination = destination,
                Departure = departureDate,
                Train = train
            });
        }
    }
}