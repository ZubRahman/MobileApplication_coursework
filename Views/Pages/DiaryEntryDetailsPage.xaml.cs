using MyMauiApp.ViewModels;

namespace MyMauiApp.Views.Pages;

[QueryProperty(nameof(EntryId), "entryId")]
[QueryProperty(nameof(CloudId), "cloudId")]
[QueryProperty(nameof(GameId), "gameId")]
[QueryProperty(nameof(GameTitle), "title")]
[QueryProperty(nameof(RatingValue), "rating")]
[QueryProperty(nameof(ReviewText), "review")]
[QueryProperty(nameof(PlayedOnValue), "playedOn")]
public partial class DiaryEntryDetailsPage : ContentPage
{
    private readonly DiaryEntryDetailsViewModel _vm;

    public DiaryEntryDetailsPage(DiaryEntryDetailsViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }

    private string _entryId = string.Empty;
    public string EntryId
    {
        get => _entryId;
        set
        {
            _entryId = value;
            if (int.TryParse(value, out var parsedId) && parsedId > 0)
            {
                _vm.EntryId = parsedId;
            }
            else
            {
                _vm.EntryId = null;
            }
        }
    }

    private string _cloudId = string.Empty;
    public string CloudId
    {
        get => _cloudId;
        set
        {
            _cloudId = value;
            if (long.TryParse(value, out var parsedCloudId))
            {
                _vm.CloudId = parsedCloudId;
            }
            else
            {
                _vm.CloudId = null;
            }
        }
    }

    private string _gameId = string.Empty;
    public string GameId
    {
        get => _gameId;
        set
        {
            _gameId = Uri.UnescapeDataString(value ?? string.Empty);
            _vm.GameId = _gameId;
        }
    }

    private string _gameTitle = string.Empty;
    public string GameTitle
    {
        get => _gameTitle;
        set
        {
            _gameTitle = Uri.UnescapeDataString(value ?? string.Empty);
            _vm.GameTitle = _gameTitle;
        }
    }

    private string _ratingValue = string.Empty;
    public string RatingValue
    {
        get => _ratingValue;
        set
        {
            _ratingValue = value;
            if (int.TryParse(value, out var parsedRating))
            {
                _vm.Rating = parsedRating;
            }
        }
    }

    private string _reviewText = string.Empty;
    public string ReviewText
    {
        get => _reviewText;
        set
        {
            _reviewText = Uri.UnescapeDataString(value ?? string.Empty);
            _vm.Review = _reviewText;
        }
    }

    private string _playedOnValue = string.Empty;
    public string PlayedOnValue
    {
        get => _playedOnValue;
        set
        {
            _playedOnValue = value;
            if (DateTime.TryParse(value, out var parsedPlayedOn))
            {
                _vm.PlayedOn = parsedPlayedOn;
            }
        }
    }
}