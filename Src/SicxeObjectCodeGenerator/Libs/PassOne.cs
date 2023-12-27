using Common.ArithmeticOps;
using Common.PassOne;

namespace SicxeObjectCodeGenerator.Libs;

public class PassOne
{
    public PassOneTable PassOneTable { get; set; } = new();
    public LabelTable LabelTable { get; set; } = new();
    public string LocationCounter { get; set; } = "";

    public PassOne(string programCode)
    {
        
    }
}