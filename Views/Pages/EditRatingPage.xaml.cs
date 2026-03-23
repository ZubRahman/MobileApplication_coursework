using MyMauiApp.ViewModels;

namespace MyMauiApp.Views.Pages;

[QueryProperty(nameof(EntryId), "entryId")]
[QueryProperty(nameof(CloudId), "cloudId")]
[QueryProperty(nameof(GameId), "gameId")]
[QueryProperty(nameof(GameTitle), "title")]
[QueryProperty(nameof(RatingValue), "rating")]
[QueryProperty(nameof(ReviewText), "review")]
public partial class EditRatingPage : ContentPage
{

    private string _ratingValue = string.Empty;
    public string RatingValue
    {
        get => _ratingValue;
        set
        {
            _ratingValue = value;

            if (double.TryParse(value, out var parsedRating))
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

    private readonly EditRatingViewModel _vm;
    private string _entryId = string.Empty;
    public string EntryId
    {
        get => _entryId;
        set
        {
            _entryId = value;

            if (int.TryParse(value, out var id) && id > 0)
            {
                _vm.EntryId = id;
                _ = _vm.LoadExistingAsync();
            }
            else
            {
                _vm.EntryId = null;
            }
        }
    }

    private string _gameId = string.Empty;
    public string GameId
    {
        get => _gameId;
        set
        {
            _gameId = value;
            _vm.GameId = value;
        }
    }

    private string _gameTitle = string.Empty;
    public string GameTitle
    {
        get => _gameTitle;
        set
        {
            _gameTitle = value;
            _vm.GameTitle = value;
        }
    }

    public EditRatingPage(EditRatingViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }

    
}