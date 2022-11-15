using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EDoorDirection
{
    Left,
    Right,
    Up,
    Down
};

public class DoorMeta : MonoBehaviour
{
    public EDoorDirection m_doorDirection;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            LevelGenerator levelGen = GameObject.FindObjectOfType<LevelGenerator>();
            if(levelGen != null)
            {
                levelGen.Travel(m_doorDirection);
            }
        }
    }
}
