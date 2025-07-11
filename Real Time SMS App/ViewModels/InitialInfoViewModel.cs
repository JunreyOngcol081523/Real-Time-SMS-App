using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Real_Time_SMS_App.ViewModels
{
    
    public partial class InitialInfoViewModel:ObservableObject
    {
        [ObservableProperty] private string stationName;
        [ObservableProperty] private string address;
        [ObservableProperty] private string respondingTeam;
        [ObservableProperty] private DateTime reportDate = DateTime.Now;
        [ObservableProperty] private TimeSpan reportTime = DateTime.Now.TimeOfDay;
        [ObservableProperty] private string involved;
        [ObservableProperty] private string alarmStatus;
        [ObservableProperty] private DateTime dateOfArrival = DateTime.Now;
        [ObservableProperty] private TimeSpan timeOfArrival = DateTime.Now.TimeOfDay;
        [ObservableProperty] private DateTime fireOutDate = DateTime.Now;
        [ObservableProperty] private TimeSpan fireOutTime = DateTime.Now.TimeOfDay;
        [ObservableProperty] private string groundCommander;
        [ObservableProperty] private string contactNumber;
        [ObservableProperty] private string sender;
        [ObservableProperty] private bool isSaveChecked;
        [ObservableProperty]
        private ObservableCollection<string> alarmStatusOptions = new()
        {
            "1st Alarm", "2nd Alarm", "3rd Alarm", "4th Alarm",
            "Task Force Alpha", "Task Force Bravo", "General Alarm"
        };
        public InitialInfoViewModel()
        {
            LoadDefaults();
        }
        [RelayCommand]
        private async Task Send()
        {
            string reportedDateTime = reportDate.ToString("dd/MM/yyyy") + " " + reportTime.ToString(@"hhmm") + "H";
            string fireOut = fireOutDate.ToString("dd/MM/yyyy") + " " + fireOutTime.ToString(@"hh\mm") + "H";
            string arrival = dateOfArrival.ToString("dd/MM/yyyy") + " " + timeOfArrival.ToString(@"hhmm") + "H";
            string reportMessage =
            $"A.1 INITIAL INFO\n\n{stationName} reporting a {involved} fire at {address}.\n\n" +
            $"RESPONDING TEAM: {respondingTeam}\n" +
            $"TIME/DATE REPORTED: {reportedDateTime}\n" +
            $"INVOLVED: {involved}\n" +
            $"ALARM STATUS: {alarmStatus}Alarm\n" +
            $"TIME OF ARRIVAL: {arrival}\n" +
            $"FIRE OUT: {fireOut}\n" +
            $"GROUND COMMANDER: {groundCommander}\n" +
            $"CONTACT NUM OF STN: {contactNumber}\n" +
            $"SENDER: {sender}";

            try
            {
                var contacts = await LoadContactsAsync();
                var phoneNumbers = contacts
                    .Select(c => c.PhoneNumber?.Trim())
                    .Where(p => !string.IsNullOrWhiteSpace(p))
                    .ToArray();

                if (phoneNumbers.Length == 0)
                {
                    await Shell.Current.DisplayAlert("Warning", "No contacts found to send SMS.", "OK");
                    return;
                }

                var uri = new Uri($"sms:{string.Join(";", phoneNumbers)}?body={Uri.EscapeDataString(reportMessage)}");
                await Launcher.Default.OpenAsync(uri);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to send SMS: " + ex.Message, "OK");
            }

        }
        private async Task<List<Contact>> LoadContactsAsync()
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, "contacts.json");

            if (!File.Exists(filePath))
                return new List<Contact>();

            var json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<Contact>>(json) ?? new List<Contact>();
        }

        partial void OnIsSaveCheckedChanged(bool value)
        {
            if (value)
            {
                SaveDefaults();
            }
            else
            {
                // Optionally, you can clear the saved preferences if unchecked
                Preferences.Clear();
            }
        }
        private void SaveDefaults()
        {
            Preferences.Set(nameof(StationName), StationName ?? string.Empty);
            Preferences.Set(nameof(Address), Address ?? string.Empty);
            Preferences.Set(nameof(RespondingTeam), RespondingTeam ?? string.Empty);
            Preferences.Set(nameof(GroundCommander), GroundCommander ?? string.Empty);
            Preferences.Set(nameof(ContactNumber), ContactNumber ?? string.Empty);
            Preferences.Set(nameof(Sender), Sender ?? string.Empty);
            Preferences.Set(nameof(IsSaveChecked), IsSaveChecked);

            // Save DateTime and TimeSpan as ticks
            Preferences.Set(nameof(ReportDate), ReportDate.Ticks);
            Preferences.Set(nameof(ReportTime), ReportTime.Ticks);
            Preferences.Set(nameof(DateOfArrival), DateOfArrival.Ticks);
            Preferences.Set(nameof(TimeOfArrival), TimeOfArrival.Ticks);
            Preferences.Set(nameof(FireOutDate), FireOutDate.Ticks);
            Preferences.Set(nameof(FireOutTime), FireOutTime.Ticks);
        }

        private void LoadDefaults()
        {
            StationName = Preferences.Get(nameof(StationName), string.Empty);
            Address = Preferences.Get(nameof(Address), string.Empty);
            RespondingTeam = Preferences.Get(nameof(RespondingTeam), string.Empty);
            GroundCommander = Preferences.Get(nameof(GroundCommander), string.Empty);
            ContactNumber = Preferences.Get(nameof(ContactNumber), string.Empty);
            Sender = Preferences.Get(nameof(Sender), string.Empty);
            IsSaveChecked = Preferences.Get(nameof(IsSaveChecked), false);

            // Load DateTime and TimeSpan from ticks
            ReportDate = new DateTime(Preferences.Get(nameof(ReportDate), DateTime.Now.Ticks));
            ReportTime = new TimeSpan(Preferences.Get(nameof(ReportTime), DateTime.Now.TimeOfDay.Ticks));
            DateOfArrival = new DateTime(Preferences.Get(nameof(DateOfArrival), DateTime.Now.Ticks));
            TimeOfArrival = new TimeSpan(Preferences.Get(nameof(TimeOfArrival), DateTime.Now.TimeOfDay.Ticks));
            FireOutDate = new DateTime(Preferences.Get(nameof(FireOutDate), DateTime.Now.Ticks));
            FireOutTime = new TimeSpan(Preferences.Get(nameof(FireOutTime), DateTime.Now.TimeOfDay.Ticks));
        }

    }
}
