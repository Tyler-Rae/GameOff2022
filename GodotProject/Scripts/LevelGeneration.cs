using Godot;
using System;

public class LevelGeneration : Resource
{
	public bool Initialized { get; private set; }

	[Export]
	public Image[] RoomImages;

	[Export]
	public PackedScene WallTile;

	[Export]
	public PackedScene FloorTile;

	[Export]
	public PackedScene Player;

	public Vector2 cameraPositionTest = new Vector2(-600.0f, -600.0f);

	public void GenerateLevel()
	{
		Initialized = true;
	}

	public void LoadRoom(SceneTree tree)
	{
		if(RoomImages.Length == 0)
		{
			return;
		}

		Image roomImage = RoomImages[0];

		roomImage.Lock();

		for(int i = 0; i < roomImage.GetWidth(); ++i)
		{
			for(int j = 0; j < roomImage.GetHeight(); ++j)
			{
				Color pixel = roomImage.GetPixel(i, j);
				CreateTileFromPixel(i, j, pixel, tree);
			}
		}

		roomImage.Unlock();
	}

	private void CreateTileFromPixel(int x, int y, Color pixel, SceneTree tree)
	{
		if(pixel.r == 0 && pixel.g == 0 && pixel.b == 0)
		{
			Node node = WallTile.Instance();

			Transform transform = node.GetNode("Transform") as Transform;
			if(transform != null)
            {
				transform.worldPosition = new Vector3(x * 40, y * 40, 0.0f);
				transform.UpdatePosition();
            }

			tree.CurrentScene.AddChild(node);
		}

		if(pixel.r == 1)
		{
			Node node = FloorTile.Instance();

			Transform transform = node.GetNode("Transform") as Transform;
			if (transform != null)
			{
				transform.worldPosition = new Vector3(x * 40, y * 40, 0.0f);
				transform.UpdatePosition();
			}

			tree.CurrentScene.AddChild(node);
		}

		if(pixel.g == 1)
		{
			Node node = Player.Instance();

			Transform transform = node.GetNode("Transform") as Transform;
			if (transform != null)
			{
				transform.worldPosition = new Vector3(x * 40, y * 40, 0.0f);
				transform.UpdatePosition();
			}

			tree.CurrentScene.AddChild(node);
		}
	}
}
