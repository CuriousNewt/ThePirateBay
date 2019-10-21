using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    const float gizmoRadius = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(GetWaypoint(i), gizmoRadius);
            Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(GetNextIndex(i)));


        }
    }

    public int GetNextIndex(int i) {
        if (i + 1 == transform.childCount) return 0;
        return i+1;
    }

    public Vector3 GetWaypoint(int i)
    {
        return transform.GetChild(i).position;
    }
}
