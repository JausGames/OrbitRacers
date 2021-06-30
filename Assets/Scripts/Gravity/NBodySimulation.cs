using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NBodySimulation : MonoBehaviour
{
    [SerializeField] List<CelestialObject> bodies = new List<CelestialObject>();
    static NBodySimulation instance;

    void Awake()
    {

        bodies.AddRange(FindObjectsOfType<CelestialObject>());
        Time.fixedDeltaTime = Universe.physicsTimeStep;
        Debug.Log("Setting fixedDeltaTime to: " + Universe.physicsTimeStep);
    }

    void FixedUpdate()
    {
        for (int i = 0; i < bodies.Count; i++)
        {
            if (bodies[i] == null) break;
            Vector3 acceleration = CalculateAcceleration(bodies[i].Position, bodies[i].GetInteractables());
            bodies[i].UpdateVelocity(acceleration, Universe.physicsTimeStep);
        }

        for (int i = 0; i < bodies.Count; i++)
        {
            if (bodies[i] == null) break;
            bodies[i].UpdatePosition(Universe.physicsTimeStep);
        }

    }

    public void AddObject(CelestialObject obj)
    {
        bodies.Add(obj);
    }
    public void SetPlanetColors()
    {
        foreach (CelestialObject body in bodies)
        {
            //body.SetSpriteColor();
        }
    }
    public void RemoveObject(CelestialObject obj)
    {
        bodies.Remove(obj);
    }

    public static Vector3 CalculateAcceleration(Vector3 point, List<CelestialObject> bodies)
    {
        Vector3 acceleration = Vector3.zero;
        foreach (var body in bodies)
        {
            if (body == null) break;
            var radius = body.GetRadius();
            var pos = body.Position;
            var mass = body.GetMass();
            if(mass < 0f)
            {
                Debug.Log("Repulse");
            }
            float sqrDst = Mathf.Clamp( (pos - point).sqrMagnitude, 
                                        (radius / 5f) * (body.GetRadius() / 5f), 
                                        (pos - point).sqrMagnitude);
            if (sqrDst <= 1.1f * (radius / 5f) * (radius / 5f))
            {
                Debug.Log("Check max force : max : " + (radius / 5f) * (radius / 5f) + " , value = " + sqrDst);
            }
            Vector3 forceDir = (pos - point).normalized;
            acceleration += (forceDir * Universe.gravitationalConstant * mass) / sqrDst;
            
        }

        return acceleration;
    }

    public static List<CelestialObject> Bodies
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