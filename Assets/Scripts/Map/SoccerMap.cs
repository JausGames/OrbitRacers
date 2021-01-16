using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerMap : Map
{
    [SerializeField] protected Vector2[] goals;
    [SerializeField] protected Vector2[] normal = new Vector2[] { Vector2.zero, Vector2.zero };
    [SerializeField] protected List<RaceDoor> doors = new List<RaceDoor>();

    [SerializeField] bool check = false;
    [SerializeField] bool inGame = false;
    [SerializeField] float checkTime = 0f;
    [SerializeField] float CHECKTIME = 0.1f;
    [SerializeField] private LayerMask layer;

    [SerializeField] protected SoccerMode mode;

    void Start()
    {
        allowedMode.Clear();
        allowedMode.Add("Soccer");
        layer = LayerMask.GetMask("Ball");
        mode = FindObjectOfType<SoccerMode>();
    }
    void Update()
    {
        if (mode == null) return;
        if (Time.time > checkTime)
        {
            check = true;
        }
        else if (Time.time - checkTime > -0.08f)
        {
            check = false;
        }
        if (check && mode.GetInGame())
        {
            Debug.Log("RaceDoor, FixedUpdate");
            var col = Physics2D.OverlapAreaAll(goals[0] - normal[0], goals[1] + normal[0], layer);
            Debug.Log("RaceDoor, FixedUpdate : col.Length = " + col.Length);
            
            if (col.Length > 0) 
            { 
                mode.Score(false);
            }

            Debug.Log("RaceDoor, FixedUpdate");
            col = Physics2D.OverlapAreaAll(goals[2] - normal[1], goals[3] + normal[1], layer);
            Debug.Log("RaceDoor, FixedUpdate : col.Length = " + col.Length);


            if (col.Length > 0)
            {
                mode.Score(true);
            }

            checkTime = Time.time + CHECKTIME;
        }
    }

    public void SetMode(SoccerMode mode)
    {
        this.mode = mode;
    }
    virtual public Vector2[] GetGoals()
    {
        return goals;
    }

    void OnDrawGizmos()
    {
        if (goals == null || goals.Length < 4 || mode == null) return;
        
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.green;
            if (check) Gizmos.color = Color.yellow;
            Gizmos.DrawLine(new Vector3((goals[0] - normal[0]).x, (goals[0] - normal[0]).y, 0f), new Vector3((goals[1] + normal[0]).x, (goals[1] + normal[0]).y, 0f));
            Gizmos.DrawLine(new Vector3((goals[0] + normal[0]).x, (goals[0] + normal[0]).y, 0f), new Vector3((goals[1] - normal[0]).x, (goals[1] - normal[0]).y, 0f));
            Gizmos.DrawLine(new Vector3((goals[2] - normal[1]).x, (goals[2] - normal[1]).y, 0f), new Vector3((goals[3] + normal[1]).x, (goals[3] + normal[1]).y, 0f));
            Gizmos.DrawLine(new Vector3((goals[2] + normal[1]).x, (goals[2] + normal[1]).y, 0f), new Vector3((goals[3] - normal[1]).x, (goals[3] - normal[1]).y, 0f));

        
    }
}
