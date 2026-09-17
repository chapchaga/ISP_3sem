public enum StationChangeType
{
    TariffAdded,
    ClientAdded
}

public class StationChangedEventArgs : EventArgs
{
    public StationChangeType ChangeType { get; }
    public string EntityName { get; }

    public StationChangedEventArgs(StationChangeType changeType, string entityName)
    {
        ChangeType = changeType;
        EntityName = entityName;
    }
}

public class CallRegisteredEventArgs : EventArgs
{
    public string ClientSurname { get; }
    public string City { get; }
    public int Minutes { get; }
    public double Cost { get; }

    public CallRegisteredEventArgs(string clientSurname, string city, int minutes, double cost)
    {
        ClientSurname = clientSurname;
        City = city;
        Minutes = minutes;
        Cost = cost;
    }
}
