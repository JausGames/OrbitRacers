﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map01 : Map
{
    Vector3[]  positions = new Vector3[] {   new Vector3(30, 0, 0),
                                        new Vector3(-30, 0, 0),
                                        new Vector3(0, 20, 0),
                                        new Vector3(0, -20, 0)
                                    };
    string mapName = "Map01";
        
    override public Vector3[] GetPositions()
    {
        Debug.Log("Map, GetPositions : PositionsLength = " + positions.Length);
        return positions;
    }
    override public string GetName()
    {
        Debug.Log("Map, GetName : MapName = " + mapName);
        return mapName;
    }
}

