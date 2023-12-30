using Common.ArithmeticOps;
using Common.PassOne;

namespace SicxeObjectCodeGenerator.Libs;

public class M
{
    public LinkedList<string> ModificationRecords { set; get; } = new();
    public M(PassOneTable passOneTable)
    {
        foreach (var line in passOneTable.Table)
        {
            if (line.Instruction.Contains('+'))
            {
                ModificationRecords.AddLast("M." + HexOperations.Addition(line.LocationCounter!, "1").PadLeft(4, '0') + ".05");
            }
        }
    }
}