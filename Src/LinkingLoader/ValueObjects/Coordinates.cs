namespace LinkingLoader.ValueObjects;

public record Coordinates
{
    public Coordinates(string x, string y)
    {
        X = x;
        Y = y;
    }

    public string X { get; set; } = "";
    public string Y { get; set; } = "";
}