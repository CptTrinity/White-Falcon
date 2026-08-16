using Godot;
using System;

public partial class Level : Node
{
	[Export]
	public PackedScene Tube = null;
	[Export]
	public PackedScene GameOver = null;
	[Export]
	public PackedScene Credits;
	[Export]
	public PackedScene MainMenu;
    private bool started = false;

    private RandomNumberGenerator rng = new RandomNumberGenerator();

	private Player player;
	private Vector2 screenSize;
	private Control menu;
	private ScrollContainer creditsInstance;

    public override void _Ready()
    {
		GetTree().Paused = false;

        player = GetNode<Player>("Player");
		player.Connect("SpacePressed", Callable.From(OnPlayerSpacePressed));
		player.Connect("PlayerFell", Callable.From(OnPlayerFellOffScreen));

		screenSize = GetViewport().GetVisibleRect().Size;
        player.GlobalPosition = new Vector2(screenSize.X * 0.15f, screenSize.Y * 0.5f);

		menu = GetTree().CurrentScene.GetNodeOrNull<Control>("CanvasLayer/MainMenu");
		menu.GetNode<Button>("CenterContainer/VBoxContainer/Credits").Connect("Pressed", Callable.From(OnCreditsButtonPressed));
    }

	private void OnCreditsButtonPressed()
	{
		player.CreditsRolling = true;
		menu.QueueFree();
		creditsInstance = (ScrollContainer)Credits.Instantiate();
		creditsInstance.Connect("CreditsFinished", Callable.From(OnCreditsFinished));
		GetNode<Control>("CanvasLayer/Credits").AddChild(creditsInstance);
	}

	private void OnCreditsFinished()
	{
        player.CreditsRolling = false;
		creditsInstance.QueueFree();
		menu = (Control)MainMenu.Instantiate();
        menu.GetNode<Button>("CenterContainer/VBoxContainer/Credits").Connect("Pressed", Callable.From(OnCreditsButtonPressed));
        GetNode<CanvasLayer>("CanvasLayer").AddChild(menu);
    }

	private void OnPlayerSpacePressed()
	{
		started = true;
        if (started)
        {
            menu.QueueFree();
        }
    }

    private void OnTubeSpawnTimerTimeout() 
	{
		if (!started) return;
        int rngInt = (int)rng.RandfRange(150, screenSize.Y - 150);
		Area2D tubeInstance = (Area2D)Tube.Instantiate();
		tubeInstance.Connect("Collision", Callable.From(OnTubeColision));
		
		tubeInstance.GlobalPosition = new Vector2(screenSize.X + 300, rngInt);
		GetNode<Node2D>("Tubes").AddChild(tubeInstance);
    }

	private void OnTubeColision()
	{
		if (player.Collided) return;

        var gameOver = (Control)GameOver.Instantiate();
        GetTree().CurrentScene.GetNode("CanvasLayer").AddChild(gameOver);

        if (Score.CurrentScore > Score.Highscore)
        {
			GetTree().CallGroup("Game Over", "AddHighscore");
        }

        Score.SetHighscore();
		player.SetCollided(true);
		GetTree().Paused = true;
    }

	private void OnPlayerFellOffScreen()
	{
		OnTubeColision();
	}
}
