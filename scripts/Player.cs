using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] private int TileSize = 16;
	[Export] private float MovementDuration = 0.3f;

	private bool isMoving = false;
	private Vector2 inputDirection = Vector2.Zero;
	private AnimatedSprite2D animatedSprite => GetNode<AnimatedSprite2D>("AnimatedSprite2D");

	public override void _PhysicsProcess(double delta)
	{
		inputDirection = Vector2.Zero;

		if (Input.IsActionJustPressed("up")) HandleMovement(Vector2.Up);
		else if (Input.IsActionJustPressed("down")) HandleMovement(Vector2.Down);
		else if (Input.IsActionJustPressed("left")) HandleMovement(Vector2.Left);
		else if (Input.IsActionJustPressed("right")) HandleMovement(Vector2.Right);
	}

	private void HandleMovement(Vector2 direction)
	{
		if (!isMoving && direction != Vector2.Zero)
		{
			isMoving = true;
			animatedSprite.Play("hop");

			Tween tween = CreateTween();
			tween.TweenProperty(this, "position", Position + direction * TileSize, MovementDuration);
			tween.TweenCallback(Callable.From(() => isMoving = false));
			tween.Finished += () => animatedSprite.Play("idle");
		}
	}
}
