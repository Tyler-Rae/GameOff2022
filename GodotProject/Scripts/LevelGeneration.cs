using Godot;
using System;

public class LevelGeneration : Resource
{
	public bool Initialized { get; private set; }

	public void GenerateLevel()
	{
		Initialized = true;
	}

	public void LoadRoom()
	{

	}
}
