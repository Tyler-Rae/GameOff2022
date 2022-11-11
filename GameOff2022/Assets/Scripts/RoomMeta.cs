using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RoomMetaData
{
    public bool m_doorLeft;
    public bool m_doorRight;
    public bool m_doorUp;
    public bool m_doorDown;

    static public RoomMetaData Create()
    {
        RoomMetaData data = new RoomMetaData();
        data.m_doorLeft = false;
        data.m_doorRight = false;
        data.m_doorUp = false;
        data.m_doorDown = false;
        return data;
    }
}

public class RoomMeta : MonoBehaviour
{
    public RoomMetaData m_metaData;

    static public void ChooseDoors(ref RoomMetaData roomMetaData, float chance)
    {
        int numDoors = 1;
        numDoors += Random.Range(0.0f, 1.0f) < chance ? 1 : 0;
        numDoors += Random.Range(0.0f, 1.0f) < chance ? 1 : 0;
        numDoors += Random.Range(0.0f, 1.0f) < chance ? 1 : 0;

        List<int> doorIndex = new List<int>();
        doorIndex.Add(0);
        doorIndex.Add(1);
        doorIndex.Add(2);
        doorIndex.Add(3);

        while (numDoors > 0)
        {
            int randomIndex = Random.Range(0, doorIndex.Count);
            int index = doorIndex[randomIndex];
            doorIndex.RemoveAt(randomIndex);

            switch(index)
            {
                case 0:
                    roomMetaData.m_doorLeft = true;
                    break;
                case 1:
                    roomMetaData.m_doorUp = true;
                    break;
                case 2:
                    roomMetaData.m_doorRight = true;
                    break;
                case 3:
                    roomMetaData.m_doorDown = true;
                    break;
            }

            --numDoors;
        }
    }

    static public void AddAvailableDirections(ref List<(int, int, int, int)> directions, ref List<(int, int)> traversals, ref RoomMetaData roomMetaData, (int, int) cell)
    {
        if (roomMetaData.m_doorLeft && !traversals.Contains((cell.Item1 - 1, cell.Item2)))
        {
            directions.Add((cell.Item1, cell.Item2, -1, 0));
        }

        if (roomMetaData.m_doorRight && !traversals.Contains((cell.Item1 + 1, cell.Item2)))
        {
            directions.Add((cell.Item1, cell.Item2, 1, 0));
        }

        if (roomMetaData.m_doorUp && !traversals.Contains((cell.Item1, cell.Item2 + 1)))
        {
            directions.Add((cell.Item1, cell.Item2, 0, 1));
        }

        if (roomMetaData.m_doorDown && !traversals.Contains((cell.Item1, cell.Item2 - 1)))
        {
            directions.Add((cell.Item1, cell.Item2, 0, -1));
        }
    }

    static public void CleanDirections(ref List<(int, int, int, int)> directions, (int, int) cell)
    {
        for(int i = 0; i < directions.Count;)
        {
            (int, int, int, int) direction = directions[i];
            bool remove = false;

            int horizontalDist = Mathf.Abs(direction.Item1 - cell.Item1);
            int verticalDist = Mathf.Abs(direction.Item2 - cell.Item2);

            if (horizontalDist < 2 && verticalDist < 2 && /*orthogonal only*/ verticalDist != horizontalDist)
            {
                if( (direction.Item1 < cell.Item1 && direction.Item3 > 0) ||
                    (direction.Item1 > cell.Item1 && direction.Item3 < 0) ||
                    (direction.Item2 < cell.Item2 && direction.Item4 > 0) ||
                    (direction.Item2 > cell.Item2 && direction.Item4 < 0) )
                {
                    remove = true;
                }
            }

            if(remove)
            {
                directions.RemoveAt(i);
            }
            else
            {
                ++i;
            }
        }
    }

    static public void AssignDoorFromDirection(ref RoomMetaData roomMetaData, (int, int) direction)
    {
        if(direction.Item1 == -1)
        {
            roomMetaData.m_doorRight = true;
        }
        else if(direction.Item1 == 1)
        {
            roomMetaData.m_doorLeft = true;
        }
        else if(direction.Item2 == -1)
        {
            roomMetaData.m_doorUp = true;
        }
        else if(direction.Item2 == 1)
        {
            roomMetaData.m_doorDown = true;
        }
    }
}
