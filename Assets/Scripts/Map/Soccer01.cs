using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soccer01 : SoccerMap
{
    void Awake()
    {
        mapName = "Stadium";
        spawnPos = new Vector3[] {   new Vector3(-25, 0, 0),
                                        new Vector3(25, 0, 0),
                                        new Vector3(-75, 0, 0),
                                        new Vector3(75, 0, 0)
                                    };
        goals = new Vector2[]
        {
            new Vector2(-75f, 12f),
            new Vector2(-75f, -12f),
            new Vector2(74f, 12f),
            new Vector2(74f, -12f)
        };

        normal = new Vector2[]
        {
            Vector2.Perpendicular(goals[0] - goals[1]) / 9f,
            Vector2.Perpendicular(goals[2] - goals[3]) / 9f,
        };
    }
}

