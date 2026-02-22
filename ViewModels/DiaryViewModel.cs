using System.Collections.ObjectModel;
using MyMauiApp.Data.Repositories;
using MyMauiApp.Models;

namespace MyMauiApp.ViewModels;

public class DiaryViewModel : BaseViewModel
{
    private readonly IDiaryRepository _repo;

    public ObservableCollection<DiaryEntry> Entries { get; } = new();

    public DiaryViewModel(IDiaryRepository repo)
    {
        _repo = repo;
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
}