using Common.ArithmeticOps;
using LinkingLoader.ValueObjects;

namespace LinkingLoader.Libs;

public class MainTable
{
    public Dictionary<Coordinates, string> Table { set; get; } = new();

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
            Coordinates programStartAddress = externalSybmols.ElementAt(currentProgram).StartAddress;
            bool isTMore = true;
            int i = 0;
            while (isTMore)
            {
                string TStartAddressCalculation = HexOperations.Addition(programStartAddress.X + programStartAddress.Y, hte[3 + i].Substring(2, 6));
                Coordinates TStartAddress = new(
                    TStartAddressCalculation.Substring(0, 3),
                    TStartAddressCalculation.Substring(3));

                string column = TStartAddress.Y;
                bool isTwoBits = false;
                string row = TStartAddress.X;
                foreach (char bit in hte[3 + i].Substring(12))
                {
                    if (isTwoBits)
                    {
                        column = HexOperations.Addition(column, "1");
                        isTwoBits = false;
                    }

                    if (column == "10")
                    {
                        column = "0";
                        row = HexOperations.Addition(row, "1");
                    }

                    Coordinates coordinates = new(row, column);
                    if (Table.ContainsKey(coordinates))
                    {
                        Table[coordinates] += bit.ToString();
                    }
                    else
                    {
                        Table.Add(coordinates, bit.ToString());
                    }

                    if (Table[coordinates].Length == 2)
                    {
                        isTwoBits = true;
                    }
                }

                if (hte[4 + i][0] != 'T')
                {
                    isTMore = false;
                }
                i++;
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

            Coordinates startAddress = externalSymbols.ElementAt(currentProgram).StartAddress;
            string varAddress = hte[currentProgram].Substring(2, 8);
            string numberOfModifiedBits = hte[currentProgram].Substring(9, 11);
            string modifiedAddress = HexOperations.Addition(startAddress.X + startAddress.Y, varAddress);
            string row = modifiedAddress.Substring(0, 3); // first three bits
            string column = modifiedAddress.Substring(-1); // last bit
            Coordinates coordinates = new(row, column);

            if (numberOfModifiedBits == "5")
            {
                Table[coordinates] = Table[coordinates].Substring(-1) + modifiedAddress[0];
            }
            else
            {
                Table[coordinates] = modifiedAddress.Substring(2);
            }

            coordinates.Y = HexOperations.Addition(coordinates.Y, "1");
            Table[coordinates] = modifiedAddress.Substring(2, 4);
            coordinates.Y = HexOperations.Addition(coordinates.Y, "1");
            Table[coordinates] = modifiedAddress.Substring(4, 6);

            currentProgram++;
        }
    }
}