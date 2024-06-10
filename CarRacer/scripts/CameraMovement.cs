using Godot;
using System;

public partial class CameraMovement : Camera3D
{
	private Node parent;
	private VehicleBody3D player;
	private Vector3 prevPosition;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		parent = GetParent();

		if(parent != null)
		{
			player = (VehicleBody3D)parent.GetNode("PlayerCar");
			prevPosition = player.Position;
		}
	}

	// Every frame camera adjusts position to follow player
	public override void _Process(double delta)
	{
		if(player != null)
		{
			Vector3 deltaPositionVect = player.Position - prevPosition;
			this.Position += deltaPositionVect;
			prevPosition = player.Position;
		}
	}
}
