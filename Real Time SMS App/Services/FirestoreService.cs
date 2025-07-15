using Google.Cloud.Firestore;
using Real_Time_SMS_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Time_SMS_App.Services
{
    public class FirestoreService
    {
        private FirestoreDb db;

        private async Task SetupFirestore()
        {             // Initialize Firestore
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
        //add a new region to the Regions collection
        public async Task AddRegionsCollection(RegionsModel rm)
        {
            await SetupFirestore();
            await db.Collection("Reg").AddAsync(rm);
        }
        //add province to each regions
        public async Task AddProvinceAsync(ProvincesModel province)
        {
            await SetupFirestore();
            await db.Collection("Provinces").AddAsync(province);
        }

        //retrieve all regions from the Regions collection
        public async Task<List<RegionsModel>> GetRegionsCollection()
        {
            await SetupFirestore();
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
            await SetupFirestore();
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
        //example for calling GetProvincesByRegionIdAsync() to output
        //public async Task ExampleUsage()
        //{
        //    var provinces = await GetProvincesByRegionIdAsync("some-region-id");
        //    foreach (var province in provinces)
        //    {
        //        Console.WriteLine($"Province Name: {province.ProvinceName}, Region ID: {province.RegionId}");
        //    }
        //}

        //method for calling any documents by field value
        public async Task<T> GetDocumentByFieldAsync<T>(string collectionName, string fieldName, string value) where T : class
        {
            await SetupFirestore();

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
