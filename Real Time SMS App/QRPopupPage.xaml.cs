using CommunityToolkit.Maui.Views;

namespace Real_Time_SMS_App;

public partial class QRPopupPage : Popup
{
    public QRPopupPage(string imageSource)
    {
        InitializeComponent();
        PopupImage.Source = imageSource;
    }

    private void Close_Clicked(object sender, EventArgs e)
    {
        Close(); 
    }
}
