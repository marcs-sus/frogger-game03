using Godot;
using System;

public partial class HighwayLane : Node2D
{
	private Timer timer => GetNode<Timer>("Timer");
	private Node2D leftMarkers => GetNode<Node2D>("LeftMarkers");
	private Node2D rightMarkers => GetNode<Node2D>("RightMarkers");

	public override void _Ready()
	{
		SpawnVehicle();

		timer.Start();
		timer.Timeout += SpawnVehicle;
	}

	private void SpawnVehicle()
	{
		Array vehicleTypes = Enum.GetValues(typeof(VehicleType));
		foreach (Marker2D marker in leftMarkers.GetChildren())
		{
			VehicleType vehicleType = (VehicleType)vehicleTypes.GetValue(new Random().Next(vehicleTypes.Length));
			marker.AddChild(Vehicle.NewVehicle(vehicleType, Vector2.Right));
		}

		foreach (Marker2D marker in rightMarkers.GetChildren())
		{
			VehicleType vehicleType = (VehicleType)vehicleTypes.GetValue(new Random().Next(vehicleTypes.Length));
			marker.AddChild(Vehicle.NewVehicle(vehicleType, Vector2.Left));
		}
	}
}
