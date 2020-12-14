using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MassicObject
{
    private float speed = 30f;
    float mass = 50f;
    [SerializeField] private Vector3 move;
    [SerializeField] private float index;

    void FixedUpdate()
    {
        AddForces();
        body.AddForce(move * speed, ForceMode2D.Impulse);
        Debug.DrawRay(transform.position, move * speed, Color.yellow);
        Debug.DrawRay(transform.position, body.velocity, Color.white);
    }
    public void SetMove(Vector2 dir)
    {
        move = dir;
    }
    public float GetPlayerIndex()
    {
        return index;
    }
    public void SetPlayerIndex(float value)
    {
        index = value;
    }
    void OnCollisionEnter2D(Collision2D colision)
    {

    }
}
