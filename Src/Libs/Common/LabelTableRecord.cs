namespace SicCompiler.Libs.Common;

public class LabelTableRecord
{
    public string Label {get; set;}
    public string Location {get; set;}
    public LabelTableRecord (string label, string location)
    {
        Label = label;
        Location = location;
    }
}