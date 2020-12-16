using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationObject : CelestialObject
{

    [SerializeField] bool hasColid = false;
    Vector3 deltaVelocity = Vector2.zero;


    void Start()
    {
        deltaVelocity = initSpeed;
    }
    protected void UpdateVelocity(List<CelestialObject> interactables, float timeStep)
    {
        foreach (CelestialObject obj in interactables)
        {
            float sqrtDst = (obj.GetBody().position - body.position).sqrMagnitude;
            Vector3 forceDir = (obj.GetBody().position - body.position).normalized;
            Vector3 force = forceDir * Universe.gravitationalConstant * mass * obj.GetMass() / sqrtDst;
            Vector3 acceleration = force / mass;
            deltaVelocity += acceleration * timeStep;
        }
    }

    override public void UpdateVelocity(Vector3 acceleration, float timeStep)
    {
        Debug.DrawRay(this.Position, acceleration, Color.yellow);
        deltaVelocity += acceleration * timeStep;
        Debug.DrawRay(transform.position, body.velocity, Color.white);
    }

    override public void UpdatePosition(float timeStep)
    {
        body.MovePosition(body.position + new Vector2(deltaVelocity.x, deltaVelocity.y) * timeStep);

    }
}

