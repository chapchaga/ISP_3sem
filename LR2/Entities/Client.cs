public class Client
{
    public string Surname { get; set; }
    public MyCustomCollection<Call> Calls { get; }

    public Client(string surname)
    {
        Surname = surname;
        Calls = new MyCustomCollection<Call>();
    }
}
