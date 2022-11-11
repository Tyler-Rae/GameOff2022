using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D m_testTexture;

    public GameObject m_wallPrefab;
    public GameObject m_floorPrefab;
    public GameObject m_playerPrefab;
    public GameObject m_emptyObjectPrefab;

    private bool m_initialized = false;

    private Dictionary<(int, int), RoomMetaData> m_levelGrid = new Dictionary<(int, int), RoomMetaData>();

    public uint m_numberOfRooms;
    public float m_doorChance;

    private (int, int) m_currentRoom = (0, 0);
    private GameObject m_generatedObjects;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!m_initialized)
        {
            int maxTries = 10;
            while (maxTries-- > 0 && !GenerateFloor()) { }

            //{
            //    int count = 0;
            //    foreach((int, int) key in m_levelGrid.Keys)
            //    {
            //        GameObject.Instantiate(m_floorPrefab, new Vector3(key.Item1, count++ * 0.05f, key.Item2), Quaternion.identity);
            //    }
            //}

            TravelToRoom(m_currentRoom, (0, -1));

            m_initialized = true;
        }
    }

    private void TravelToRoom((int, int) coords, (int, int) fromDirection)
    {
        // destroy previous rooom
        if(m_generatedObjects != null)
        {
            GameObject.Destroy(m_generatedObjects);
        }

        m_generatedObjects = GameObject.Instantiate(m_emptyObjectPrefab, Vector3.zero, Quaternion.identity);
        m_generatedObjects.name = "Generated Objects (LevelGen)";

        // create new room
        LoadLevel(m_testTexture);

        // place player at the appropriate doorway
        Quaternion facingQuat = Quaternion.identity;
        if (fromDirection.Item1 == 1)
        {
            facingQuat = Quaternion.Euler(0.0f, 270.0f, 0.0f);
        }
        else if (fromDirection.Item1 == -1)
        {
            facingQuat = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        }
        else if (fromDirection.Item2 == 1)
        {
            facingQuat = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else if (fromDirection.Item2 == -1)
        {
            facingQuat = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }

        GameObject playerObject = GameObject.Instantiate(m_playerPrefab, Vector3.zero, facingQuat);
        playerObject.transform.SetParent(m_generatedObjects.transform);
    }

    private bool GenerateFloor()
    {
        uint roomsToPlace = m_numberOfRooms;


        RoomMetaData startRoom = RoomMetaData.Create();

        RoomMeta.ChooseDoors(ref startRoom, m_doorChance);
        roomsToPlace -= 1;

        m_levelGrid.Add((0, 0), startRoom);

        List<(int, int, int, int)> availableDirections = new List<(int, int, int, int)>();
        List<(int, int)> traversals = new List<(int, int)>();

        RoomMeta.AddAvailableDirections(ref availableDirections, ref traversals, ref startRoom, (0, 0));
        traversals.Add((0, 0));

        while(roomsToPlace > 0)
        {
            if(availableDirections.Count == 0)
            {
                m_levelGrid.Clear();
                return false;
            }

            int index = Random.Range(0, availableDirections.Count);
            (int, int, int, int) direction = availableDirections[index];
            availableDirections.RemoveAt(index);

            RoomMetaData newRoom = RoomMetaData.Create();
            RoomMeta.AssignDoorFromDirection(ref newRoom, (direction.Item3, direction.Item4));
            RoomMeta.ChooseDoors(ref newRoom, m_doorChance);
            roomsToPlace -= 1;

            (int, int) newCell = (direction.Item1 + direction.Item3, direction.Item2 + direction.Item4);
            m_levelGrid.Add(newCell, newRoom);
            RoomMeta.AddAvailableDirections(ref availableDirections, ref traversals, ref newRoom, newCell);
            traversals.Add(newCell);

            RoomMeta.CleanDirections(ref availableDirections, newCell);
        }

        return true;
    }

    private void LoadLevel(Texture2D levelTexture)
    {
        for (int x = 0; x < levelTexture.width; ++x)
        {
            for (int y = 0; y < levelTexture.height; ++y)
            {
                Color pixel = levelTexture.GetPixel(x, y);

                if (pixel.r == 0 && pixel.g == 0 && pixel.b == 0)
                {
                    if (m_wallPrefab != null)
                    {
                        GameObject newInstance = GameObject.Instantiate(m_wallPrefab, new Vector3(x, 0.0f, y), Quaternion.identity);
                        newInstance.transform.SetParent(m_generatedObjects.transform);
                    }
                }

                if (pixel.r == 1)
                {
                    if (m_floorPrefab != null)
                    {
                        GameObject newInstance = GameObject.Instantiate(m_floorPrefab, new Vector3(x, 0.0f, y), Quaternion.identity);
                        newInstance.transform.SetParent(m_generatedObjects.transform);
                    }
                }
            }
        }
    }
}
