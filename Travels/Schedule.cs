using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Travels
{
    public class Schedule
    {
        private readonly List<ScheduleEntry> _schedules = new List<ScheduleEntry>();

        public void SetNextSchedule(string destination, string departureDate, Train train)
        {
            _schedules.Add(new ScheduleEntry
            {
                Destination = destination,
                Departure = departureDate,
                Train = train
            });
        }

        public string[] GetUniqueDestinations()
        {
            return _schedules
                .Select(entry => entry.Destination)
                .Distinct()
                .OrderBy(destination => destination)
                .ToArray();
        }

        public string[] GetUniqueDepartures(string destination)
        {
            return _schedules
                .Where(entry => entry.Destination == destination)
                .Select(entry => entry.Departure)
                .Distinct()
                .OrderBy(
                    departureDate => DateTime.ParseExact(departureDate, "dd.MM.yyyy", CultureInfo.InvariantCulture))
                .ToArray();
        }

        public Train GetTrainForDestinationAndDeparture(string destination, string departure)
        {
            ScheduleEntry entry =
                _schedules.FirstOrDefault(e => e.Destination == destination && e.Departure == departure);

            return entry?.Train;
        }
    }
}