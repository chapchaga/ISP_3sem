public class Call
{
    public string City { get; set; }
    public int Minutes { get; set; }

    public Tariff? Tariff { get; set; }

    public Call(string city, int minutes)
    {
        City = city;
        Minutes = minutes;
    }

    public double GetTotal()
    {
        if (Tariff == null)
        {
            throw new InvalidOperationException(
                $"Для звонка в город {City} не назначен тариф.");
        }

        return Tariff.PriceInMinute * Minutes;
    }
}
