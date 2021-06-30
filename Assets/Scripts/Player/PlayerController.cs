using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MassicObject
{
    private float speed = .5f;
    private float MAX_SPEED = 45f;
    private float dash = 120f;
    bool isDashing = false;
    float COUTNER_DASH_TIME = 0.1f;
    float counterDashTime;
    [SerializeField] private Vector3 move;
    [SerializeField] private float index;
    [SerializeField] Renderer bodyRenderer;


    // Start is called before the first frame update
    void Awake()
    {
        mass = 1000f;
        body = GetComponent<Rigidbody2D>();
        radius = transform.localScale.x * 10;
        surfaceGravity = (mass * Universe.gravitationalConstant * 6f) / (radius * radius); 
        body.mass = mass;
        gameObject.AddComponent<TestRadius>();
    }
    override public void SetSpriteColor()
    {
        bodyRenderer.materials[0].color = color;

        var trail = GetComponent<TrailRenderer>();
        if (trail == null) trail = GetComponentInChildren<TrailRenderer>();

        if (trail == null) return;
        // A simple 2 color gradient with a fixed alpha of 1.0f.
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(color, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
        );
        trail.colorGradient = gradient;
    }
    public void StopMotion()
    {
        body.velocity = Vector2.zero;
    }
    void FixedUpdate()
    {
        var currentSpeed = body.velocity;
        if (currentSpeed.magnitude < MAX_SPEED || Vector3.Dot(currentSpeed.normalized, move.normalized) <= 0f)
        {
            body.AddForce(move * speed * mass, ForceMode2D.Impulse);
            Debug.DrawRay(transform.position, move * speed * mass, Color.cyan);
        }
        else
        {
            var value = Vector3.Dot(currentSpeed.normalized, move.normalized);
            var perpValue = Vector3.Dot(Vector2.Perpendicular(currentSpeed).normalized, move.normalized);
            body.AddForce(Vector2.Perpendicular(currentSpeed) * value * move.magnitude * speed * mass * Mathf.Sign(perpValue));
            Debug.DrawRay(transform.position, Vector2.Perpendicular(currentSpeed) * value * speed * Mathf.Sign(perpValue), Color.cyan);
        }
        Debug.DrawRay(transform.position, body.velocity, Color.white);
        if (counterDashTime < Time.time && isDashing) CounterDash();

    }
    public void SetInteractables()
    {
        interactables.Clear();
        
        var interacts = FindObjectsOfType<CelestialObject>();
        foreach (CelestialObject obj in interacts)
        {
            if (obj.gameObject != this.gameObject && 
                (obj.GetSize() > size || obj.GetSize() == size && enableSameSizeInteract)) interactables.Add(obj);
        }
    }
    public void ClearInteractables()
    {
        interactables.Clear();
        //body.isKinematic = true;
    }

    public void SetMove(Vector2 dir)
    {
        move = dir;
    }
    public float GetPlayerIndex()
    {
        return index;
    }
    public void SetPlayerIndex(float value)
    {
        index = value;
    }
    public void SetCanMove(bool value)
    {
        if (value) body.bodyType = RigidbodyType2D.Dynamic; 
        else body.bodyType = RigidbodyType2D.Static;
    }
    public void Dash(bool perf, bool canc)
    {
        body.AddForce(move * dash, ForceMode2D.Impulse);
        Debug.DrawRay(transform.position, move * dash, Color.green);
        counterDashTime = Time.time + COUTNER_DASH_TIME;
        isDashing = true;
    }
    public void CounterDash()
    {
        body.AddForce(-body.velocity.normalized * dash, ForceMode2D.Impulse);
        Debug.DrawRay(transform.position, -body.velocity.normalized * dash, Color.green);
        isDashing = false;
    }
    void OnCollisionEnter2D(Collision2D colision)
    {
        //empty on purpose
    }
}
