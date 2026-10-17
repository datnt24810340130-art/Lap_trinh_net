using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace bai4._10;

public sealed class CheckoutRecord : INotifyPropertyChanged
{
    private string _checkoutId = string.Empty;
    private string _studentId = string.Empty;
    private string _studentName = string.Empty;
    private string _equipment = string.Empty;
    private int _quantity = 1;
    private DateTime _borrowDate = DateTime.Today;
    private DateTime _dueDate = DateTime.Today.AddDays(7);
    private DateTime? _actualReturnDate;

    public string CheckoutId
    {
        get => _checkoutId;
        set => SetProperty(ref _checkoutId, value);
    }

    public string StudentId
    {
        get => _studentId;
        set => SetProperty(ref _studentId, value);
    }

    public string StudentName
    {
        get => _studentName;
        set => SetProperty(ref _studentName, value);
    }

    public string Equipment
    {
        get => _equipment;
        set => SetProperty(ref _equipment, value);
    }

    public int Quantity
    {
        get => _quantity;
        set => SetProperty(ref _quantity, value);
    }

    public DateTime BorrowDate
    {
        get => _borrowDate;
        set => SetProperty(ref _borrowDate, value);
    }

    public DateTime DueDate
    {
        get => _dueDate;
        set
        {
            if (SetProperty(ref _dueDate, value))
                OnPropertyChanged(nameof(Status));
        }
    }

    public DateTime? ActualReturnDate
    {
        get => _actualReturnDate;
        set
        {
            if (SetProperty(ref _actualReturnDate, value))
                OnPropertyChanged(nameof(Status));
        }
    }

    public string Status => ActualReturnDate.HasValue
        ? ActualReturnDate.Value.Date > DueDate.Date ? "Quá hạn" : "Đã trả"
        : DueDate.Date < DateTime.Today ? "Quá hạn" : "Đang mượn";

    public event PropertyChangedEventHandler? PropertyChanged;

    private bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    private void OnPropertyChanged(string? propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
