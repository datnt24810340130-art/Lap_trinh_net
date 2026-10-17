using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace bai4._9
{
    public sealed class Appointment : INotifyPropertyChanged
    {
        private string appointmentId = string.Empty;
        private string patientName = string.Empty;
        private string phoneNumber = string.Empty;
        private DateTime appointmentDate;
        private string doctor = string.Empty;
        private string reason = string.Empty;
        private string status = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string AppointmentId
        {
            get => appointmentId;
            set => SetProperty(ref appointmentId, value);
        }

        public string PatientName
        {
            get => patientName;
            set => SetProperty(ref patientName, value);
        }

        public string PhoneNumber
        {
            get => phoneNumber;
            set => SetProperty(ref phoneNumber, value);
        }

        public DateTime AppointmentDate
        {
            get => appointmentDate;
            set => SetProperty(ref appointmentDate, value);
        }

        public string Doctor
        {
            get => doctor;
            set => SetProperty(ref doctor, value);
        }

        public string Reason
        {
            get => reason;
            set => SetProperty(ref reason, value);
        }

        public string Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }

        private void SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
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
