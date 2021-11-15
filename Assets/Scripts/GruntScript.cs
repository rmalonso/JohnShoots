using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntScript : MonoBehaviour
{
    public GameObject John;
    public GameObject BulletPrefab;
    private Animator Animator;
    public AudioClip DeathSound;
    public AudioClip HitSound;
    Rigidbody2D Rb;

    public int Health = 3;

    private float LastShoot;


    private void Start()
    {
        Animator = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (John == null) return;
        Vector3 direction = John.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        float distance = Mathf.Abs(John.transform.position.x - transform.position.x);
        if(distance < 1.0f && Time.time > LastShoot + 1f && Health > 0)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MobilePlatform")
        {
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MobilePlatform")
        {
            transform.parent = null;
        }
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.2f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        Health--;
        Camera.main.GetComponent<AudioSource>().PlayOneShot(HitSound);
        if (Health >= 1) { StartCoroutine(Damager()); }
        if (Health <= 0)
        {
            Animator.SetBool("isDead", true);
        }
    }
    public void DestroyGrunt()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(DeathSound);
        Destroy(gameObject);
    }
    IEnumerator Damager()
    {
        GetComponent<SpriteRenderer>().material = GetComponent<BlinkScript>().Blink;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().material = GetComponent<BlinkScript>().Origin;
    }

}
