public interface IPhoneStation
{
    event EventHandler<StationChangedEventArgs>? DataChanged;
    event EventHandler<CallRegisteredEventArgs>? CallRegistered;

    IEnumerable<Tariff> Tariffs { get; }
    IEnumerable<Client> Clients { get; }

    void AddTariff(Tariff tariff);
    void AddClient(Client client);
    void RegisterCall(Client client, Call call);
    double GetClientCallCost(string surname);
    double GetTotalCallsCost();
    int GetCallsCountByCity(string city);
}
