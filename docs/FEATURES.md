# Goal
An app for users to browse games, rate them from 1–10, and write reviews. Data is stored locally for offline use and synced to a Supabase cloud database after login.

## Main User Features (Functional Requirements)
1. Browse Games: View a scrolling list of games using a CollectionView.
2. Search & Filter: Find games by title or platform using local data or API search endpoints.
3. Game Details: View summaries, cover art, and release dates for specific games.
4. Rating System: Rate games 1–10 and write reviews (stored locally first, then updated to cloud).
5. Edit/Delete: Full CRUD control over personal ratings and logs.
6. My Library: A personalized list of rated games that can be sorted by score or date.
7. Profile & Stats: View user-specific stats like "Total Games Played" or "Average Rating".

## Authentication & Cloud Integration
8. Secure Login: Sign up and log in using Supabase Auth (satisfies the Auth API requirement).
9. Cloud Sync: Ratings are saved to a cloud database and visible only to the creator using Row Level Security (RLS).
10. Sync on Reconnect: Local changes automatically sync to the cloud when the device reconnects to the internet.

## Professional Architecture (Technical Specs)
11. MVVM Excellence: Business logic is isolated in ViewModels and Services, with minimal code-behind limited to UI concerns where unavoidable.
12. 8 ViewModels: The app uses 8+ dedicated ViewModels (e.g. LoginViewModel, GameDetailsViewModel, SearchViewModel, etc.).
13. Design Patterns:
- Repository Pattern (local and cloud data access)
- Dependency Injection (service and ViewModel resolution)
- Singleton (managed database lifecycle)
- Observer pattern via ObservableObject for UI state updates
14. Custom Control: Includes a Custom-built 1-10 Rating Control to demonstrate UI mastery.

## Performance & Quality (Non-Functional)
15. Offline-First: All features work without internet; changes queue up for later sync.
16. Smooth UI: Asynchronous programming ensures the app doesn't freeze during API calls.
17. Security Policy: Uses Supabase Row Level Security (RLS) for data isolation and secure local storage for authentication tokens.
18. Accessibility: High contrast buttons, readable fonts, and clear tappable areas.

## Definition of Done (Checklist)
* UI built in XAML and bound to a dedicated ViewModel.
* Local data persistence working via SQLite.
* Cloud integration verified with Supabase.
* Error handling implemented for "No Internet" or "Login Failed" scenarios.
* Feature recorded and explained for the 8-minute demo video.
* Feature implemented on a dedicated Git branch and merged back into development.