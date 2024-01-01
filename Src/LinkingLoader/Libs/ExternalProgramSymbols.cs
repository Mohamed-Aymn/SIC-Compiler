using Common.ArithmeticOps;
using LinkingLoader.ValueObjects;

namespace LinkingLoader.Libs;

public class ExternalProgramSymbols
{
    public string ControlSectionName { get; set; }
    public Coordinates EndAddress { get; set; }
    public Coordinates StartAddress { get; set; }
    public string Length { get; set; }
    public Dictionary<string, string> Symbols { get; set; } = new();
    public ExternalProgramSymbols(string[] hte, Coordinates startAddress)
    {
        StartAddress = startAddress;

        ControlSectionName = hte[0].Substring(2, 7).Replace("X", "");

        Length = hte[0].Substring(hte[0].Length - 6).Replace("0", "");

        string endAddress = HexOperations.Addition(StartAddress.X + StartAddress.Y, Length);
        EndAddress = new(
            endAddress.Substring(0, 3),
            endAddress.Substring(3));

        // get each variable name and location
        string refinedD = hte[1].Substring(2).Replace(".", "");
        for (int i = 0; i < refinedD.Length / 12; i++)
        {
            // get each 6 characters
            string varData = refinedD.Substring(12 * i, i == 0 ? 12 : 12 * i);

            // divide them by two, store the first one as key and the second one as value 
            // Console.WriteLine(varData.Substring(6).Replace("X", ""));
            Symbols.Add(varData.Substring(0, 6).Replace("X", ""), varData.Substring(6, 6).Replace("0", ""));
        }
    }
}