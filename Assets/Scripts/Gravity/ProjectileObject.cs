using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObject : MassicObject
{
    private float initScale;
    private float initMass;
    private float initTime;
    private float initTrailSize;
    private float initTrailTime;
    private float timeBeforeConsuming = 3f;
    private float LIFETIME = 3f;
    private float timeLeft = 3f;
    [SerializeField] private NBodySimulation nBody;
    private TrailRenderer trail;
    // Start is called before the first frame update
    void Start()
    {
        trail = GetComponentInChildren<TrailRenderer>();
        nBody = FindObjectOfType<NBodySimulation>();
        nBody.AddObject(this);
        initTrailSize = trail.startWidth;
        initTrailTime = trail.time;
        initScale = transform.localScale.x;
        initMass = mass;
        initTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time < initTime + timeBeforeConsuming && !hasColid) return;
        timeLeft -= Time.fixedDeltaTime;
        mass = initMass * timeLeft / LIFETIME;
        body.mass = mass;
        trail.startWidth = initTrailSize * timeLeft / LIFETIME;
        trail.time = initTrailTime * timeLeft / LIFETIME;
        var value = initScale * timeLeft / LIFETIME;
        if (timeLeft > 1.5f)
        {
            transform.localScale = new Vector3(value, value, 0f);
        }
        else
        {
            value = value * timeLeft / 1.5f;
            transform.localScale = new Vector3(value, value, 0f);
            if (transform.localScale.x < 0.01f)
            {
                nBody.RemoveObject(this);
                Destroy(this.gameObject);
            }
        }

    }

    void OnCollisionEnter2D(Collision2D colision)
    {
        if (hasColid) return;
        hasColid = true;
    }
}
