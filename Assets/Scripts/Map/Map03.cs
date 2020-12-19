using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map03 : Map
{
    void Awake()
    {
        mapName = "Map03";
        spawnPos = new Vector3[] {   new Vector3(-10, 10, 0),
                                        new Vector3(-10, 5, 0),
                                        new Vector3(-10, 0, 0),
                                        new Vector3(-10, -5, 0)
                                    };
    }
}

