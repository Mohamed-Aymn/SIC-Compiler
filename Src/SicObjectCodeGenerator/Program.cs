using Common.Utils;

namespace SicObjectCodeGenerator.Libs;

internal class Program
{
    private static void Main(string[] args)
    {
        // Read code
        string programCode = ReadWrite.ReadProgramCode();

        // pass one
        PassOne passOne = new(programCode);

        // pass two
        PassTwo passTwo = new(passOne.MainTable, passOne.LabelTable);

        // HTE calulation
        HTE hte = new(passOne.MainTable, passTwo.ObjectCodeList);

        // formatted print
        ReadWrite.FormattedTableWrite(passOne.MainTable, passTwo.ObjectCodeList);
        ReadWrite.FormattedHteWrite(hte.H, hte.T, hte.E);
    }
}