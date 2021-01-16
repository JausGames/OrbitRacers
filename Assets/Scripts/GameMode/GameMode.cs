using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class GameMode : MonoBehaviour
{
    protected string mode;
    static private List<string> modes = new List<string>{"Race", "Soccer"};

    abstract public void ResetGame();

    virtual public void StartGame()
    {

    }

    static public List<string> GetModes()
    {
        return modes;
    }
    static public string GetMode(string  value)
    {
        if (value.Substring(0, 4) == "Race") return modes[0] + "Mode";
        return modes[1] + "Mode"; 
    }
    static public int GetIdByName(string mode)
    {
        for (int i = 0; i < modes.Count; i++)
        {
            if (modes[i] == mode) return i;
        }
        return 0;
    }
    virtual public void PlayFireworks(ParticleSystem particle)
    {
        particle.Play();
    }
}
