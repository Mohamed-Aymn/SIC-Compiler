using Common.SystemModules;
using Common.ArithmeticOps;

namespace SicxeObjectCodeGenerator.Libs;

public class PassOne
{
    public LinkedList<PassOneTableRecord> MainTable { get; set; } = new();
    public LinkedList<LabelTableRecord> LabelTable { get; set; } = new();
    public string LocationCounter { get; set; } = "";
    public PassOne(string programCode)
    {

    }
}