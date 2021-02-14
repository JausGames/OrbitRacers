using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] private bool isAlive = true;
    //[SerializeField] private float health = 200f;
    [SerializeField] private float playerIndex = 0;
    [SerializeField] public PlayerController controller = null;
    //[SerializeField] public PlayerCombat combat = null;
    [SerializeField] public Rigidbody body;
    [SerializeField] Sprite image;
    [SerializeField] string name;
    [SerializeField] Color baseColor;
    TrailRenderer trail;
    // Start is called before the first frame update
    void Awake()
    {
        //visual = transform.Find("Visual");
        trail = GetComponentInChildren<TrailRenderer>();
        controller = GetComponent<PlayerController>();
        //combat = GetComponent<PlayerCombat>();
        body = GetComponent<Rigidbody>();
    }
    public Sprite GetPicture()
    {
        return image;
    }
    public string GetName()
    {
        return name;
    }

    public Color GetBaseColor()
    {
        return baseColor;
    }
    /*public bool GetAlive()
    {
        return isAlive;
    }
    public void SetAlive(bool value)
    {
        isAlive = value;
    }*/
    public void SetCanMove(bool value)
    {
        //combat.SetCanMove(value);
        controller.SetCanMove(value);
    }
    public void SetPlayerIndex(int value)
    {
        playerIndex = value;
        //controller.SetPlayerIndex(value);
        //combat.SetPlayerIndex(value);
    }
    public void StopMotion()
    {
        controller.StopMotion();
        trail.Clear();
        //controller.SetFreeze(true);
    }


}
