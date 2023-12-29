using Common.Utils;
using Common.Libs;

namespace SicxeObjectCodeGenerator.Libs;

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

        // HTE and M calulation
        HTE hte = new(passOne.PassOneTable, passTwo.ObjectCodeList);
        M m = new(passOne.PassOneTable);

        // formatted print
        ReadWrite.FormattedTableWrite(passOne.PassOneTable, passTwo.ObjectCodeList);
        ReadWrite.FormattedLineWrite("H", hte.H);
        ReadWrite.FormattedListWrite("T", hte.T);
        ReadWrite.FormattedListWrite("M", m.ModificationRecords);
        ReadWrite.FormattedLineWrite("E", hte.E);

    }
}