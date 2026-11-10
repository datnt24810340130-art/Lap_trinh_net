namespace bai6._3.DTO;

public sealed class TaskDto
{
    public int MaTask { get; set; }
    public int MaDuAn { get; set; }
    public string TenTask { get; set; } = string.Empty;
    public string? MoTa { get; set; }
    public DateTime? HanNop { get; set; }
    public string? DoUuTien { get; set; }
    public string TrangThai { get; set; } = "To Do";
    public string? TenDuAn { get; set; }
}
