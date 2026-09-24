public class Client
{
    public string Surname { get; set; }
    public List<Call> Calls { get; }

    public Client(string surname)
    {
        Surname = surname;
        Calls = new List<Call>();
    }
}
