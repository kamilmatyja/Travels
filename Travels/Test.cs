namespace Travels
{
    public class Test
    {
    }
}

// using System;
// 
// namespace Project
// {
//     public class Test
//     {
//         private static readonly uint Destination = 1;
//         private static readonly uint Departure = 2;
//         private static readonly uint CountReservationSeats = 4;
//         private static readonly uint[] Columns = new uint[CountReservationSeats];
//         private static readonly uint[] Rows = new uint[CountReservationSeats];
//         
//         public Test(Initialize initialize)
//         {
//             Console.Clear();
//             
//             Console.WriteLine("Rozpoczynanie testów...");
//             
//             TestReserveSeats(initialize);
//             TestCancelReserveSeat(initialize);
//             
//             Console.WriteLine("Testy zakończone.");
// 
//             initialize.WaitForReady();
//         }
//         
//         private static void TestReserveSeats(Initialize initialize)
//         {
//             for (uint seat = 0; seat < CountReservationSeats; seat++)
//             {
//                 Columns[seat] = seat;
//                 Rows[seat] = seat + 2;
//             }
// 
//             if (!initialize.ReserveSeats(Destination, Departure, Columns, Rows))
//             {
//                 Console.WriteLine("Błąd podczas rezerwowania!");
//             }
//             
//             if (initialize.TotalFreeSeats(Destination, Departure) != (40 - CountReservationSeats))
//             {
//                 Console.WriteLine("Błędna liczba zarezerwowanych miejsc!");
//             }
//             
//             for (uint seat = 0; seat < CountReservationSeats; seat++)
//             {
//                 if (initialize.IsSeatFree(Destination, Departure, Columns[seat], Rows[seat]))
//                 {
//                     Console.WriteLine("Błąd, miejsce wolne, a powinno być zarezerwowane!");
// 
//                     break;
//                 }
//             }
//         }
// 
//         private static void TestCancelReserveSeat(Initialize initialize)
//         {
//             for (uint seat = 0; seat < CountReservationSeats; seat++)
//             {
//                 if (!initialize.CancelReserveSeat(Destination, Departure, Columns[seat], Rows[seat]))
//                 {
//                     Console.WriteLine("Błęd podczas anulowania rezerwacji!");
// 
//                     break;
//                 }
//             }
//         }
//     }
// }