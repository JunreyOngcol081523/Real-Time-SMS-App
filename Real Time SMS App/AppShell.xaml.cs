namespace Real_Time_SMS_App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(SendReportPage), typeof(SendReportPage));
            Routing.RegisterRoute(nameof(ContactPage), typeof(ContactPage));
            Routing.RegisterRoute(nameof(GeneralInfoPage), typeof(GeneralInfoPage));
            Routing.RegisterRoute(nameof(SpecificDetailsPage), typeof(SpecificDetailsPage));
        }
    }
}
