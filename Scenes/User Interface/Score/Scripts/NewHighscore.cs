using Godot;
using System;

public partial class NewHighscore : Godot.RichTextLabel
{

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Text = $"NOVO HIGHSCORE: {Score.Highscore}";
	}
}
