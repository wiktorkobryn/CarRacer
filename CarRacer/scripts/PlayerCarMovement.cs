using Godot;
using System;

public partial class PlayerCarMovement : VehicleBody3D
{
	// KEY FUNCTIONALITIES
	// movement: arrow keys (opposite for breaking)
	// breaks: Space key
	// reset position: R key
	
	public float MaxSteer {get; set;} = 0.4f; 
	public float EnginePower { get; set; } = 1400.0f;
	public float BrakeForce { get; set; } = 2000.0f;
	public float BrakingFactor { get; set; } = 0.95f;
	public Vector3 SpawnPosition {get; set;} = new Vector3(0.0f, 0.3f, 20.0f);
	public Vector3 SpawnRotation {get; set;} =  new Vector3(0.0f, Mathf.DegToRad(-45.0f), 0.0f);
	
	// called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Setup
	}

	// called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// car movement
		Steering = Input.GetAxis("ui_right", "ui_left") * MaxSteer;
		if (Input.IsActionPressed("ui_break"))
			BrakeCar();
		else
			EngineForce = Input.GetAxis("ui_down", "ui_up") * EnginePower;

		if(Input.IsActionJustPressed("ui_reset"))
			ResetPosToDefault();
	}

	private void ResetPosToDefault()
	{
		Basis basis = new Basis(Quaternion.FromEuler(SpawnRotation));
		Transform3D newTransform = new Transform3D(basis, SpawnPosition);
		this.Transform = newTransform;
		LinearVelocity = Vector3.Zero;
		AngularVelocity = Vector3.Zero;
	}

	private void BrakeCar()
	{
		if(LinearVelocity.Length() > 0.0f)
		{
			//apply breaking force over time
			EngineForce = 0.0f;
			LinearVelocity *= BrakingFactor;
			AngularVelocity *= BrakingFactor;
		}
		else
		{
			// stop the car
			EngineForce = 0.0f;
			LinearVelocity = Vector3.Zero;
			AngularVelocity = Vector3.Zero;
		}
	}
}
