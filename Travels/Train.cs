using System.Collections.Generic;

namespace Travels
{
    public class Train
    {
        public string TrainName { get; set; }
        public uint TrainColumns { get; set; }
        public uint TrainRows { get; set; }
        private List<List<char>> TrainSeats { get; set; }
        private readonly char _freeSeat = 'o';
        private readonly char _takenSeat = 'x';

        public Train(string name, uint columns, uint rows)
        {
            TrainName = name;
            TrainColumns = columns;
            TrainRows = rows;
            TrainSeats = new List<List<char>>();

            for (uint column = 0; column < TrainColumns; column++)
            {
                List<char> rowSeats = new List<char>();
                for (uint row = 0; row < TrainRows; row++)
                {
                    rowSeats.Add(_freeSeat);
                }

                TrainSeats.Add(rowSeats);
            }
        }

        public char GetSeatReservation(uint column, uint row)
        {
            return TrainSeats[(int)column][(int)row];
        }

        private void SetSeatReservation(uint column, uint row, bool reserve)
        {
            TrainSeats[(int)column][(int)row] = reserve ? _takenSeat : _freeSeat;
        }

        private bool UpdateReserveSeat(uint column, uint row, bool reserve)
        {
            bool isFree = IsSeatFree(column, row);

            if ((isFree && reserve) || (!isFree && !reserve))
            {
                SetSeatReservation(column, row, reserve);

                return true;
            }

            return false;
        }

        private bool IsSeatFree(uint column, uint row)
        {
            return GetSeatReservation(column, row) == _freeSeat;
        }

        public uint TotalFreeSeats()
        {
            uint count = 0;

            for (uint column = 0; column < TrainColumns; column++)
            {
                for (uint row = 0; row < TrainRows; row++)
                {
                    if (IsSeatFree(column, row))
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public bool ReserveASeat(uint column, uint row)
        {
            return UpdateReserveSeat(column, row, true);
        }

        public bool ReserveSeats(uint[] columns, uint[] rows)
        {
            bool allIsFree = true;
            uint reservation;

            for (reservation = 0; reservation < columns.Length; reservation++)
            {
                if (!ReserveASeat(columns[reservation], rows[reservation]))
                {
                    allIsFree = false;

                    break;
                }
            }

            if (!allIsFree)
            {
                for (uint cancelReservation = 0; cancelReservation < reservation; cancelReservation++)
                {
                    CancelReserveSeat(columns[cancelReservation], rows[cancelReservation]);
                }
            }

            return allIsFree;
        }

        public bool CancelReserveSeat(uint column, uint row)
        {
            return UpdateReserveSeat(column, row, false);
        }
    }
}