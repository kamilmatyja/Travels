namespace Travels
{
    public class Timetable
    {
        public Timetable()
        {
            Schedule schedules = new Schedule();
            schedules.SetNextSchedule("Poznań", "10.01.2024", new Train("InterCityPolska", 4, 10));
            schedules.SetNextSchedule("Poznań", "15.01.2024", new Train("ExpressPolski", 3, 8));
            schedules.SetNextSchedule("Poznań", "20.01.2024", new Train("SzybkiMazur", 2, 12));
            schedules.SetNextSchedule("Poznań", "25.01.2024", new Train("PolRegioEkspress", 1, 10));
            schedules.SetNextSchedule("Warszawa", "10.01.2024", new Train("WarmiaSprinter", 6, 11));
            schedules.SetNextSchedule("Warszawa", "15.01.2024", new Train("GórskiEkspres", 4, 9));
            schedules.SetNextSchedule("Warszawa", "20.01.2024", new Train("BalticSeaExpress", 5, 10));
            schedules.SetNextSchedule("Wrocław", "10.01.2024", new Train("KarpatyExpress", 2, 8));
            schedules.SetNextSchedule("Wrocław", "20.01.2024", new Train("WisłaRapide", 3, 12));
            schedules.SetNextSchedule("Kraków", "10.01.2024", new Train("KujawyExpress", 3, 10));
            schedules.SetNextSchedule("Kraków", "25.01.2024", new Train("MalopolskiExpress", 2, 8));
            schedules.SetNextSchedule("Gdańsk", "15.01.2024", new Train("OdraEkspres", 6, 12));
            schedules.SetNextSchedule("Gdańsk", "20.01.2024", new Train("WartaSprinter", 5, 10));
            schedules.SetNextSchedule("Łódź", "05.01.2024", new Train("PomeraniaExpress", 4, 9));
            schedules.SetNextSchedule("Łódź", "30.01.2024", new Train("KarpatyRapide", 3, 11));
            schedules.SetNextSchedule("Szczecin", "05.01.2024", new Train("BieszczadyExpress", 2, 10));
            schedules.SetNextSchedule("Szczecin", "30.01.2024", new Train("PodlasieSprinter", 4, 10));

            new View(schedules.GetAllSchedules());
        }
    }
}