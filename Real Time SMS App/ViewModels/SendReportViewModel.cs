
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Real_Time_SMS_App.ViewModels
{
    public partial class SendReportViewModel : ObservableObject
    {
        public SendReportViewModel()
        {
            
            LoadGeneralInfo();
            LoadSpecificDetails();
            LoadOperationalStatus();
            LoadCasualtyList();
            LoadInvestigatorInfo();
            LoadShifts();
            LoadEngines();
        }
        private void LoadShifts()
        {
            Shifts = PreferencesHelper.LoadCollection<string>("SavedShifts");
            ShiftOnDuty = Shifts.FirstOrDefault() ?? string.Empty;
        }
        private void LoadEngines()
        {
            Engines = PreferencesHelper.LoadCollection<string>("SavedEngines");
            EngineName = Engines.FirstOrDefault() ?? string.Empty;
        }

        [ObservableProperty]
        private ObservableCollection<string> typeOfReport = new()
        {
            "Real time SMS Report",
            "Updated SMS Report",
            "Complete SMS Report"
        };
        [ObservableProperty]
        private string selectedReportType;
        [ObservableProperty]
        //Fire Station Name (FS)
        private string fireStationName;

        [ObservableProperty]
        private ObservableCollection<string> shifts;
        [ObservableProperty]
        private string shiftOnDuty;

        [ObservableProperty]
        private string engineName;
        [ObservableProperty]
        private ObservableCollection<string> engines;
        
        [ObservableProperty]
        private string districtProvince;
        [ObservableProperty]
        private ObservableCollection<string> region = new()
        {
            "Region I – Ilocos Region",
            "Region II – Cagayan Valley",
            "Region III – Central Luzon",
            "Region IV-A – CALABARZON",
            "MIMAROPA Region",
            "Region V – Bicol Region",
            "Region VI – Western Visayas",
            "Region VII – Central Visayas",
            "Region VIII – Eastern Visayas",
            "Region IX – Zamboanga Peninsula",
            "Region X – Northern Mindanao",
            "Region XI – Davao Region",
            "Region XII – SOCCSKSARGEN",
            "Region XIII – Caraga",
            "NCR – National Capital Region",
            "CAR – Cordillera Administrative Region",
            "BARMM – Bangsamoro Autonomous Region in Muslim Mindanao"
        };
        [ObservableProperty]
        private string selectedRegion;
        [ObservableProperty]
        private ObservableCollection<string> provinces = new();
        [ObservableProperty]
        private string selectedProvince;
        [ObservableProperty]
        private string stationAddress;
        //Incident Type (I)
        [ObservableProperty]
        private ObservableCollection<string> incidentTypes = new()
        {
            "Fire Incident",
            "Rescue",
            "Medical Emergency",
            "Hazardous Materials Incident",
            "Other"
        };
        [ObservableProperty]
        private string selectedIncidentType;
        //Place of Occurrence (PO)
        [ObservableProperty]
        private string placeOfOccurrence;
        [ObservableProperty]
        //Date and Time Reported (DTR) dd HHmmH MMMM yyyy
        private DateTime dateReported = DateTime.Now;
        [ObservableProperty]
        private TimeSpan timeReported = DateTime.Now.TimeOfDay;
        [ObservableProperty]
        private string dateTimeReported;
        //date and time engine dispatched (TED)
        [ObservableProperty]
        private DateTime dateEngineDispatched = DateTime.Now;
        [ObservableProperty]
        private TimeSpan timeEngineDispatched = DateTime.Now.TimeOfDay;
        //date and time engine arrived at Scene(TAS)
        [ObservableProperty]
        private string dateTimeEngineDispatched;
        [ObservableProperty]
        private DateTime dateEngineArrived = DateTime.Now;
        [ObservableProperty]
        private TimeSpan timeEngineArrived = DateTime.Now.TimeOfDay;
        [ObservableProperty]
        private string dateTimeEngineArrived;
        //response time (RT)
        [ObservableProperty]
        private string responseTime;
        //Distance to Incident Location (DIST)
        [ObservableProperty]
        private string distanceToIncidentLocation;
        //Date and time a Late reported incident occured:
        [ObservableProperty]
        private DateTime dateIncidentOccurred = DateTime.Now;
        [ObservableProperty]
        private TimeSpan timeIncidentOccurred = DateTime.Now.TimeOfDay;

        //other tab specific details

        //involved/structure
        [ObservableProperty]
        private string involved;
        //name of owner/occupant
        [ObservableProperty]
        private string nameOfOwnerOccupant;
        //number of families affected
        [ObservableProperty]
        private string numberOfFamiliesAffected;
        //number of individuals affected
        [ObservableProperty]
        private string numberOfIndividualsAffected;
        //number of structures burned
        [ObservableProperty]
        private string numberOfStructuresBurned;
        //affected estimated floor area(FA) (sqm)
        [ObservableProperty]
        private string affectedEstimatedFloorArea;
        //responders
        [ObservableProperty]
        private string fireTrucksResponding;
        [ObservableProperty]
        private string ambulancesResponding;
        [ObservableProperty]
        private string auxiliaryVehiclesResponding;

        //operational details tab
        //operational status (status)
        [ObservableProperty]
        private string firstAlarmDeclaredBy;
        [ObservableProperty]
        private TimeSpan firstAlarmTime;

        [ObservableProperty]
        private string secondAlarmDeclaredBy;
        [ObservableProperty]
        private TimeSpan secondAlarmTime;

        [ObservableProperty]
        private string thirdAlarmDeclaredBy;
        [ObservableProperty]
        private TimeSpan thirdAlarmTime;

        [ObservableProperty]
        private string fourthAlarmDeclaredBy;
        [ObservableProperty]
        private TimeSpan fourthAlarmTime;

        [ObservableProperty]
        private string taskForceAlphaDeclaredBy;
        [ObservableProperty]
        private TimeSpan taskForceAlphaTime;

        [ObservableProperty]
        private string taskForceBravoDeclaredBy;
        [ObservableProperty]
        private TimeSpan taskForceBravoTime;

        [ObservableProperty]
        private string generalAlarmDeclaredBy;
        [ObservableProperty]
        private TimeSpan generalAlarmTime;
        [ObservableProperty]
        private string underControlDeclaredBy;
        [ObservableProperty]
        private TimeSpan underControlTime;
        [ObservableProperty]
        private string fireOutDeclaredBy;
        [ObservableProperty]
        private TimeSpan fireOutlTime;

        [ObservableProperty] private bool firstAlarmChecked;
        [ObservableProperty] private bool secondAlarmChecked;
        [ObservableProperty] private bool thirdAlarmChecked;
        [ObservableProperty] private bool fourthAlarmChecked;
        [ObservableProperty] private bool taskForceAlphaAlarmChecked;
        [ObservableProperty] private bool taskForceBravoAlarmChecked;
        [ObservableProperty] private bool generalAlarmChecked;
        [ObservableProperty] private bool underControlChecked;
        [ObservableProperty] private bool fireOutChecked;

        // casualty details tab
        [ObservableProperty]
        private string numberOfCasualties;
        [ObservableProperty]
        private string numberOfInjured;
        [ObservableProperty]
        private string numberOfFatalities;
        [ObservableProperty]
        private string numberOfMissing;
        [ObservableProperty]
        private ObservableCollection<string> casualtyType = new()
        {
            "Injured","Fatality","Missing"
        };
        [ObservableProperty]
        private string selectedCasualtyType;
        [ObservableProperty]
        private string casualtyName;
        [ObservableProperty]
        private string casualtyAge;
        [ObservableProperty]
        private string casualtyCause;
        [ObservableProperty]
        private string casualtyPerson;//BFP,Civilian,Auxiliary
        public ObservableCollection<Casualty> CasualtyList { get; } = new();
        public ObservableCollection<CasualtyGroup> GroupedCasualtyList { get; } = new();

        //investigator's info tab
        [ObservableProperty]
        private string investigatorICP;
        [ObservableProperty]
        private string investigatorSourceContact;
        [ObservableProperty]
        private string investigatorFCOS;
        [ObservableProperty]
        private string otherRelevantInformation;


        private async Task OpenPage(string pageName)
        {
            try
            {
                await Shell.Current.GoToAsync(pageName);
            }
            catch (Exception ex)
            {
                // Log or display error
                await Shell.Current.DisplayAlert("Navigation Error", ex.Message, "OK");

            }
        }
        [RelayCommand]
        private async Task GeneralInfoPage()
        {
            LoadGeneralInfo();
            await OpenPage(nameof(GeneralInfoPage));
        }
        [RelayCommand]
        private async Task SpecificDetailsPage()
        {
            await OpenPage(nameof(SpecificDetailsPage));
        }
        [RelayCommand]
        private async Task OperationalDetailsPage()
        {
            await OpenPage(nameof(OperationalDetailsPage));
        }
        [RelayCommand]
        private async Task CasualtyDetailsPage()
        {
            await OpenPage(nameof(CasualtyDetailsPage));
        }
        [RelayCommand]
        private async Task InvestigatorInfoPage()
        {
            await OpenPage(nameof(InvestigatorInfoPage));
        }
        [RelayCommand]
        private async Task LaunchSMSApp()
        {
            List<string> settings = new List<string>();
            settings.Add(Preferences.Get("settingsHotline",string.Empty));
            settings.Add(Preferences.Get("settingsStation", string.Empty));
            settings.Add(Preferences.Get("settingsAddress", string.Empty));
            settings.Add(Preferences.Get("settingsEmail", string.Empty));

            if(settings.Any(s => string.IsNullOrWhiteSpace(s)))
            {
                await Shell.Current.DisplayAlert("Warning", "Please set your station settings before sending SMS.", "OK");
                return;
            }
            else
            {
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

                    var uri = new Uri($"sms:{string.Join(";", phoneNumbers)}?body={Uri.EscapeDataString(CompileSMSReport())}");
                    await Launcher.Default.OpenAsync(uri);
                    this.PushReportToFirebase();
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Error", "Failed to send SMS: " + ex.Message, "OK");
                }
            }

            //await Shell.Current.DisplayAlert("Info", CompileSMSReport(), "OK");
        }
        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        private async Task BackToMain()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        //---------------------save general info---------------------//
        [RelayCommand]
        private async Task SaveGeneralInfo()
        {
            Preferences.Set(nameof(SelectedReportType), SelectedReportType ?? string.Empty);
            //Preferences.Set(nameof(FireStationName), FireStationName ?? string.Empty);
            Preferences.Set(nameof(EngineName), EngineName ?? string.Empty);
            Preferences.Set(nameof(ShiftOnDuty), ShiftOnDuty ?? string.Empty);
            //Preferences.Set(nameof(StationAddress), StationAddress ?? string.Empty);
            Preferences.Set(nameof(SelectedProvince), SelectedProvince ?? string.Empty);
            Preferences.Set(nameof(SelectedRegion), SelectedRegion ?? string.Empty);
            Preferences.Set(nameof(SelectedIncidentType), SelectedIncidentType ?? string.Empty);
            Preferences.Set(nameof(PlaceOfOccurrence), PlaceOfOccurrence ?? string.Empty);
            Preferences.Set(nameof(DistanceToIncidentLocation), DistanceToIncidentLocation ?? string.Empty);
            Preferences.Set(nameof(ResponseTime), ResponseTime ?? string.Empty);
            await Shell.Current.DisplayAlert("Info", "General Info saved.\n\nProceed to Step 2", "OK");
            await Shell.Current.GoToAsync("..");
        }
        //---------------------load general info---------------------//
        private void LoadGeneralInfo()
        {
            SelectedReportType = Preferences.Get(nameof(SelectedReportType), "Real time SMS");
            FireStationName = Preferences.Get("settingsStation", string.Empty);
            EngineName = Preferences.Get(nameof(EngineName), string.Empty);
            ShiftOnDuty = Preferences.Get(nameof(ShiftOnDuty), string.Empty);
            StationAddress = Preferences.Get("settingsAddress", string.Empty);
            SelectedProvince = Preferences.Get(nameof(SelectedProvince), string.Empty);
            SelectedRegion = Preferences.Get(nameof(SelectedRegion), string.Empty);
            SelectedIncidentType = Preferences.Get(nameof(SelectedIncidentType), string.Empty);
            PlaceOfOccurrence = Preferences.Get(nameof(PlaceOfOccurrence), string.Empty);
            DistanceToIncidentLocation = Preferences.Get(nameof(DistanceToIncidentLocation), string.Empty);

            // Load DateTime and TimeSpan from ticks
            DateTimeReported = Preferences.Get(nameof(DateTimeReported), string.Empty);
            DateTimeEngineDispatched = Preferences.Get(nameof(DateTimeEngineDispatched), string.Empty);
            DateTimeEngineArrived = Preferences.Get(nameof(DateTimeEngineArrived), string.Empty);
            ResponseTime = Preferences.Get(nameof(ResponseTime), string.Empty);
        }
        //------------------when region is selected----------------------//
        partial void OnSelectedRegionChanged(string value)
        {
            Provinces.Clear();

            if (string.IsNullOrWhiteSpace(value))
                return;

            switch (value)
            {
                case "Region I – Ilocos Region":
                    AddProvinces("Ilocos Norte", "Ilocos Sur", "La Union", "Pangasinan");
                    break;
                case "Region II – Cagayan Valley":
                    AddProvinces("Batanes", "Cagayan", "Isabela", "Nueva Vizcaya", "Quirino");
                    break;
                case "Region III – Central Luzon":
                    AddProvinces("Aurora", "Bataan", "Bulacan", "Nueva Ecija", "Pampanga", "Tarlac", "Zambales");
                    break;
                case "Region IV-A – CALABARZON":
                    AddProvinces("Cavite", "Laguna", "Batangas", "Rizal", "Quezon");
                    break;
                case "MIMAROPA Region":
                    AddProvinces("Marinduque", "Occidental Mindoro", "Oriental Mindoro", "Palawan", "Romblon");
                    break;
                case "Region V – Bicol Region":
                    AddProvinces("Albay", "Camarines Norte", "Camarines Sur", "Catanduanes", "Masbate", "Sorsogon");
                    break;
                case "Region VI – Western Visayas":
                    AddProvinces("Aklan", "Antique", "Capiz", "Guimaras", "Iloilo", "Negros Occidental");
                    break;
                case "Region VII – Central Visayas":
                    AddProvinces("Bohol", "Cebu", "Negros Oriental", "Siquijor");
                    break;
                case "Region VIII – Eastern Visayas":
                    AddProvinces("Biliran", "Eastern Samar", "Leyte", "Northern Samar", "Samar", "Southern Leyte");
                    break;
                case "Region IX – Zamboanga Peninsula":
                    AddProvinces("Zamboanga del Norte", "Zamboanga del Sur", "Zamboanga Sibugay");
                    break;
                case "Region X – Northern Mindanao":
                    AddProvinces("Bukidnon", "Camiguin", "Lanao del Norte", "Misamis Occidental", "Misamis Oriental");
                    break;
                case "Region XI – Davao Region":
                    AddProvinces("Davao de Oro", "Davao del Norte", "Davao del Sur", "Davao Occidental", "Davao Oriental");
                    break;
                case "Region XII – SOCCSKSARGEN":
                    AddProvinces("Cotabato", "Sarangani", "South Cotabato", "Sultan Kudarat");
                    break;
                case "Region XIII – Caraga":
                    AddProvinces("Agusan del Norte", "Agusan del Sur", "Dinagat Islands", "Surigao del Norte", "Surigao del Sur");
                    break;
                case "NCR – National Capital Region":
                    AddProvinces("Manila", "Quezon City", "Makati", "Pasig", "Taguig", "Caloocan", "Mandaluyong", "Others...");
                    break;
                case "CAR – Cordillera Administrative Region":
                    AddProvinces("Abra", "Apayao", "Benguet", "Ifugao", "Kalinga", "Mountain Province");
                    break;
                case "BARMM – Bangsamoro Autonomous Region in Muslim Mindanao":
                    AddProvinces("Basilan", "Lanao del Sur", "Maguindanao del Norte", "Maguindanao del Sur", "Sulu", "Tawi-Tawi");
                    break;
                default:
                    break;
            }
        }

        private void AddProvinces(params string[] items)
        {
            foreach (var item in items)
                Provinces.Add(item);
        }

        //----------------capture date time reported---------------------//
        [RelayCommand]
        private async Task CaptureDateTimeReported()
        {
            // Combine date and time into a single DateTime object
            var combinedDateTime = DateReported.Date + TimeReported;
            DateTimeReported = combinedDateTime.ToString("dd HHmm'H' MMMM yyyy");
            Preferences.Set(nameof(DateTimeReported), DateTimeReported ?? string.Empty);
            await Shell.Current.DisplayAlert("Info", "Date and Time Reported captured.", "OK");
        }
        //----------------capture date time engine dispatched---------------------//
        private DateTime _combinedDateTimeDis = DateTime.Now;
        private DateTime _combinedDateTimeArr = DateTime.Now;
        [RelayCommand]
        private async Task CaptureDateTimeEngineDispatched()
        {
            // Combine date and time into a single DateTime object
            _combinedDateTimeDis = DateEngineDispatched.Date + TimeEngineDispatched;
            DateTimeEngineDispatched = _combinedDateTimeDis.ToString("dd HHmm'H' MMMM yyyy");
            ComputeResponseTime();
            Preferences.Set(nameof(DateTimeEngineDispatched), DateTimeEngineDispatched ?? string.Empty);
            await Shell.Current.DisplayAlert("Info", "Date and Time Engine Dispatched captured.", "OK");
        }
        //----------------capture date time engine arrived---------------------//
        [RelayCommand]
        private async Task CaptureDateTimeEngineArrived()
        {
            // Combine date and time into a single DateTime object
            _combinedDateTimeArr = DateEngineArrived.Date + TimeEngineArrived;
            DateTimeEngineArrived = _combinedDateTimeArr.ToString("dd HHmm'H' MMMM yyyy");
            ComputeResponseTime();
            Preferences.Set(nameof(DateTimeEngineArrived), DateTimeEngineArrived ?? string.Empty);
            await Shell.Current.DisplayAlert("Info", "Date and Time Engine Arrived captured.", "OK");

        }
        private void ComputeResponseTime()
        {
            if (_combinedDateTimeArr != null && _combinedDateTimeDis != null)
            {
                var dispatched = _combinedDateTimeDis;
                var arrived = _combinedDateTimeArr;

                if (arrived < dispatched)
                {
                    ResponseTime = "Invalid times: Arrived time cannot be before Dispatched time.";
                    return;
                }

                var timeSpan = arrived - dispatched;

                int totalMinutes = (int)timeSpan.TotalMinutes;
                int days = totalMinutes / (24 * 60);
                int hours = (totalMinutes % (24 * 60)) / 60;
                int minutes = totalMinutes % 60;

                List<string> parts = new();

                if (days > 0)
                    parts.Add($"{days} {(days == 1 ? "day" : "days")}");
                if (hours > 0)
                    parts.Add($"{hours} {(hours == 1 ? "hour" : "hours")}");
                if (minutes > 0 || parts.Count == 0) // show minutes if no other unit exists
                    parts.Add($"{minutes} {(minutes == 1 ? "minute" : "minutes")}");

                ResponseTime = string.Join(" ", parts);
            }
            else
            {
                ResponseTime = "N/A";
            }
        }


        //------------------------save specific details------------------------//
        [RelayCommand]
        private async Task SaveSpecificDetails()
        {
            Preferences.Set(nameof(Involved), Involved ?? string.Empty);
            Preferences.Set(nameof(NameOfOwnerOccupant), NameOfOwnerOccupant ?? string.Empty);
            Preferences.Set(nameof(NumberOfFamiliesAffected), NumberOfFamiliesAffected ?? string.Empty);
            Preferences.Set(nameof(NumberOfIndividualsAffected), NumberOfIndividualsAffected ?? string.Empty);
            Preferences.Set(nameof(NumberOfStructuresBurned), NumberOfStructuresBurned ?? string.Empty);
            Preferences.Set(nameof(AffectedEstimatedFloorArea), AffectedEstimatedFloorArea ?? string.Empty);
            Preferences.Set(nameof(FireTrucksResponding), FireTrucksResponding ?? string.Empty);
            Preferences.Set(nameof(AmbulancesResponding), AmbulancesResponding ?? string.Empty);
            Preferences.Set(nameof(AuxiliaryVehiclesResponding), AuxiliaryVehiclesResponding ?? string.Empty);

            await Shell.Current.DisplayAlert("Info", "Specific details saved.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        //------------------------load specific details------------------------//
        private void LoadSpecificDetails()
        {
            Involved = Preferences.Get(nameof(Involved), string.Empty);
            NameOfOwnerOccupant = Preferences.Get(nameof(NameOfOwnerOccupant), string.Empty);
            NumberOfFamiliesAffected = Preferences.Get(nameof(NumberOfFamiliesAffected), string.Empty);
            NumberOfIndividualsAffected = Preferences.Get(nameof(NumberOfIndividualsAffected), string.Empty);
            NumberOfStructuresBurned = Preferences.Get(nameof(NumberOfStructuresBurned), string.Empty);
            AffectedEstimatedFloorArea = Preferences.Get(nameof(AffectedEstimatedFloorArea), string.Empty);
            FireTrucksResponding = Preferences.Get(nameof(FireTrucksResponding), string.Empty);
            AmbulancesResponding = Preferences.Get(nameof(AmbulancesResponding), string.Empty);
            AuxiliaryVehiclesResponding = Preferences.Get(nameof(AuxiliaryVehiclesResponding), string.Empty);
        }

        //------------------------save operational details------------------------//
        [RelayCommand]
        private async Task SaveOperationalStatus()
        {


            // Save only if checked
            if (FirstAlarmChecked)
            {
                Preferences.Set(nameof(FirstAlarmDeclaredBy), FirstAlarmDeclaredBy ?? string.Empty);
                Preferences.Set(nameof(FirstAlarmTime), FirstAlarmTime.ToString());
                Preferences.Set(nameof(FirstAlarmChecked), true);
            }

            if (SecondAlarmChecked)
            {
                Preferences.Set(nameof(SecondAlarmDeclaredBy), SecondAlarmDeclaredBy ?? string.Empty);
                Preferences.Set(nameof(SecondAlarmTime), SecondAlarmTime.ToString());
                Preferences.Set(nameof(SecondAlarmChecked), true);
            }

            if (ThirdAlarmChecked)
            {
                Preferences.Set(nameof(ThirdAlarmDeclaredBy), ThirdAlarmDeclaredBy ?? string.Empty);
                Preferences.Set(nameof(ThirdAlarmTime), ThirdAlarmTime.ToString());
                Preferences.Set(nameof(ThirdAlarmChecked), true);
            }

            if (FourthAlarmChecked)
            {
                Preferences.Set(nameof(FourthAlarmDeclaredBy), FourthAlarmDeclaredBy ?? string.Empty);
                Preferences.Set(nameof(FourthAlarmTime), FourthAlarmTime.ToString());
                Preferences.Set(nameof(FourthAlarmChecked), true);
            }

            if (TaskForceAlphaAlarmChecked)
            {
                Preferences.Set(nameof(TaskForceAlphaDeclaredBy), TaskForceAlphaDeclaredBy ?? string.Empty);
                Preferences.Set(nameof(TaskForceAlphaTime), TaskForceAlphaTime.ToString());
                Preferences.Set(nameof(TaskForceAlphaAlarmChecked), true);
            }

            if (TaskForceBravoAlarmChecked)
            {
                Preferences.Set(nameof(TaskForceBravoDeclaredBy), TaskForceBravoDeclaredBy ?? string.Empty);
                Preferences.Set(nameof(TaskForceBravoTime), TaskForceBravoTime.ToString());
                Preferences.Set(nameof(TaskForceBravoAlarmChecked), true);
            }

            if (GeneralAlarmChecked)
            {
                Preferences.Set(nameof(GeneralAlarmDeclaredBy), GeneralAlarmDeclaredBy ?? string.Empty);
                Preferences.Set(nameof(GeneralAlarmTime), GeneralAlarmTime.ToString());
                Preferences.Set(nameof(GeneralAlarmChecked), true);
            }
            if (UnderControlChecked)
            {
                Preferences.Set(nameof(UnderControlDeclaredBy), UnderControlDeclaredBy ?? string.Empty);
                Preferences.Set(nameof(UnderControlTime), UnderControlTime.ToString());
                Preferences.Set(nameof(UnderControlChecked), true);
            }
            if(FireOutChecked)
            {
                Preferences.Set(nameof(FireOutDeclaredBy), FireOutDeclaredBy ?? string.Empty);
                Preferences.Set(nameof(FireOutlTime), FireOutlTime.ToString());
                Preferences.Set(nameof(FireOutChecked), true);
            }

            await Shell.Current.DisplayAlert("Success", "Operational status saved.", "OK");
        }


        //------------------------load operational details------------------------//
        private void LoadOperationalStatus()
        {
            FirstAlarmChecked = Preferences.Get(nameof(FirstAlarmChecked), false);
            if (FirstAlarmChecked)
            {
                FirstAlarmDeclaredBy = Preferences.Get(nameof(FirstAlarmDeclaredBy), string.Empty);
                FirstAlarmTime = TimeSpan.TryParse(Preferences.Get(nameof(FirstAlarmTime), ""), out var t1) ? t1 : TimeSpan.Zero;
            }

            SecondAlarmChecked = Preferences.Get(nameof(SecondAlarmChecked), false);
            if (SecondAlarmChecked)
            {
                SecondAlarmDeclaredBy = Preferences.Get(nameof(SecondAlarmDeclaredBy), string.Empty);
                SecondAlarmTime = TimeSpan.TryParse(Preferences.Get(nameof(SecondAlarmTime), ""), out var t2) ? t2 : TimeSpan.Zero;
            }

            ThirdAlarmChecked = Preferences.Get(nameof(ThirdAlarmChecked), false);
            if (ThirdAlarmChecked)
            {
                ThirdAlarmDeclaredBy = Preferences.Get(nameof(ThirdAlarmDeclaredBy), string.Empty);
                ThirdAlarmTime = TimeSpan.TryParse(Preferences.Get(nameof(ThirdAlarmTime), ""), out var t3) ? t3 : TimeSpan.Zero;
            }

            FourthAlarmChecked = Preferences.Get(nameof(FourthAlarmChecked), false);
            if (FourthAlarmChecked)
            {
                FourthAlarmDeclaredBy = Preferences.Get(nameof(FourthAlarmDeclaredBy), string.Empty);
                FourthAlarmTime = TimeSpan.TryParse(Preferences.Get(nameof(FourthAlarmTime), ""), out var t4) ? t4 : TimeSpan.Zero;
            }

            TaskForceAlphaAlarmChecked = Preferences.Get(nameof(TaskForceAlphaAlarmChecked), false);
            if (TaskForceAlphaAlarmChecked)
            {
                TaskForceAlphaDeclaredBy = Preferences.Get(nameof(TaskForceAlphaDeclaredBy), string.Empty);
                TaskForceAlphaTime = TimeSpan.TryParse(Preferences.Get(nameof(TaskForceAlphaTime), ""), out var ta) ? ta : TimeSpan.Zero;
            }

            TaskForceBravoAlarmChecked = Preferences.Get(nameof(TaskForceBravoAlarmChecked), false);
            if (TaskForceBravoAlarmChecked)
            {
                TaskForceBravoDeclaredBy = Preferences.Get(nameof(TaskForceBravoDeclaredBy), string.Empty);
                TaskForceBravoTime = TimeSpan.TryParse(Preferences.Get(nameof(TaskForceBravoTime), ""), out var tb) ? tb : TimeSpan.Zero;
            }

            GeneralAlarmChecked = Preferences.Get(nameof(GeneralAlarmChecked), false);
            if (GeneralAlarmChecked)
            {
                GeneralAlarmDeclaredBy = Preferences.Get(nameof(GeneralAlarmDeclaredBy), string.Empty);
                GeneralAlarmTime = TimeSpan.TryParse(Preferences.Get(nameof(GeneralAlarmTime), ""), out var tg) ? tg : TimeSpan.Zero;
            }
            UnderControlChecked = Preferences.Get(nameof(UnderControlChecked), false);
            if (UnderControlChecked)
            {
                UnderControlDeclaredBy = Preferences.Get(nameof(UnderControlDeclaredBy), string.Empty);
                UnderControlTime = TimeSpan.TryParse(Preferences.Get(nameof(UnderControlTime), ""), out var tu) ? tu : TimeSpan.Zero;
            }
            FireOutChecked = Preferences.Get(nameof(FireOutChecked), false);
            if (FireOutChecked)
            {
                FireOutDeclaredBy = Preferences.Get(nameof(FireOutDeclaredBy), string.Empty);
                FireOutlTime = TimeSpan.TryParse(Preferences.Get(nameof(FireOutlTime), ""), out var tf) ? tf : TimeSpan.Zero;
            }
        }

        //----------------------add casualty----------------------//
        [RelayCommand]
        private void AddToCasualtyList()
        {

            if (!int.TryParse(CasualtyAge, out int age))
                age = 0; // Or show validation error

            var newEntry = new Casualty
            {
                Type = SelectedCasualtyType,
                Name = CasualtyName,
                Age = age,
                Person = CasualtyPerson,
                Cause = CasualtyCause
            };

            CasualtyList.Add(newEntry);
            GroupCasualties();
            // Optional: Clear fields after add
            CasualtyName = string.Empty;
            CasualtyAge = string.Empty;
            CasualtyCause = string.Empty;
            CasualtyPerson = string.Empty;
            SelectedCasualtyType = null;
        }
        //----------------------group casualties----------------------//
        private void GroupCasualties()
        {
            var grouped = CasualtyList
                .GroupBy(c => c.Type)
                .OrderBy(g => g.Key)
                .Select(g => new CasualtyGroup(g.Key, g))
                .ToList();

            GroupedCasualtyList.Clear();
            foreach (var group in grouped)
                GroupedCasualtyList.Add(group);
        }
        //----------------------clear casualty list----------------------//
        [RelayCommand]
        private void ClearCasualtyList()
        {
            CasualtyList.Clear();
            GroupedCasualtyList.Clear();
            CasualtyName = string.Empty;
            CasualtyAge = string.Empty;
            CasualtyCause = string.Empty;
            CasualtyPerson = string.Empty;
            SelectedCasualtyType = null;
            GroupCasualties();
        }

        //-------------saving casualty details-----------------//
        [RelayCommand]
        public async Task SaveCasualtyList()
        {
            // Generate the grouped summary
            string summary = GetCasualtyListSummary();

            // Show confirmation alert with Yes/No buttons
            bool confirm = await Shell.Current.DisplayAlert("Save Casualty List?", summary, "Yes", "No");

            if (confirm)
            {
                // User chose 'Yes' - proceed to save
                var json = JsonSerializer.Serialize(CasualtyList);
                Preferences.Set("CasualtyList", json);

                await Shell.Current.DisplayAlert("Info","Casualty list saved successfully.", "OK");
                Back();
            }
            else
            {
                // User chose 'No' - do nothing
                System.Diagnostics.Debug.WriteLine("Save canceled.");
            }
        }

        //----------------------get casualty list from preference----------------------//
        public string GetCasualtyListSummary()
        {
            var sb = new StringBuilder();

            var grouped = CasualtyList
                .GroupBy(c => c.Type)
                .OrderBy(g => g.Key);

            foreach (var group in grouped)
            {
                sb.AppendLine($"{group.Key}: {group.Count()}");

                foreach (var casualty in group)
                {
                    sb.AppendLine($"{casualty.Name}, {casualty.Age}, {casualty.Cause}, {casualty.Person}");
                }

                sb.AppendLine();
            }

            return sb.ToString().TrimEnd();
        }
        //----------------------load casualty list from preference----------------------//
        public void LoadCasualtyList()
        {
            // Retrieve JSON from Preferences
            var json = Preferences.Get("CasualtyList", string.Empty);

            if (!string.IsNullOrWhiteSpace(json))
            {
                try
                {
                    // Deserialize to a list of Casualty objects
                    var list = JsonSerializer.Deserialize<List<Casualty>>(json);

                    // Clear existing list and repopulate
                    CasualtyList.Clear();
                    foreach (var item in list)
                        CasualtyList.Add(item);

                    // Regroup after loading
                    GroupCasualties();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error loading casualties: {ex.Message}");
                }
            }
        }

        //----------------------save investigator info----------------------//
        [RelayCommand]
        public async Task SaveInvestigatorInfo()
        {
            Preferences.Set(nameof(InvestigatorICP), InvestigatorICP ?? string.Empty);
            Preferences.Set(nameof(InvestigatorSourceContact), InvestigatorSourceContact ?? string.Empty);
            Preferences.Set(nameof(InvestigatorFCOS), InvestigatorFCOS ?? string.Empty);
            Preferences.Set(nameof(OtherRelevantInformation), OtherRelevantInformation ?? string.Empty);
            await Shell.Current.DisplayAlert("Info", "Investigator info saved.", "OK");
            Back();
        }
        //----------------------load investigator info----------------------//
        public void LoadInvestigatorInfo()
        {
            InvestigatorICP = Preferences.Get(nameof(InvestigatorICP), string.Empty);
            InvestigatorSourceContact = Preferences.Get(nameof(InvestigatorSourceContact), string.Empty);
            InvestigatorFCOS = Preferences.Get(nameof(InvestigatorFCOS), string.Empty);
            OtherRelevantInformation = Preferences.Get(nameof(OtherRelevantInformation), string.Empty);
        }

        //------------------------compile sms report------------------------//
        private string CompileSMSReport()
        {
            LoadGeneralInfo();
            LoadSpecificDetails();
            LoadOperationalStatus();
            LoadCasualtyList();
            LoadInvestigatorInfo();

            var sb = new StringBuilder();
            sb.AppendLine($"{SelectedReportType}");
            sb.AppendLine($"FS: {EngineName},{ShiftOnDuty},{FireStationName},{StationAddress},{SelectedProvince},{SelectedRegion}");
            sb.AppendLine($"IPO: {SelectedIncidentType} @ {PlaceOfOccurrence}");
            sb.AppendLine($"DTR: {DateTimeReported}");
            sb.AppendLine($"TED: {DateTimeEngineDispatched}");
            sb.AppendLine($"TAS: {DateTimeEngineArrived}");
            sb.AppendLine($"RT: {ResponseTime}");
            sb.AppendLine($"DIST: {DistanceToIncidentLocation}");
            sb.AppendLine($"Involved: {Involved}");
            sb.AppendLine($"Owner/Occupant: {NameOfOwnerOccupant}");
            sb.AppendLine($"Families: {NumberOfFamiliesAffected}");
            sb.AppendLine($"Individuals: {NumberOfIndividualsAffected}");
            sb.AppendLine($"Structures Burned: {NumberOfStructuresBurned}");
            sb.AppendLine($"FA: {AffectedEstimatedFloorArea} sqm");
            sb.AppendLine($"Responders:");
            sb.AppendLine($"BFP FT: {FireTrucksResponding}");
            sb.AppendLine($"Ambu: {AmbulancesResponding}");
            sb.AppendLine($"Auxiliary: {AuxiliaryVehiclesResponding}");
            sb.AppendLine($"Operational Status:");
            if (FirstAlarmChecked)
                sb.AppendLine($"{DateTime.Today.Add(FirstAlarmTime):HHmm}H - 1st Alarm by {FirstAlarmDeclaredBy}");

            if (SecondAlarmChecked)
                sb.AppendLine($"{DateTime.Today.Add(SecondAlarmTime):HHmm}H - 2nd Alarm by {SecondAlarmDeclaredBy}");

            if (ThirdAlarmChecked)
                sb.AppendLine($"{DateTime.Today.Add(ThirdAlarmTime):HHmm}H - 3rd Alarm by {ThirdAlarmDeclaredBy}");

            if (FourthAlarmChecked)
                sb.AppendLine($"{DateTime.Today.Add(FourthAlarmTime):HHmm}H - 4th Alarm by {FourthAlarmDeclaredBy}");

            if (TaskForceAlphaAlarmChecked)
                sb.AppendLine($"{DateTime.Today.Add(TaskForceAlphaTime):HHmm}H - Task Force Alpha by {TaskForceAlphaDeclaredBy}");

            if (TaskForceBravoAlarmChecked)
                sb.AppendLine($"{DateTime.Today.Add(TaskForceBravoTime):HHmm}H - Task Force Bravo by {TaskForceBravoDeclaredBy}");

            if (GeneralAlarmChecked)
                sb.AppendLine($"{DateTime.Today.Add(GeneralAlarmTime):HHmm}H - General Alarm by {GeneralAlarmDeclaredBy}");

            if (!string.IsNullOrWhiteSpace(UnderControlDeclaredBy))
                sb.AppendLine($"{DateTime.Today.Add(UnderControlTime):HHmm}H - Under Control by {UnderControlDeclaredBy}");

            if (!string.IsNullOrWhiteSpace(FireOutDeclaredBy))
                sb.AppendLine($"{DateTime.Today.Add(FireOutlTime):HHmm}H - Fire Out by {FireOutDeclaredBy}");

            sb.AppendLine($"Casualties: {GetCasualtyListSummary()}");
            sb.AppendLine($"ICP: {InvestigatorICP}");
            sb.AppendLine($"Source Contact: {InvestigatorSourceContact}");
            sb.AppendLine($"FCOS: {InvestigatorFCOS}");
            sb.AppendLine($"Other Info: {OtherRelevantInformation}");

            return sb.ToString();
        }
        private async Task<List<Contact>> LoadContactsAsync()
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, "contacts.json");

            if (!File.Exists(filePath))
                return new List<Contact>();

            var json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<Contact>>(json) ?? new List<Contact>();
        }
        private async void PushReportToFirebase()
        {
            var smsData = new SmsReport
            {
                StationName = Preferences.Get("settingsStation", string.Empty),
                HotlineNumber = Preferences.Get("settingsHotline", string.Empty),
                StationEmail = Preferences.Get("settingsEmail", string.Empty),
                SMSContent = CompileSMSReport()
            };

            try
            {
                var firebaseService = new FirebaseService();
                await firebaseService.PushSmsReportAsync(smsData);
                System.Diagnostics.Debug.WriteLine("Report pushed successfully.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error pushing report: {ex.Message}");
            }
        }


    }
}
