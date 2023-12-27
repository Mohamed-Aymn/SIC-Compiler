namespace Common.ValueObjects;

public record LabelTableElement
{
    public LabelTableElement(string label, string location)
    {
        Label = label;
        Location = location;
    }

    public string Label { get; set; } = "";
    public string Location { get; set; } = "";
}