using Real_Time_SMS_App.ViewModels;

namespace Real_Time_SMS_App;

public partial class SpecificDetailsPage : ContentPage
{
	public SpecificDetailsPage(SendReportViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();

    }
}