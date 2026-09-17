class Program
{
    static void Main()
    {
        PhoneStation station = new PhoneStation();
        Journal journal = new Journal();

        station.DataChanged += journal.LogEvent;

        station.CallRegistered += (sender, e) =>
        {
            Console.WriteLine($"  [событие] {e.ClientSurname} → {e.City}, {e.Minutes} мин., {e.Cost} руб.");
        };

        Console.WriteLine("=== Ввод тарифов ===");

        station.AddTariff(new Tariff("Moscow", 2.5));
        station.AddTariff(new Tariff("Minsk", 1.5));

        foreach (Tariff tariff in station.Tariffs)
        {
            Console.WriteLine($"  {tariff.City} — {tariff.PriceInMinute} руб./мин");
        }

        Console.WriteLine();
        Console.WriteLine("=== Ввод клиентов ===");

        Client ivanov = new Client("Ivanov");
        Client petrov = new Client("Petrov");

        station.AddClient(ivanov);
        station.AddClient(petrov);

        foreach (Client client in station.Clients)
        {
            Console.WriteLine($"  {client.Surname}");
        }

        Console.WriteLine();
        Console.WriteLine("=== Регистрация звонков ===");

        station.RegisterCall(ivanov, new Call("Moscow", 10));
        station.RegisterCall(ivanov, new Call("Minsk", 20));
        station.RegisterCall(petrov, new Call("Moscow", 5));

        Console.WriteLine();
        Console.WriteLine("=== Звонки по абонентам ===");

        foreach (Client client in station.Clients)
        {
            Console.WriteLine($"  {client.Surname}:");

            foreach (Call call in client.Calls)
            {
                Console.WriteLine($"    {call.City}, {call.Minutes} мин. — {call.GetTotal()} руб.");
            }
        }

        Console.WriteLine();
        Console.WriteLine("=== Расчёты ===");

        Console.WriteLine($"Стоимость звонков Ivanov: {station.GetClientCallCost("Ivanov")} руб.");
        Console.WriteLine($"Общая стоимость всех звонков: {station.GetTotalCallsCost()} руб.");
        Console.WriteLine($"Количество звонков в Moscow: {station.GetCallsCountByCity("Moscow")}");

        Console.WriteLine();
        Console.WriteLine("=== Журнал событий ===");

        journal.PrintEvents();

        station.DataChanged -= journal.LogEvent;

        station.AddTariff(new Tariff("Brest", 1.2));

        Console.WriteLine($"После отписки добавлен тариф Brest, записей в журнале: {journal.Count}");

        DemonstrateExceptions();
    }

    static void DemonstrateExceptions()
    {
        Console.WriteLine();
        Console.WriteLine("=== Обработка исключений коллекции ===");

        MyCustomCollection<int> numbers = new MyCustomCollection<int>();

        numbers.Add(10);
        numbers.Add(20);
        numbers.Add(30);

        Console.Write("Элементы коллекции:");

        foreach (int number in numbers)
        {
            Console.Write($" {number}");
        }

        Console.WriteLine($" (всего: {numbers.Count})");

        try
        {
            Console.WriteLine(numbers[5]);
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine($"  IndexOutOfRangeException: {ex.Message}");
        }

        try
        {
            numbers[-1] = 0;
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine($"  IndexOutOfRangeException: {ex.Message}");
        }

        try
        {
            numbers.Remove(99);
        }
        catch (ItemNotFoundException ex)
        {
            Console.WriteLine($"  ItemNotFoundException: {ex.Message}");
        }

        try
        {
            numbers.Remove(20);
            Console.WriteLine($"  Элемент 20 удалён, осталось: {numbers.Count}");
        }
        catch (ItemNotFoundException ex)
        {
            Console.WriteLine($"  ItemNotFoundException: {ex.Message}");
        }

        try
        {
            MyCustomCollection<int> empty = new MyCustomCollection<int>();
            empty.Current();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"  InvalidOperationException: {ex.Message}");
        }
    }
}