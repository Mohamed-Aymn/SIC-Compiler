namespace LinkingLoader.Libs;

public class ExternalProgramSymbols
{
    public string ControlSectionName { get; set; }
    public string StartingAddress { get; set; }
    public string Length { get; set; }
    public Dictionary<string, string> Symbols { get; set; }
    public ExternalProgramSymbols(string hte)
    {
    }
}