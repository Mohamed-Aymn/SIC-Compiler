using Common.ArithmeticOps;
using Common.PassOne;
using Common.ValueObjects;

namespace SicObjectCodeGenerator.Libs;

public class PassOne
{
    public PassOneTable PassOneTable { get; set; } = new();
    public LabelTable LabelTable { get; set; } = new();
    public string LocationCounter { get; set; } = "";
    public PassOne(string programCode)
    {
        // create organized table (this is a vertical linked list as the variable here is table length not width)
        SicOneFormatter codeFormatter = new(programCode);
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
        if (instruction == "RESW")
        {
            int incrementValue = 3 * int.Parse(reference);
            LocationCounter = HexOperations.Addition(LocationCounter, incrementValue.ToString("X"));
        }
        else if (instruction == "RESB")
        {
            int incrementValue = 1 * int.Parse(reference);
            LocationCounter = HexOperations.Addition(LocationCounter, incrementValue.ToString("X"));
        }
        else if (instruction == "BYTE")
        {
            int incrementValue = 0;
            char operation = reference[0];
            // it didn't enter this if condiditon
            if (operation == 'C')
            {
                incrementValue = ConstantByteReferenceCalculation(reference);
            }
            LocationCounter = HexOperations.Addition(LocationCounter, incrementValue.ToString("X"));
        }
        else if (isFirstLine)
        {
            LocationCounter = reference;
        }
        else
        {
            LocationCounter = HexOperations.Addition(LocationCounter, "3");
        }

        return LocationCounter;
    }

    public int ConstantByteReferenceCalculation(string reference)
    {
        string value = reference.Substring(2, reference.Length - 3);
        return value.Length;
    }
}