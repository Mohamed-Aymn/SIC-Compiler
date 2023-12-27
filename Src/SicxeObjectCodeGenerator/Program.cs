using Common.Utils;

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

        // HTE calulation

        // formatted print
    }
}