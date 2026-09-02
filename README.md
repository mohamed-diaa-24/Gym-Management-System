# Gym Management System

Final Assessment Project - **Summer Training, ITI Mansoura**
**Group 2** - **1 Month Duration**
Built with ASP.NET Core MVC (.NET 10) as part of the MVC course final assessment.

## Project Overview

A simple gym management system with two roles:

- **Trainer**: browses gym classes, filters by trainer, views class details and enrolled members.
- **Admin**: manages trainers, classes, members, and enrolls members into classes.

## Tech Stack

- ASP.NET Core MVC (.NET 10)
- Entity Framework Core (Code First) + SQL Server
- ASP.NET Core Identity (UserManager / SignInManager)
- Bootstrap 5 (via CDN)
- jQuery + AJAX + Partial Views
- SweetAlert2 (delete confirmations)
- Session

## Database

- **Trainer** 1 : M **GymClass**
- **Member** M : M **GymClass** via **Enrollment** (with `EnrollmentDate`)

Entities: `Trainer`, `GymClass`, `Member`, `Enrollment`, `ApplicationUser` (Identity).

## Pages Implemented

### Public / Landing

- Landing page (`Home/Index`) shown first, with a link to Login — the app does not redirect straight to the login page.

### Auth

- Login page (Email + Password) using ASP.NET Core Identity.
- Seeded Admin: `admin@gym.com` / `Admin123`.
- Seeded Trainer: `trainer@gym.com` / `Trainer123`.
- After login, `IsAdmin` is stored in Session based on the user's Identity role.

### Trainer Pages

- **Classes Page**: lists all classes (Name, Description, Schedule, Trainer) with a Trainer dropdown filter and a search box.
- **Class Details**: shows class info and the list of enrolled members.

### Admin Pages

- **Manage Trainers**: List / Create / Edit / Delete / Details.
- **Manage Gym Classes**: List / Create / Edit / Delete / Details (with Trainer dropdown).
- **Manage Members**: List / Create / Edit / Delete / Details.
- **Enroll Member**: select Member + Gym Class + Enrollment Date.

## AJAX + Partial View

On the Classes page, changing the Trainer dropdown (or typing in the search box) fires an AJAX call to `Classes/FilterByTrainer`, which returns a partial view (`_ClassesPartial`) and updates only the classes section — no full page reload.

## Admin Access Control

- Session stores `IsAdmin` after login.
- The layout hides/shows Admin nav links based on `IsAdmin`.
- Every Admin controller action re-checks `IsAdmin` from Session server-side before executing — navigation hiding alone is not relied on.

## Required Concepts Covered

- EF Core + SQL Server, Code First + Migrations
- 1:M and M:M relationships
- ViewModels
- Tag Helpers
- Repository Pattern (`ITrainerRepository`, `IGymClassRepository`, `IMemberRepository`, `IEnrollmentRepository` + implementations) — controllers depend on interfaces, not `DbContext` directly
- Session
- AJAX + Partial Views
- Simple Login & Authorization (via Identity)

## Optional Features Implemented

- Prevent duplicate enrollment (unique index + check in `EnrollmentService`)
- Search classes (by name, alongside the trainer filter)
- SweetAlert delete confirmation on all Admin list pages

## Bonus Features Implemented

- **Service Layer**: `TrainerService`, `GymClassService`, `MemberService`, `EnrollmentService` sit between controllers and repositories.
- **Pagination**: `PaginatedList<T>` used on all Admin Index pages (Trainers, Classes, Members).

## Notes

- `Program.cs` is organized using extension methods (`AddIdentityServices`, `AddSessionServices`, `AddRepositoryServices`, `AddApplicationServices`) to keep service registration clean and readable.

## Setup

```bash
dotnet restore
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

Update the connection string in `appsettings.json` under `ConnectionStrings:DefaultConnection` if needed.

## Login Credentials

| Role    | Email           | Password   |
| ------- | --------------- | ---------- |
| Admin   | admin@gym.com   | Admin123   |
| Trainer | trainer@gym.com | Trainer123 |
