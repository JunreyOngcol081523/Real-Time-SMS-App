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
            Routing.RegisterRoute(nameof(OperationalDetailsPage), typeof(OperationalDetailsPage));
            Routing.RegisterRoute(nameof(CasualtyDetailsPage), typeof(CasualtyDetailsPage));
            Routing.RegisterRoute(nameof(InvestigatorInfoPage), typeof(InvestigatorInfoPage));
            Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
        }
    }
}
