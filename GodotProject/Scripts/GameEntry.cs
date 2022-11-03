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

	}
}
