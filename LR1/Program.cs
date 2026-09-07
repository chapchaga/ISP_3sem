class Program
{
    static void Main()
    {
        PhoneStation station = new PhoneStation();

        Tariff tariff1 = new Tariff();
        tariff1.City = "Moscow";
        tariff1.PriceInMinute = 2.5;

        Tariff tariff2 = new Tariff();
        tariff2.City = "Minsk";
        tariff2.PriceInMinute = 1.5;

        station.AddTariff(tariff1);
        station.AddTariff(tariff2);

        Console.WriteLine("Добавлены тарифы:");
        Console.WriteLine($"{tariff1.City} — {tariff1.PriceInMinute} руб./мин");
        Console.WriteLine($"{tariff2.City} — {tariff2.PriceInMinute} руб./мин");

        Client client1 = new Client();
        client1.Surname = "Ivanov";

        Client client2 = new Client();
        client2.Surname = "Petrov";

        station.AddClient(client1);
        station.AddClient(client2);

        Console.WriteLine();
        Console.WriteLine("Добавлены клиенты:");
        Console.WriteLine(client1.Surname);
        Console.WriteLine(client2.Surname);

        Call call1 = new Call();
        call1.City = "Moscow";
        call1.Minutes = 10;

        Call call2 = new Call();
        call2.City = "Minsk";
        call2.Minutes = 20;

        Call call3 = new Call();
        call3.City = "Moscow";
        call3.Minutes = 5;

        station.RegisterCall(client1, call1);
        station.RegisterCall(client1, call2);
        station.RegisterCall(client2, call3);

        Console.WriteLine();
        Console.WriteLine("Зарегистрированы звонки:");
        Console.WriteLine($"{client1.Surname} → {call1.City}, {call1.Minutes} мин.");
        Console.WriteLine($"{client1.Surname} → {call2.City}, {call2.Minutes} мин.");
        Console.WriteLine($"{client2.Surname} → {call3.City}, {call3.Minutes} мин.");

        double ivanovCost = station.GetClientCallCost("Ivanov");

        Console.WriteLine();
        Console.WriteLine("Стоимость звонков Ivanov:");
        Console.WriteLine($"{ivanovCost} руб.");

        double totalCost = station.GetTotalCallsCost();

        Console.WriteLine();
        Console.WriteLine("Общая стоимость всех звонков:");
        Console.WriteLine($"{totalCost} руб.");

        int moscowCalls = station.GetCallsCountByCity("Moscow");

        Console.WriteLine();
        Console.WriteLine("Количество звонков в Moscow:");
        Console.WriteLine(moscowCalls);

        MyCustomCollection<int> numbers = new MyCustomCollection<int>();

        numbers.Add(10);
        numbers.Add(20);
        numbers.Add(30);

        Console.WriteLine();
        Console.WriteLine("MyCustomCollection<T>:");
        Console.WriteLine($"Тип элементов: int");
        Console.WriteLine($"Количество элементов: {numbers.Count}");
        Console.WriteLine($"Элемент [0]: {numbers[0]}");
        Console.WriteLine($"Элемент [1]: {numbers[1]}");
        Console.WriteLine($"Элемент [2]: {numbers[2]}");
    }
}