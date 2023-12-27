namespace Common.ValueObjects;

public record LineElements
{
    public string LocationCounter { get; set; } = "";
    public string Label { get; set; } = "";
    public string Instruction { get; set; } = "";
    public string Reference { get; set; } = "";
}