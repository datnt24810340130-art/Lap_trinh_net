using System.ComponentModel;

namespace bai4._6
{
    public class Student : INotifyPropertyChanged
    {
        private string _studentId = string.Empty;
        private string _fullName = string.Empty;
        private int _age;
        private string _gender = "Nam";
        private string _avatarPath = string.Empty;

        public string StudentId
        {
            get => _studentId;
            set => SetField(ref _studentId, value);
        }

        public string FullName
        {
            get => _fullName;
            set => SetField(ref _fullName, value);
        }

        public int Age
        {
            get => _age;
            set => SetField(ref _age, value);
        }

        public string Gender
        {
            get => _gender;
            set => SetField(ref _gender, value);
        }

        public string AvatarPath
        {
            get => _avatarPath;
            set => SetField(ref _avatarPath, value);
        }

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
}
