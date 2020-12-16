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


    // Start is called before the first frame update
    void Awake()
    {
        mass = 1000f;
        body = GetComponent<Rigidbody2D>();
        radius = transform.localScale.x * 10;
        body.mass = mass;
        var interacts = FindObjectsOfType<CelestialObject>();
        foreach (CelestialObject obj in interacts)
        {
            if (obj.gameObject != this.gameObject &&
                (obj.GetSize() > size || obj.GetSize() == size && enableSameSizeInteract)) interactables.Add(obj);
        }
    }
    void Start()
    {
    }
    /*override protected void UpdateVelocity(List<CelestialObject> interactables)
    {
        var force = Vector2.zero;
        foreach (CelestialObject obj in interactables)
        {
            var rbody = obj.GetBody();
            var vect = obj.transform.position - this.transform.position;
            var dist = vect.sqrMagnitude;
            force += new Vector2((vect.normalized * (Universe.gravitationalConstant * (mass + obj.GetMass())) / dist).x,
                                (vect.normalized * (Universe.gravitationalConstant * (mass + obj.GetMass())) / dist).y);
        }
        body.AddForce(10f * force, ForceMode2D.Impulse);
        Debug.DrawRay(this.Position, force, Color.yellow);
    }*/
    void FixedUpdate()
    {
        //UpdateVelocity(interactables);

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
            if (obj.gameObject != this.gameObject && obj.GetSize() >= size) interactables.Add(obj);
        }
        body.isKinematic = true;
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
        body.isKinematic = !value;
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

    }
}
