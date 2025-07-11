
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Real_Time_SMS_App.ViewModels
{
    public partial class SendReportViewModel : ObservableObject
    {
        public SendReportViewModel()
        {
            // Load saved general info when the view model is initialized
            LoadGeneralInfo();
            LoadSpecificDetails();
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
        private string engineName;
        [ObservableProperty]
        private string shiftOnDuty;
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
        private string firstAlarmStatus;
        [ObservableProperty]
        private string secondAlarmStatus;
        [ObservableProperty]
        private string thirdAlarmStatus;
        [ObservableProperty]
        private string fourthAlarmStatus;
        [ObservableProperty]
        private string taskForceAlphaStatus;
        [ObservableProperty]
        private string taskForceBravoStatus;
        [ObservableProperty]
        private string generalAlarmStatus;

        // casualty details tab
        [ObservableProperty]
        private string numberOfCasualties;
        [ObservableProperty]
        private string numberOfInjured;
        [ObservableProperty]
        private string numberOfFatalities;
        [ObservableProperty]
        private string numberOfMissing;

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
            //await OpenPage(nameof(null));
        }
        [RelayCommand]
        private async Task CasualtyDetailsPage()
        {
            //await OpenPage(nameof(null));
        }
        [RelayCommand]
        private async Task InvestigatorInfoPage()
        {
            //await OpenPage(nameof(null));
        }
        [RelayCommand]
        private async Task LaunchSMSApp()
        {

        }

        //---------------------save general info---------------------//
        [RelayCommand]
        private async Task SaveGeneralInfo()
        {
            Preferences.Set(nameof(SelectedReportType), SelectedReportType ?? string.Empty);
            Preferences.Set(nameof(FireStationName), FireStationName ?? string.Empty);
            Preferences.Set(nameof(EngineName), EngineName ?? string.Empty);
            Preferences.Set(nameof(ShiftOnDuty), ShiftOnDuty ?? string.Empty);
            Preferences.Set(nameof(StationAddress), StationAddress ?? string.Empty);
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
            FireStationName = Preferences.Get(nameof(FireStationName), string.Empty);
            EngineName = Preferences.Get(nameof(EngineName), string.Empty);
            ShiftOnDuty = Preferences.Get(nameof(ShiftOnDuty), string.Empty);
            StationAddress = Preferences.Get(nameof(StationAddress), string.Empty);
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


    }
}
