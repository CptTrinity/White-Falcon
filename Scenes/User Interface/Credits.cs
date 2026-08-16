using Godot;
using System;

public partial class Credits : ScrollContainer
{
	[Signal]
	public delegate void CreditsFinishedEventHandler();
	MarginContainer margin;

	private float textBoxSize;
	private float windowSize;

	public override void _Ready()
	{
		margin = GetNode<MarginContainer>("MarginContainer");

		textBoxSize = GetNode<Godot.RichTextLabel>("MarginContainer/RichTextLabel").Size.Y;
		GD.Print(textBoxSize);

		windowSize = GetViewport().GetVisibleRect().Size.Y;

		margin.AddThemeConstantOverride("margin_top", (int)windowSize);
		margin.AddThemeConstantOverride("margin_bottom", (int)windowSize);

		StartTween();
	}

	public async void StartTween()
	{
		var tween = CreateTween();
		tween.TweenProperty(this, "scroll_vertical", 1786, 25);
		tween.Play();
		await ToSignal(tween, Tween.SignalName.Finished);
		EmitSignal(SignalName.CreditsFinished);
	}
}
