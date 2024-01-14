using System;

namespace Travels
{
    public class Test : View
    {
        public Test()
        {
            Console.Clear();

            Schedule = new Schedule();
            Schedule.SetNextSchedule("Poznań", "10.01.2024", new Train("InterCityPolska", 2, 5));

            TestReserveSeats(this);

            Console.Clear();

            TestCancelReserveSeat(this);
        }

        protected override uint GetReadLine(string text, uint min, uint max)
        {
            Random random = new Random();
            uint next = (uint)random.Next((int)min, (int)max + 1);

            SetWriteLine($"{text} ({min}-{max}): 1");

            return 1;
        }

        protected override void Clear()
        {
        }

        private static void TestReserveSeats(Test view)
        {
            view.SetWriteLine("Test: rezerwacja miejsca...");

            view.ShowReserveSeats();
        }

        private static void TestCancelReserveSeat(Test view)
        {
            view.SetWriteLine("Test: anulowanie rezerwacji...");

            view.ShowCancelReserveSeat();
        }
    }
}