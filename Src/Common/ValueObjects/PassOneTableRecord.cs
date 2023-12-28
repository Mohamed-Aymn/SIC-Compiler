namespace Common.PassOne;

public record PassOneTableElement

{
    public string? LocationCounter { get; set; }
    public string? Label { get; set; }
    public string Instruction { get; set; }
    public string Reference { get; set; }
    public PassOneTableElement(string? locationCounter, string? label, string instruction, string reference)
    {
        ;
        LocationCounter = locationCounter;
        Label = label;
        Instruction = instruction;
        Reference = reference;
    }
}