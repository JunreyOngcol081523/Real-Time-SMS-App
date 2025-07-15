using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Time_SMS_App.Models
{
    [FirestoreData]
    public class RegionsModel
    {
        [FirestoreProperty]
        public string RegionId { get; set; } // Unique identifier for the region
        [FirestoreProperty]
        public string RegionName { get; set; }
        [FirestoreProperty]
        public string RegionDescription { get; set; } // Optional description of the region

    }
    // a class name ProvincesModel that implements regionsmodel
    [FirestoreData]
    public class ProvincesModel 
    {
        [FirestoreProperty]
        public string ProvinceId { get; set; } // Unique identifier for the province
        [FirestoreProperty]
        public string ProvinceName { get; set; }
        [FirestoreProperty]
        public string RegionId { get; set; } // Reference to the region this province belongs to
    }
    // stations class that belongs to provinces
    public class StationsModel
    {
        [FirestoreProperty]
        public string StationID { get; set; }       
        [FirestoreProperty]
        public string StationName { get; set; }
        [FirestoreProperty]
        public string ProvinceId { get; set; } // Reference to the province this station belongs to
        //public string StationAddress { get; set; }
        //public string StationEmail { get; set; }
        //public string HotlineNumber { get; set; }
    }
}