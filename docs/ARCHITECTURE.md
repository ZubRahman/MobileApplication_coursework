# Architecture (MVVM + Services + Repositories)

# Goals
- Strict separation of concerns.
- Offline-first without UI hacks.
- Business logic is isolated in ViewModels/Services; code-behind is limited to UI-only concerns.
- Cloud integration isolated from UI.


# Project Structure

Models/
- Game.cs
- Rating.cs
- SyncState.cs
- UserProfile.cs (optional)

Views/Pages/
- GamesPage.xaml
- GameDetailsPage.xaml
- DiaryPage.xaml
- EditRatingPage.xaml
- LoginPage.xaml
- ProfilePage.xaml

ViewModels/
- GamesViewModel
- GameDetailsViewModel
- DiaryViewModel
- EditRatingViewModel
- AuthViewModel
- ProfileViewModel
- StatsViewModel
- SyncViewModel (backs a visible sync indicator in Profile or Diary)

(8 ViewModels minimum for an outstanding MVVM integration)

Services/

Interfaces:
- IGameCatalogService
- IRatingService
- IAuthService
- ISyncService
- INavigationService
- IRatingRepository
- ICloudRatingRepository
- IGameLogService
- IGameLogRepository
- ICloudGameLogRepository

Implementations:
- GameCatalogService
- RatingService
- AuthService
- SyncService
- NavigationService
- GameLogService
- GameLogRepository
- SupabaseGameLogRepository

Data/

Local:
- SqliteConnectionWrapper
- RatingRepository

Remote:
- SupabaseRatingRepository

Helpers/
- Converters
- Validators
- Constants
- Custom RatingControl


# MVVM Rules

- Views contain only bindings and minimal UI events.
- ViewModels expose:
  - Observable properties
  - ICommand actions
  - Async methods (no blocking calls)
- Services own business logic.
- Repositories perform data access only.
- Navigation abstracted via INavigationService.

# Dependency Injection Strategy

- All services, repositories, and ViewModels are registered in MauiProgram.cs using the built-in .NET MAUI Dependency Injection container.
- ViewModels receive required services via constructor injection.
- No service is instantiated manually inside a ViewModel.


# Design Patterns Used

1. MVVM
2. Repository Pattern
3. Dependency Injection
4. Singleton will be used to manage SQLite connection lifecycle to avoid multiple open connections
5. Observer Pattern (ObservableObject)


# Error Handling

- Services return Result objects or throw domain exceptions.
- ViewModels convert errors into user-friendly messages.
- No raw exceptions shown to users.
