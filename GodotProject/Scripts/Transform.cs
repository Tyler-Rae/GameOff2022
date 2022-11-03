using Godot;
using System;

public class Transform : Sprite
{
    public Vector3 worldPosition;

    public override void _Process(float delta)
    {
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        var levelGen = (LevelGeneration)GD.Load("res://Resources/LevelGeneration.tres");

        Vector3 screenSpacePosition = Coordinates.ToScreenSpace(worldPosition, levelGen.cameraPositionTest);

        Position = new Vector2(screenSpacePosition.x, screenSpacePosition.y);
    }
}
