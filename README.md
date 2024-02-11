# BeautyLuxe - Beauty Services Booking and Management System

## Tools Used

- dotNet Core 7
- Entity Framework Core
- SQL Server
- Rider IDE / Visual Studio / Visual Studio Code

## Important Lesson

- `.gitignore` file must be used when working with git VCS to avoid uploading unnecessary files to the repository like in current case obj, bin, .rider, etc

## How To Start

- Clone the repository
- Open the project in Rider or Visual Studio
- Update the connection string in `appsettings.json` file to point to your local database
- Run `dotnet clean` to clean the project
- Run `dotnet build` to build the project
- Run `dotnet ef database update` to make your database in sync with the project
- Run `dotnet run` to run the project

## Developers Note
**Run the following commands in order**

1- `git switch main`

2- `git rm -r --cached .`

3- `git add .`

4- `git commit -m "removed gitignored files`

5- `git push -u main`

Running the above commands shall switch you to `main` branch then removed all the cached files that have been now added to gitignored. 
After that it creates a new commit with files removed and adds a upstream to main and pushed new commit.

## Developed By

- **[Abdul-Rehman](https://google.com)**
- Other contributors
