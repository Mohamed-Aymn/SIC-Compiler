using Common.ArithmeticOps;
using Common.Helpers;
using Common.PassOne;
using SicxeObjectCodeGenerator.ValueObjects;

namespace SicxeObjectCodeGenerator.Libs;

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
            if (line.Instruction == "END" || line.Instruction == "End")
            {
                break; // Exit the loop
            }

            // check special instruction cases
            if (line.Instruction == "RESW" ||
                line.Instruction == "RESB" ||
                line.Instruction == "BYTE" ||
                line.Instruction == "BASE")
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

            // all of these should return decimal
            string opcode = OpcodeHandler(line.Instruction);
            Nixbpe nixbpe = NixbpeHandler(line.Instruction, line.Reference);
            string thirdPart = ThirdPartHandler(labelTable, line.Reference, nixbpe.B, nixbpe.P);

            // object code hex convertor
            string objectCode = ObjectCodeGenerator(opcode, nixbpe, thirdPart);

            ObjectCodeList.AddLast(objectCode);
        }
    }

    private string ObjectCodeGenerator(string opcode, Nixbpe nixbpe, string thirdPart)
    {
        string binary = $"{opcode}{nixbpe.N}{nixbpe.I}{nixbpe.X}{nixbpe.B}{nixbpe.P}{nixbpe.E}{thirdPart}";
        string result = "";

        for (int i = 0; i < binary.Length / 4; i++)
        {
            string firstFourCharacters = binary.Substring(0, 4);
            binary = binary.Substring(4);
            result += BinaryOperations.ToHex(firstFourCharacters.ToString());
        }

        return (nixbpe.E == "1") ? result.PadRight(8, '0') : result.PadRight(6, '0');
    }

    private string ThirdPartHandler(LabelTable labelTable, string reference, string b, string p)
    {
        string label = "";
        if (reference.Contains(",X"))
        {
            label = reference.Substring(0, reference.Length - 2);
        }
        else if (reference.Contains('#') || reference.Contains('@'))
        {
            label = reference.Substring(1);
        }

        string targetAddress = labelTable.LabelLocationFinder(reference);
        string thirdPartResult = "";
        if (targetAddress != "")
        {
            foreach (char character in targetAddress)
            {
                thirdPartResult += HexOperations.ToBinray(character.ToString());
            }
        }
        else
        {
            foreach (char character in label)
            {
                thirdPartResult += HexOperations.ToBinray(character.ToString()).PadLeft(12, '0');
            }
        }

        return thirdPartResult;
    }

    private Nixbpe NixbpeHandler(string instruction, string reference)
    {
        Nixbpe nixbpe = new();

        // n and i
        if (reference.Contains('#') && !reference.Contains('@'))
        {
            nixbpe.I = "1";
        }
        else if (!reference.Contains('#') && reference.Contains('@'))
        {
            nixbpe.N = "1";
        }
        else
        {
            nixbpe.N = "1";
            nixbpe.I = "1";
        }

        // x
        if (reference.Contains(','))
        {
            nixbpe.X = "1";
        }

        // b and p
        if (nixbpe.N == "1" && nixbpe.I == "1")
        {
            nixbpe.P = "1";
        }

        //e 
        if (instruction.Contains('+'))
        {
            nixbpe.E = "1";
        }


        return nixbpe;
    }

    private string OpcodeHandler(string instruction)
    {
        if (instruction.Contains('+'))
        {
            instruction = instruction.Substring(1);
        }
        string hexObjectCode = Convertor.InstructionOpCode[instruction];
        string result = "";
        foreach (char character in hexObjectCode)
        {
            result += HexOperations.ToBinray(character.ToString());
        }
        return result.Substring(0, result.Length - 2);
    }
}