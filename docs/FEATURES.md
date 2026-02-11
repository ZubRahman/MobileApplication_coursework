# Goal
A “Letterboxd for Games” mobile app built in .NET MAUI that allows users to browse a game catalogue, rate games from 1–10, write reviews, and sync ratings to the cloud using Supabase.

# Main User Features (Functional Requirements)

## 1. Game Catalogue:
- Browse Games: View a scrolling list of games using a CollectionView.
- Search and filtering are handled locally using the seeded JSON game catalogue. This ensures offline-first support and reduces cloud dependency. Remote API search may be added as a stretch feature.
- View game details including:
    - cover art
    - release dates for specific games.
    - Summary/description

## 2. Rating & Reviews:
- Rate games 1–10.
- Write optional text reviews.
- Edit/Delete: Full CRUD control over personal ratings, logs, and reviews.
- Diary logs: Users can add/edit/delete play logs with date, platform and status (Backlog/Playing/Completed/Dropped)
- Diary/Library: A personalized list of rated games that can be sorted by score or date.

## 3. Profile & Stats where you can view:
- Total Games Played.
- Average Rating.
- Most played platform: calculated by counting diary log entries per platform.

# Authentication & Cloud Integration

## 4. Secure Login (Supabase Auth)
- Email/password authentication.
- JWT securely stored using SecureStorage.
- Supabase Row Level Security (RLS) ensures users only access their own ratings.

## 5. Offline-First Sync
- Ratings stored locally in SQLite first.
- Changes marked with SyncState.
- On login or reconnect:
  - Pull cloud data
  - Merge using last-write-wins (UpdatedAt)
  - Push pending local changes

# Professional Architecture (Technical Specs, Requirements)
- Full MVVM architecture.
- Repository pattern for local and cloud data.
- Dependency Injection for services and ViewModels.
- Custom-built 1–10 rating control.

# Non-Functional Requirements
- Asynchronous operations to avoid UI freezing.
- Clean navigation using Shell routes.
- Clear error handling (no raw exceptions shown to users).
- Accessible UI (contrast, readable fonts, tappable targets).

# Definition of Done
- All screens bound to dedicated ViewModels.
- SQLite fully integrated with CRUD.
- Supabase cloud integration verified.
- Sync demonstrated in video.
- Features developed in separate Git branches and merged into main.