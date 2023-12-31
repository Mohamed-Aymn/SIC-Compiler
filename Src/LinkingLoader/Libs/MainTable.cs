using Common.ArithmeticOps;

namespace LinkingLoader.Libs;

public class MainTable
{
    public Dictionary<string, Dictionary<string, string>> Table { set; get; } = new();

    public MainTable(LinkedList<string[]> htes, LinkedList<ExternalProgramSymbols> externalSymbols)
    {
        TableFiller(htes, externalSymbols);
        TableModifier(htes, externalSymbols);
    }
    private void TableFiller(LinkedList<string[]> htes, LinkedList<ExternalProgramSymbols> externalSybmols)
    {
        int currentProgram = 0;
        foreach (string[] hte in htes)
        {
            bool isTMore = true;
            string startAddress = externalSybmols.ElementAt(currentProgram).StartAddress;
            int i = 0;
            while (isTMore)
            {
                int column = 0;
                bool isTwoBits = false;
                string row = startAddress;
                foreach (char bit in hte[3 + i])
                {
                    if (isTwoBits)
                    {
                        column++;
                    }
                    if (column == 16)
                    {
                        column = 0;
                    }
                    row = HexOperations.Addition(row, "10");
                    Table[row][BinaryOperations.ToHex(i.ToString())] += bit.ToString();
                    if (Table[row][BinaryOperations.ToHex(i.ToString())].Length == 2)
                    {
                        isTwoBits = true;
                    }
                }

                if (hte[4 + i][0] != 'T')
                {
                    isTMore = false;
                }
            }
            currentProgram++;
        }
    }

    private void TableModifier(LinkedList<string[]> htes, LinkedList<ExternalProgramSymbols> externalSymbols)
    {
        int currentProgram = 0;
        foreach (string[] hte in htes)
        {
            if (hte[currentProgram][0] != 'M')
            {
                continue;
            }

            string startAddress = externalSymbols.ElementAt(currentProgram).StartAddress;
            string varAddress = hte[currentProgram].Substring(2, 8);
            string numberOfModifiedBits = hte[currentProgram].Substring(9, 11);
            string modifiedAddress = HexOperations.Addition(startAddress, varAddress);
            string row = modifiedAddress.Substring(0, 3); // first three bits
            string column = modifiedAddress.Substring(-1); // last bit

            if (numberOfModifiedBits == "5")
            {
                Table[row][column] = Table[row][column].Substring(-1) + modifiedAddress[0];
            }
            else
            {
                Table[row][column] = modifiedAddress.Substring(2);
            }

            Table[HexOperations.Addition(row, "1")][column] = modifiedAddress.Substring(2, 4);
            Table[HexOperations.Addition(row, "2")][column] = modifiedAddress.Substring(4, 6);

            currentProgram++;
        }
    }
}