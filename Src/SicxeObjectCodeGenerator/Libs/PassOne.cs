using Common.ArithmeticOps;
using Common.PassOne;
using Common.ValueObjects;

namespace SicxeObjectCodeGenerator.Libs;

public class PassOne
{
    public PassOneTable PassOneTable { get; set; } = new();
    public LabelTable LabelTable { get; set; } = new();
    public string LocationCounter { get; set; } = "";

    public PassOne(string programCode)
    {
        // create organized table (this is a vertical linked list as the variable here is table length not width)
        CodeFormatter codeFormatter = new(programCode);
        bool isFirstLine = true;
        foreach (string line in codeFormatter.Lines)
        {
            LineElements lineElements = codeFormatter.LineFormatter(line, isFirstLine, LocationCounter);

            // location counter addition
            LocationCounter = LocationCounterHandler(
                    lineElements.Instruction,
                    lineElements.Reference,
                    isFirstLine);
            isFirstLine = false;

            // add record (line of code) in the formatted table
            PassOneTable.AddElement(lineElements);

            // if it is a new lable add it to symbol table, else ignore it
            LabelTable.LabelHandler(lineElements.LocationCounter, lineElements.Label);
        }
    }

    public string LocationCounterHandler(string instruction, string reference, bool isFirstLine)
    {
        if (isFirstLine)
        {
            LocationCounter = reference;
            return LocationCounter;
        }

        // format 4
        if (instruction[0] == '+')
        {
            LocationCounter = HexOperations.Addition(LocationCounter, "4");
            return LocationCounter;
        }

        switch (instruction)
        {
            case "BASE":
                break;
            case "RESW":
                int incrementValue = 3 * int.Parse(reference);
                LocationCounter = HexOperations.Addition(LocationCounter, incrementValue.ToString("X"));
                break;
            default:
                LocationCounter = HexOperations.Addition(LocationCounter, "3");
                break;
        }

        return LocationCounter;
    }
}