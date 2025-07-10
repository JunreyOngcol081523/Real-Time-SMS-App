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
        private async Task Initial()
        {
            await Shell.Current.GoToAsync(nameof(InitialInfoPage));
        }

        [RelayCommand]
        private async Task Progress()
        {
            // Navigate or perform logic
        }

        [RelayCommand]
        private async Task Complete()
        {
            // Navigate or perform logic
        }

        [RelayCommand]
        private async Task Contacts()
        {
            // Navigate or perform logic
        }

    }
}
