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
namespace Real_Time_SMS_App.Services
{
    public class FirebaseService
    {
        private readonly FirebaseClient _firebase;
        private readonly HttpClient _httpClient = new();

        public async Task PushSmsReportAsync(object smsData)
        {
            await _firebase
                .Child(Preferences.Get("settingsStation", "smsReport"))
                .PostAsync(smsData);
        }
    }
}
