using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassicObject : CelestialObject
{

    [SerializeField] bool hasColid = false;
    Vector2 deltaVelocity = Vector2.zero;

    void Start()
    {
        body.velocity = initSpeed;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hasColid)
        {
            //UpdateVelocity(interactables);
            Debug.DrawRay(transform.position, body.velocity, Color.white);
        }
        else
        {
        }
    }
    public Vector3 GetSpeed()
    {
        return body.velocity;
    }
    /*virtual protected void UpdateVelocity(List<CelestialObject> interactables)
    {
        var force = Vector2.zero;
        foreach (CelestialObject obj in interactables)
        {
            var rbody = obj.GetBody();
            var vect =  obj.transform.position - this.transform.position;
            var dist = vect.sqrMagnitude;
            force += new Vector2((vect.normalized * Universe.gravitationalConstant * (mass + obj.GetMass()) / dist).x, 
                                ( vect.normalized * Universe.gravitationalConstant * (mass + obj.GetMass()) / dist).y);
        }
        body.AddForce(force * mass, ForceMode2D.Impulse);
        Debug.DrawRay(this.Position, force, Color.yellow);
    }*/
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
            /*if (obj.GetSize() > size && this.size <= Size.Satelite)
            {
                obj.AddMass(mass);
                mass = 0f;
                body.velocity = Vector3.zero;
                body.freezeRotation = true;
                body.isKinematic = true;
                transform.SetParent(obj.gameObject.transform);
                hasColid = true;
            }*/
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


