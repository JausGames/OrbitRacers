using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NBodySimulation : MonoBehaviour
{
    CelestialObject[] bodies;
    static NBodySimulation instance;

    void Awake()
    {

        bodies = FindObjectsOfType<CelestialObject>();
        Time.fixedDeltaTime = Universe.physicsTimeStep;
        Debug.Log("Setting fixedDeltaTime to: " + Universe.physicsTimeStep);
    }

    void FixedUpdate()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            Vector3 acceleration = CalculateAcceleration(bodies[i].Position, bodies[i].GetInteractables());
            bodies[i].UpdateVelocity(acceleration, Universe.physicsTimeStep);
            //bodies[i].UpdateVelocity (bodies[i].GetInteractables(), Universe.physicsTimeStep);
        }

        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].UpdatePosition(Universe.physicsTimeStep);
        }

    }

    public static Vector3 CalculateAcceleration(Vector3 point, List<CelestialObject> bodies)
    {
        Vector3 acceleration = Vector3.zero;
        foreach (var body in bodies)
        {
            float sqrDst = Mathf.Clamp( (body.Position - point).sqrMagnitude, 
                                        (body.GetRadius() / 5f) * (body.GetRadius() / 5f), 
                                        (body.Position - point).sqrMagnitude);
            if (sqrDst <= 1.1f * (body.GetRadius() / 5f) * (body.GetRadius() / 5f))
            {
                Debug.Log("Check max force : max : " + (body.GetRadius() / 5f) * (body.GetRadius() / 5f) + " , value = " + sqrDst);
            }
            Vector3 forceDir = (body.Position - point).normalized;
            acceleration += forceDir * Universe.gravitationalConstant * body.GetMass() / sqrDst;
            
        }

        return acceleration;
    }

    public static CelestialObject[] Bodies
    {
        get
        {
            return Instance.bodies;
        }
    }

    static NBodySimulation Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<NBodySimulation>();
            }
            return instance;
        }
    }
}