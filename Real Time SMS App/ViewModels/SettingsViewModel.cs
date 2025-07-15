using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Google.Cloud.Firestore.V1;
using Microsoft.Maui.Controls;
using Real_Time_SMS_App.Models;
using Real_Time_SMS_App.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


namespace Real_Time_SMS_App.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        //constructor
        public SettingsViewModel()
        {
 
            // Load regions, provinces, and stations
            Regions = new ObservableCollection<string>();
            Provinces = new ObservableCollection<string>();
            Stations = new ObservableCollection<string>();
            Shifts = PreferencesHelper.LoadCollection<string>(shiftKey);
            Engines = PreferencesHelper.LoadCollection<string>(engineKey);
            _firestoreService = new FirestoreService();
            _ = InitializeAsync();

        }
        [RelayCommand]
        private async Task InitializeAsync()
        {
            await LoadRegionsAsync(); // Populate Regions
            await LoadSettings();     // Load preferences AFTER Regions are ready
        }
        public string ProvinceID { get; private set; }
        public string RegionID { get; private set; }
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
        private string selectedRegionName;  

        [ObservableProperty]
        private string selectedProvinceName;            
   
 
        [ObservableProperty]
        private string hotlineNumber;
        [ObservableProperty]
        private string stationEmail;
        [ObservableProperty]
        private string selectedStationName;
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
            Preferences.Set("settingsRegion", SelectedRegionName ?? string.Empty);
            Preferences.Set("settingsProvince", SelectedProvinceName ?? string.Empty);
            Preferences.Set("settingsHotline", HotlineNumber ?? string.Empty);
            Preferences.Set("settingsStation", SelectedStationName ?? string.Empty);
            Preferences.Set("settingsAddress", StationAddress ?? string.Empty);
            Preferences.Set("settingsEmail", StationEmail ?? string.Empty);
            //show dialog success after saving
            await Shell.Current.DisplayAlert("Settings Saved", "Your settings have been saved successfully.", "OK");
            // after saving settings, close this page
            await Shell.Current.GoToAsync("..");
        }
        //load settings
        [RelayCommand]
        private async Task LoadSettings()
        {
            // Get saved values first
            var savedRegion = Preferences.Get("settingsRegion", string.Empty);
            var savedProvince = Preferences.Get("settingsProvince", string.Empty);
            var savedStation = Preferences.Get("settingsStation", string.Empty);

            HotlineNumber = Preferences.Get("settingsHotline", string.Empty);
            StationAddress = Preferences.Get("settingsAddress", string.Empty);
            StationEmail = Preferences.Get("settingsEmail", string.Empty);

            // Set the region name first
            if (!string.IsNullOrWhiteSpace(savedRegion))
            {
                SelectedRegionName = savedRegion;

                // Load provinces and wait
                await LoadProvinces(savedRegion);

                // Now set province
                if (!string.IsNullOrWhiteSpace(savedProvince))
                    SelectedProvinceName = savedProvince;

                // Load stations and wait
                await LoadStations(savedProvince); // if this depends on province

                // Now set station
                if (!string.IsNullOrWhiteSpace(savedStation))
                    SelectedStationName = savedStation;
            }
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
                    if (!String.IsNullOrWhiteSpace(region.RegionName) && !Regions.Contains(region.RegionName))
                    {
                        Regions.Add(region.RegionName);
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to load regions: {ex.Message}", "OK");
            }
        }

        //load all provinces to the provinces collection by selected region id
        public async Task LoadProvinces(string regName)
        {
            // Clear the collection first to avoid duplicate entries
            Provinces.Clear();

            if (string.IsNullOrWhiteSpace(regName))
            {
                await Shell.Current.DisplayAlert("Error", "Region name is invalid or empty.", "OK");
                return;
            }

            var regionModel = await _firestoreService.GetDocumentByFieldAsync<RegionsModel>("Reg", "RegionName", regName);
            if (regionModel == null || string.IsNullOrWhiteSpace(regionModel.RegionId))
            {
                await Shell.Current.DisplayAlert("Error", $"No matching region found for '{regName}'.", "OK");
                return;
            }

            var regionID = regionModel.RegionId;

            var provinces = await _firestoreService.GetProvincesByRegionIdAsync(regionID);
            if (provinces == null || provinces.Count == 0)
            {
                await Shell.Current.DisplayAlert("No Provinces", $"No provinces found for region: {regionID}", "OK");
                return;
            }

            foreach (var province in provinces)
            {
                if (!string.IsNullOrWhiteSpace(province.ProvinceName) && !Provinces.Contains(province.ProvinceName))
                {
                    Provinces.Add(province.ProvinceName);
                }
            }

            // Optional: notify after loading
            
        }


        //onitem changed for selected region name
        partial void OnSelectedRegionNameChanged(string value)
        {


            if (!string.IsNullOrWhiteSpace(SelectedRegionName))
            {
                _ = LoadProvinces(value);
            }
            else
            {
                Provinces.Clear(); // Clear provinces if no region is selected
            }
        }
        [RelayCommand]

        //load all stations to the stations collection by selected province id
        private async Task LoadStations(string provinceName)
        {
            // Clear the collection first to avoid duplicate entries
            Stations.Clear();
            if (string.IsNullOrWhiteSpace(provinceName))
            {
                await Shell.Current.DisplayAlert("Error", "Province name is invalid or empty.", "OK");
                return;
            }
            var provinceModel = await _firestoreService.GetDocumentByFieldAsync<ProvincesModel>("Provinces", "ProvinceName", provinceName);
            if (provinceModel == null || string.IsNullOrWhiteSpace(provinceModel.ProvinceId))
            {
                await Shell.Current.DisplayAlert("Error", $"No matching province found for '{provinceName}'.", "OK");
                return;
            }
            var provinceID = provinceModel.ProvinceId;
            var stations = await _firestoreService.GetStationsByProvinceIdAsync(provinceID);
            if (stations == null || stations.Count == 0)
            {
                await Shell.Current.DisplayAlert("No Stations", $"No stations found for province: {provinceID}", "OK");
                return;
            }
            foreach (var station in stations)
            {
                if (!string.IsNullOrWhiteSpace(station.StationName) && !Stations.Contains(station.StationName))
                {
                    Stations.Add(station.StationName);
                }
            }
        }
        //method for onitem changed for selected province name
        partial void OnSelectedProvinceNameChanged(string value)
        {
            if (!string.IsNullOrWhiteSpace(SelectedProvinceName))
            {
                _ = LoadStations(value);
            }
            else
            {
                Stations.Clear(); // Clear stations if no province is selected
            }
        }
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
        [RelayCommand]
        public async Task AddStation()
        {
            var stations = new List<StationsModel>
            {
                new StationsModel { StationID = "aloranmfs", StationName = "Aloran Municipal Fire Station", ProvinceId = "MisOcc" },
                new StationsModel { StationID = "panaonmfs", StationName = "Panaon Municipal Fire Station", ProvinceId = "MisOcc" },                
                new StationsModel { StationID = "oroquietacfs", StationName = "Oroquieta City Fire Station", ProvinceId = "MisOcc" },
                new StationsModel { StationID = "ozamizcfs", StationName = "Ozamiz City Fire Station", ProvinceId = "MisOcc" },
                new StationsModel { StationID = "tangubcfs", StationName = "Tangub City Fire Station", ProvinceId = "MisOcc" },
                new StationsModel { StationID = "donvicmfs", StationName = "Don Victoriano Municipal Fire Station", ProvinceId = "MisOcc" },
                new StationsModel { StationID = "bonifaciomfs", StationName = "Bonifacio Municipal Fire Station", ProvinceId = "MisOcc" },
                new StationsModel { StationID = "clarinmfs", StationName = "Clarin Municipal Fire Station", ProvinceId = "MisOcc" },
                new StationsModel { StationID = "sinacabanmfs", StationName = "Sinacaban Municipal Fire Station", ProvinceId = "MisOcc" },
                new StationsModel { StationID = "jimenezmfs", StationName = "Jimenez Municipal Fire Station", ProvinceId = "MisOcc" },
                new StationsModel { StationID = "lopezjaenamfs", StationName = "Lopez Jaena Municipal Fire Station", ProvinceId = "MisOcc" },
                new StationsModel { StationID = "plaridelmfs", StationName = "Plaridel Municipal Fire Station", ProvinceId = "MisOcc" },
                new StationsModel { StationID = "calambamfs", StationName = "Calamba Municipal Fire Station", ProvinceId = "MisOcc" },
                new StationsModel { StationID = "sapangdalagamfs", StationName = "Sapang Dalaga Municipal Fire Station", ProvinceId = "MisOcc" },
                new StationsModel { StationID = "concepcionmfs", StationName = "Concepcion Municipal Fire Station", ProvinceId = "MisOcc" }

            };
            foreach(var stn in stations)
            {
                await _firestoreService.AddStationAsync(stn);
            }
        }
    }
}
