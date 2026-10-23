using System.Text;

namespace bai2;

public class BookModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string CoverImagePath { get; set; } = string.Empty;

    public static BookModel FromCsv(string csvLine)
    {
        var values = ParseCsvLine(csvLine);
        if (values.Count < 7)
        {
            throw new InvalidOperationException("Dòng CSV không hợp lệ.");
        }

        return new BookModel
        {
            Id = int.TryParse(values[0], out var id) ? id : 0,
            Title = values[1],
            Author = values[2],
            Category = values[3],
            Price = decimal.TryParse(values[4], out var price) ? price : 0m,
            Quantity = int.TryParse(values[5], out var quantity) ? quantity : 0,
            CoverImagePath = values[6]
        };
    }

    public string ToCsvLine()
    {
        return string.Join(",",
            EscapeCsvField(Id.ToString()),
            EscapeCsvField(Title),
            EscapeCsvField(Author),
            EscapeCsvField(Category),
            EscapeCsvField(Price.ToString("0.##")),
            EscapeCsvField(Quantity.ToString()),
            EscapeCsvField(CoverImagePath));
    }

    private static List<string> ParseCsvLine(string line)
    {
        var result = new List<string>();
        var current = new StringBuilder();
        var insideQuotes = false;

        for (var i = 0; i < line.Length; i++)
        {
            var ch = line[i];
            if (ch == '"')
            {
                if (insideQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    current.Append('"');
                    i++;
                }
                else
                {
                    insideQuotes = !insideQuotes;
                }
            }
            else if (ch == ',' && !insideQuotes)
            {
                result.Add(current.ToString());
                current.Clear();
            }
            else
            {
                current.Append(ch);
            }
        }

        result.Add(current.ToString());
        return result;
    }

    private static string EscapeCsvField(string value)
    {
        if (value.Contains('"') || value.Contains(',') || value.Contains('\n') || value.Contains('\r'))
        {
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }

        return value;
    }
}
