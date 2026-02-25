using System.Collections.ObjectModel;
using MyMauiApp.Data.Repositories;
using MyMauiApp.Models;
using System.Windows.Input;

namespace MyMauiApp.ViewModels;

public class DiaryViewModel : BaseViewModel
{
    private readonly IDiaryRepository _repo;

    public ObservableCollection<DiaryEntry> Entries { get; } = new();
    public ICommand EditEntryCommand { get; }

    public DiaryViewModel(IDiaryRepository repo)
    {
        _repo = repo;

        EditEntryCommand = new Command<DiaryEntry>(async (entry) => await EditEntry(entry));

        _ = LoadEntries();
    }

    public async Task LoadEntries()
    {
        try
        {
            var items = await _repo.GetEntriesAsync();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Entries.Clear();
                foreach (var e in items)
                    Entries.Add(e);
            });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.ToString());
        }
    }

    private async Task EditEntry(DiaryEntry? entry)
    {
        if (entry is null) return;

        await Shell.Current.GoToAsync(
            $"editrating?entryId={entry.Id}&gameId={Uri.EscapeDataString(entry.GameId)}&title={Uri.EscapeDataString(entry.GameTitle)}"
        );
    }
}