using Godot;
using System;

public class PlayerController : AnimatedSprite
{
    public override void _Ready()
    {
        Stop();

		Transform transform = GetNode("Transform") as Transform;
		transform.worldPosition.z = 40.0f;
	}

    public override void _Process(float delta)
    {
        Vector3 moveVec = new Vector3();

		if (Input.IsActionPressed("ui_left"))
		{
			moveVec.x -= 1.0f;
			moveVec.y -= 1.0f;
		}
		if (Input.IsActionPressed("ui_right"))
		{
			moveVec.x += 1.0f;
			moveVec.y += 1.0f;
		}
		if (Input.IsActionPressed("ui_up"))
		{
			moveVec.x += 1.0f;
			moveVec.y -= 1.0f;
		}
		if (Input.IsActionPressed("ui_down"))
		{
			moveVec.x -= 1.0f;
			moveVec.y += 1.0f;
		}

		if(moveVec.LengthSquared() > 0.0f)
        {
			Transform transform = GetNode("Transform") as Transform;
			transform.worldPosition += moveVec.Normalized() * 3.0f;

			Play("walk");
        }
        else
        {
			Stop();
        }
	}
}
