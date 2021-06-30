using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpeed : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] LayerMask ignoreLayer;
    [SerializeField] Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        var speedX = Random.Range(0.1f, speed * speed - 0.1f);
        var signTable = new float[2] { -1f, 1f};
        var signX = Random.Range(0, 1);
        var signY = Random.Range(0, 1);
        var speedVect = new Vector3(signTable[signX] * Mathf.Sqrt(speedX), signTable[signY] * Mathf.Sqrt((speed * speed) - speedX), 0);
        body.velocity = speedVect;
    }
    private void FixedUpdate()
    {
        body.velocity = body.velocity * 1.02f;
    }
    /*void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == ignoreLayer) return;
        Debug.Log("BaseSpeed, OnCollisionEnter : speed in = " + body.velocity);
        body.velocity = new Vector3(collision.relativeVelocity.x, collision.relativeVelocity.y, 0) * 5f;
        Debug.Log("BaseSpeed, OnCollisionEnter : speed out = " + body.velocity);
    }*/

}
