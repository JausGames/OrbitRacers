using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] Rigidbody body;
    void FixedUpdate()
    {
        transform.position = body.position - offset;
    }
}
