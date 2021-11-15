using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlatform : MonoBehaviour
{
    public Transform from;

    public Transform Target;
    public float speed;

    private Vector3 start, end;


    private void Start()
    {
        if(Target.parent != null)
        {
            Target.parent = null;
            start = transform.position;
            end = Target.position;
        }
    }
    // pinta linea de movimiento de la plataforma
    private void OnDrawGizmosSelected()
    {
        if(from != null && Target != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(from.position, Target.position);
            Gizmos.DrawSphere(from.position, 0.02f);
            Gizmos.DrawSphere(Target.position, 0.02f);

        }
    }
    private void FixedUpdate()
    {
        if(Target != null)
        {
            float fixedSpeed = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, Target.position, fixedSpeed);
        }
        if(transform.position == Target.position)
        {
            Target.position = (Target.position == start) ? end : start;
        }
    }
}
