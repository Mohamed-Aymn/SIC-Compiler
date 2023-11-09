namespace SicCompiler.Utils;

public static class BinaryOperations
{
    public static string ToHex(string binary)
    {
    int padding = 4 - (binary.Length % 4);
    if (padding != 4)
    {
        binary = new string('0', padding) + binary;
    }

    // Convert binary to hexadecimal
    string hex = "";
    for (int i = 0; i < binary.Length; i += 4)
    {
        string binaryChunk = binary.Substring(i, 4);
        int decimalValue = Convert.ToInt32(binaryChunk, 2);
        hex += decimalValue.ToString("X"); // Convert decimal to hexadecimal
    }

    return hex;
    }
}