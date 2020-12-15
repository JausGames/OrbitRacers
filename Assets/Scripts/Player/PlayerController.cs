using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MassicObject
{
    private float speed = 30f;
    private float MAX_SPEED = 50f;
    private float dash = 120f;
    float mass = 50f;
    bool isDashing = false;
    float COUTNER_DASH_TIME = 0.1f;
    float counterDashTime;
    [SerializeField] private Vector3 move;
    [SerializeField] private float index;

    void Start()
    {
        body.isKinematic = true;
    }
    void FixedUpdate()
    {
        AddForces();
        var currentSpeed = body.velocity;
        if (currentSpeed.magnitude < MAX_SPEED || Vector3.Dot(currentSpeed.normalized, move.normalized) <= 0f)
        {
            body.AddForce(move * speed, ForceMode2D.Impulse);
            Debug.DrawRay(transform.position, move * speed, Color.yellow);
        }
        else
        {
            var value = Vector3.Dot(currentSpeed.normalized, move.normalized);
            var perpValue = Vector3.Dot(Vector2.Perpendicular(currentSpeed).normalized, move.normalized);
            body.AddForce(Vector2.Perpendicular(currentSpeed) * value * move.magnitude * Mathf.Sign(perpValue));
            Debug.DrawRay(transform.position, Vector2.Perpendicular(currentSpeed) * value * Mathf.Sign(perpValue), Color.yellow);
        }
        Debug.DrawRay(transform.position, body.velocity, Color.white);
        if (counterDashTime < Time.time && isDashing) CounterDash();

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
        Debug.DrawRay(transform.position, move * dash, Color.blue);
        counterDashTime = Time.time + COUTNER_DASH_TIME;
        isDashing = true;
    }
    public void CounterDash()
    {
        body.AddForce(-body.velocity.normalized * dash, ForceMode2D.Impulse);
        Debug.DrawRay(transform.position, -body.velocity.normalized * dash, Color.blue);
        isDashing = false;
    }
    void OnCollisionEnter2D(Collision2D colision)
    {

    }
}
