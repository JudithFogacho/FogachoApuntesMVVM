namespace FogachoApuntes.Views;

public partial class AFAboutPage : ContentPage
{
	public AFAboutPage()
	{
		InitializeComponent();
	}

    private async void LearnMore_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.AFAbout about)
        {
            // Navigate to the specified URL in the system browser.
            await Launcher.Default.OpenAsync(about.MoreInfoUrl);
        }

    }
}