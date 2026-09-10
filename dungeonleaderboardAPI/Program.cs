using DungeonLeaderboardAPI;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

builder.Services.AddSingleton<LeaderboardStore>();

var app = builder.Build();

app.MapGet("/", () =>
{
    return "Dungeon Adventure Leaderboard Server is running!";
});


// GET ALLE SCORES

app.MapGet("/api/leaderboard",
    (LeaderboardStore store) =>
    {
        var leaderboard = store.GetLeaderboard();

        return Results.Ok(leaderboard);
    });


// POST EEN NIEUWE SCORE

app.MapPost("/api/leaderboard",
    (LeaderboardEntry entry, LeaderboardStore store) =>
    {
        if (string.IsNullOrWhiteSpace(entry.PlayerName))
        {
            return Results.BadRequest(
                "Player name is required."
            );
        }

        if (entry.Score < 0)
        {
            return Results.BadRequest(
                "Invalid score."
            );
        }

        store.AddScore(entry);

        return Results.Ok(new
        {
            message = "Score added!"
        });
    });
