## GAME CLUB API

### Environment
- Require install .Net sdk 6.0
- Default local environment in development
- Can set a machine environment ```Env:ASPNETCORE_ENVIRONMENT = "Development"```

### Run
- Goto API folder to run: ```dotnet run```
- The API in local development run at: ```https://localhost:7227```

#### Migrations CLI
- Go to Infrastructure folder to create new migration(or remove):
- Add a migration: ```dotnet ef migrations add [AddedFileName] -o DataAccess/Migrations```
- Remove before migration: ```dotnet ef migrations remove```
- Remove before migration force: ```dotnet ef migrations remove -f```
- Apply migration: ```dotnet ef database update```
