using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.MauiMTAdmob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Extensions;
namespace Real_Time_SMS_App.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        string imageSource;
        public MainPageViewModel()
        {
            ShowInterstitialAd();
        }
        private void ShowInterstitialAd()
        {
            // Load your interstitial
            CrossMauiMTAdmob.Current.LoadInterstitial("ca-app-pub-8158194714551266/8902787255");

            // Optional: handle events
            CrossMauiMTAdmob.Current.OnInterstitialLoaded += (s, e) =>
            {
                Console.WriteLine("Interstitial loaded; showing now...");
                CrossMauiMTAdmob.Current.ShowInterstitial();
            };

            CrossMauiMTAdmob.Current.OnInterstitialFailedToLoad += (s, e) =>
                Console.WriteLine($"Failed to load interstitial: {e.ErrorMessage}");

            // Or later in your logic:
            if (CrossMauiMTAdmob.Current.IsInterstitialLoaded())
                CrossMauiMTAdmob.Current.ShowInterstitial();
            else
                Console.WriteLine("Interstitial not ready yet.");

        }
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
        [RelayCommand]
        private async Task About()
        {
            //await NavigateToPage(nameof(AboutPage));
        }
        [RelayCommand]
        private async Task ClearPreferences()
        {
            Preferences.Clear();
            await Shell.Current.DisplayAlert("Info", "Old Data Cleared", "OK");
        }
        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        private async Task OpenPopup()
        {
            var popup = new PopupPage();
            await Shell.Current.ShowPopupAsync(popup);
        }
    }
}
