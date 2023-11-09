using SicCompiler.Libs.Common;
using SicCompiler.Utils;

namespace SicCompiler.Libs;

public class PassOne
{
    public LinkedList<PassOneTableRecord> MainTable {get; set;} = new();
    public LinkedList<LabelTableRecord> LabelTable{get; set;} = new();
    public string LocationCounter {get; set;} = "";
    public PassOne (string programCode)
    {
        // create organized table (this is a vertical linked list as the variable here is table length not width)
        string[] lines = programCode.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        bool isFirstLine = true;
        foreach (string line in lines)
        {
            // Split the string into words using whitespace as the delimiter
            string [] words = FormatCodeLine(line);
            int wordCount = words.Length;

            string locationCounter = "";
            string label = "";
            string instruction = "";
            string reference = "";
            if(isFirstLine)
            {
                locationCounter = LocationCounter; // make it empty for the current use
                label = words[0];
                instruction = words[1];
                reference = words[2];
                LocationCounter = words[2]; // for the next use
            }
            if(wordCount == 2)
            {
                locationCounter = LocationCounter!;
                label = "";
                instruction = words[0];
                reference = words[1];
            }
            if(wordCount == 3 && !isFirstLine)
            {
                locationCounter = LocationCounter!;
                label = words[0];
                instruction = words[1];
                reference = words[2];
            }

            // location counter addition
            LocationCounter = LocationCounterHandler(instruction, reference, isFirstLine);
            isFirstLine = false;

            // add record (line of code) in the formatted table
            MainTable.AddLast(new PassOneTableRecord(
                        locationCounter: locationCounter,
                        label: label,
                        instruction: instruction,
                        reference: reference));

            // if it is a new lable add it to symbol table, else ignore it
            LabelHandler(locationCounter, label);
        }
    }

    public string [] FormatCodeLine(string line)
    {
        string trimmedString = line.Trim();
        string[] words = line.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        return words; 
    }

    public void LabelHandler(string locationCounter, string label)
    {
        bool isLabelFound = LabelTable.Any(record => record.Label == label);

        if (!isLabelFound)
        {
            LabelTable.AddLast(new LabelTableRecord(
                location: locationCounter,
                label: label
            ));
        }
    }

    public string LocationCounterHandler(string instruction, string reference, bool isFirstLine)
    {
        if (instruction == "RESW")
        {
            int incrementValue = 3 * int.Parse(reference);
            LocationCounter = HexOperations.Addition(LocationCounter, incrementValue.ToString("X"));
        }
        else if(instruction == "RESB")
        {
            int incrementValue = 1 * int.Parse(reference);
            LocationCounter = HexOperations.Addition(LocationCounter, incrementValue.ToString("X"));
        }
        else if(instruction == "BYTE")
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
        else if(isFirstLine)
        {
            LocationCounter = reference;
        }
        else{
            LocationCounter = HexOperations.Addition(LocationCounter, "3");
        }

        return LocationCounter;
    }

    public int ConstantByteReferenceCalculation (string reference)
    {
        string value = reference.Substring(2, reference.Length - 3);
        return value.Length;
    }
}