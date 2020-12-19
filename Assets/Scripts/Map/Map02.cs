using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map02 : Map
{
    void Awake()
    {
        mapName = "Map02";
        spawnPos = new Vector3[] {   new Vector3(-30, 10, 0),
                                        new Vector3(-30, 5, 0),
                                        new Vector3(-30, 0, 0),
                                        new Vector3(-30, -5, 0)
                                    };
    }

}

