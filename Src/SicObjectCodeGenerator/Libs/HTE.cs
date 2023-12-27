using Common.SystemModules;

namespace SicObjectCodeGenerator.Libs;

public class HTE
{
    public string H { set; get; }
    public LinkedList<string> T { set; get; } = new();
    public string E { set; get; }

    private bool isFirstLine { set; get; } = true;
    private int currentIndex { set; get; }

    public HTE(LinkedList<PassOneTableRecord> mainTable, LinkedList<string> objectCodeList)
    {
        HGenerator(mainTable, objectCodeList);
        TGenerator(mainTable, objectCodeList);
        EGenerator(objectCodeList);
    }

    public void HGenerator(LinkedList<PassOneTableRecord> mainTable, LinkedList<string> objectCodeList)
    {
        string programName = mainTable.First!.Value.Label!;
        if (programName.Length < 6)
        {
            programName = programName.PadRight(6, 'X');
        }
        string firstObjectCode = objectCodeList.First!.Value!.PadLeft(6, '0');
        string lasttObjectCode = objectCodeList.Last!.Value!.PadLeft(6, '0');
        H = "H." + programName + "." + firstObjectCode + "." + lasttObjectCode;
    }
    public void TGenerator(LinkedList<PassOneTableRecord> mainTable, LinkedList<string> objectCodeList)
    {
        string t = "T";
        foreach (var line in mainTable)
        {
            string currentObjectCode = "";
            try
            {
                currentObjectCode = objectCodeList.ElementAt(currentIndex);
            }
            catch
            {
                if (t != "T")
                {
                    T.AddLast(t);
                }
                break;
            }

            // skip the first line as there is no object code associated to it
            if (isFirstLine)
            {
                isFirstLine = false;
                continue;
            }

            // check if no object code
            if (currentObjectCode?.Length == 0)
            {
                // if (T.Last!.Value != null && T.Last!.Value != "T")
                string currentT = "";
                try
                {
                    currentT = T.Last!.Value;
                }
                catch
                {
                    currentT = t;
                }
                bool isExist = T.Any(t => t == currentT);
                if (!isExist)
                {
                    T.AddLast(currentT);
                }
                // T.AddLast(t);
                // }
                t = "T";
                // break;
                currentIndex++;
                // TGenerator(mainTable, objectCodeList);
                continue;
            }

            // check the end of the loop
            if (currentObjectCode == null)
            {
                T.AddLast(t);
                break;
            }

            // add object code in the T string
            t += "." + currentObjectCode.PadLeft(6, '0');

            currentIndex++;
        }
    }

    public void EGenerator(LinkedList<string> objectCodeList)
    {
        string lasttObjectCode = objectCodeList.Last!.Value!.PadLeft(6, '0');
        E = "E." + lasttObjectCode;
    }
}