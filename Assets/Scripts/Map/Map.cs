using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Map : MonoBehaviour
{
    [SerializeField] protected Vector3[] spawnPos = new Vector3[4];
    [SerializeField] protected string mapName = "";
    [SerializeField] protected List<string> allowedMode;
    [SerializeField] protected Sprite picture;

    virtual public Vector3[] GetPositions()
    {
        Debug.Log("Map, GetPositions : PositionsLength = " + spawnPos.Length);
        Debug.Log("Map, GetPositions : Position[0] = " + spawnPos[0]);
        Debug.Log("Map, GetPositions : Position[1] = " + spawnPos[1]);
        Debug.Log("Map, GetPositions : Position[2] = " + spawnPos[2]);
        Debug.Log("Map, GetPositions : Position[3] = " + spawnPos[3]);
        return spawnPos;
    }

    virtual public string GetName()
    {
        Debug.Log("Map, GetName : MapName = " + mapName);
        return mapName;
    }
    public bool FindInModes(string name)
    {
        foreach (string mode in allowedMode)
        {
            if (mode == name) return true;
        }
        return false;
    }
    public Sprite GetPicture()
    {
        return picture;
    }


}

