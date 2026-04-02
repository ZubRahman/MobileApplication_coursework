using System.Collections.ObjectModel;
using MyMauiApp.Data.Repositories;
using MyMauiApp.Models;
using System.Windows.Input;
using MyMauiApp.Services.Interfaces;

namespace MyMauiApp.ViewModels;



public class DiaryViewModel : BaseViewModel
{

    private readonly IDiaryRepository _repo;
    private readonly ISupabaseService _supabaseService;

    public ObservableCollection<DiaryEntry> Entries { get; } = new();
    public ICommand EditEntryCommand { get; }

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

    private string _selectedViewMode = "All Logs";
    public string SelectedViewMode
    {
        get => _selectedViewMode;
        set
        {
            if (_selectedViewMode == value) return;
            _selectedViewMode = value;
            _ = LoadEntriesAsync();
        }
    }

    public List<string> ViewModes { get; } = new()
    {
        "All Logs",
        "Unique Games"
    };

    

    public DiaryViewModel(IDiaryRepository repo, ISupabaseService supabaseService)
    {
        _repo = repo;
        _supabaseService = supabaseService;
        EditEntryCommand = new Command<DiaryEntry>(async (entry) => await EditEntry(entry));

        _ = LoadEntriesAsync();
    }

    public async Task LoadEntriesAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;

            var currentUserId = _supabaseService.GetCurrentUserId();

            List<DiaryEntry> entries;

            

            if (!string.IsNullOrWhiteSpace(currentUserId))
            {
                entries = await _supabaseService.GetCurrentUserDiaryEntriesAsync();
            }
            else
            {
                entries = await _repo.GetEntriesAsync();
            }

            if (SelectedViewMode == "Unique Games")
            {
                entries = entries
                    .GroupBy(e => e.GameId)
                    .Select(g => g.OrderByDescending(x => x.PlayedOn).First())
                    .ToList();
            }

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
            await Shell.Current.DisplayAlert("Load failed", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
    private async Task EditEntry(DiaryEntry? entry)
    {
        if (entry is null) return;

        var route =
            $"diaryentrydetails?entryId={(entry.Id > 0 ? entry.Id.ToString() : "")}" +
            $"&cloudId={entry.CloudId}" +
            $"&gameId={Uri.EscapeDataString(entry.GameId)}" +
            $"&title={Uri.EscapeDataString(entry.GameTitle)}" +
            $"&rating={entry.Rating}" +
            $"&review={Uri.EscapeDataString(entry.Review ?? string.Empty)}" +
            $"&playedOn={Uri.EscapeDataString(entry.PlayedOn.ToString("O"))}" +
            $"&progressLevel={Uri.EscapeDataString(entry.ProgressLevel ?? string.Empty)}";

        await Shell.Current.GoToAsync(route);
    }
    
   
}