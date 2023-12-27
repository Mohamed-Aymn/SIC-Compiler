using Common.ValueObjects;

namespace Common.PassOne;

public class CodeFormatter
{
    public string[] Lines { get; set; }
    public CodeFormatter(string programCode)
    {
        Lines = SplitLines(programCode);
    }

    public string[] SplitLines(string programCode)
    {
        string[] lines = programCode.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        return lines;
    }

    public LineElements LineFormatter(string line, bool isFirstLine, string locationCounter)
    {
        LineElements lineElements = new();
        // Split the string into words using whitespace as the delimiter
        string[] words = FormatCodeLine(line);
        int wordCount = words.Length;

        if (isFirstLine)
        {
            lineElements.LocationCounter = locationCounter; // make it empty for the current use
            lineElements.Label = words[0];
            lineElements.Instruction = words[1];
            lineElements.Reference = words[2];
            lineElements.LocationCounter = words[2]; // for the next use
        }
        if (wordCount == 2)
        {
            lineElements.LocationCounter = locationCounter;
            lineElements.Label = "";
            lineElements.Instruction = words[0];
            lineElements.Reference = words[1];
        }
        if (wordCount == 3 && !isFirstLine)
        {
            lineElements.LocationCounter = locationCounter;
            lineElements.Label = words[0];
            lineElements.Instruction = words[1];
            lineElements.Reference = words[2];
        }

        return lineElements;
    }

    public string[] FormatCodeLine(string line)
    {
        string trimmedString = line.Trim();
        string[] words = line.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        return words;
    }
}