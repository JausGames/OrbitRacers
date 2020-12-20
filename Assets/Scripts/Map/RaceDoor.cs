using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceDoor : MonoBehaviour
{
    [SerializeField] private Vector2[] position = new Vector2[2];
    [SerializeField] private Vector2 normal = Vector2.zero;
    [SerializeField] private uint number;
    [SerializeField] private uint maxNumber;
    [SerializeField] bool passingBy = false;
    [SerializeField] bool check = false;
    [SerializeField] float checkTime = 0f;
    [SerializeField] float CHECKTIME = 0.1f;
    [SerializeField] private LayerMask layer;
    [SerializeField] private RaceMode raceManager;

    void Awake()
    {
        raceManager = FindObjectOfType<RaceMode>();
        Debug.Log("RaceDoor, Awake : Layer = " + LayerMask.NameToLayer("Player"));
        layer = LayerMask.GetMask("Player");
    }
    public void SetPosition(Vector2 posA, Vector2 posB)
    {
        position[0] = posA;
        position[1] = posB;
        normal = Vector2.Perpendicular(posB - posA) / 9f;
    }
    public void SetNumber(uint number)
    {
        this.number = number;
    }
    public void SetMaxNumber(uint max)
    {
        this.maxNumber = max;
    }
    public void SetLayer(LayerMask layer)
    {
        this.layer = layer;
    }
    void FixedUpdate()
    {
        if (raceManager == null) return;
        if (Time.time > checkTime)
        {
            check = true;
        }
        else if (Time.time - checkTime > -0.08f)
        {
            check = false;
        }
        if (check)
        {
            Debug.Log("RaceDoor, FixedUpdate");
            var col = Physics2D.OverlapAreaAll(position[0] - normal, position[1] + normal, layer);
            Debug.Log("RaceDoor, FixedUpdate : col.Length = " + col.Length);
            foreach (var hit in col)
            {
                var player = hit.GetComponent<Player>();
                raceManager.PlayerPassingDoor(number, player);
                passingBy = true;
                //hitCollider.SendMessage("RaceDoor, FixedUpdate : Player Touched = " + hitCollider.gameObject);
            }
            if (col.Length > 0) { passingBy = true; }
            else { passingBy = false; }
            checkTime = Time.time + CHECKTIME;
        }
    }

    void OnDrawGizmos()
    {
        if ((position[0] != Vector2.zero && position[0] != Vector2.zero) || raceManager != null)
        {
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.green;
            if (check) Gizmos.color = Color.yellow;
            if (passingBy) Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3((position[0] - normal).x, (position[0] - normal).y,0f) , new Vector3((position[1] + normal).x, (position[1] + normal).y,0f));
            Gizmos.DrawLine(new Vector3((position[0] + normal).x, (position[0] + normal).y, 0f) , new Vector3((position[1] - normal).x, (position[1] - normal).y,0f));

        }
    }
}
