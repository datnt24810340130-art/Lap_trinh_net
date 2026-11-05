using System.Globalization;

namespace bai6._2.BUS;

internal static class MonthValidator
{
    public static string Normalize(string month)
    {
        if (!DateTime.TryParseExact(month, "MM/yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime parsed))
        {
            throw new ArgumentException("Month must use the MM/yyyy format.", nameof(month));
        }

        return parsed.ToString("MM/yyyy", CultureInfo.InvariantCulture);
    }
}
