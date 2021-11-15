using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float Speed;
    public AudioClip Sound;

    private Rigidbody2D Rigidbody2D;
    private Vector2 Direction;
    public float KnockbackForceX;
    public float KnockbackForceY;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction*Speed;
    }

    public void SetDirection(Vector2 direction)
    {
        if (direction.x < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (direction.x > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        Direction = direction;
    }
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        JohnScript John = collision.GetComponent<JohnScript>();
        GruntScript Grunt = collision.GetComponent<GruntScript>();
        BossScript Boss = collision.GetComponent<BossScript>();
        BulletScript Bullet = collision.GetComponent<BulletScript>();
        if (John != null && John.isAlive()) { 
            John.Hit();
        }
        if (Grunt != null) Grunt.Hit();
        if (Boss != null) Boss.Hit();
        if (Bullet == null)  DestroyBullet();
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
