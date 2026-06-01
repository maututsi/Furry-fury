using System.Collections;
using UnityEngine;

public class HitboxDebugger : MonoBehaviour
{
    private Vector3 position;
    private Vector3 halfExtents;
    private float radius;
    private float time;
    private float duration;

    public void ShowSphere(Vector3 pos, float rad, float dur)
    {
        position = pos;
        radius = rad;
        duration = dur;
        time = Time.time;
    }

    public void ShowBox(Vector3 pos, Vector3 he, float dur)
    {
        position = pos;
        halfExtents = he;
        duration = dur;
        time = Time.time;
    }

    void OnDrawGizmos()
    {
        if (Time.time - time > duration) return;

        Gizmos.color = Color.red;
        
        if (radius != 0)
        {
            Gizmos.DrawWireSphere(position, radius);
        }
        else
        {
            Gizmos.DrawWireCube(position, halfExtents);
        }
    }
}