using System.Reflection;
using GameLibrary;

namespace ADS_Games.Services;

public class GameLoader
{
    private readonly string _dllDirectory;

    public GameLoader(string dllDirectory)
    {
        _dllDirectory = dllDirectory;
    }

    public IGame LoadGame(string gameName)
    {
        var dllPath = Path.Combine(_dllDirectory, $"{gameName}.dll");

        if (!File.Exists(dllPath))
        {
            throw new FileNotFoundException($"Game DLL not found: {dllPath}");
        }

        var assembly = Assembly.LoadFrom(dllPath);
        var gameType = assembly.GetTypes().FirstOrDefault(
            t => typeof(IGame).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        if (gameType == null)
        {
            throw new Exception("No class implementing IGame was found in the DLL.");
        }

        return (IGame)Activator.CreateInstance(gameType);
    }

    public IEnumerable<string> GetAvailableGames()
    {
        return Directory.GetFiles(_dllDirectory, "*.dll").Select(Path.GetFileNameWithoutExtension);
    }
}