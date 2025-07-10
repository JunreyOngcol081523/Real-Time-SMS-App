using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        public InitialInfoViewModel()
        {
            LoadDefaults();
        }
        [RelayCommand]
        private async Task Send()
        {
            string reportedDateTime = reportDate.ToString("dd/MM/yyyy") + " " + reportTime.ToString(@"hh\mm") + "H";
            string fireOut = fireOutDate.ToString("dd/MM/yyyy") + " " + fireOutTime.ToString(@"hh\mm") + "H";
            string arrival = dateOfArrival.ToString("dd/MM/yyyy") + " " + timeOfArrival.ToString(@"hh\mm") + "H";
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
            Preferences.Set(nameof(StationName), StationName);
            Preferences.Set(nameof(RespondingTeam), RespondingTeam);
            Preferences.Set(nameof(GroundCommander), GroundCommander);
            Preferences.Set(nameof(ContactNumber), ContactNumber);
            Preferences.Set(nameof(Sender), Sender);
            Preferences.Set(nameof(IsSaveChecked), IsSaveChecked);
        }

        private void LoadDefaults()
        {
            StationName = Preferences.Get(nameof(StationName), string.Empty);
            RespondingTeam = Preferences.Get(nameof(RespondingTeam), string.Empty);
            GroundCommander = Preferences.Get(nameof(GroundCommander), string.Empty);
            ContactNumber = Preferences.Get(nameof(ContactNumber), string.Empty);
            IsSaveChecked = Preferences.Get(nameof(IsSaveChecked), false);
            Sender = Preferences.Get(nameof(Sender), string.Empty);
        }
    }
}
