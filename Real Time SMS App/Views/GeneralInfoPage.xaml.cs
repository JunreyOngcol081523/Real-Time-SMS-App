using Real_Time_SMS_App.ViewModels;

namespace Real_Time_SMS_App;

public partial class GeneralInfoPage : ContentPage
{
	public GeneralInfoPage( SendReportViewModel viewModel)
	{
		BindingContext = viewModel;
        InitializeComponent();
		
	}
}