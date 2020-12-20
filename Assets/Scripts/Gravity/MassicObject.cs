using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassicObject : CelestialObject
{

    [SerializeField] protected bool hasColid = false;
    Vector2 deltaVelocity = Vector2.zero;

    void Start()
    {
        body.velocity = initSpeed;
    }
    public Vector3 GetSpeed()
    {
        return body.velocity;
    }
    void OnCollisionEnter2D(Collision2D colision)
    {
        if (hasColid) return;
        foreach (ContactPoint2D contact in colision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.red);
        }
        if (colision.gameObject.GetComponent<CelestialObject>())
        {
            var obj = colision.gameObject.GetComponent<CelestialObject>();
            if (colision.gameObject.GetComponent<Player>()) return;
        }
    }

    override public void UpdateVelocity(Vector3 acceleration, float timeStep)
    {
        Debug.DrawRay(this.Position, acceleration, Color.yellow);
        deltaVelocity = new Vector2(acceleration.x * timeStep, acceleration.y * timeStep);
        Debug.DrawRay(transform.position, body.velocity, Color.white);
    }

    override public void UpdatePosition(float notUsed)
    {
        body.velocity += deltaVelocity;

    }
}


