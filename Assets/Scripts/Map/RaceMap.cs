using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceMap : Map
{
    [SerializeField] protected Vector2[] doorPos;
    [SerializeField] protected List<RaceDoor> doors = new List<RaceDoor>();

    private void Start()
    {
        allowedMode.Clear();
        allowedMode.Add("Race");
    }
    virtual public Vector2[] GetDoors()
    {
        return doorPos;
    }
    virtual protected void CreateDoors()
    {
        for (int i = 0; i < doorPos.Length / 2; i++)
        {
            var door = gameObject.AddComponent(typeof(RaceDoor)) as RaceDoor;
            door.SetPosition(doorPos[2 * i], doorPos[2 * i + 1]);
            door.SetNumber((uint)(int)i);
            door.SetMaxNumber((uint)(int)doorPos.Length / 2);
            doors.Add(door);
        }
    }
}
