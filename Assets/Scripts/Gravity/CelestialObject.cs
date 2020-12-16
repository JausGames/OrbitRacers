using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialObject : MonoBehaviour
{
    public enum Size { Object, Satelite, Celestial, Planet , Star };

    [SerializeField] protected List<CelestialObject> interactables = new List<CelestialObject>();
    [SerializeField] protected float mass;
    [SerializeField] protected float surfaceGravity;
    [SerializeField] protected float radius;
    [SerializeField] protected bool enableSameSizeInteract = false;
    [SerializeField] protected Size size;
    [SerializeField] protected Rigidbody2D body;
    [SerializeField] protected Vector3 initSpeed = new Vector3();
    [SerializeField] protected CelestialObject parent;


    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        radius = transform.localScale.x * 10;
        SetMass();
        body.mass = mass;
        var interacts = FindObjectsOfType<CelestialObject>();
        foreach (CelestialObject obj in interacts)
        {
            if (obj.gameObject != this.gameObject && 
                (obj.GetSize() > size || obj.GetSize() == size && enableSameSizeInteract)) interactables.Add(obj);
        }
    }
    public float GetRadius()
    {
        return radius;
    }
    public float GetMass()
    {
        return mass;
    }
    public void AddMass(float value)
    {
        mass += value;
    }
    public Rigidbody2D GetBody()
    {
        return body;
    }
    public Size GetSize()
    {
        return size;
    }
    public List<CelestialObject> GetInteractables()
    {
        return interactables;
    }

    public Vector3 Position
    {
        get
        {
            return body.position;
        }
    }
    virtual public void UpdateVelocity(Vector3 acceleration, float timeStep)
    {

    }

    virtual public void UpdatePosition(float timeStep)
    {

    }

    public void SetMass()
    {
        mass = surfaceGravity * radius * radius / Universe.gravitationalConstant;
    }
}
