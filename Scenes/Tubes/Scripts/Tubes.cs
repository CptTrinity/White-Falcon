using Godot;
using System;

public partial class Tubes : Area2D
{
    private int speed = 400;

    [Signal]
    public delegate void CollisionEventHandler();
    public override void _PhysicsProcess(double delta)
    {
        Vector2 direction = Vector2.Left;

        Position += direction * (float)delta * speed;
        if (Position.X < -300)
        {
            QueueFree();
        }
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body.Name == this.Name) return;

        EmitSignal(SignalName.Collision);
    }

    private void OnScoreBodyEntered(Node2D body)
    {
        if (body.Name == this.Name) return;
        Score.IncrementScore();
        GetNode<AudioStreamPlayer2D>("ScoreSound").Play();
    }
}
