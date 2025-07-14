using Real_Time_SMS_App.ViewModels;

namespace Real_Time_SMS_App;

public partial class InvestigatorInfoPage : ContentPage
{
	public InvestigatorInfoPage(SendReportViewModel vm)
	{
		BindingContext = vm;
        InitializeComponent();
	}
}