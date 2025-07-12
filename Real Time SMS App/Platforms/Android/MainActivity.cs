using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;
using Plugin.MauiMTAdmob;

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
        string appId = "ca-app-pub-8158194714551266~1573842015";

        string license = "ZIrXQSue3vFYNfMSzKXkStXSLUgy6vEfkwHfvSnC1lBKifhqgmAOL6QzX3YZdz6q/9yWdo1LWRxQy3pBHuapiubn7tcVNE8Z8A=="; //<-- Your license key here
        string deviceId = "9cdc0cea-dfce-49cd-ab2a-beec9ad74057"; //<--- Your test device id here
        string OPENADS_AD_UNIT_ID = "ca-app-pub-3940256099942544/9257395921";
        string NATIVEADS_AD_UNIT_ID = "ca-app-pub-3940256099942544/2247696110";
        bool enableOpenAds = false;
        bool initialiseConsentAtStartup = true;
        //If you have a license code:

        //CrossMauiMTAdmob.Current.SkipConsent = true;

        //CrossMauiMTAdmob.Current.Init(this, appId, license, NATIVEADS_AD_UNIT_ID, OPENADS_AD_UNIT_ID, enableOpenAds, false, deviceId, true, Plugin.MauiMTAdmob.Extra.DebugGeography.DEBUG_GEOGRAPHY_EEA, initialiseConsentAtStartup);

        //If you don't have a license code, you can use the following line instead:
        CrossMauiMTAdmob.Current.Init(this, appId);
    }
}
