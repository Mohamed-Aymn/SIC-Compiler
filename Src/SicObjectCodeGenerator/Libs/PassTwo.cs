using Common.PassOne;
using Common.ArithmeticOps;
using Common.Helpers;

namespace SicObjectCodeGenerator.Libs;

public class PassTwo
{
    public LinkedList<string> ObjectCodeList { get; set; } = new();
    public PassTwo(PassOneTable passOneTable, LabelTable labelTable)
    {
        bool isFirstLine = true;
        foreach (var line in passOneTable.Table)
        {
            // first and last line handling
            if (isFirstLine)
            {
                isFirstLine = false;
                continue;  // Skip the current iteration and move to the next one
            }
            if (line.Instruction == "End")
            {
                break; // Exit the loop
            }

            // check special instruction cases
            if (line.Instruction == "RESW" || line.Instruction == "RESB" || line.Instruction == "BYTE")
            {
                ObjectCodeList.AddLast("");
                continue;
            }
            if (line.Instruction == "WORD")
            {
                int intValue = int.Parse(line.Reference);
                string decimalHexCode = intValue.ToString("X");
                if (decimalHexCode.Length < 6)
                {
                    decimalHexCode = decimalHexCode.PadLeft(6, '0');
                }
                ObjectCodeList.AddLast(decimalHexCode);
                continue;
            }

            // object code calculation
            string objectCode = line.Reference.Contains(",X")
                ? IndirectAddressing(labelTable, line.Label!, line.Instruction, line.Reference)
                : DicrectAddressing(labelTable, line.Reference, line.Instruction);

            // add object code to the object code linked list
            ObjectCodeList.AddLast(objectCode);
        }
    }

    public string DicrectAddressing(LabelTable labelTable, string reference, string instruction)
    {
        return Convertor.InstructionOpCode[instruction] + labelTable.LabelLocationFinder(reference);
    }

    public string IndirectAddressing(LabelTable labelTable, string label, string instruction, string reference)
    {
        reference = reference.Substring(0, reference.Length - 2);
        // string location = LabelLocationFinder(labelTable, label);
        string location = labelTable.LabelLocationFinder(reference);
        string binaryAddressCode = HexOperations.ToBinray(location);
        binaryAddressCode = "1" + binaryAddressCode.Substring(1); // replace the first elemnt with 1
        return Convertor.InstructionOpCode[instruction] + BinaryOperations.ToHex(binaryAddressCode);
    }

}