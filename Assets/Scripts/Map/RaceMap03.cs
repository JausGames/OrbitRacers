using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceMap03 : RaceMap
{
    void Awake()
    {
        mapName = "RaceMap03";
        spawnPos = new Vector3[] {   new Vector3(-10, 10, 0),
                                        new Vector3(-10, 5, 0),
                                        new Vector3(-10, 0, 0),
                                        new Vector3(-10, -5, 0)
                                    };
    }
}

