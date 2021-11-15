using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackScript : MonoBehaviour
{
    public float Speed;
    Rigidbody2D Rb;
    Vector2 Direction;
    JohnScript target;

    public float KnockbackForceX;
    public float KnockbackForceY;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<JohnScript>();
        Rb = GetComponent<Rigidbody2D>();
        Direction = (target.transform.position - transform.position).normalized * Speed;
        Rb.velocity = new Vector2(Direction.x, Direction.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        JohnScript John = collision.GetComponent<JohnScript>();
        if (John != null && John.isAlive())
        {
            John.Hit();
        }
        if (collision.transform.position.x > transform.position.x)
        {
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(KnockbackForceX, KnockbackForceY), ForceMode2D.Force);
        }
        else
        {
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(-KnockbackForceX, KnockbackForceY), ForceMode2D.Force);
        }
    }    
}
