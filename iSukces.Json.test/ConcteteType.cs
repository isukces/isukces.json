namespace iSukces.Json.test;

public interface IConcteteType
{
    string Name    { get; set; }
    string Country { get; set; }
}

public class ConcteteType : IConcteteType
{
    public string Name    { get; set; }
    public string Country { get; set; }
}