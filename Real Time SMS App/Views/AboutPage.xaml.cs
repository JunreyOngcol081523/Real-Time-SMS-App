using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Extensions;
using Microsoft.Maui.Controls.Shapes;
using Real_Time_SMS_App.ViewModels;

namespace Real_Time_SMS_App;

public partial class AboutPage : ContentPage
{
	public AboutPage(MainPageViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
    
}