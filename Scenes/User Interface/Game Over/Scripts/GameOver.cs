using Godot;
using System;

public partial class GameOver : Control
{
	[Export]
	public PackedScene NHighscore = GD.Load<PackedScene>("res://Scenes/User Interface/Score/new_highscore.tscn");
	public void AddHighscore()
	{
		var newHighscore = (Control)NHighscore.Instantiate();
		GetNode("VBoxContainer").AddChild(newHighscore);
		GetNode("VBoxContainer").MoveChild(newHighscore, 0);
	}
}
