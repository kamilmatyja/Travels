using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Travels
{
    public class View
    {
        private List<ScheduleEntry> Schedules { get; set; }

        public View(List<ScheduleEntry> schedules)
        {
            Schedules = schedules;

            while (true)
            {
                ShowState();

                switch (GetReadLine(1, 6))
                {
                    case 1:
                        ShowReserveASeat();
                        break;
                    case 2:
                        ShowReserveSeats();
                        break;
                    case 3:
                        ShowTrainTimetable();
                        break;
                    case 4:
                        ShowCancelReserveSeat();
                        break;
                    case 5:
                        new Test();
                        break;
                    case 6:
                        return;
                }
            }
        }

        private void ShowState()
        {
            Console.Clear();

            Console.WriteLine("=== MENU ===");
            Console.WriteLine("1. Zarezerwuj miejsce");
            Console.WriteLine("2. Zarezerwuj kilka miejsc");
            Console.WriteLine("3. Sprawdź dostępność miejsc");
            Console.WriteLine("4. Anuluj rezerwację");
            Console.WriteLine("5. Run Tests");
            Console.WriteLine("6. Zakończ program");
            Console.Write("Wybierz opcję (1-6): ");
        }

        private void ShowTrainCar(Train train)
        {
            Console.Clear();

            Console.WriteLine("=== UKŁAD POCIĄGU ===");

            Console.Write("   ");
            for (uint column = 0; column < train.TrainColumns; column++)
            {
                Console.Write($"{column + 1:D2} ");
            }

            Console.WriteLine();

            for (uint row = 0; row < train.TrainRows; row++)
            {
                Console.Write($"{row + 1:D2} ");
                for (uint column = 0; column < train.TrainColumns; column++)
                {
                    Console.Write($" {train.GetSeatReservation(column, row)} ");
                }

                Console.WriteLine();
            }
        }

        private void ShowTrainDestinations(string[] uniqueDestinations)
        {
            Console.Clear();

            Console.WriteLine("=== MIEJSCA DOCELOWE ===");
            for (uint destination = 0; destination < uniqueDestinations.Length; destination++)
            {
                Console.WriteLine($"{destination + 1}. {uniqueDestinations[destination]}");
            }
        }

        private void ShowTrainDepartures(string[] uniqueDepartures)
        {
            Console.Clear();

            Console.WriteLine("=== DATY ODJAZDÓW ===");
            for (uint departure = 0; departure < uniqueDepartures.Length; departure++)
            {
                Console.WriteLine($"{departure + 1}. {uniqueDepartures[departure]}");
            }
        }

        private void ShowReserveASeat()
        {
            string[] uniqueDestinations = GetUniqueDestinations();
            uint destination = GetTrainDestination(uniqueDestinations);
            string[] uniqueDepartures = GetUniqueDepartures(uniqueDestinations[destination]);
            uint departure = GetTrainDeparture(uniqueDepartures);

            Train train = GetTrainForDestinationAndDeparture(uniqueDestinations[destination],
                uniqueDepartures[departure]);

            ShowTrainCar(train);

            uint column = GetTrainColumn(train);
            uint row = GetTrainRow(train);

            while (!train.ReserveASeat(column, row))
            {
                Console.WriteLine(
                    $"Miejsce w kolumnie {column + 1} rzędzie {row + 1} jest już zajęte, wybierz inne miejsce.");

                column = GetTrainColumn(train);
                row = GetTrainRow(train);
            }

            Console.WriteLine($"Miejsce w kolumnie {column + 1} rzędzie {row + 1} zostało zarezerwowane pomyślnie.");

            WaitForReady();
        }

        private void ShowReserveSeats()
        {
            string[] uniqueDestinations = GetUniqueDestinations();
            uint destination = GetTrainDestination(uniqueDestinations);
            string[] uniqueDepartures = GetUniqueDepartures(uniqueDestinations[destination]);
            uint departure = GetTrainDeparture(uniqueDepartures);

            Train train = GetTrainForDestinationAndDeparture(uniqueDestinations[destination],
                uniqueDepartures[departure]);

            ShowTrainCar(train);

            uint countReservationSeats = GetSumOfReservationSeats(train);
            uint[] columns = new uint[countReservationSeats];
            uint[] rows = new uint[countReservationSeats];

            for (uint seat = 0; seat < countReservationSeats; seat++)
            {
                Console.WriteLine($"Miejsce {seat + 1}: ");

                columns[seat] = GetTrainColumn(train);
                rows[seat] = GetTrainRow(train);
            }

            while (!train.ReserveSeats(columns, rows))
            {
                Console.WriteLine("Przynajmniej jedno z wybranych miejsc jest już zajęte, wybierz inne miejsca.");

                for (uint seat = 0; seat < countReservationSeats; seat++)
                {
                    Console.WriteLine($"Miejsce {seat + 1}: ");

                    columns[seat] = GetTrainColumn(train);
                    rows[seat] = GetTrainRow(train);
                }
            }

            Console.WriteLine("Wszystkie miejsca zarezerwowane pomyślnie.");

            WaitForReady();
        }

        private void ShowTrainTimetable()
        {
            string[] uniqueDestinations = GetUniqueDestinations();
            uint destination = GetTrainDestination(uniqueDestinations);
            string[] uniqueDepartures = GetUniqueDepartures(uniqueDestinations[destination]);
            uint departure = GetTrainDeparture(uniqueDepartures);

            Train train = GetTrainForDestinationAndDeparture(uniqueDestinations[destination],
                uniqueDepartures[departure]);

            ShowTrainCar(train);

            WaitForReady();
        }

        private void ShowCancelReserveSeat()
        {
            string[] uniqueDestinations = GetUniqueDestinations();
            uint destination = GetTrainDestination(uniqueDestinations);
            string[] uniqueDepartures = GetUniqueDepartures(uniqueDestinations[destination]);
            uint departure = GetTrainDeparture(uniqueDepartures);

            Train train = GetTrainForDestinationAndDeparture(uniqueDestinations[destination],
                uniqueDepartures[departure]);

            ShowTrainCar(train);

            uint column = GetTrainColumn(train);
            uint row = GetTrainRow(train);

            while (!train.CancelReserveSeat(column, row))
            {
                Console.WriteLine(
                    $"Miejsce w kolumnie {column + 1} rzędzie {row + 1} jest wolne, wybierz zarezerwowane miejsce.");

                column = GetTrainColumn(train);
                row = GetTrainRow(train);
            }

            Console.WriteLine(
                $"Miejsce w kolumnie {column + 1} rzędzie {row + 1} zostało zwolnione z rezerwacji pomyślnie.");

            WaitForReady();
        }

        private static uint GetReadLine(uint min, uint max)
        {
            uint userInput;
            bool isValidInput;

            do
            {
                string input = Console.ReadLine();

                isValidInput = uint.TryParse(input, out userInput);

                if (!isValidInput || userInput < min || userInput > max)
                {
                    Console.Write($"Błędne dane, podaj liczbę całkowitą ({min}-{max}): ");
                }
            } while (!isValidInput || userInput < min || userInput > max);

            return userInput;
        }

        private void WaitForReady()
        {
            Console.Write("Naciśnij dowolny klawisz, aby kontynuować: ");
            Console.ReadKey();
        }

        private uint GetTrainDestination(string[] uniqueDestinations)
        {
            ShowTrainDestinations(uniqueDestinations);
            Console.Write($"Wybierz miejsce docelowe (1-{uniqueDestinations.Length}): ");
            return GetReadLine(1, (uint)uniqueDestinations.Length) - 1;
        }

        private uint GetTrainDeparture(string[] uniqueDepartures)
        {
            ShowTrainDepartures(uniqueDepartures);
            Console.Write($"Wybierz datę pociągu (1-{uniqueDepartures.Length}): ");
            return GetReadLine(1, (uint)uniqueDepartures.Length) - 1;
        }

        private uint GetTrainColumn(Train train)
        {
            Console.Write($"Podaj numer kolumny (1-{train.TrainColumns}): ");
            return GetReadLine(1, train.TrainColumns) - 1;
        }

        private uint GetTrainRow(Train train)
        {
            Console.Write($"Podaj numer rzędu (1-{train.TrainRows}): ");
            return GetReadLine(1, train.TrainRows) - 1;
        }

        private uint GetSumOfReservationSeats(Train train)
        {
            Console.Write("Podaj liczbę miejsc do zarezerwowania: ");
            return GetReadLine(1, train.TotalFreeSeats());
        }

        private string[] GetUniqueDestinations()
        {
            return Schedules
                .Select(entry => entry.Destination)
                .Distinct()
                .OrderBy(destination => destination)
                .ToArray();
        }

        private string[] GetUniqueDepartures(string destination)
        {
            return Schedules
                .Where(entry => entry.Destination == destination)
                .Select(entry => entry.Departure)
                .Distinct()
                .OrderBy(departureDate => DateTime.ParseExact(departureDate, "dd.MM.yyyy", CultureInfo.InvariantCulture))
                .ToArray();
        }

        private Train GetTrainForDestinationAndDeparture(string destination, string departure)
        {
            ScheduleEntry entry =
                Schedules.FirstOrDefault(e => e.Destination == destination && e.Departure == departure);

            return entry?.Train;
        }
    }
}