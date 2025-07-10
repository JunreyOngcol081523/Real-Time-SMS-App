using Real_Time_SMS_App.ViewModels;

namespace Real_Time_SMS_App;

public partial class ContactPage : ContentPage
{
	public ContactPage(ContactViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}