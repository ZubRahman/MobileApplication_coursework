using MyMauiApp.Data.Repositories;
using MyMauiApp.Models;
using MyMauiApp.Services.Interfaces;

namespace MyMauiApp.ViewModels;

public class StatsViewModel : BaseViewModel
{
    private readonly IDiaryRepository _repo;
    private readonly ISupabaseService _supabaseService;

    private int _diaryLogs;
    public int DiaryLogs
    {
        get => _diaryLogs;
        set => SetProperty(ref _diaryLogs, value);
    }

    private int _totalReviews;
    public int TotalReviews
    {
        get => _totalReviews;
        set => SetProperty(ref _totalReviews, value);
    }

    private int _uniqueGamesLogged;
    public int UniqueGamesLogged
    {
        get => _uniqueGamesLogged;
        set => SetProperty(ref _uniqueGamesLogged, value);
    }

    private double _averageRating;
    public double AverageRating
    {
        get => _averageRating;
        set => SetProperty(ref _averageRating, value);
    }

    private string _highestRatedGame = "N/A";
    public string HighestRatedGame
    {
        get => _highestRatedGame;
        set => SetProperty(ref _highestRatedGame, value);
    }

    private string _lowestRatedGame = "N/A";
    public string LowestRatedGame
    {
        get => _lowestRatedGame;
        set => SetProperty(ref _lowestRatedGame, value);
    }

    private string _mostLoggedGame = "N/A";
    public string MostLoggedGame
    {
        get => _mostLoggedGame;
        set => SetProperty(ref _mostLoggedGame, value);
    }

    private string _mostRecentGame = "N/A";
    public string MostRecentGame
    {
        get => _mostRecentGame;
        set => SetProperty(ref _mostRecentGame, value);
    }

    private string _oldestGameLog = "N/A";
    public string OldestGameLog
    {
        get => _oldestGameLog;
        set => SetProperty(ref _oldestGameLog, value);
    }

    private string _ratingCurveText = "No data";
    public string RatingCurveText
    {
        get => _ratingCurveText;
        set => SetProperty(ref _ratingCurveText, value);
    }

    public StatsViewModel(IDiaryRepository repo, ISupabaseService supabaseService)
    {
        _repo = repo;
        _supabaseService = supabaseService;
    }

    public async Task LoadStatsAsync()
    {
        try
        {
            var userId = _supabaseService.GetCurrentUserId();

            List<DiaryEntry> entries =
                !string.IsNullOrWhiteSpace(userId)
                ? await _supabaseService.GetCurrentUserDiaryEntriesAsync()
                : await _repo.GetEntriesAsync();

            DiaryLogs = entries.Count;

            TotalReviews = entries.Count(e => !string.IsNullOrWhiteSpace(e.Review));

            UniqueGamesLogged = entries
                .Select(e => e.GameId)
                .Where(id => !string.IsNullOrWhiteSpace(id))
                .Distinct()
                .Count();

            if (entries.Count > 0)
            {
                var uniqueLatestEntries = entries
                    .GroupBy(e => e.GameId)
                    .Select(g => g.OrderByDescending(x => x.PlayedOn).First())
                    .ToList();

                AverageRating = uniqueLatestEntries.Count > 0
                    ? uniqueLatestEntries.Average(e => e.Rating)
                    : 0;

                var highestRated = uniqueLatestEntries
                    .OrderByDescending(e => e.Rating)
                    .ThenByDescending(e => e.PlayedOn)
                    .First();

                HighestRatedGame = $"{highestRated.GameTitle} ({highestRated.Rating}/10)";

                var lowestRated = uniqueLatestEntries
                    .OrderBy(e => e.Rating)
                    .ThenByDescending(e => e.PlayedOn)
                    .First();

                LowestRatedGame = $"{lowestRated.GameTitle} ({lowestRated.Rating}/10)";

                var mostLogged = entries
                    .GroupBy(e => e.GameTitle)
                    .OrderByDescending(g => g.Count())
                    .ThenBy(g => g.Key)
                    .First();

                MostLoggedGame = $"{mostLogged.Key} ({mostLogged.Count()} logs)";

                var mostRecent = entries
                    .OrderByDescending(e => e.PlayedOn)
                    .First();

                MostRecentGame = $"{mostRecent.GameTitle} ({mostRecent.PlayedOn:dd MMM yyyy})";

                var oldest = entries
                    .OrderBy(e => e.PlayedOn)
                    .First();

                OldestGameLog = $"{oldest.GameTitle} ({oldest.PlayedOn:dd MMM yyyy})";

                var curveLines = uniqueLatestEntries
                    .GroupBy(e => e.Rating)
                    .OrderByDescending(g => g.Key)
                    .Select(g => $"{g.Key}/10 - {g.Count()}");

                RatingCurveText = string.Join(Environment.NewLine, curveLines);
            }
            else
            {
                AverageRating = 0;
                HighestRatedGame = "N/A";
                LowestRatedGame = "N/A";
                MostLoggedGame = "N/A";
                MostRecentGame = "N/A";
                OldestGameLog = "N/A";
                RatingCurveText = "No data";
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.ToString());
        }
    }
}