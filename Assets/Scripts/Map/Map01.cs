using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map01 : Map
{
    void Awake()
    {
        mapName = "Map01";
        spawnPos = new Vector3[] 
        {   
            new Vector3(-30, 10, 0),
            new Vector3(-30, 5, 0),
            new Vector3(-30, 0, 0),
            new Vector3(-30, -5, 0)
        };
        doorPos = new Vector2[]
        {
            new Vector2(8.6f,24),
            new Vector2(8.6f,-24),
            new Vector2(43,-21),
            new Vector2(43,-47),
            new Vector2(90,-12),
            new Vector2(117,-12),
            new Vector2(72,27),
            new Vector2(72,57),
            new Vector2(-31,51),
            new Vector2(-31,25),
            new Vector2(-33,18),
            new Vector2(-33,-10)
        };
        CreateDoors();
    }
}

