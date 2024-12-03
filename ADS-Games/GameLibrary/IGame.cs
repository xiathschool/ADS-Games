namespace GameLibrary;

public interface IGame
{
    string Name { get; }
    string Description { get; }
    void Start(); // Initialize the game
    string GetOutput(); // Get the current state or prompt
    void ProcessInput(string input); // Process user input
    bool IsComplete { get; } // Indicate whether the game has ended
}
