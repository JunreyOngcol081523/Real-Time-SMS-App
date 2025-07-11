using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Time_SMS_App.ViewModels
{
    public partial class MainPageViewModel:ObservableObject
    {
        [RelayCommand]
        private async Task SendReport()
        {
            await NavigateToPage(nameof(SendReportPage));
        }


        [RelayCommand]
        private async Task Contacts()
        {
            await NavigateToPage(nameof(ContactPage));
        }
        private async Task NavigateToPage(string pageName)
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

    }
}
