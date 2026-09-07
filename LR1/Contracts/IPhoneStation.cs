public interface IPhoneStation
{
    void AddTariff( Tariff tariff );
    void AddClient( Client client );
    void RegisterCall(Client client, Call call );
    double GetClientCallCost(string surname );
    double GetTotalCallsCost();
    int GetCallsCountByCity( string City );
}