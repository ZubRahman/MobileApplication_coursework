using System.Windows.Input;

namespace MyMauiApp.ViewModels;

public class DiaryEntryDetailsViewModel : BaseViewModel
{
    public int? EntryId { get; set; }
    public long? CloudId { get; set; }
    public string GameId { get; set; } = string.Empty;

    private string _gameTitle = string.Empty;
    public string GameTitle
    {
        get => _gameTitle;
        set => SetProperty(ref _gameTitle, value);
    }

    private int _rating;
    public int Rating
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

    private DateTime _playedOn;
    public DateTime PlayedOn
    {
        get => _playedOn;
        set => SetProperty(ref _playedOn, value);
    }

    public ICommand EditCommand { get; }

    public DiaryEntryDetailsViewModel()
    {
        EditCommand = new Command(async () => await OpenEditPage());
    }

    private async Task OpenEditPage()
    {
        var route =
            $"editrating?entryId={(EntryId.HasValue ? EntryId.Value.ToString() : "")}" +
            $"&cloudId={CloudId}" +
            $"&gameId={Uri.EscapeDataString(GameId)}" +
            $"&title={Uri.EscapeDataString(GameTitle)}" +
            $"&rating={Rating}" +
            $"&review={Uri.EscapeDataString(Review ?? string.Empty)}";

        await Shell.Current.GoToAsync(route);
    }
}