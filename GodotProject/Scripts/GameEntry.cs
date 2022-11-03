using Godot;
using System;

public class GameEntry : Node2D
{
	public override void _Ready()
	{
		var levelGen = (LevelGeneration)GD.Load("res://Resources/LevelGeneration.tres");

		if(!levelGen.Initialized)
		{
			GD.Print("generating level ...");

			levelGen.GenerateLevel();
			GetTree().ReloadCurrentScene();

			return;
		}

		GD.Print("entered level ...");

		levelGen.LoadRoom(GetTree());
	}

    public override void _Process(float delta)
    {
		var levelGen = (LevelGeneration)GD.Load("res://Resources/LevelGeneration.tres");

		if (Input.IsActionPressed("ui_left"))
        {
			levelGen.cameraPositionTest.x += 5.0f;
        }
		if (Input.IsActionPressed("ui_right"))
		{
			levelGen.cameraPositionTest.x -= 5.0f;
		}
		if (Input.IsActionPressed("ui_down"))
		{
			levelGen.cameraPositionTest.y -= 5.0f;
		}
		if (Input.IsActionPressed("ui_up"))
		{
			levelGen.cameraPositionTest.y += 5.0f;
		}
	}
}
