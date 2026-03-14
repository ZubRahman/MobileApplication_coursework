using System.Collections.ObjectModel;
using MyMauiApp.Data.Repositories;
using MyMauiApp.Models;
using System.Windows.Input;

namespace MyMauiApp.ViewModels;

public class DiaryViewModel : BaseViewModel
{

    private string _selectedSort = "Newest";
    public string SelectedSort
    {
        get => _selectedSort;
        set
        {
            if (_selectedSort == value) return;
            _selectedSort = value;
            _ = LoadEntriesAsync();
        }
    }

    public List<string> SortOptions { get; } = new()
    {
        "Newest",
        "Oldest",
        "Highest Rated",
        "Lowest Rated"
    };

    private readonly IDiaryRepository _repo;

    public ObservableCollection<DiaryEntry> Entries { get; } = new();
    public ICommand EditEntryCommand { get; }

    public DiaryViewModel(IDiaryRepository repo)
    {
        _repo = repo;

        EditEntryCommand = new Command<DiaryEntry>(async (entry) => await EditEntry(entry));

        _ = LoadEntriesAsync();
    }

    public async Task LoadEntriesAsync()
    {
        try
        {
            var entries = await _repo.GetEntriesAsync();

            entries = SelectedSort switch
            {
                "Oldest" => entries.OrderBy(e => e.PlayedOn).ToList(),
                "Highest Rated" => entries.OrderByDescending(e => e.Rating).ToList(),
                "Lowest Rated" => entries.OrderBy(e => e.Rating).ToList(),
                _ => entries.OrderByDescending(e => e.PlayedOn).ToList()
            };

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Entries.Clear();
                foreach (var entry in entries)
                    Entries.Add(entry);
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