
namespace LinkingLoader.Libs;

public class MainTable
{
    public Dictionary<string, Dictionary<char, string>> Table { set; get; } = new();

    public MainTable(LinkedList<string[]> htes, LinkedList<ExternalProgramSymbols> externalSymbols)
    {
        FillFromT(htes);
        FillFromExternalSymboles(externalSymbols);

        TableModifier(htes, externalSymbols);
    }
    private void FillFromT(LinkedList<string[]> htes)
    {
        foreach (string[] hte in htes)
        {
            bool isTMore = true;
            int i = 1;
            while (isTMore)
            {
                // type import logic here starting form hte[3]
                if (hte[3 + i][0] != 'T')
                {
                    isTMore = false;
                }

            }
        }
    }

    private void FillFromExternalSymboles(LinkedList<ExternalProgramSymbols> externalSymbols)
    {
        throw new NotImplementedException();
    }

    private void TableModifier(LinkedList<string[]> htes, LinkedList<ExternalProgramSymbols> externalSymbols)
    {
    }
}