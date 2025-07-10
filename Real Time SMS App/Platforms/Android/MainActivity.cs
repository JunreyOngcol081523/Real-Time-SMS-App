using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;

namespace Real_Time_SMS_App;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        // Request permissions
        ActivityCompat.RequestPermissions(this, new string[]
        {
        Android.Manifest.Permission.SendSms,
        Android.Manifest.Permission.ReceiveSms,
        Android.Manifest.Permission.ReadSms
        }, 0);
    }
}
