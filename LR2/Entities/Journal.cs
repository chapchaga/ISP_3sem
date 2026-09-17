public class Journal
{
    private class Entry
    {
        public DateTime Timestamp { get; }
        public string Description { get; }
        public string EntityName { get; }

        public Entry(DateTime timestamp, string description, string entityName)
        {
            Timestamp = timestamp;
            Description = description;
            EntityName = entityName;
        }

        public override string ToString()
        {
            return $"{Timestamp:dd.MM.yyyy HH:mm:ss} | {Description} | {EntityName}";
        }
    }

    private readonly MyCustomCollection<Entry> entries;

    public Journal()
    {
        entries = new MyCustomCollection<Entry>();
    }

    public int Count
    {
        get { return entries.Count; }
    }

    public void LogEvent(object? sender, StationChangedEventArgs e)
    {
        LogEvent(DescribeChange(e.ChangeType), e.EntityName);
    }

    public void LogEvent(string description, string entityName)
    {
        Entry entry = new Entry(
            DateTime.Now,
            description,
            entityName);

        entries.Add(entry);
    }

    public void PrintEvents()
    {
        Console.WriteLine($"Журнал событий (записей: {entries.Count}):");

        if (entries.Count == 0)
        {
            Console.WriteLine("  журнал пуст");
            return;
        }

        foreach (Entry entry in entries)
        {
            Console.WriteLine("  " + entry);
        }
    }

    private static string DescribeChange(StationChangeType changeType)
    {
        switch (changeType)
        {
            case StationChangeType.TariffAdded:
                return "Изменён список тарифов: добавлен тариф";

            case StationChangeType.ClientAdded:
                return "Изменён список абонентов: добавлен абонент";

            default:
                return "Данные АТС изменены";
        }
    }
}