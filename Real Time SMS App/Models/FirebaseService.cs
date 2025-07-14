using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Real_Time_SMS_App
{
    public class FirebaseService
    {
        private readonly FirebaseClient _firebase;
        private readonly HttpClient _httpClient = new();
        private readonly string _projectId = "real-time-sms-app";
        private readonly string _accessToken; // Optional if Firestore rules are public

        private string BaseUrl => $"https://firestore.googleapis.com/v1/projects/{_projectId}/databases/(default)/documents";

        private async Task<List<string>> FetchCollectionAsync(string path)
        {
            var url = $"{BaseUrl}/{path}";
            if (!string.IsNullOrEmpty(_accessToken))
                url += $"?access_token={_accessToken}";

            var response = await _httpClient.GetStringAsync(url);
            var document = JsonDocument.Parse(response);

            var results = new List<string>();
            if (!document.RootElement.TryGetProperty("documents", out var docs)) return results;

            foreach (var doc in docs.EnumerateArray())
            {
                if (doc.TryGetProperty("fields", out var fields) &&
                    fields.TryGetProperty("name", out var nameField) &&
                    nameField.TryGetProperty("stringValue", out var value))
                {
                    results.Add(value.GetString());
                }
            }

            return results;
        }

        public Task<List<string>> FetchAllRegions()
        {
            return FetchCollectionAsync("Regions");
        }

        public async Task<List<string>> FetchAllProvinces(string regionName)
        {
            var regionId = await GetDocumentIdByFieldValue("Regions", regionName);
            if (regionId == null) return new List<string>();

            return await FetchCollectionAsync($"Regions/{regionId}/provinces");
        }

        public async Task<List<string>> FetchAllStations(string provinceName)
        {
            // Find the region that contains this province
            var regions = await FetchCollectionAsync("Regions");
            foreach (var region in regions)
            {
                var regionId = await GetDocumentIdByFieldValue("Regions", region);
                if (regionId == null) continue;

                var provinces = await FetchCollectionAsync($"Regions/{regionId}/provinces");
                foreach (var prov in provinces)
                {
                    if (prov == provinceName)
                    {
                        var provinceId = await GetDocumentIdByFieldValue($"Regions/{regionId}/provinces", prov);
                        if (provinceId == null) continue;

                        return await FetchCollectionAsync($"Regions/{regionId}/provinces/{provinceId}/stations");
                    }
                }
            }

            return new List<string>();
        }

        public async Task<List<string>> FetchStations() // Fetch all stations from all regions and provinces
        {
            var allStations = new List<string>();
            var regions = await FetchCollectionAsync("Regions");

            foreach (var region in regions)
            {
                var regionId = await GetDocumentIdByFieldValue("Regions", region);
                if (regionId == null) continue;

                var provinces = await FetchCollectionAsync($"Regions/{regionId}/provinces");
                foreach (var province in provinces)
                {
                    var provinceId = await GetDocumentIdByFieldValue($"Regions/{regionId}/provinces", province);
                    if (provinceId == null) continue;

                    var stations = await FetchCollectionAsync($"Regions/{regionId}/provinces/{provinceId}/stations");
                    allStations.AddRange(stations);
                }
            }

            return allStations;
        }

        private async Task<string?> GetDocumentIdByFieldValue(string collectionPath, string fieldValue)
        {
            var url = $"{BaseUrl}/{collectionPath}";
            if (!string.IsNullOrEmpty(_accessToken))
                url += $"?access_token={_accessToken}";

            var response = await _httpClient.GetStringAsync(url);
            var document = JsonDocument.Parse(response);

            if (!document.RootElement.TryGetProperty("documents", out var docs)) return null;

            foreach (var doc in docs.EnumerateArray())
            {
                if (doc.TryGetProperty("fields", out var fields) &&
                    fields.TryGetProperty("name", out var nameField) &&
                    nameField.TryGetProperty("stringValue", out var value) &&
                    value.GetString() == fieldValue)
                {
                    var fullName = doc.GetProperty("name").GetString(); // full path
                    var parts = fullName.Split('/');
                    return parts[^1]; // Get document ID from full path
                }
            }

            return null;
        }
        public static async Task<List<string>> GetAllRegionsDocumentsFormatted()
        {
            // Remember to set up Application Default Credentials for authentication.
            // This is crucial for your .NET application to connect to Firestore.

            // Initialize the list to store our formatted output
            List<string> formattedRegionData = new List<string>();

            // 1. Initialize the Firestore client
            FirestoreDb db = FirestoreDb.Create("real-time-sms-app");



            // 2. Get a reference to your 'Regions' collection
            CollectionReference regionsCollectionRef = db.Collection("Regions");

            // 3. Execute the query to get all documents in the collection
            QuerySnapshot regionsSnapshot = await regionsCollectionRef.GetSnapshotAsync();

            // 4. Iterate through the documents and format the data
            if (regionsSnapshot.Documents.Count > 0)
            {

                foreach (DocumentSnapshot document in regionsSnapshot.Documents)
                {
                    // Add the document ID to our list
                    formattedRegionData.Add(document.Id);


                    // Convert the document data to a dictionary
                    Dictionary<string, object> regionData = document.ToDictionary();

                    // Add each field's key and value to our list
                    foreach (KeyValuePair<string, object> field in regionData)
                    {
                        formattedRegionData.Add($"  {field.Key}: {field.Value}");
                    }
                }
            }
            // Return the list of formatted strings
            return formattedRegionData;
        }
        public FirebaseService()
        {
            _firebase = new FirebaseClient("https://real-time-sms-app-default-rtdb.asia-southeast1.firebasedatabase.app/");
        }

        public async Task PushSmsReportAsync(object smsData)
        {
            await _firebase
                .Child(Preferences.Get("settingsStation", "smsReport"))
                .PostAsync(smsData);
        }
    }
}
