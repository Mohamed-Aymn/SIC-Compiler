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
        foreach (var line in passOne.MainTable)
        {
            Console.WriteLine(line.LocationCounter);
        }

        // pass two
        PassTwo passTwo = new(passOne.MainTable, passOne.LabelTable);

       // HTE calulation
        // HTE hte = new(passOne.MainTable, passTwo.ObjectCodeList);
        // Console.WriteLine(hte.H);
        // Console.WriteLine("*********8");
        // foreach (var line in hte.T)
        // {
        //     Console.WriteLine(line);
        // }
        // Console.WriteLine("*********8");
        // Console.WriteLine(hte.E);


        // formatted print
        // ReadWrite.FormattedWrite(passOne.MainTable, passTwo.ObjectCode);


        // print HTE
    }
}