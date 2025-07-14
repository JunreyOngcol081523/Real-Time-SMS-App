using Real_Time_SMS_App.ViewModels;

namespace Real_Time_SMS_App;

public partial class InitialInfoPage : ContentPage
{
	public InitialInfoPage(InitialInfoViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}