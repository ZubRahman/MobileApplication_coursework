# Data Model (Local + Supabase)

# Entities

## Game
- Id (string) – stable identifier
- Title (string)
- Platform (string)
- Genre (string)
- ReleaseDate (DateTime)
- Summary (string)
- CoverUrl (string, optional)

Note:
The game catalogue is bundled locally as seeded JSON.
Ratings only are persisted in SQLite.

## GameLog
- Id (Guid)
- UserId
- GameId
- PlayedOn (DateTime)
- Platform (string)
- Status (Backlog / Playing / Completed / Dropped)
- CreatedAt
- UpdatedAt
- SyncState

## Rating
- Id (Guid) – consistent across local and cloud
- UserId (Guid) – Supabase auth user id
- GameId (string)
- Score (int, 1–10)
- Review (string, optional)
- PlayedOn (DateTime?, optional)
- Platform (string)
- CreatedAt (DateTime)
- UpdatedAt (DateTime)
- SyncState (enum):
  - PendingCreate
  - PendingUpdate
  - PendingDelete
  - Synced


# Local Storage (SQLite)

Tables:
- Ratings
- GameLog

Indexes:
- GameId
- UpdatedAt

Purpose:
- SQLite is the local source of truth for UI rendering; Supabase acts as the cross-device canonical store.
- Store unsynced changes
- Enable offline-first usage

# Supabase Table: ratings

Description:
- Unique constraint on (user_id, game_id)

Columns:
- id (uuid, primary key)
- user_id (uuid, references auth.users.id)
- game_id (text)
- score (int)
- review (text)
- played_on (timestamp, nullable)
- created_at (timestamp)
- updated_at (timestamp)

# Supabase Table: game_logs

Columns:
- id (uuid, primary key)
- user_id (uuid, references auth.users.id)
- game_id (text)
- played_on (timestamp)
- platform (text)
- status (text)
- created_at (timestamp)
- updated_at (timestamp)

Row Level Security:
- Users can only access rows where user_id = auth.uid()


# Sync Strategy

1. App always reads from SQLite for UI.
2. On login:
   - Pull cloud ratings.
   - Merge by Id.
   - Prefer newest UpdatedAt (last-write-wins).
3. On local change:
   - Write to SQLite.
   - Update SyncState.
4. Sync loop (online + authenticated):
   - Push pending changes.
   - On success, mark as Synced.

Conflict rule:
- Last-write-wins based on UpdatedAt.
