using System.Collections.ObjectModel;
using MyMauiApp.Data.Repositories;
using MyMauiApp.Models;
using MyMauiApp.Services.Interfaces;
using System.Windows.Input;

namespace MyMauiApp.ViewModels;

public class ProfileViewModel : BaseViewModel
{
    public ICommand OpenSignUpCommand { get; }
    public ICommand SyncNowCommand { get; }
    private string _syncStatusMessage = string.Empty;
    public string SyncStatusMessage
    {
        get => _syncStatusMessage;
        set => SetProperty(ref _syncStatusMessage, value);
    }
    private readonly IDiaryRepository _repo;
    private readonly ISupabaseService _supabaseService;

    private string _userEmail = "Not logged in";
    public string UserEmail
    {
        get => _userEmail;
        set => SetProperty(ref _userEmail, value);
    }

    private string _authStatus = "Logged out";
    public string AuthStatus
    {
        get => _authStatus;
        set => SetProperty(ref _authStatus, value);
    }

    private int _totalLogs;
    public int TotalLogs
    {
        get => _totalLogs;
        set => SetProperty(ref _totalLogs, value);
    }

    private double _averageRating;
    public double AverageRating
    {
        get => _averageRating;
        set => SetProperty(ref _averageRating, value);
    }

    private async Task SyncNow()
    {
        if (IsBusy) return;

        try
        {
            var userId = _supabaseService.GetCurrentUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                SyncStatusMessage = "Log in to sync your diary.";
                return;
            }

            IsBusy = true;
            ((Command)SyncNowCommand).ChangeCanExecute();
            var pending = await _repo.GetEntriesNeedingSyncAsync();
            SyncStatusMessage = $"Pending local updates: {pending.Count}";

            var result = await _supabaseService.SyncAllAsync(_repo);
            await LoadProfileAsync();

            SyncStatusMessage = result;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.ToString());
            SyncStatusMessage = $"Sync failed: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
            ((Command)SyncNowCommand).ChangeCanExecute();
        }
    }

    public ProfileViewModel(IDiaryRepository repo, ISupabaseService supabaseService)
    {
        _repo = repo;
        _supabaseService = supabaseService;

        OpenLoginCommand = new Command(async () => await OpenLogin());
        OpenSignUpCommand = new Command(async () => await OpenSignUp());
        LogOutCommand = new Command(async () => await LogOut());
        SyncNowCommand = new Command(async () => await SyncNow(), () => !IsBusy);

        _ = LoadProfileAsync();
    }
    
    private async Task OpenSignUp()
    {
        await Shell.Current.GoToAsync("signup");
    }

    public async Task LoadProfileAsync()
    {
        try
        {
            var userId = _supabaseService.GetCurrentUserId();
            var email = _supabaseService.GetCurrentUserEmail();

            if (!string.IsNullOrWhiteSpace(userId))
            {
                IsLoggedIn = true;
                OnPropertyChanged(nameof(IsLoggedOut));

                AuthStatus = "Logged in";
                UserEmail = email ?? "Unknown email";
            }
            else
            {
                IsLoggedIn = false;
                OnPropertyChanged(nameof(IsLoggedOut));

                AuthStatus = "Logged out";
                UserEmail = "Not logged in";
            }

            List<DiaryEntry> entries;

            if (!string.IsNullOrWhiteSpace(userId))
            {
                entries = await _supabaseService.GetCurrentUserDiaryEntriesAsync();
            }
            else
            {
                entries = await _repo.GetEntriesAsync();
            }

            TotalLogs = entries.Count;
            AverageRating = entries.Count > 0 ? entries.Average(e => e.Rating) : 0;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.ToString());
        }
    }

    private bool _isLoggedIn;
    public bool IsLoggedIn
    {
        get => _isLoggedIn;
        set => SetProperty(ref _isLoggedIn, value);
    }

    public bool IsLoggedOut => !IsLoggedIn;

    public ICommand OpenLoginCommand { get; }
    public ICommand LogOutCommand { get; }

    private async Task OpenLogin()
    {
        await Shell.Current.GoToAsync("login");
    }

    private async Task LogOut()
    {
        try
        {
            await _supabaseService.SignOutAsync();
            await LoadProfileAsync();
            await Shell.Current.DisplayAlert("Logged out", "You have been logged out.", "OK");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.ToString());
            await Shell.Current.DisplayAlert("Logout failed", ex.Message, "OK");
        }
    }


}