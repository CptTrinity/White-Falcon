using Godot;
using System;

public partial class CreditsButton : Button
{
	[Signal]
	public delegate void PressedEventHandler();
	private void OnPressed()
	{
		EmitSignal(SignalName.Pressed);
	}
}
