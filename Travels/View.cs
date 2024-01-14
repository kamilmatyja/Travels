using System;

namespace Travels
{
    public class View
    {
        public Schedule Schedule { get; set; }
        private string CurrentDestination { get; set; }
        private string CurrentDeparture { get; set; }
        private Train CurrentTrain { get; set; }

        public void ShowInitialize()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            while (true)
            {
                switch (ShowState())
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

        private uint ShowState()
        {
            SetHeader("MENU");
            SetWriteLine("1. Zarezerwuj miejsce");
            SetWriteLine("2. Zarezerwuj kilka miejsc");
            SetWriteLine("3. Sprawdź dostępność miejsc");
            SetWriteLine("4. Anuluj rezerwację");
            SetWriteLine("5. Run Tests");
            SetWriteLine("6. Zakończ program");
            return GetReadLine("Wybierz opcję", 1, 6);
        }

        private void ShowTimetable()
        {
            string[] uniqueDestinations = Schedule.GetUniqueDestinations();
            uint destination = GetTrainDestination(uniqueDestinations);
            CurrentDestination = uniqueDestinations[destination];

            string[] uniqueDepartures = Schedule.GetUniqueDepartures(CurrentDestination);
            uint departure = GetTrainDeparture(uniqueDepartures);
            CurrentDeparture = uniqueDepartures[departure];

            CurrentTrain = Schedule.GetTrainForDestinationAndDeparture(CurrentDestination, CurrentDeparture);

            ShowTrainCar(CurrentDestination, CurrentDeparture, CurrentTrain);
        }

        private void ShowTrainCar(string destination, string departure, Train train)
        {
            SetHeader("UKŁAD POCIĄGU");
            SetWriteLine($"Pociąg: {train.TrainName}, miejsce docelowe: {destination}, data odjazdu: {departure}.");

            Console.Write("   ");
            for (uint column = 0; column < train.TrainColumns; column++)
            {
                Console.Write($"{column + 1:D2} ");
            }

            SetWriteLine("");

            for (uint row = 0; row < train.TrainRows; row++)
            {
                Console.Write($"{row + 1:D2} ");
                for (uint column = 0; column < train.TrainColumns; column++)
                {
                    Console.Write($" {train.GetSeatReservation(column, row)} ");
                }

                SetWriteLine("");
            }
        }

        private void ShowTrainDestinations(string[] uniqueDestinations)
        {
            SetHeader("MIEJSCA DOCELOWE");
            for (uint destination = 0; destination < uniqueDestinations.Length; destination++)
            {
                SetWriteLine($"{destination + 1}. {uniqueDestinations[destination]}");
            }
        }

        private void ShowTrainDepartures(string[] uniqueDepartures)
        {
            SetHeader("DATY ODJAZDÓW");
            for (uint departure = 0; departure < uniqueDepartures.Length; departure++)
            {
                SetWriteLine($"{departure + 1}. {uniqueDepartures[departure]}");
            }
        }

        private void ShowReserveASeat()
        {
            ShowTimetable();

            uint column = GetTrainColumn(CurrentTrain);
            uint row = GetTrainRow(CurrentTrain);

            while (!CurrentTrain.ReserveASeat(column, row))
            {
                SetWriteLine(
                    $"Miejsce w kolumnie {column + 1}, rzędzie {row + 1} jest już zajęte, wybierz inne miejsce.");

                column = GetTrainColumn(CurrentTrain);
                row = GetTrainRow(CurrentTrain);
            }

            ShowTrainCar(CurrentDestination, CurrentDeparture, CurrentTrain);

            SetWriteLine($"Miejsce w kolumnie {column + 1}, rzędzie {row + 1} zostało zarezerwowane pomyślnie.");

            WaitForReady();
        }

        protected void ShowReserveSeats()
        {
            ShowTimetable();

            uint countReservationSeats = GetSumOfReservationSeats(CurrentTrain);
            uint[] columns = new uint[countReservationSeats];
            uint[] rows = new uint[countReservationSeats];

            for (uint seat = 0; seat < countReservationSeats; seat++)
            {
                SetWriteLine($"Miejsce {seat + 1}: ");

                columns[seat] = GetTrainColumn(CurrentTrain);
                rows[seat] = GetTrainRow(CurrentTrain);
            }

            while (!CurrentTrain.ReserveSeats(columns, rows))
            {
                SetWriteLine("Przynajmniej jedno z wybranych miejsc jest już zajęte, wybierz inne miejsca.");

                for (uint seat = 0; seat < countReservationSeats; seat++)
                {
                    SetWriteLine($"Miejsce {seat + 1}: ");

                    columns[seat] = GetTrainColumn(CurrentTrain);
                    rows[seat] = GetTrainRow(CurrentTrain);
                }
            }

            ShowTrainCar(CurrentDestination, CurrentDeparture, CurrentTrain);

            SetWriteLine("Wszystkie miejsca zarezerwowane pomyślnie.");

            WaitForReady();
        }

        private void ShowTrainTimetable()
        {
            ShowTimetable();

            WaitForReady();
        }

        protected void ShowCancelReserveSeat()
        {
            ShowTimetable();

            uint column = GetTrainColumn(CurrentTrain);
            uint row = GetTrainRow(CurrentTrain);

            while (!CurrentTrain.CancelReserveSeat(column, row))
            {
                SetWriteLine(
                    $"Miejsce w kolumnie {column + 1}, rzędzie {row + 1} jest wolne, wybierz zarezerwowane miejsce.");

                column = GetTrainColumn(CurrentTrain);
                row = GetTrainRow(CurrentTrain);
            }

            ShowTrainCar(CurrentDestination, CurrentDeparture, CurrentTrain);

            SetWriteLine(
                $"Miejsce w kolumnie {column + 1}, rzędzie {row + 1} zostało zwolnione z rezerwacji pomyślnie.");

            WaitForReady();
        }

        protected virtual uint GetReadLine(string text, uint min, uint max)
        {
            uint userInput;
            bool isValidInput;

            Console.Write($"{text} ({min}-{max}): ");

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

        protected void WaitForReady()
        {
            Console.Write("Naciśnij dowolny klawisz, aby kontynuować: ");
            Console.ReadKey();
        }

        protected virtual void Clear()
        {
            Console.Clear();
        }

        protected void SetWriteLine(string text)
        {
            Console.WriteLine(text);
        }

        private void SetHeader(string text)
        {
            Clear();

            SetWriteLine($"=== {text} ===");
        }

        private uint GetTrainDestination(string[] uniqueDestinations)
        {
            ShowTrainDestinations(uniqueDestinations);
            return GetReadLine("Wybierz miejsce docelowe", 1, (uint)uniqueDestinations.Length) - 1;
        }

        private uint GetTrainDeparture(string[] uniqueDepartures)
        {
            ShowTrainDepartures(uniqueDepartures);
            return GetReadLine("Wybierz datę pociągu", 1, (uint)uniqueDepartures.Length) - 1;
        }

        private uint GetTrainColumn(Train train)
        {
            return GetReadLine("Podaj numer kolumny", 1, train.TrainColumns) - 1;
        }

        private uint GetTrainRow(Train train)
        {
            return GetReadLine("Podaj numer rzędu", 1, train.TrainRows) - 1;
        }

        private uint GetSumOfReservationSeats(Train train)
        {
            return GetReadLine("Podaj liczbę miejsc do zarezerwowania", 1, train.TotalFreeSeats());
        }
    }
}