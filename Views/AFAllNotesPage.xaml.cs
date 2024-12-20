namespace FogachoApuntes.Views;

public partial class AFAllNotesPage : ContentPage
{
    public AFAllNotesPage()
    {
        InitializeComponent();

        BindingContext = new Models.AFAllNotes();
    }

    protected override void OnAppearing()
    {
        ((Models.AFAllNotes)BindingContext).LoadNotes();
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AFNotePage));
    }

    private async void notesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            // Get the note model
            var note = (Models.AFNote)e.CurrentSelection[0];

            // Should navigate to "NotePage?ItemId=path\on\device\XYZ.notes.txt"
            await Shell.Current.GoToAsync($"{nameof(AFNotePage)}?{nameof(AFNotePage.ItemId)}={note.Filename}");

            // Unselect the UI
            notesCollection.SelectedItem = null;
        }
    }
}