using Common.Utils;
using LinkingLoader.Libs;
using LinkingLoader.Utils;

internal class Program
{
    private static void Main(string[] args)
    {
        // Read HTE's and starting location counter
        bool isEnterMore = true;
        int i = 1;
        LinkedList<string> htes = new();
        while (isEnterMore)
        {
            htes.AddLast(ReadWrite.ReadProgramCode($"Enter Program {i} HTE (Press Enter twice to finish):"));
            if (i == 3)
            {
                string response = ReadWrite.ReadLine("Do you want to import more programs? (response with y/n)");
                if (response == "n")
                {
                    isEnterMore = false;
                }
            }
            i++;
        }
        string startingVariable = ReadWrite.ReadLine("Enter starting Location Counter:");

        // external symbols
        LinkedList<ExternalProgramSymbols> externalSymbols = new();
        foreach (string programHte in htes)
        {
            externalSymbols.AddLast(new ExternalProgramSymbols(programHte));
        }

        // main table variable
        MainTable mainTable = new(htes, externalSymbols);

        // formatted print
        Write.FormattedPrint();
    }
}