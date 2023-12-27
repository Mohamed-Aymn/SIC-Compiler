using Common.PassOne;

namespace SicxeObjectCodeGenerator.Libs;

public class PassTwo
{
    public LinkedList<string> ObjectCodeList { get; set; } = new();
    public PassTwo(PassOneTable passOneTable, LabelTable labelTable)
    {
    }
}