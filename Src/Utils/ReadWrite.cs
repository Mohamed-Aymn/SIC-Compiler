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
}