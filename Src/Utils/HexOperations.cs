namespace SicCompiler.Utils;

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
}