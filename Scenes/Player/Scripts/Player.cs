using Godot;
using System;
using System.Collections.Generic;

public partial class Player : CharacterBody2D
{
	public bool CreditsRolling { get; set; } = false;
    public bool Collided { get; private set; } = false;
	private bool spacePressedSignalEmitted = false;
    private bool flying = true;
	private const float jumpVelocity = -400.0f;

    [Signal]
	public delegate void SpacePressedEventHandler();
	[Signal]
	public delegate void PlayerFellEventHandler();

	public void SetCollided(bool value)
	{
        if (!value)
		{
            Collided = value;
			return;
		}
		Collided = value;
		Vector2 velocity = Velocity;
		velocity.Y = 0;
		Velocity = velocity;
        GetNode<AudioStreamPlayer2D>("CollisionSound").Play();
    }


    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		var animation = GetNode<AnimationPlayer>("Sprite2D/AnimationPlayer");
		if (GlobalPosition.Y > GetViewport().GetVisibleRect().Size.Y + 300 && !Collided)
		{
            GetNode<AudioStreamPlayer2D>("CollisionSound").Play();
            EmitSignal(SignalName.PlayerFell);
		}
        
        if (!IsOnFloor() && !flying)
		{
			velocity += GetGravity() * (float)delta;
		}
		else
		{
			animation.Play("flapSlower");
		}

		if (!flying && !spacePressedSignalEmitted)
		{
            EmitSignal(SignalName.SpacePressed);
			spacePressedSignalEmitted = !spacePressedSignalEmitted;
        }

		if (Input.IsActionJustPressed("ui_accept") && !Collided && !CreditsRolling)
		{
			flying = false;
            animation.Play("flap");
            GetNode<AudioStreamPlayer2D>("FlappingSound").Play();

            velocity.Y = jumpVelocity;
		}

		Velocity = velocity;
		MoveAndSlide();
	}

    public override void _Process(double delta)
    {
        if (Velocity.Y < 0)
		{
			if (Rotation <= 0) return;
			Rotate((float)delta * Velocity.Y / 100);
		}
		else
		{
			if (Rotation >= Mathf.Pi / 2) return;
            Rotate(2f * (float)delta * Velocity.Y / 1000);
        }
    }
}
