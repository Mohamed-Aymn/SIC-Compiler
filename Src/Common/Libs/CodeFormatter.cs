namespace Common.Libs;

public static class CodeFormatter
{
    public static string[] SplitLines(string programCode)
    {
        return programCode.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
    }
}