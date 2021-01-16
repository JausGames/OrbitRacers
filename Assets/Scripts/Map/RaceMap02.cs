using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceMap02 : RaceMap
{
    void Awake()
    {
        mapName = "RaceMap02";
        spawnPos = new Vector3[] {   new Vector3(-30, 10, 0),
                                        new Vector3(-30, 5, 0),
                                        new Vector3(-30, 0, 0),
                                        new Vector3(-30, -5, 0)
                                    };

        doorPos = new Vector2[]
        {
            new Vector2(1.8f,33.3f),
            new Vector2(1.8f,0f),
            new Vector2(48,76),
            new Vector2(95,76),
            new Vector2(-9,126),
            new Vector2(-9,155),
            new Vector2(-78,84),
            new Vector2(-119,84)
        };
        CreateDoors();
    }

}

