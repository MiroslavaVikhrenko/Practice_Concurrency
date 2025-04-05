namespace Task2_20250405
{
    /*
     Создайте базу данных хранящую информацию о «Спортивных мероприятиях». 
    Используя обычные и скомпилированные запросы, выполните следующие действия:

1) Создайте новое спортивное мероприятие с указанием названия, даты, места проведения и других деталей.
2) Обновите информацию о мероприятии, включая название, дату, место проведения и другие детали.
3) Удалите выбранное мероприятие из базы данных.
4) Отобразите информацию о каждом мероприятии, включая его название, дату, место проведения и другие детали.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                SeedDatabase(db);

                // 1) Создайте новое спортивное мероприятие с указанием названия, даты, места проведения и других деталей.
            }
        }

        // 1) Создайте новое спортивное мероприятие с указанием названия, даты, места проведения и других деталей.
        public static void SeedDatabase(ApplicationContext db)
        {
            db.SportEvents.AddRange(new List<SportEvent>
            {
                new() { Name = "24 hour run", Date = DateTime.Now.AddDays(+20), Venue = "Calgary" },
                new() { Name = "60K run", Date = DateTime.Now.AddDays(+40), Venue = "Calgary" },
                new() { Name = "12 hour run", Date = DateTime.Now.AddDays(+70), Venue = "Edmonton" },
                new() { Name = "Edmonton Marathon", Date = DateTime.Now.AddDays(+90), Venue = "Edmonton" },
                new() { Name = "Spartan", Date = DateTime.Now.AddDays(+120), Venue = "Red Deer" },
            });
            db.SaveChanges();
        }
    }
}
