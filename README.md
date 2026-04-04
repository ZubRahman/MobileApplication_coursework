# Game Diary Mobile Application

## Overview
This project is a mobile game diary application built in **.NET MAUI** using **C#**. It was inspired by platforms like Letterboxd, but adapted for video games instead of films. The application allows users to browse a game catalogue, log ratings and reviews, track progress, and view personalised statistics such as most rated games & a user's rating curve.

Diary entries are stored locally using SQLite to support offline use, and can then be manually synced to the cloud using Supabase. The project was developed to explore core mobile development concepts including multi-page navigation, MVVM architecture, local persistence, authentication, cloud storage, and user interface design.

## Key Features
- Browse a game catalogue with poster images and search functionality
- View detailed information for each game
- Add diary entries with:
  - rating
  - written review
  - progress level
- Edit and delete diary entries
- View diary entries in different modes:
  - all logs
  - unique games
- Sort diary entries by:
  - newest
  - oldest
  - highest rated
  - lowest rated
- Filter diary entries by progress level
- Sign up and log in using Supabase authentication
- Manually sync diary entries with cloud storage
- View a statistics page showing:
  - diary logs
  - total reviews
  - unique games logged
  - average rating
  - highest and lowest rated games
  - most logged game
  - most recent and oldest game log
  - rating curve based on unique games

## Technologies Used
- **.NET MAUI**
- **C#**
- **SQLite** for local offline storage
- **Supabase** for authentication and cloud database storage
- **GitHub** for version control and pull request workflow

## Architecture
The application follows an **MVVM** structure to separate user interface, business logic, and data handling. The project is organised into folders such as:
- `Models`
- `ViewModels`
- `Services`
- `Data`
- `Views/Pages`

This structure improves maintainability, separation of concerns, and scalability.

## Running the App
1. Open the project in Visual Studio or VS Code with .NET MAUI support installed
2. Restore packages and dependencies
3. Run the application on an Android emulator
4. Ensure the Supabase project is active before testing authentication or sync features

## Current Limitation
The application currently uses a manual sync workflow for cloud updates. While local and cloud persistence are both supported, the project remains a prototype rather than a production-ready commercial system.

## Motivation
The main motivation behind this project was to create a mobile application that combines game discovery, personal logging, and progress tracking in one place. It also provided an opportunity to apply mobile development techniques such as navigation, data binding, local and cloud storage, and interface design in a practical project.