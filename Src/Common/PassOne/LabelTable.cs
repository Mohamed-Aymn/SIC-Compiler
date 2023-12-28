using Common.ValueObjects;

namespace Common.PassOne;

public class LabelTable
{
    public LinkedList<LabelTableElement> Table { get; set; } = new();
    public void LabelHandler(string locationCounter, string label)
    {
        bool isLabelFound = Table.Any(record => record.Label == label);

        if (!isLabelFound && label != "")
        {
            Table.AddLast(new LabelTableElement(
                label: label,
                location: locationCounter
            ));
        }
    }

    public string LabelLocationFinder(string reference)
    {
        LabelTableElement foundRecord = Table.FirstOrDefault(record => record.Label == reference)!;
        return foundRecord.Location ?? "";
    }
}