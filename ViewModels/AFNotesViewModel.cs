using CommunityToolkit.Mvvm.Input;
using FogachoApuntes.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FogachoApuntes.ViewModels;

internal class AFNotesViewModel : IQueryAttributable
{
    public ObservableCollection<ViewModels.AFNoteViewModel> AllNotes { get; }
    public ICommand NewCommand { get; }
    public ICommand SelectNoteCommand { get; }

    public AFNotesViewModel()
    {
        AllNotes = new ObservableCollection<ViewModels.AFNoteViewModel>(Models.AFNote.LoadAll().Select(n => new AFNoteViewModel(n)));
        NewCommand = new AsyncRelayCommand(NewNoteAsync);
        SelectNoteCommand = new AsyncRelayCommand<ViewModels.AFNoteViewModel>(SelectNoteAsync);
    }
    private async Task NewNoteAsync()
    {
        await Shell.Current.GoToAsync(nameof(Views.AFNotePage));
    }

    private async Task SelectNoteAsync(ViewModels.AFNoteViewModel note)
    {
        if (note != null)
            await Shell.Current.GoToAsync($"{nameof(Views.AFNotePage)}?load={note.Identifier}");
    }
    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("deleted"))
        {
            string noteId = query["deleted"].ToString();
            AFNoteViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

            // If note exists, delete it
            if (matchedNote != null)
                AllNotes.Remove(matchedNote);
        }
        else if (query.ContainsKey("saved"))
        {
            string noteId = query["saved"].ToString();
            AFNoteViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

            // If note is found, update it
            if (matchedNote != null)
            {
                matchedNote.Reload();
                AllNotes.Move(AllNotes.IndexOf(matchedNote), 0);
            }  

            // If note isn't found, it's new; add it.
            else
                AllNotes.Insert(0, new AFNoteViewModel(Models.AFNote.Load(noteId)));
        }
    }
}

