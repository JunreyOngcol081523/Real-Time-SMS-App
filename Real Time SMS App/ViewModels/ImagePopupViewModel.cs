using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Time_SMS_App.ViewModels
{
    public partial class ImagePopupViewModel : ObservableObject
    {

        [RelayCommand]
        private async Task ClosePopup()
        {
            // Close the popup
            await Shell.Current.Navigation.PopModalAsync();
        }

    }
}
