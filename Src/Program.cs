using SicCompiler.Libs;
using SicCompiler.Utils;

namespace SicCompiler;

internal class Program
{
    private static void Main(string[] args)
    {
        // Read SIC code from us
        Console.WriteLine("Enter a multi-line string (Press Enter twice to finish):");
        string programCode = ReadWrite.ReadProgramCode();

        // pass one
        PassOne passOne = new(programCode);

        // pass two
        PassTwo passTwo = new(passOne.MainTable, passOne.LabelTable);

        // formatted print
        // ReadWrite.FormattedWrite(passOne.MainTable, passTwo.ObjectCode);

        // HTE calulation

        // print HTE
    }
}