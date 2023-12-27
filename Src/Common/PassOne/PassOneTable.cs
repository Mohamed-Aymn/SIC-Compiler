using Common.ValueObjects;

namespace Common.PassOne;

public class PassOneTable
{
    public LinkedList<PassOneTableElement> Table { get; set; } = new();

    public void AddElement(LineElements lineElements)
    {
        Table.AddLast(new PassOneTableElement(
                    locationCounter: lineElements.LocationCounter,
                    label: lineElements.Label,
                    instruction: lineElements.Instruction,
                    reference: lineElements.Reference));
    }
}