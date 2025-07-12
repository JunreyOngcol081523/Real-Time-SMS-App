using Real_Time_SMS_App.ViewModels;

namespace Real_Time_SMS_App;

public partial class OperationalDetailsPage : ContentPage
{
	public OperationalDetailsPage(SendReportViewModel vm)
	{
		BindingContext	= vm;
        InitializeComponent();
	}
}