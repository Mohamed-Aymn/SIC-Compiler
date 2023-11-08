namespace SicCompiler.Libs;

public class PassOne
{
    public LinkedList<PassOneTableRecord> MainTable {set; get;} = new();
    public LinkedList<SymbolTable> SymbolTable {set; get;} = new();
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

            // if it met a new label add it in the symbol table 
            if (wordCount == 3)
            {
                LabelHandler(words[0]); // if it is a new lable add it to symbol table, else ignore it
            }

            // save the record in the vertical linked list
            if(isFirstLine)
            {
                MainTable.AddLast(new PassOneTableRecord(null, words[0], words[1], words[2]));
                isFirstLine = false;
            }
            if(wordCount == 2)
            {
                MainTable.AddLast(
                    new PassOneTableRecord(
                            locationCounter: LocationCounterHandler(words[0], words[1]),
                            label: null,
                            instruction words[0],
                            reference words[1]));
            }
            if(wordCount == 3)
            {
                MainTable.AddLast(
                    new PassOneTableRecord(
                        locationCounter: LocationCounterHandler(words[1], words[2]),
                        label: words[0],
                        instruction: words[1],
                        reference: words[2]));
            }
        }
    }

    private class SymbolTable
    {
        public string Symbol {get; set;}
        public string Location {get; set;}
        public SymbolTable (string symbol, string location)
        {
            Symbol = symbol;
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