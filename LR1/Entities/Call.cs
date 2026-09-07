using System.Runtime.CompilerServices;

public class Call
{
    public Tariff Tariff { get; set; }
    public string City { get; set; }
    public int Minutes { get; set; }

    public double GetTotal()
      => Tariff.PriceInMinute * Minutes;
}