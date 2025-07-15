using Google.Cloud.Firestore;
using Real_Time_SMS_App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Real_Time_SMS_App.Services
{
    public class FirestoreService 
    {
        private FirestoreDb db;

        private async Task SetupFirestore()

        {             // Initialize Firestore

            if (db == null)

            {

                var stream = await FileSystem.OpenAppPackageFileAsync("serviceAccountKey.json");

                var reader = new StreamReader(stream);

                var contents = reader.ReadToEnd();

                db = new FirestoreDbBuilder

                {

                    ProjectId = "real-time-sms-app", // Replace with your Firestore project ID

                    JsonCredentials = contents

                }.Build();

            }

        }

        public async Task InitializeFirestore()
        {
            await SetupFirestore();
        }

        //add a new region to the Regions collection
        public async Task AddRegionsCollection(RegionsModel rm)
        {
            await InitializeFirestore();
            await db.Collection("Reg").AddAsync(rm);
        }
        //add province to each regions
        public async Task AddProvinceAsync(ProvincesModel province)
        {
            await InitializeFirestore();
            await db.Collection("Provinces").AddAsync(province);
        }
        //add station to each province
        public async Task AddStationAsync(StationsModel station)
        {
            await InitializeFirestore();
            await db.Collection("Stations").AddAsync(station);
        }

        //retrieve all regions from the Regions collection
        public async Task<List<RegionsModel>> GetRegionsCollection()
        {
            await InitializeFirestore();
            var data = await db.Collection("Reg").GetSnapshotAsync();
            var regions = new List<RegionsModel>();
            foreach (var doc in data.Documents)
            {
                var region = doc.ConvertTo<RegionsModel>();
                regions.Add(region);

            }
            return regions;
        }
        //method for getting all province collection by field regionId
        public async Task<List<ProvincesModel>> GetProvincesByRegionIdAsync(string regionId)
        {
            await InitializeFirestore();
            var querySnapshot = await db.Collection("Provinces")
                                        .WhereEqualTo("RegionId", regionId)
                                        .GetSnapshotAsync();
            var provinces = new List<ProvincesModel>();
            foreach (var doc in querySnapshot.Documents)
            {
                var province = doc.ConvertTo<ProvincesModel>();
                provinces.Add(province);
            }
            return provinces;
        }
        //method for getting all stations by field provinceId
        public async Task<List<StationsModel>> GetStationsByProvinceIdAsync(string provinceId)
        {
            await InitializeFirestore();
            var querySnapshot = await db.Collection("Stations")
                                        .WhereEqualTo("ProvinceId", provinceId)
                                        .GetSnapshotAsync();
            var stations = new List<StationsModel>();
            foreach (var doc in querySnapshot.Documents)
            {
                var station = doc.ConvertTo<StationsModel>();
                stations.Add(station);
            }
            return stations;
        }

        //method for calling any documents by field value
        public async Task<T> GetDocumentByFieldAsync<T>(string collectionName, string fieldName, string value) where T : class
        {
            await InitializeFirestore();

            var querySnapshot = await db.Collection(collectionName)
                                        .WhereEqualTo(fieldName, value)
                                        .Limit(1)
                                        .GetSnapshotAsync();

            if (querySnapshot.Documents.Count == 0)
                return null;

            return querySnapshot.Documents[0].ConvertTo<T>();
        }

    }
}
