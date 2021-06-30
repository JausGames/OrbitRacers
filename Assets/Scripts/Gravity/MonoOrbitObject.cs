using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoOrbitObject : PresetOrbitObject
{
    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        radius = transform.localScale.x * 10;
        SetMass();
        body.mass = mass;
        interactables.Add(parent);
        //SetSpriteColor();
    }
}
