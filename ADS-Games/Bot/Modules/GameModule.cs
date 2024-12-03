using ADS_Games.Services;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace ADS_Games.Modules;

public class GameModule : ApplicationCommandModule<ApplicationCommandContext>
{
    [SlashCommand("play_game", "Play a game from the available list.")]
    public async Task PlayGame(string gameName)
    {
        var gameLoader = new GameLoader("GameArtifacts");

        try
        {
            var game = gameLoader.LoadGame(gameName);
            game.Start();
            await RespondAsync(InteractionCallback.Message($"Starting game: {game.Name}\n{game.Description}"));

            while (!game.IsComplete)
            {
                await RespondAsync(InteractionCallback.Message(game.GetOutput()));
                var userInput = await GetUserInput(); // Implement this method for Discord interactions
                game.ProcessInput(userInput);
            }

            await RespondAsync(InteractionCallback.Message("Game Over!"));
        }
        catch (Exception ex)
        {
            await RespondAsync(InteractionCallback.Message($"Error: {ex.Message}"));
        }
    }

    [SlashCommand("list_games", "List all available games.")]
    public async Task ListGames()
    {
        var gameLoader = new GameLoader("GameArtifacts");
        var games = gameLoader.GetAvailableGames();
        await RespondAsync(InteractionCallback.Message($"Available games: {string.Join(", ", games)}"));
    }

    private async Task<string> GetUserInput()
    {
        // Implement Discord-specific logic to collect user input
        // For now, return a placeholder
        await Task.Delay(1000); // Simulate waiting for input
        return "example input";
    }
}