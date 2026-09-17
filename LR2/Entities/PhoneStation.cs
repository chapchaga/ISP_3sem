using System.Numerics;

public class PhoneStation : IPhoneStation
{
    private readonly MyCustomCollection<Tariff> tariffs;
    private readonly MyCustomCollection<Client> clients;

    public event EventHandler<StationChangedEventArgs>? DataChanged;
    public event EventHandler<CallRegisteredEventArgs>? CallRegistered;

    public PhoneStation()
    {
        tariffs = new MyCustomCollection<Tariff>();
        clients = new MyCustomCollection<Client>();
    }

    public IEnumerable<Tariff> Tariffs
    {
        get { return tariffs; }
    }

    public IEnumerable<Client> Clients
    {
        get { return clients; }
    }

    public void AddTariff(Tariff tariff)
    {
        tariffs.Add(tariff);
        OnDataChanged(new StationChangedEventArgs(StationChangeType.TariffAdded, tariff.City));
    }

    public void AddClient(Client client)
    {
        clients.Add(client);
        OnDataChanged(new StationChangedEventArgs(StationChangeType.ClientAdded, client.Surname));
    }

    public void RegisterCall(Client client, Call call)
    {
        Tariff? tariff = FindTariff(call.City);

        if (tariff == null)
        {
            throw new InvalidOperationException(
                $"Тариф для города {call.City} не задан, звонок зарегистрировать нельзя.");
        }

        call.Tariff = tariff;
        client.Calls.Add(call);

        OnCallRegistered(new CallRegisteredEventArgs(
            client.Surname, call.City, call.Minutes, call.GetTotal()));
    }

    public double GetClientCallCost(string surname)
    {
        double total = Zero<double>();

        for (int i = 0; i < clients.Count; i++)
        {
            Client client = clients[i];

            if (client.Surname != surname)
            {
                continue;
            }

            for (int j = 0; j < client.Calls.Count; j++)
            {
                total = Add(total, client.Calls[j].GetTotal());
            }
        }

        return total;
    }

    public double GetTotalCallsCost()
    {
        double total = Zero<double>();

        for (int i = 0; i < clients.Count; i++)
        {
            Client client = clients[i];

            for (int j = 0; j < client.Calls.Count; j++)
            {
                total = Add(total, client.Calls[j].GetTotal());
            }
        }

        return total;
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

    private Tariff? FindTariff(string city)
    {
        for (int i = 0; i < tariffs.Count; i++)
        {
            if (tariffs[i].City == city)
            {
                return tariffs[i];
            }
        }

        return null;
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
}
