namespace FogachoApuntes.Views;

public partial class AFAllNotesPage : ContentPage
{
    public AFAllNotesPage()
    {
        InitializeComponent();
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        notesCollection.SelectedItem = null;
    }
}