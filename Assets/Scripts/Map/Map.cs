using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Map : MonoBehaviour
{
    [SerializeField] protected Vector3[] spawnPos = new Vector3[4];
    [SerializeField] protected Vector2[] doorPos;
    [SerializeField] protected List<RaceDoor> doors = new List<RaceDoor>();
    [SerializeField] protected string mapName = "";


    virtual public Vector3[] GetPositions()
    {
        Debug.Log("Map, GetPositions : PositionsLength = " + spawnPos.Length);
        return spawnPos;
    }

    virtual public string GetName()
    {
        Debug.Log("Map, GetName : MapName = " + mapName);
        return mapName;
    }

    virtual public Vector2[] GetDoors()
    {
        return doorPos;
    }
    virtual protected void CreateDoors()
    {
        for(int i = 0; i < doorPos.Length / 2; i++)
        {
            var door = gameObject.AddComponent(typeof(RaceDoor)) as RaceDoor;
            door.SetPosition(doorPos[2*i], doorPos[2*i + 1]);
            door.SetNumber((uint) (int) i);
            door.SetMaxNumber((uint) (int) doorPos.Length / 2);
            doors.Add(door);
        }
    }

}

