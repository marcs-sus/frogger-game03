using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] private int TileSize = 16;
	[Export] private float MovementDuration = 0.3f;

	private bool isMoving = false;
	private Vector2 inputDirection = Vector2.Zero;

	public override void _PhysicsProcess(double delta)
	{
		inputDirection = Vector2.Zero;
		if (Input.IsActionPressed("up")) inputDirection += Vector2.Up;
		if (Input.IsActionPressed("down")) inputDirection += Vector2.Down;
		if (Input.IsActionPressed("left")) inputDirection += Vector2.Left;
		if (Input.IsActionPressed("right")) inputDirection += Vector2.Right;

		if (inputDirection != Vector2.Zero)
			HandleMovement(inputDirection.Normalized());
	}

	private void HandleMovement(Vector2 direction)
	{
		if (!isMoving && direction != Vector2.Zero)
		{
			isMoving = true;

			Tween tween = CreateTween();
			tween.TweenProperty(this, "position", Position + direction * TileSize, MovementDuration);
			tween.TweenCallback(Callable.From(() => isMoving = false));
		}
	}
}
