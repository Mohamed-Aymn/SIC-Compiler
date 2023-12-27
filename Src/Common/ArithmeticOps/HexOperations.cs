namespace Common.ArithmeticOps;

public static class HexOperations
{
    public static string Addition(string n1, string n2)
    {
        // Parse hexadecimal strings to integers
        int num1 = int.Parse(n1, System.Globalization.NumberStyles.HexNumber);
        int num2 = int.Parse(n2, System.Globalization.NumberStyles.HexNumber);

        // Perform addition
        int sum = num1 + num2;

        // Convert the result back to a hexadecimal string
        return sum.ToString("X");
    }

    public static string ToBinray(string hex)
    {
        string binary = "";

        foreach (char c in hex)
        {
            if (char.IsDigit(c))
            {
                // If the character is a digit, convert it to its binary representation
                int digit = c - '0';
                binary += Convert.ToString(digit, 2).PadLeft(4, '0');
            }
            else
            {
                // If the character is a letter (A-F), convert it to its binary representation
                int value = char.ToUpper(c) - 'A' + 10;
                binary += Convert.ToString(value, 2).PadLeft(4, '0');
            }
        }

        return binary;
    }
}