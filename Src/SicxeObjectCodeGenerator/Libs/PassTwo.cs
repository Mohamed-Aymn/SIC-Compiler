using Common.SystemModules;
using Common.ArithmeticOps;

namespace SicxeObjectCodeGenerator.Libs;

public class PassTwo
{
    public LinkedList<string> ObjectCodeList { get; set; } = new();
    public PassTwo(LinkedList<PassOneTableRecord> mainTable, LinkedList<LabelTableRecord> labelTable)
    { }
}