using System.ComponentModel;

namespace bai4._11;

public static class RepairStatuses
{
    public const string New = "Mới tiếp nhận";
    public const string InProgress = "Đang sửa";
    public const string WaitingForParts = "Chờ linh kiện";
    public const string Completed = "Hoàn tất";
    public const string Cancelled = "Đã hủy";

    public static readonly string[] All = [New, InProgress, WaitingForParts, Completed, Cancelled];
}

public sealed class RepairOrder : INotifyPropertyChanged
{
    private string _orderId = string.Empty;
    private string _customerName = string.Empty;
    private string _phoneNumber = string.Empty;
    private string _deviceType = string.Empty;
    private string _problemDescription = string.Empty;
    private DateTime _receivedDate = DateTime.Today;
    private decimal _estimatedCost;
    private string _status = RepairStatuses.New;
    private string? _imagePath;

    public string OrderId { get => _orderId; set => SetField(ref _orderId, value); }
    public string CustomerName { get => _customerName; set => SetField(ref _customerName, value); }
    public string PhoneNumber { get => _phoneNumber; set => SetField(ref _phoneNumber, value); }
    public string DeviceType { get => _deviceType; set => SetField(ref _deviceType, value); }
    public string ProblemDescription { get => _problemDescription; set => SetField(ref _problemDescription, value); }
    public DateTime ReceivedDate { get => _receivedDate; set => SetField(ref _receivedDate, value); }
    public decimal EstimatedCost { get => _estimatedCost; set => SetField(ref _estimatedCost, value); }
    public string Status { get => _status; set => SetField(ref _status, value); }
    public string? ImagePath { get => _imagePath; set => SetField(ref _imagePath, value); }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void SetField<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return;
        }

        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
