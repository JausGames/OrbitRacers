using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrower : MapObject
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Vector3 direction;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float force = 2000f;
    [SerializeField] private float coolDown = 0f;
    [SerializeField] private float COOLDOWN = 1f;
    [SerializeField] private NBodySimulation nBody;
    [SerializeField] private SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
        nBody = FindObjectOfType<NBodySimulation>();
    }

    void Update()
    {
        var value = coolDown - Time.time;
        Debug.Log("ObjectThrower, Update : Color Value = " + value);
        if (Time.time > coolDown)
        {
            ThrowObject();
            coolDown = Time.time + COOLDOWN;
        }
        renderer.color = new Color(1f, value, value);
    }

    void ThrowObject()
    {
        var obj = Instantiate(projectile, startPosition, Quaternion.identity, transform);

        obj.GetComponent<Rigidbody2D>().AddForce(direction * force * obj.GetComponent<CelestialObject>().GetMass(), ForceMode2D.Impulse);
        
        Debug.Log("ObjectThrower, ThroObject : Object = " + obj.GetComponent<CelestialObject>());
    }
}
