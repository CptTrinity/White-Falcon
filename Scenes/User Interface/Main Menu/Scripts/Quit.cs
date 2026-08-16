using Godot;
using System;
using System.Threading.Tasks;

public partial class Quit : Button
{
	private bool canPress = false;
	
	public override void _Ready()
	{
		Timer();
	}

	private async Task Timer()
	{
		await ToSignal(GetTree().CreateTimer(0.2), SceneTreeTimer.SignalName.Timeout);
		canPress = true;
	}
	private void OnPressed()
	{
		if (canPress)
		{
			GetTree().Quit();
		}
	}
}
