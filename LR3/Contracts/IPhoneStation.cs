public interface IPhoneStation
{
    string? GetTopPayingClientName();
    Dictionary<string, double> GetClientPaymentsByTariff(string surname);
    int GetClientsCountAbovePayment(double threshold);
    event EventHandler<StationChangedEventArgs>? DataChanged;
    event EventHandler<CallRegisteredEventArgs>? CallRegistered;

    IEnumerable<Tariff> Tariffs { get; }
    IEnumerable<Client> Clients { get; }
    IEnumerable<string> GetTariffNamesSortedByPrice();

    void AddTariff(Tariff tariff);
    void AddClient(Client client);
    void RegisterCall(Client client, Call call);
    double GetClientCallCost(string surname);
    double GetTotalCallsCost();
    int GetCallsCountByCity(string city);
}
