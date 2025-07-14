using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Time_SMS_App
{
    public class SmsReport
    {
        public string StationName { get; set; }
        public string HotlineNumber { get; set; }
        public string StationEmail { get; set; }
        public string SMSContent { get; set; }
        public string SelectedReportType { get; set; }

       // public string FireStationName { get; set; }
        public string ShiftOnDuty { get; set; }
        public string EngineName { get; set; }
        public string DistrictProvince { get; set; }
        public string SelectedRegion { get; set; }
        public string SelectedProvince { get; set; }
        public string StationAddress { get; set; }
        public string SelectedIncidentType { get; set; }
        public string PlaceOfOccurrence { get; set; }
        public string DateTimeReported { get; set; }
        public string DateTimeEngineDispatched { get; set; }
        public string DateTimeEngineArrived { get; set; }
        public string ResponseTime { get; set; }
        public string DistanceToIncidentLocation { get; set; }
        public string Involved { get; set; }
        public string NameOfOwnerOccupant { get; set; }
        public string NumberOfFamiliesAffected { get; set; }
        public string NumberOfIndividualsAffected { get; set; }
        public string NumberOfStructuresBurned { get; set; }
        public string AffectedEstimatedFloorArea { get; set; }
        public string FireTrucksResponding { get; set; }
        public string AmbulancesResponding { get; set; }
        public string AuxiliaryVehiclesResponding { get; set; }

        public string AlarmDeclared { get; set; }
        public string NumberOfCasualties { get; set; }
        public string NumberOfInjured { get; set; }
        public string NumberOfFatalities { get; set; }
        public string NumberOfMissing { get; set; }
        public string CasulatySummary { get; set; }

        public string InvestigatorICP { get; set; }
        public string InvestigatorSourceContact { get; set; }
        public string InvestigatorFCOS { get; set; }
        public string OtherRelevantInformation { get; set; }

    }

}
