using Real_Time_SMS_App.ViewModels;

namespace Real_Time_SMS_App;

public partial class CasualtyDetailsPage : ContentPage
{
	public CasualtyDetailsPage(SendReportViewModel vm)
	{
		BindingContext = vm;
        InitializeComponent();
	}
}