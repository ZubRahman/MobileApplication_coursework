using System.Windows.Input;
using MyMauiApp.Data.Repositories;
using MyMauiApp.Models;

namespace MyMauiApp.ViewModels;

public class EditRatingViewModel : BaseViewModel
{
    private readonly IDiaryRepository _repo;

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

    public EditRatingViewModel(IDiaryRepository repo)
    {
        _repo = repo;
        SaveCommand = new Command(async () => await Save());
    }

    private async Task Save()
    {
        if (string.IsNullOrWhiteSpace(GameId)) return;

        var entry = new DiaryEntry
        {
            GameId = GameId,
            GameTitle = GameTitle,
            Rating = (int)Math.Round(Rating),
            Review = Review,
            PlayedOn = DateTime.Now,
            CreatedAt = DateTime.Now
        };

        await _repo.AddEntryAsync(entry);

        await Shell.Current.GoToAsync(".."); // back
    }
}