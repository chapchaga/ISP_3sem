using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Numerics;

public class PhoneStation : IPhoneStation
{
    private MyCustomCollection<Tariff> tariffs;
    private MyCustomCollection<Client> clients;

    public PhoneStation()
    {
        tariffs = new MyCustomCollection<Tariff>();
        clients = new MyCustomCollection<Client>();
    }

    public void AddTariff( Tariff tariff )
    {
        tariffs.Add(tariff);
    }

    public void AddClient( Client client )
    {
        clients.Add(client);
    }

    public void RegisterCall( Client client, Call call )
    {
        client.Calls.Add(call);
    }

    public int GetCallsCountByCity( string city )
    {
        int count = 0;
        
        for ( int i = 0; i < clients.Count; i++ )
        {
            Client client = clients[i];

            for ( int j = 0; j < client.Calls.Count; j++ )
            {
                Call call = client.Calls[j];

                if ( call.City == city )
                {
                    count++;
                }
            }
        }

        return count;
    }
   
    public double GetClientCallCost( string surname )
    {
        double total = 0;

        for ( int i = 0; i < clients.Count; i++ )
        {
            Client client = clients[i];

            if ( client.Surname == surname )
            {
                for ( int j = 0; j < client.Calls.Count; j++ )
                {
                    Call call = client.Calls[j];

                    for ( int k = 0; k < tariffs.Count; k++)
                    {
                        Tariff tariff = tariffs[k];

                        if ( tariff.City == call.City )
                        {
                            total += call.Minutes * tariff.PriceInMinute;
                        }
                    }
                }
            }
        }    

        return total;
    }

private static T Add<T>(T a, T b)
    where T : IAdditionOperators<T, T, T>
{
    return a + b;
}

public double GetTotalCallsCost()
{
    double total = 0;

    for (int i = 0; i < clients.Count; i++)
    {
        Client client = clients[i];

        for (int j = 0; j < client.Calls.Count; j++)
        {
            Call call = client.Calls[j];

            for (int k = 0; k < tariffs.Count; k++)
            {
                Tariff tariff = tariffs[k];

                if (tariff.City == call.City)
                {
                    total = Add(total, call.Minutes * tariff.PriceInMinute);
                }
            }
        }
    }

    return total;
}

}