using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace baitap1
{
    public class MemberModel : INotifyPropertyChanged
    {
        private string _memberId = string.Empty;
        private string _fullName = string.Empty;
        private string _phone = string.Empty;
        private string _package = "Theo tháng";
        private DateTime _startDate = DateTime.Today;
        private int _fee;

        public string MemberId
        {
            get => _memberId;
            set
            {
                if (_memberId != value)
                {
                    _memberId = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FullName
        {
            get => _fullName;
            set
            {
                if (_fullName != value)
                {
                    _fullName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Package
        {
            get => _package;
            set
            {
                if (_package != value)
                {
                    _package = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Fee
        {
            get => _fee;
            set
            {
                if (_fee != value)
                {
                    _fee = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
