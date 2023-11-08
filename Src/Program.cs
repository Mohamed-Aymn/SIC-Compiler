using SicCompiler.Utils;

namespace SicCompiler;

internal class Program
{
    private static void Main(string[] args)
    {
        // Read SIC code from user
        Console.WriteLine("Enter a multi-line string (Press Enter twice to finish):");
        string programCode = ReadWrite.ReadProgramCode();
    }
}