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
			Sprite sprite = (Sprite)WallTile.Instance();
			sprite.Position = new Vector2(x * 25, y * 25);
			tree.CurrentScene.AddChild(sprite);
		}

		if(pixel.r == 1)
		{
			Sprite sprite = (Sprite)FloorTile.Instance();
			sprite.Position = new Vector2(x * 25, y * 25);
			tree.CurrentScene.AddChild(sprite);
		}

		if(pixel.g == 1)
		{
			// player spawn
		}
	}
}
