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
                locationCounter = "";
                label = words[0];
                instruction = words[1];
                reference = words[2];
                LocationCounter = words[2];
                isFirstLine = false;
            }
            if(wordCount == 2)
            {
                locationCounter = LocationCounter!;
                label = "";
                instruction = words[0];
                reference = words[1];
            }
            if(wordCount == 3)
            {
                locationCounter = LocationCounter!;
                label = words[0];
                instruction = words[1];
                reference = words[2];
            }

            // location counter addition
            LocationCounter = LocationCounterHandler(instruction, reference);

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

    public string LocationCounterHandler(string instruction, string reference)
    {
        if (reference == "RESW")
        {
            int incrementValue = 3 * int.Parse(reference);
            LocationCounter = HexOperations.Addition(LocationCounter, incrementValue.ToString());
        }else if(reference == "RESB")
        {
            int incrementValue = 1 * int.Parse(reference);
            LocationCounter = HexOperations.Addition(LocationCounter, incrementValue.ToString());
        }else if(reference == "BYTE")
        {
            int incrementValue = 0;
            char operation = reference[0];
            if (operation == 'C')
            {
                incrementValue = ConstantByteReferenceCalculation(reference);            
            }
            LocationCounter = HexOperations.Addition(LocationCounter, incrementValue.ToString());
        }else{
            LocationCounter = HexOperations.Addition(LocationCounter, "3");
        }

        return LocationCounter;
    }

    public int ConstantByteReferenceCalculation (string reference)
    {
        string value = reference.Substring(2, reference.Length - 3);
        return value.Length;
    }

    public class LabelTableRecord
    {
        public string Label {get; set;}
        public string Location {get; set;}
        public LabelTableRecord (string label, string location)
        {
            Label = label;
            Location = location;
        }
    }

    public class PassOneTableRecord
    {
        public string? LocationCounter {get; set;}
        public string? Label {get; set;}
        public string Instruction {get; set;}
        public string Reference {get; set;}
        public PassOneTableRecord(string? locationCounter, string? label, string instruction, string reference)
        {
            LocationCounter = locationCounter;
            Label = label;
            Instruction = instruction;
            Reference = reference;
        }
    }
}