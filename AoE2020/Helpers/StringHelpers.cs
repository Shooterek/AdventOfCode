using System.Text.RegularExpressions;

public static class StringHelpers{
    public static string RemoveWhitespaceCharacters(this string value) {
        return Regex.Replace(value, @"\s+", "");
    }
}