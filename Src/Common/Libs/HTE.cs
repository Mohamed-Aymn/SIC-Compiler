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
                firstLocationCounter = mainTable.First!.Value.LocationCounter!.PadLeft(6, '0');
                t += "." + firstLocationCounter;
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
                    string length = "." + HexOperations.Subtraction(line.LocationCounter!, firstLocationCounter).PadLeft(2, '0');
                    if (length.Length > 3)
                    {
                        length = length.Substring(3);
                    }
                    currentT = currentT.Insert(8, length);
                    T.AddLast(currentT);
                }
                t = "T";
                currentIndex++;
                continue;
            }

            // check the end of the loop
            if (currentObjectCode == null)
            {
                // for (int i = 0; i < 2; i++)
                // {
                //     char c = t[i];
                // }

                T.AddLast(t);
                break;
            }

            // add object code in the T string
            t += "." + currentObjectCode.PadLeft(6, '0');

            currentIndex++;
        }
    }

    public void EGenerator(LinkedList<PassOneTableElement> mainTable)
    {
        string locationCoutnerStart = mainTable.First!.Value.LocationCounter!;
        E = "E." + locationCoutnerStart.PadLeft(6, '0');
    }
}