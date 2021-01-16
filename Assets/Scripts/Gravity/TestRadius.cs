using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRadius : MonoBehaviour
{
    CelestialObject parent;
    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponent<CelestialObject>();
        var circleCol = gameObject.AddComponent<CircleCollider2D>();
        circleCol.radius = 10f / parent.GetRadius() * parent.GetGravity() * 4f;
        circleCol.isTrigger = true;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        MassicObject obj = null;
        Debug.Log("CelestialObject, OnTriggerEnter : collider " + other);
        if (other.GetComponent<MassicObject>()) obj = other.GetComponent<MassicObject>();
        else if (other.GetComponentInParent<MassicObject>()) obj = other.GetComponentInParent<MassicObject>();
        else return;
        if (obj) obj.AddToCurrentInteractables(parent);

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("CelestialObject, OnTriggerExit : collider " + other);
        MassicObject obj = null;
        if (other.GetComponent<MassicObject>()) obj = other.GetComponent<MassicObject>();
        else if (other.GetComponentInParent<MassicObject>()) obj = other.GetComponentInParent<MassicObject>();
        else return;
        if (obj) obj.RemoveToCurrentInteractables(parent);

    }
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 1f);
        Gizmos.DrawWireSphere(transform.position, parent.GetGravity() * 4f);
    }
}
