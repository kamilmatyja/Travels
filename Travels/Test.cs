namespace Travels
{
    public class Test : View
    {
        public Test()
        {
            View view = new View();

            view.Clear();

            Schedule = new Schedule();
            Schedule.SetNextSchedule("Poznań", "10.01.2024", new Train("InterCityPolska", 2, 5));

            TestReserveSeats(this);

            view.Clear();

            TestCancelReserveSeat(this);
        }

        protected override uint GetReadLine(string text, uint min, uint max)
        {
            SetWriteLine($"{text} ({min}-{max}): 1");

            return 1;
        }

        public override void Clear()
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