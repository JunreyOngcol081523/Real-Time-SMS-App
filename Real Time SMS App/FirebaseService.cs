using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Time_SMS_App
{
    public class FirebaseService
    {
        private readonly FirebaseClient _firebase;

        public FirebaseService()
        {
            _firebase = new FirebaseClient("https://real-time-sms-app-default-rtdb.asia-southeast1.firebasedatabase.app/");
        }

        public async Task PushSmsReportAsync(object smsData)
        {
            await _firebase
                .Child("smsReports")
                .PostAsync(smsData);
        }
    }
}
