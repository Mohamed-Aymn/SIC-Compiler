using Common.ArithmeticOps;
using Common.PassOne;

namespace Common.Libs;

public class HTE
{
    public string H { set; get; }
    public LinkedList<string> T { set; get; } = new();
    public string E { set; get; }

    private bool isFirstLine { set; get; } = true;
    private int currentIndex { set; get; }

    public HTE(PassOneTable passOneTable, LinkedList<string> objectCodeList)
    {
        HGenerator(passOneTable.Table);
        TGenerator(passOneTable.Table, objectCodeList);
        EGenerator(passOneTable.Table);
    }

    public void HGenerator(LinkedList<PassOneTableElement> mainTable)
    {
        string programName = mainTable.First!.Value.Label!;
        string locationCounterStart = mainTable.First!.Value.LocationCounter!;
        string locationCoutnerEnd = mainTable.Last!.Value.LocationCounter!;
        string programLength = HexOperations.Subtraction(locationCoutnerEnd, locationCounterStart);

        H = "H." + programName.PadRight(6, 'X') + "." + locationCounterStart.PadLeft(6, '0') + "." + programLength.PadLeft(6, '0');
    }

    public void TGenerator(LinkedList<PassOneTableElement> mainTable, LinkedList<string> objectCodeList)
    {
        string t = "T";
        string firstLocationCounter = "";
        int numberOfElements = 0;
        foreach (var line in mainTable)
        {
            if (numberOfElements == 10)
            {
                // T closing 
                if (t.Count(c => c == '.') < 2)
                {
                    continue;
                }
                string length = "." + HexOperations.Subtraction(line.LocationCounter!, firstLocationCounter).PadLeft(2, '0');
                if (length.Length > 3)
                {
                    length = length.Substring(3);
                }
                t = t.Insert(8, length);
                isFirstLine = true;
                numberOfElements = 0;
                T.AddLast(t);
                t = "T";
            }
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
            if (t == "T" && line.LocationCounter != "")
            {
                firstLocationCounter = line.LocationCounter!;
                t += "." + firstLocationCounter.PadLeft(6, '0');
                continue;
            }

            // check if no object code
            if (currentObjectCode?.Length == 0)
            {
                if (t != "T")
                {
                    // t closing
                    if (t.Count(c => c == '.') < 2)
                    {
                        continue;
                    }
                    string length = "." + HexOperations.Subtraction(line.LocationCounter!, firstLocationCounter).PadLeft(2, '0');
                    if (length.Length > 3)
                    {
                        length = length.Substring(3);
                    }
                    t = t.Insert(8, length);
                    isFirstLine = true;
                    numberOfElements = 0;
                    T.AddLast(t);
                }
                t = "T";
                currentIndex++;
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
            numberOfElements++;

            currentIndex++;
        }
    }

    public void EGenerator(LinkedList<PassOneTableElement> mainTable)
    {
        string locationCoutnerStart = mainTable.First!.Value.LocationCounter!;
        E = "E." + locationCoutnerStart.PadLeft(6, '0');
    }

    private void TClosing()
    {

    }
}