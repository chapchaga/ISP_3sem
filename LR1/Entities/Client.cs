using System.Runtime.CompilerServices;

public class Client
{
    public string Surname { get; set; }
    public MyCustomCollection<Call> Calls { get; set;}

    public Client()
    {
        Calls = new MyCustomCollection<Call>();
    }
}
