
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Plugin.MauiMTAdmob;
using Real_Time_SMS_App.ViewModels;


namespace Real_Time_SMS_App;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
                    .UseMauiApp<App>()
                    .UseMauiMTAdmob()
                    .UseMauiCommunityToolkit()
                    .ConfigureFonts(fonts =>
                    {
                        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    });

#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder.Services.AddTransient<SendReportPage>();
        builder.Services.AddTransient<SendReportViewModel>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<MainPageViewModel>();
		builder.Services.AddTransient<ContactPage>();
		builder.Services.AddTransient<ContactViewModel>();
		builder.Services.AddTransient<GeneralInfoPage>();
		builder.Services.AddTransient<SpecificDetailsPage>();
		builder.Services.AddTransient<OperationalDetailsPage>();
		builder.Services.AddTransient<CasualtyDetailsPage>();
		builder.Services.AddTransient<InvestigatorInfoPage>();
		builder.Services.AddTransient<AboutPage>();
		builder.Services.AddTransient<SettingsPage>();
		builder.Services.AddTransient<SettingsViewModel>();
        return builder.Build();
	}
}
