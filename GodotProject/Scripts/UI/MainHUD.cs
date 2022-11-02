using Godot;
using System;

public class MainHUD : CanvasLayer
{
	private void OnStartGameButtonPressed()
	{
		GetTree().ChangeScene("res://Levels/GameEntry.tscn");
	}

	private void OnExitGameButtonPressed()
	{
		GetTree().Quit();
	}
}
