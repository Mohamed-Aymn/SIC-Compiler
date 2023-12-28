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
        foreach (var line in passTwo.ObjectCodeList)
        {
            Console.WriteLine(line);
        }

        // HTE calulation

        // formatted print
    }
}