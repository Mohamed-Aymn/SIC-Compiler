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
        // debugging stuff
        // foreach (var line in passOne.MainTable)
        // {
        //     Console.WriteLine(line.LocationCounter + ": " + line.Instruction + " : " + line.Reference);
        // }
        // Console.WriteLine("first line counter: " + passOne.MainTable.First.Value.LocationCounter);
        // foreach (var line in passOne.LabelTable)
        // {
        //     Console.WriteLine(line.Location + ": " + line.Label);
        // }

        // pass two
        PassTwo passTwo = new(passOne.MainTable, passOne.LabelTable);
        // debuggin stuff
        // foreach (var line in passTwo.ObjectCodeList)
        // {
        //     Console.WriteLine(line);
        // }

       // HTE calulation
        HTE hte = new(passOne.MainTable, passTwo.ObjectCodeList);
        // debugging stuf
        // Console.WriteLine(hte.H);
        // Console.WriteLine("*********8");
        // foreach (var line in hte.T)
        // {
        //     Console.WriteLine(line);
        // }
        // Console.WriteLine("*********8");
        // Console.WriteLine(hte.E);

        // formatted print
        // program table
        // ReadWrite.FormattedTableWrite(passOne.MainTable, passTwo.ObjectCode);
        // HTE
        // ReadWrite.FormattedLinesWrite(hte.H, hte.T. hte.E);
    }
}