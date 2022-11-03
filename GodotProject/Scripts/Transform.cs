using Godot;
using System;

public class Transform : Node
{
    public Vector3 worldPosition;
    public Vector3 screenSpacePosition;

    public override void _Process(float delta)
    {
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        var levelGen = (LevelGeneration)GD.Load("res://Resources/LevelGeneration.tres");

        Vector3 screenSpacePosition = Coordinates.ToScreenSpace(worldPosition, levelGen.cameraPositionTest);

        Sprite sprite = GetParent() as Sprite;
        if(sprite != null)
        {
            sprite.Position = new Vector2(screenSpacePosition.x, screenSpacePosition.y);
            sprite.ZIndex = (int)screenSpacePosition.z;
        }

        AnimatedSprite animatedSprite = GetParent() as AnimatedSprite;
        if (animatedSprite != null)
        {
            animatedSprite.Position = new Vector2(screenSpacePosition.x, screenSpacePosition.y);
            animatedSprite.ZIndex = (int)screenSpacePosition.z;
        }
    }
}
