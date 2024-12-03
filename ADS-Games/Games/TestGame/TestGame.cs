using GameLibrary;

namespace TestGame;

public class TestGame : IGame
{
    private int _turns;
    private string _output;
    private bool _isComplete;

    public string Name => "Test Game";
    public string Description => "A simple test game to demonstrate game integration.";

    public bool IsComplete => _isComplete;

    public void Start()
    {
        _turns = 0;
        _output = "Welcome to the Test Game! Type anything to play.";
        _isComplete = false;
    }

    public string GetOutput()
    {
        return _output;
    }

    public void ProcessInput(string input)
    {
        _turns++;

        if (_turns < 3)
        {
            _output = $"Turn {_turns}: You typed '{input}'. Keep going!";
        }
        else
        {
            _output = "Game over! Thanks for playing.";
            _isComplete = true;
        }
    }
}