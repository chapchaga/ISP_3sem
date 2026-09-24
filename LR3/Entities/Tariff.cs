public class Tariff
{
    public string City { get; set; }
    public double PriceInMinute { get; set; }

    public Tariff(string city, double priceInMinute)
    {
        City = city;
        PriceInMinute = priceInMinute;
    }
}