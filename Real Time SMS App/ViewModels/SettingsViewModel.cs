using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Google.Cloud.Firestore.V1;
using Microsoft.Maui.Controls;
using Real_Time_SMS_App.Models;
using Real_Time_SMS_App.Services;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


namespace Real_Time_SMS_App.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        //constructor
        public SettingsViewModel()
        {
            // Load initial settings
            LoadSettings();
            // Load regions, provinces, and stations
            Regions = new ObservableCollection<string>();
            Provinces = new ObservableCollection<string>();
            Stations = new ObservableCollection<string>();
            Shifts = PreferencesHelper.LoadCollection<string>(shiftKey);
            Engines = PreferencesHelper.LoadCollection<string>(engineKey);
            _firestoreService = new FirestoreService();
        }

        private readonly FirestoreService _firestoreService;
        const string shiftKey = "SavedShifts";
        const string engineKey = "SavedEngines";
        [ObservableProperty]
        private ObservableCollection<string> regions;
        [ObservableProperty]
        private ObservableCollection<string> provinces;        
        [ObservableProperty]
        private ObservableCollection<string> stations;
        [ObservableProperty]
        private string selectedRegionID;  
        [ObservableProperty]
        private string regionName;
        [ObservableProperty]
        private string selectedProvinceID;            
        [ObservableProperty]
        private string provinceName;        
 
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

        //load all regions to the regions collection
        [RelayCommand]
        public async Task LoadRegionsAsync()
        {
            try
            {
                var regionsList = await _firestoreService.GetRegionsCollection();
                Regions.Clear();
                foreach (var region in regionsList)
                {
                    if (!String.IsNullOrWhiteSpace(region.RegionId) && !Regions.Contains(region.RegionId))
                    {
                        Regions.Add(region.RegionId);
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to load regions: {ex.Message}", "OK");
            }
        }
        //display region name by selectedregionid
        async partial void OnSelectedRegionIDChanged(string value)
        {
            var regionModel = await _firestoreService.GetDocumentByFieldAsync<RegionsModel>("Reg", "RegionId", value);
            RegionName = regionModel.RegionName;
            await LoadProvinces();
        }

        //load all provinces to the provinces collection by selected region id
        public async Task LoadProvinces()
        {
            var provinces = await _firestoreService.GetProvincesByRegionIdAsync(SelectedRegionID);
            foreach (var province in provinces)
            {
                //check if the province ID is not null or empty and not already in the collection
                if (!string.IsNullOrWhiteSpace(province.ProvinceId) && !this.Provinces.Contains(province.ProvinceId))
                    //add the province ID to the provinces collection
                    this.Provinces.Add(province.ProvinceId);
            }
        }
        async partial void OnSelectedProvinceIDChanged(string value)
        {
            var provinceModel = await _firestoreService.GetDocumentByFieldAsync<ProvincesModel>("Provinces", "ProvinceId", value);
            ProvinceName = provinceModel.ProvinceName;
        }
        [RelayCommand]
        private async Task AddRegion()
        {
            var regions = new List<RegionsModel>
    {
        new() { RegionId = "reg1", RegionName = "Region I – Ilocos Region", RegionDescription = "Region I – Ilocos Region" },
        new() { RegionId = "reg2", RegionName = "Region II – Cagayan Valley", RegionDescription = "Region II – Cagayan Valley" },
        new() { RegionId = "reg3", RegionName = "Region III – Central Luzon", RegionDescription = "Region III – Central Luzon" },
        new() { RegionId = "reg4a", RegionName = "Region IV-A – CALABARZON", RegionDescription = "Region IV-A – CALABARZON" },
        new() { RegionId = "reg4b", RegionName = "MIMAROPA Region", RegionDescription = "MIMAROPA Region" },
        new() { RegionId = "reg5", RegionName = "Region V – Bicol Region", RegionDescription = "Region V – Bicol Region" },
        new() { RegionId = "reg6", RegionName = "Region VI – Western Visayas", RegionDescription = "Region VI – Western Visayas" },
        new() { RegionId = "reg7", RegionName = "Region VII – Central Visayas", RegionDescription = "Region VII – Central Visayas" },
        new() { RegionId = "reg8", RegionName = "Region VIII – Eastern Visayas", RegionDescription = "Region VIII – Eastern Visayas" },
        new() { RegionId = "reg9", RegionName = "Region IX – Zamboanga Peninsula", RegionDescription = "Region IX – Zamboanga Peninsula" },
        new() { RegionId = "reg10", RegionName = "Region X – Northern Mindanao", RegionDescription = "Region X – Northern Mindanao" },
        new() { RegionId = "reg11", RegionName = "Region XI – Davao Region", RegionDescription = "Region XI – Davao Region" },
        new() { RegionId = "reg12", RegionName = "Region XII – SOCCSKSARGEN", RegionDescription = "Region XII – SOCCSKSARGEN" },
        new() { RegionId = "reg13", RegionName = "Region XIII – Caraga", RegionDescription = "Region XIII – Caraga" },
        new() { RegionId = "ncr", RegionName = "NCR – National Capital Region", RegionDescription = "NCR – National Capital Region" },
        new() { RegionId = "car", RegionName = "CAR – Cordillera Administrative Region", RegionDescription = "CAR – Cordillera Administrative Region" },
        new() { RegionId = "barmm", RegionName = "BARMM – Bangsamoro Autonomous Region in Muslim Mindanao", RegionDescription = "BARMM – Bangsamoro Autonomous Region in Muslim Mindanao" }
    };

            foreach (var region in regions)
            {
                await _firestoreService.AddRegionsCollection(region);
            }
        }
        
        [RelayCommand]
        private async Task AddProvincesToAllRegions()
        {
            async Task AddProvinces(string regionId, params string[] provinces)
            {
                int counter = 1;
                foreach (var name in provinces)
                {
                    var province = new ProvincesModel
                    {
                        ProvinceId = $"{regionId.ToLower()}_{counter++}",
                        ProvinceName = name,
                        RegionId = regionId
                    };

                    await _firestoreService.AddProvinceAsync(province);
                }
            }

            await AddProvinces("reg1", "Ilocos Norte", "Ilocos Sur", "La Union", "Pangasinan");
            await AddProvinces("reg2", "Batanes", "Cagayan", "Isabela", "Nueva Vizcaya", "Quirino");
            await AddProvinces("reg3", "Aurora", "Bataan", "Bulacan", "Nueva Ecija", "Pampanga", "Tarlac", "Zambales");
            await AddProvinces("reg4a", "Cavite", "Laguna", "Batangas", "Rizal", "Quezon");
            await AddProvinces("reg4b", "Marinduque", "Occidental Mindoro", "Oriental Mindoro", "Palawan", "Romblon");
            await AddProvinces("reg5", "Albay", "Camarines Norte", "Camarines Sur", "Catanduanes", "Masbate", "Sorsogon");
            await AddProvinces("reg6", "Aklan", "Antique", "Capiz", "Guimaras", "Iloilo", "Negros Occidental");
            await AddProvinces("reg7", "Bohol", "Cebu", "Negros Oriental", "Siquijor");
            await AddProvinces("reg8", "Biliran", "Eastern Samar", "Leyte", "Northern Samar", "Samar", "Southern Leyte");
            await AddProvinces("reg9", "Zamboanga del Norte", "Zamboanga del Sur", "Zamboanga Sibugay");
            await AddProvinces("reg10", "Bukidnon", "Camiguin", "Lanao del Norte", "Misamis Occidental", "Misamis Oriental");
            await AddProvinces("reg11", "Davao de Oro", "Davao del Norte", "Davao del Sur", "Davao Occidental", "Davao Oriental");
            await AddProvinces("reg12", "Cotabato", "Sarangani", "South Cotabato", "Sultan Kudarat");
            await AddProvinces("reg13", "Agusan del Norte", "Agusan del Sur", "Dinagat Islands", "Surigao del Norte", "Surigao del Sur");
            await AddProvinces("ncr", "Manila", "Quezon City", "Makati", "Pasig", "Taguig", "Caloocan", "Mandaluyong");
            await AddProvinces("car", "Abra", "Apayao", "Benguet", "Ifugao", "Kalinga", "Mountain Province");
            await AddProvinces("barmm", "Basilan", "Lanao del Sur", "Maguindanao del Norte", "Maguindanao del Sur", "Sulu", "Tawi-Tawi");

            await Shell.Current.DisplayAlert("Success", "Provinces added to Firestore.", "OK");
        }

    }
}
