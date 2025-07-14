using Real_Time_SMS_App.ViewModels;

namespace Real_Time_SMS_App;

public partial class SendReportPage : ContentPage
{
	public SendReportPage(SendReportViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}