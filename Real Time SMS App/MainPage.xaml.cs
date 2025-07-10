using Real_Time_SMS_App.ViewModels;

namespace Real_Time_SMS_App
{
    public partial class MainPage : ContentPage
    {


        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }


    }

}
