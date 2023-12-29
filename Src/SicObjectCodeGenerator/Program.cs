using Common.Utils;
using Common.Libs;

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
        PassTwo passTwo = new(passOne.PassOneTable, passOne.LabelTable);

        // HTE calulation
        HTE hte = new(passOne.PassOneTable, passTwo.ObjectCodeList);

        // formatted print
        ReadWrite.FormattedTableWrite(passOne.PassOneTable, passTwo.ObjectCodeList);
        ReadWrite.FormattedLineWrite("H", hte.H);
        ReadWrite.FormattedListWrite("T", hte.T);
        ReadWrite.FormattedLineWrite("E", hte.E);
    }
}