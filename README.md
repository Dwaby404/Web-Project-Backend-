# Anime Catalogue API

ASP.NET Core Web API for managing an anime catalogue with user profiles and watchlists.

## Features

- User registration and management with role-based access
- Anime catalogue with CRUD operations
- User watchlists for tracking anime
- Duplicate prevention (unique emails and anime codes)
- Filtered queries (by role, genre, release year)
- Full async/await implementation
- Data validation using Data Annotations

## Technologies

- ASP.NET Core 8.0 Web API
- Entity Framework Core 9.0
- SQL Server LocalDB
- Swagger/OpenAPI for documentation

## Database Schema

**Users**
- Id, Email (unique), Username, Password, Role

**Animes**
- Id, Title, Code (unique), Synopsis, Episodes, Genre, ReleaseYear, Rating

**Watchlists**
- Id, UserId (FK), AnimeId (FK), AddedDate

**Relationships:**
- User → Watchlists (One-to-Many)
- Anime → Watchlists (One-to-Many)

## Setup Instructions

1. **Clone the repository**
```bash
git clone https://github.com/Dwaby404/Web-Project-Backend-
cd Web-Project-Backend-
cd AnimeApi
```

2. **Install dependencies**
```bash
dotnet restore
```

3. **Update database connection string**

Edit `appsettings.json` if needed (default uses SQL Server LocalDB):
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=AnimeDb;Integrated Security=true;TrustServerCertificate=true;"
}
```

4. **Apply migrations**
```bash
dotnet ef database update
```

5. **Run the application**
```bash
dotnet run
```

6. **Access Swagger UI**

Navigate to `https://localhost:7045/swagger`

## API Endpoints

### Users

- `GET /api/Users` - Get all users
- `GET /api/Users/{id}` - Get user by ID
- `GET /api/Users/role/{role}` - Get users by role (filtered query)
- `POST /api/Users/register` - Register new user (also available at `/api/register`)
- `PUT /api/Users/{id}` - Update user
- `DELETE /api/Users/{id}` - Delete user

### Animes

- `GET /api/Animes` - Get all anime
- `GET /api/Animes/{id}` - Get anime by ID
- `GET /api/Animes/genre/{genre}` - Get anime by genre (filtered query)
- `GET /api/Animes/year/{year}` - Get anime by release year (filtered query)
- `POST /api/Animes` - Create new anime
- `PUT /api/Animes/{id}` - Update anime
- `DELETE /api/Animes/{id}` - Delete anime

## Example Requests

**Create User:**
```json
POST /api/Users/register
{
  "email": "user@example.com",
  "username": "testuser",
  "password": "password123",
  "role": "Member"
}
```

**Create Anime:**
```json
POST /api/Animes
{
  "title": "Attack on Titan",
  "code": "AOT-2013",
  "synopsis": "Humanity fights giant humanoid Titans",
  "episodes": 75,
  "genre": "Action",
  "releaseYear": 2013,
  "rating": 9.0
}
```

## Validation Rules

**User:**
- Email: Required, valid email format, max 100 characters, must be unique
- Username: Required, max 50 characters
- Password: Required, min 6 characters
- Role: Required, max 20 characters

**Anime:**
- Title: Required, max 200 characters
- Code: Required, max 50 characters, must be unique
- Synopsis: Max 1000 characters
- Episodes: Required, between 1 and 10000
- Genre: Required, max 50 characters
- ReleaseYear: Required, between 1900 and 2100
- Rating: Between 0 and 10

## Project Structure
```
AnimeApi/
├── Controllers/
│   ├── UsersController.cs
│   └── AnimesController.cs
├── Models/
│   ├── User.cs
│   ├── Anime.cs
│   ├── Watchlist.cs
│   └── AnimeDbContext.cs
├── Migrations/
├── appsettings.json
└── Program.cs
```

## Author

### Tlotliso

