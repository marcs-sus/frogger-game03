using Godot;
using System;
using System.Collections.Generic;

public enum VehicleType
{
	Truck,
	Motorcycle,
	NormalCar,
	SportCar
}

public enum VehicleStates
{
	NormalSpeed,
	SlowSpeed,
	Overtaking,
}

public partial class Vehicle : Area2D
{
	[Export] public VehicleType vehicleType = VehicleType.NormalCar;

	private readonly Dictionary<VehicleType, float> vehicleSpeeds = new Dictionary<VehicleType, float>()
	{
		{ VehicleType.Truck, 50.0f },
		{ VehicleType.Motorcycle, 150.0f },
		{ VehicleType.NormalCar, 100.0f },
		{ VehicleType.SportCar, 200.0f }
	};
	private Vector2 direction = Vector2.Left;
	private readonly static PackedScene vehicleScene = GD.Load<PackedScene>("res://scenes/vehicle.tscn");

	public static Vehicle NewVehicle(VehicleType vehicleType, Vector2 direction)
	{
		Vehicle vehicle = vehicleScene.Instantiate<Vehicle>();
		vehicle.vehicleType = vehicleType;
		vehicle.direction = direction;
		return vehicle;
	}

	public override void _Process(double delta)
	{
		Position = Position.Lerp(Position + direction * vehicleSpeeds[vehicleType] * (float)delta, 1.0f);
	}
}
