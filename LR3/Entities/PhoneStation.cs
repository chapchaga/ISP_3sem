using System.Numerics;
using System.Linq;
public class PhoneStation : IPhoneStation
{
    private readonly Dictionary<string ,Tariff> tariffs;
    private readonly List<Client> clients;

    public event EventHandler<StationChangedEventArgs>? DataChanged;
    public event EventHandler<CallRegisteredEventArgs>? CallRegistered;

    public PhoneStation()
    {
        tariffs = new Dictionary<string ,Tariff>();
        clients = new List<Client>();
    }

    public IEnumerable<Tariff> Tariffs
    {
        get { return tariffs.Values; }
    }

    public IEnumerable<Client> Clients
    {
        get { return clients; }
    }

    public void AddTariff(Tariff tariff)
    {
        tariffs.Add(tariff.City, tariff);
        OnDataChanged(new StationChangedEventArgs(StationChangeType.TariffAdded, tariff.City));
    }

    public void AddClient(Client client)
    {
        clients.Add(client);
        OnDataChanged(new StationChangedEventArgs(StationChangeType.ClientAdded, client.Surname));
    }

    public void RegisterCall(Client client, Call call)
    {
        if (!tariffs.TryGetValue(call.City, out Tariff? tariff))
        {
            throw new InvalidOperationException($"Тариф для города {call.City} не задан, звонок зарегистрировать нельзя");
        }
        call.Tariff = tariff;
        client.Calls.Add(call);

        OnCallRegistered(new CallRegisteredEventArgs(
            client.Surname, call.City, call.Minutes, call.GetTotal()));
    }

    public double GetClientCallCost(string surname)
    {
        return clients
            .Where(c => c.Surname == surname)
            .SelectMany(c => c.Calls)
            .Sum(call => call.GetTotal());
    }

    public double GetTotalCallsCost()
    {
        return clients
            .SelectMany(c => c.Calls)
            .Sum(call => call.GetTotal());
    }

    public int GetCallsCountByCity(string city)
    {
        int count = Zero<int>();

        for (int i = 0; i < clients.Count; i++)
        {
            Client client = clients[i];

            for (int j = 0; j < client.Calls.Count; j++)
            {
                if (client.Calls[j].City == city)
                {
                    count = Add(count, 1);
                }
            }
        }

        return count;
    }

    private static T Add<T>(T left, T right)
        where T : IAdditionOperators<T, T, T>
    {
        return left + right;
    }

    private static T Zero<T>()
        where T : IAdditiveIdentity<T, T>
    {
        return T.AdditiveIdentity;
    }

    protected virtual void OnDataChanged(StationChangedEventArgs e)
    {
        DataChanged?.Invoke(this, e);
    }

    protected virtual void OnCallRegistered(CallRegisteredEventArgs e)
    {
        CallRegistered?.Invoke(this, e);
    }

    public IEnumerable<string> GetTariffNamesSortedByPrice()
    {
        return tariffs.Values
            .OrderBy(t => t.PriceInMinute)
            .Select(t => t.City);
            //.ToList();
    }

    public string? GetTopPayingClientName()
    {
        return clients
            .OrderByDescending(c => c.Calls.Sum(call => call.GetTotal()))
            .Select(c => c.Surname)
            .FirstOrDefault();
    }

    public int GetClientsCountAbovePayment(double threshold)
    {
        return clients.Aggregate(0, (count, client) =>
        {
            double clientTotal = client.Calls.Sum(call => call.GetTotal());
            return clientTotal > threshold ? count + 1 : count;
        });
    }

    public Dictionary<string, double> GetClientPaymentsByTariff(string surname)
    {
        Client? client = clients.FirstOrDefault(c => c.Surname == surname);

        if (client == null)
        {
            return new Dictionary<string, double>();
        }

        return client.Calls
            .GroupBy(call => call.City)
            .ToDictionary(g => g.Key, g => g.Sum(call => call.GetTotal()));
    }
}
