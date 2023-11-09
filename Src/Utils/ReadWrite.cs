using SicCompiler.Libs.Common;

namespace SicCompiler.Utils;

public static class ReadWrite
{
    public static string ReadProgramCode()
    {
        string input = "";
        string line;

        while ((line = Console.ReadLine()!) != "")
        {
            // Environment.NewLine == "\n"
            input += line + Environment.NewLine;
        }

        return input;
    }

    public static void FormattedTableWrite(LinkedList<PassOneTableRecord> mainTable, LinkedList<string> objectCodeList)
    {
        // Table Header
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("{0,-20}", "Location Coutner");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("{0,-20} {1,-20} {2,-20}", "Label", "Instruction", "Reference");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("{0,-20}", "Object Code");
        Console.Write("\n");
        Console.ResetColor();
        Console.WriteLine(new string('-', 95));

        // Print table rows
        int currentRow = 0;
        bool isFirstRow = true;
        foreach (var row in mainTable)
        {
            // Location Counter
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("{0,-20}", row.LocationCounter);

            // Label, instruction and reference
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("{0,-20} {1,-20} {2,-20}", row.Label, row.Instruction, row.Reference);

            // Object Code
            if(isFirstRow)
            {
                Console.Write("\n");
                isFirstRow = false;
                continue;
            }
            string objectCode = "";
            try
            {
                objectCode = objectCodeList.ElementAt(currentRow);
            }
            catch
            {
                Console.Write("\n");
                break;
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("{0,-20}", objectCodeList.ElementAt(currentRow));

            Console.Write("\n");
            currentRow++;
        }
        Console.ResetColor();
        Console.Write("\n");
    }
}
