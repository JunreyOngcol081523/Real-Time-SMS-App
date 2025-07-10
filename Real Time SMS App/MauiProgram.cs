
using Microsoft.Extensions.Logging;
using Real_Time_SMS_App.ViewModels;


namespace Real_Time_SMS_App;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder.Services.AddTransient<InitialInfoPage>();
        builder.Services.AddTransient<InitialInfoViewModel>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<MainPageViewModel>();
		builder.Services.AddTransient<ContactPage>();
		builder.Services.AddTransient<ContactViewModel>();
        return builder.Build();
	}
}
