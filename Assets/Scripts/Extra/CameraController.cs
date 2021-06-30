using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] List<Player> players;
    [SerializeField] float maxZoom = -30f;
    [SerializeField] float minZoom = -150f;
    // Start is called before the first frame update
    void Start()
    {
        players.AddRange(FindObjectsOfType<Player>());
    }

    // Update is called once per frame
    void Update()
    {
        

        var moy = new Vector3();

        for (int i = 0; i < players.Count; i++)
        {
            moy += players[i].transform.position;
        }

        if (players.Count == 1)
        {
            var pos = players[0].transform.position + new Vector3(0f, 0f , -70f);
            transform.position = Vector3.Slerp(transform.position, pos, 0.04f);
            transform.position = pos;
            return;

        }
        if (players.Count == 0)
        {
            return;
        }
        var dist = new List<float>();
        for (int i = 0; i < players.Count - 1; i++)
        {
            dist.Add(Vector3.Distance(players[i].transform.position, players[i + 1].transform.position) / players.Count);
        }
        var farest = dist[0];
        for (int i = 0; i < dist.Count - 1; i++)
        {
            if (dist[i + 1] > dist[i]) farest = dist[i+1];
        }

        var z = minZoom - minZoom / (farest * 0.08f + 0.2f);

        var posMoy = moy / players.Count + new Vector3(0f, 0f, Mathf.Clamp(z, minZoom, maxZoom));
        transform.position = Vector3.Slerp(transform.position, posMoy, 0.05f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position - new Vector3(0f, 18f, -25f), 2f);
    }
}
