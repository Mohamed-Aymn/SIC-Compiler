namespace SicCompiler.Libs;

public class PassOne
{
    public LinkedList<PassOneTableRecord> MainTable {set; get;} = new();
    public LinkedList<LabelTabel> LabelTabel{set; get;} = new();
    public string LocationCounter {get; set;}
    public PassOne (string programCode)
    {
        // create organized table (this is a vertical linked list as the variable here is table length not width)
        string[] lines = programCode.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        bool isFirstLine = true;
        foreach (string line in lines)
        {
            // Split the string into words using whitespace as the delimiter
            string[] words = inputString.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int wordCount = words.Length;

            string locationCounter;
            string label;
            string instruction;
            string reference;
            if(isFirstLine)
            {
                locationCounter = "";
                label = "";
                instruction = words[1];
                reference = words[2];
                LocationCounter = words[3];
                isFirstLine = false;
            }
            if(wordCount == 2)
            {
                // locationCounter = LocationCounterHandler(instruction: words[0], reference: words[1])
                locationCounter = LocationCounter;
                label = "";
                instruction = words[0];
                reference = words[1];
            }
            if(wordCount == 3)
            {
                locationCounter = LocationCounter;
                label = words[0];
                instruction = words[1];
                reference = words[2];
            }

            // location counter addition
            LocationCounter = LocationCounterHandler(string instruction, string reference);

            // add record (line of code) in the formatted table
            MainTable.AddLast(new PassOneTableRecord(
                        locationCounter: locationCounter,
                        label: label,
                        instruction: instruction,
                        reference: reference));

            // if it is a new lable add it to symbol table, else ignore it
            LabelHandler(locationCounter, words[1]);
        }
    }

    public void LabelHandler(string locationCounter, string label)
    {
        bool isLabelFound = LabelTabel.Any(st => st.Symbol == label);

        if (!isLabelFound)
        {
            LabelTabel.AddLast(new LabelTableRecord(
                label: label;
                location: location;
            ));
        }
    }

    public string LocationCounterHandler(string instruction, string reference)
    {
        if (reference == "RESW")
        {
            int intValue = 3 * int.Parse(reference);
            LocationCounter = HexOperations.Addition(LocationCounter, intValue.ToString());
        }else if(reference == "RESB")
        {
            int intValue = 1 * int.Parse(reference);
            LocationCounter = HexOperations.Addition(LocationCounter, intValue.ToString());
        }else if(reference == "BYTE")
        {
            //
        }else{
            LocationCounter = HexOperations.Addition(LocationCounter, "3");
        }

        return LocationCounter;
    }

    private class LabelTableRecord
    {
        public string Label {get; set;}
        public string Location {get; set;}
        public LabelTableRecord (string label, string location)
        {
            Label = label;
            Location = location;
        }
    }

    private class PassOneTableRecord
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