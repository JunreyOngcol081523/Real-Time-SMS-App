using Real_Time_SMS_App.ViewModels;

namespace Real_Time_SMS_App;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel vm)
	{
		BindingContext = vm;
        InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if(BindingContext is SettingsViewModel vm)
        {
            // Load regions, provinces, and stations
            await vm.LoadRegionsAsync();
        }
    }
}