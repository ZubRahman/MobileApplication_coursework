using System.Windows.Input;
using MyMauiApp.Data.Repositories;
using MyMauiApp.Models;
using MyMauiApp.Services.Interfaces;

namespace MyMauiApp.ViewModels;

public class EditRatingViewModel : BaseViewModel
{
    private readonly IDiaryRepository _repo;
    private readonly ISupabaseService _supabaseService;

    public int? EntryId { get; set; }
    public long? CloudId { get; set; }
    public string GameId { get; set; } = string.Empty;

    private string _gameTitle = string.Empty;
    public string GameTitle
    {
        get => _gameTitle;
        set => SetProperty(ref _gameTitle, value);
    }

    private double _rating = 7;
    public double Rating
    {
        get => _rating;
        set => SetProperty(ref _rating, value);
    }

    private string _review = string.Empty;
    public string Review
    {
        get => _review;
        set => SetProperty(ref _review, value);
    }

    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }

    public EditRatingViewModel(IDiaryRepository repo, ISupabaseService supabaseService)
    {
        _repo = repo;
        _supabaseService = supabaseService;

        SaveCommand = new Command(async () => await Save(), () => !IsBusy);
        DeleteCommand = new Command(async () => await Delete(), () => !IsBusy);
    }

    public async Task LoadExistingAsync()
    {
        try
        {
            if (EntryId is null) return;

            var existing = await _repo.GetByIdAsync(EntryId.Value);
            if (existing is null) return;

            GameId = existing.GameId;
            GameTitle = existing.GameTitle;
            Rating = existing.Rating;
            Review = existing.Review;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.ToString());
            await Shell.Current.DisplayAlert("Load failed", ex.Message, "OK");
        }
    }

    private async Task Save()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            ((Command)SaveCommand).ChangeCanExecute();
            ((Command)DeleteCommand).ChangeCanExecute();

            if (string.IsNullOrWhiteSpace(GameId))
                throw new InvalidOperationException("GameId is missing (navigation parameters not set).");

            if (EntryId is null)
            {
                var now = DateTime.UtcNow;
                var entry = new DiaryEntry
                {
                    UserId = _supabaseService.GetCurrentUserId() ?? string.Empty,
                    NeedsSync = true,
                    GameId = GameId,
                    GameTitle = GameTitle,
                    Rating = (int)Math.Round(Rating),
                    Review = Review,
                    PlayedOn = now,
                    CreatedAt = now,
                    UpdatedAt = now
                };

                await _repo.AddEntryAsync(entry);
                try
                {
                    var cloudSaved = await _supabaseService.AddDiaryEntryAsync(entry);
                    
                    if (cloudSaved is not null)
                    {
                        entry.CloudId = cloudSaved.CloudId;
                        entry.UserId = cloudSaved.UserId;
                        entry.NeedsSync = false;
                        await _repo.UpdateEntryAsync(entry);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Supabase insert failed: {ex}");
                }
            }
            else
            {
                var existing = await _repo.GetByIdAsync(EntryId.Value);
                if (existing is null)
                    throw new InvalidOperationException("Could not find the diary entry to update.");

                var now = DateTime.UtcNow;
                existing.Rating = (int)Math.Round(Rating);
                existing.Review = Review;
                existing.PlayedOn = now;
                existing.UpdatedAt = now;
                existing.NeedsSync = true;

                await _repo.UpdateEntryAsync(existing);

                try
                {
                    await _supabaseService.UpdateDiaryEntryAsync(existing);
                    existing.NeedsSync = false;
                    await _repo.UpdateEntryAsync(existing);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Supabase update failed: {ex}");
                }
            }

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.ToString());
            await Shell.Current.DisplayAlert("Save failed", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            ((Command)SaveCommand).ChangeCanExecute();
            ((Command)DeleteCommand).ChangeCanExecute();
        }
    }

    private async Task Delete()
    {
        if (IsBusy) return;

        try
        {
            if (EntryId is null && CloudId is null)
            {
                await Shell.Current.DisplayAlert("Delete", "Nothing to delete yet (this entry hasn’t been saved).", "OK");
                return;
            }

            var confirm = await Shell.Current.DisplayAlert(
                "Delete entry?",
                "This will permanently delete your review/rating.",
                "Delete",
                "Cancel"
            );

            if (!confirm) return;

            IsBusy = true;
            ((Command)SaveCommand).ChangeCanExecute();
            ((Command)DeleteCommand).ChangeCanExecute();

            DiaryEntry? existing = null;

            if (EntryId is not null)
            {
                existing = await _repo.GetByIdAsync(EntryId.Value);
            }

            if (existing is null && CloudId is not null)
            {
                existing = new DiaryEntry
                {
                    Id = EntryId ?? 0,
                    CloudId = CloudId,
                    GameId = GameId,
                    GameTitle = GameTitle,
                    Rating = (int)Math.Round(Rating),
                    Review = Review,
                    UserId = _supabaseService.GetCurrentUserId() ?? string.Empty
                };
            }

            if (existing is null)
                throw new InvalidOperationException("Could not find the diary entry to delete.");

            try
            {
                await _supabaseService.DeleteDiaryEntryAsync(existing);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Supabase delete failed: {ex}");
            }

            if (EntryId is not null)
            {
                await _repo.DeleteEntryAsync(EntryId.Value);
            }

            await Shell.Current.GoToAsync("../..");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.ToString());
            await Shell.Current.DisplayAlert("Delete failed", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            ((Command)SaveCommand).ChangeCanExecute();
            ((Command)DeleteCommand).ChangeCanExecute();
        }
    }
}