using System.Windows.Input;
using MyMauiApp.Services.Interfaces;
using MyMauiApp.Data.Repositories;


namespace MyMauiApp.ViewModels;

public class AuthViewModel : BaseViewModel
{
    private readonly IDiaryRepository _repo;
    private readonly ISupabaseService _supabaseService;

    private string _email = string.Empty;
    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    private string _password = string.Empty;
    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    private string _statusMessage = string.Empty;
    public string StatusMessage
    {
        get => _statusMessage;
        set => SetProperty(ref _statusMessage, value);
    }

    public ICommand SignUpCommand { get; }
    public ICommand SignInCommand { get; }
    public ICommand SignOutCommand { get; }

    public AuthViewModel(ISupabaseService supabaseService, IDiaryRepository repo)
    {
        _supabaseService = supabaseService;
        _repo = repo;

        SignUpCommand = new Command(async () => await SignUp(), () => !IsBusy);
        SignInCommand = new Command(async () => await SignIn(), () => !IsBusy);
        SignOutCommand = new Command(async () => await SignOut(), () => !IsBusy);
    }

    private async Task SignUp()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            RefreshCommandStates();

            await _supabaseService.SignUpAsync(Email, Password);
            StatusMessage = "Sign up succeeded. If email confirmation is enabled, check your inbox.";
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.ToString());
            StatusMessage = $"Sign up failed: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
            RefreshCommandStates();
        }
    }

    private async Task SignIn()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            RefreshCommandStates();

            await _supabaseService.SignInAsync(Email, Password);

            var unsynced = await _repo.GetUnsyncedEntriesAsync();
            await _supabaseService.SyncUnsyncedLocalEntriesAsync(unsynced, _repo);

            StatusMessage = "Logged in successfully. Local entries synced to cloud.";
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.ToString());
            StatusMessage = $"Login failed: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
            RefreshCommandStates();
        }
    }

    private async Task SignOut()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            RefreshCommandStates();

            await _supabaseService.SignOutAsync();
            StatusMessage = "Logged out successfully.";
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.ToString());
            StatusMessage = $"Logout failed: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
            RefreshCommandStates();
        }
    }

    private void RefreshCommandStates()
    {
        ((Command)SignUpCommand).ChangeCanExecute();
        ((Command)SignInCommand).ChangeCanExecute();
        ((Command)SignOutCommand).ChangeCanExecute();
    }
}