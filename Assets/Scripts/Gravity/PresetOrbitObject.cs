using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetOrbitObject : GravitationObject
{
    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        radius = transform.localScale.x * 10;
        SetMass();
        body.mass = mass;
        SetSpriteColor();
    }
}
