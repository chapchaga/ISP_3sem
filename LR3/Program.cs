class Program
{
    static void Main()
    {
        PhoneStation station = new PhoneStation();
        Journal journal = new Journal();

        station.DataChanged += journal.LogEvent;
        station.CallRegistered += (sender, e) =>
            Console.WriteLine($"  [событие] {e.ClientSurname} → {e.City}, {e.Minutes} мин., {e.Cost} руб.");

        SetupData(station, out Client ivanov);

        PrintSection("Тарифы", station.Tariffs.Select(t => $"{t.City} — {t.PriceInMinute} руб./мин"));
        PrintSection("Клиенты", station.Clients.Select(c => c.Surname));

        PrintSection("Расчёты", new[]
        {
            $"Стоимость звонков Ivanov: {station.GetClientCallCost("Ivanov")} руб.",
            $"Общая стоимость всех звонков: {station.GetTotalCallsCost()} руб.",
            $"Количество звонков в Moscow: {station.GetCallsCountByCity("Moscow")}"
        });

        Console.WriteLine("\n=== Журнал событий ===");
        journal.PrintEvents();

        station.DataChanged -= journal.LogEvent;
        station.AddTariff(new Tariff("Brest", 1.2));
        Console.WriteLine($"После отписки добавлен тариф Brest, записей в журнале: {journal.Count}");

        PrintLinqResults(station, ivanov.Surname);
    }

    static void SetupData(PhoneStation station, out Client ivanov)
    {
        Console.WriteLine("=== Ввод исходных данных ===");

        station.AddTariff(new Tariff("Moscow", 2.5));
        station.AddTariff(new Tariff("Minsk", 1.5));

        ivanov = new Client("Ivanov");
        Client petrov = new Client("Petrov");
        station.AddClient(ivanov);
        station.AddClient(petrov);

        station.RegisterCall(ivanov, new Call("Moscow", 10));
        station.RegisterCall(ivanov, new Call("Minsk", 20));
        station.RegisterCall(petrov, new Call("Moscow", 5));
    }

    static void PrintLinqResults(PhoneStation station, string surname)
    {
        var data = station.GetTariffNamesSortedByPrice().ToList();

        station.AddTariff(new Tariff("MMM", 100));

        PrintSection("Тарифы по стоимости (LINQ)", data);

        station.AddTariff(new Tariff("NNN", 10));

        PrintSection("Тарифы по стоимости (LINQ)", data);

        int threshold = 10;

        PrintSection("LINQ-запросы", new[]
        {
            $"Клиент, заплативший больше всех: {station.GetTopPayingClientName()}",
            $"Клиентов, заплативших больше {threshold} руб.: {station.GetClientsCountAbovePayment(threshold)}"
        });

        PrintSection($"Суммы {surname} по каждому тарифу",
            station.GetClientPaymentsByTariff(surname).Select(p => $"{p.Key}: {p.Value} руб."));
    }

    static void PrintSection(string title, IEnumerable<string> lines)
    {
        Console.WriteLine($"\n=== {title} ===");

        foreach (string line in lines)
        {
            Console.WriteLine($"  {line}");
        }
    }

}