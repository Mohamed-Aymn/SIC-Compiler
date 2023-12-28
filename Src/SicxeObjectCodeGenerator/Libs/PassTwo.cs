using Common.ArithmeticOps;
using Common.Helpers;
using Common.PassOne;

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

            /**
             * opcode calculation
             * */
            if (line.Instruction.Contains('+'))
            {
                line.Instruction = line.Instruction.Substring(1);
            }
            string opcode = Convertor.InstructionOpCode[line.Instruction];

            /**
             * bits
             * */
            string n = "0";
            string i = "0";
            string x = "0";
            string b = "0";
            string p = "0";
            string e = "0";
            // n and i
            if (line.Reference.Contains('#') && !line.Reference.Contains('@'))
            {
                i = "1";
            }
            else if (!line.Reference.Contains('#') && line.Reference.Contains('@'))
            {
                n = "1";
            }
            else
            {
                n = "1";
                i = "1";
            }
            // x
            if (line.Reference.Contains(','))
            {
                x = "1";
            }
            // b and p
            if (n == "1" && i == "1")
            {
                p = "1";
            }
            //e 
            if (line.Instruction.Contains('+'))
            {
                e = "1";
            }

            /**
             * Address
             * */
            string reference = "";
            if (line.Reference.Contains(",X"))
            {
                // Console.Write(line.Reference);
                reference = line.Reference.Substring(0, line.Reference.Length - 2);
            }
            else if (line.Reference.Contains('#') || line.Reference.Contains('@'))
            {
                reference = line.Reference.Substring(1);
            }
            string targetAddress = labelTable.LabelLocationFinder(reference);
            if (targetAddress == "")
            {
                reference = BinaryOperations.ToHex(reference).PadLeft(12, '0');
            }
            else
            {
                string displacement = HexOperations.Addition(line.LocationCounter!, "3");
                reference = HexOperations.Subtraction(targetAddress, displacement);
            }

            string objectCode = "";
            objectCode += HexOperations.ToBinray(opcode[0].ToString());
            objectCode += HexOperations.ToBinray(opcode[1].ToString())[0];
            objectCode += HexOperations.ToBinray(opcode[1].ToString())[1];
            objectCode += $"{n}{i}{x}{b}{p}{e}";
            objectCode += reference;

            ObjectCodeList.AddLast(objectCode);
        }
    }
}