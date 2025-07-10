using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Time_SMS_App.ViewModels
{
    public partial class InitialInfoViewModel:ObservableObject
    {
        [ObservableProperty] private string stationName;
        [ObservableProperty] private string address;
        [ObservableProperty] private string respondingTeam;
        [ObservableProperty] private DateTime reportDate = DateTime.Now;
        [ObservableProperty] private TimeSpan reportTime = DateTime.Now.TimeOfDay;
        [ObservableProperty] private string involved;
        [ObservableProperty] private string alarmStatus;
        [ObservableProperty] private TimeSpan timeOfArrival = DateTime.Now.TimeOfDay;
        [ObservableProperty] private DateTime fireOutDate = DateTime.Now;
        [ObservableProperty] private TimeSpan fireOutTime = DateTime.Now.TimeOfDay;
        [ObservableProperty] private string groundCommander;
        [ObservableProperty] private string contactNumber;
        [ObservableProperty] private string sender;

        [RelayCommand]
        private async Task Send()
        {
            // Your send logic
            await App.Current.MainPage.DisplayAlert("Success", "Data Sent!", "OK");
        }
    }
}
