using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassicObject : MonoBehaviour
{
    public enum Size { Ball, Player, Celestial, Planet };

    [SerializeField] private float mass;
    [SerializeField] List<MassicObject> interactables = new List<MassicObject>();
    [SerializeField] Vector3 initForce = new Vector3();
    [SerializeField] Size size;
    [SerializeField] bool hasColid = false;
    [SerializeField] MassicObject parent;
    protected Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.mass = mass;
        var interacts = FindObjectsOfType<MassicObject>();
        foreach(MassicObject obj in interacts)
        {
            if (obj.gameObject != this.gameObject && obj.GetSize() <= size) interactables.Add(obj);
        }
        body.AddForce(initForce, ForceMode2D.Impulse);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hasColid)
        {
            AddForces();
            Debug.DrawRay(transform.position, body.velocity, Color.white);
        }
        else
        {
        }
    }

    public Rigidbody2D GetRigidbody()
    {
        return body;
    }
    public float GetMass()
    {
        return mass;
    }
    public void AddMass(float value)
    {
        Debug.Log("Add mass : " + this.gameObject);
        mass += value;
    }
    public Size GetSize()
    {
        return size;
    }
    public Vector3 GetSpeed()
    {
        return body.velocity;
    }
    protected void AddForces()
    {
        foreach (MassicObject obj in interactables)
        {
            var rbody = obj.GetRigidbody();
            var vect = this.transform.position - obj.transform.position;
            var dist = vect.magnitude;
            rbody.AddForce(vect.normalized * (mass + obj.GetMass()) / (dist * dist), ForceMode2D.Impulse);
        }
    }
    void OnCollisionEnter2D(Collision2D colision)
    {
        foreach (ContactPoint2D contact in colision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.red);
        }
        if (colision.gameObject.GetComponent<MassicObject>())
        {
            var obj = colision.gameObject.GetComponent<MassicObject>();
            /*if (obj.GetSize() > size)
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
}
