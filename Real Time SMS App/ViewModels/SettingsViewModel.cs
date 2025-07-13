using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;


namespace Real_Time_SMS_App.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        // Constructor
        public SettingsViewModel()
        {
            // Load settings when the ViewModel is initialized
            LoadSettings();
            Shifts = PreferencesHelper.LoadCollection<string>(shiftKey);
            Engines = PreferencesHelper.LoadCollection<string>(engineKey);

        }
        const string shiftKey = "SavedShifts";
        const string engineKey = "SavedEngines";

        [ObservableProperty]
        private string hotlineNumber;
        [ObservableProperty]
        private string stationEmail;
        [ObservableProperty]
        private string stationName;
        [ObservableProperty]
        private string stationAddress;

        [ObservableProperty]
        ObservableCollection<string> shifts;
        [ObservableProperty]
        private string shift;
        [ObservableProperty]
        ObservableCollection<string> engines;
        [ObservableProperty]
        private string engine;
        //save settings
        [RelayCommand]
        private async Task SaveSettings()
        {
            Preferences.Set("settingsHotline", HotlineNumber ?? string.Empty);
            Preferences.Set("settingsStation", StationName ?? string.Empty);
            Preferences.Set("settingsAddress", StationAddress ?? string.Empty);
            Preferences.Set("settingsEmail", StationEmail ?? string.Empty);
            //show dialog success after saving
            await Shell.Current.DisplayAlert("Settings Saved", "Your settings have been saved successfully.", "OK");
            // after saving settings, close this page
            await Shell.Current.GoToAsync("..");
        }
        //load settings
        [RelayCommand]
        private void LoadSettings()
        {
            HotlineNumber = Preferences.Get("settingsHotline", string.Empty);
            StationName = Preferences.Get("settingsStation", string.Empty);
            StationAddress = Preferences.Get("settingsAddress", string.Empty);
            StationEmail = Preferences.Get("settingsEmail", string.Empty);
        }




        [RelayCommand]
        private void SaveShifts()
        {
            PreferencesHelper.SaveCollection(shiftKey, Shifts);
        }
        //add method button for adding a shift to shifts collection
        [RelayCommand]
        private void AddShift()
        {
            if (!string.IsNullOrWhiteSpace(Shift) && !Shifts.Contains(Shift))
            {
                Shifts.Add(Shift);
                Shift = string.Empty; // Clear the input after adding
                SaveShifts(); // Save the updated collection
            }
            else
            {
                // Show an error message if the shift is empty or already exists
                Shell.Current.DisplayAlert("Error", "Please enter a valid shift that does not already exist.", "OK");
            }
        }
        //remove method button for removing a shift from shifts collection
        [RelayCommand]
        private void RemoveShift(string shiftToRemove)
        {
            if (Shifts.Contains(shiftToRemove))
            {
                Shifts.Remove(shiftToRemove);
                PreferencesHelper.SaveCollection(shiftKey, Shifts); // or use RemoveFromCollection
            }
        }

        // Save Engines
        [RelayCommand]
        private void AddEngine()
        {
            if (!string.IsNullOrWhiteSpace(Engine) && !Engines.Contains(Engine))
            {
                Engines.Add(Engine);
                Engine = string.Empty;
                PreferencesHelper.SaveCollection(engineKey, Engines);
            }
            else
            {
                Shell.Current.DisplayAlert("Error", "Please enter a valid engine that does not already exist.", "OK");
            }
        }
        // Remove Engines
        [RelayCommand]
        private void RemoveEngine(string engineToRemove)
        {
            if (Engines.Contains(engineToRemove))
            {
                Engines.Remove(engineToRemove);
                PreferencesHelper.SaveCollection(engineKey, Engines);
            }
        }
    }
}
