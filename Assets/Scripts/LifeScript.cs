using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeScript : MonoBehaviour
{
    public AudioClip HealSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        JohnScript john = collision.GetComponent<JohnScript>();
        if (john != null)
        {
            john.Heal();
            Camera.main.GetComponent<AudioSource>().PlayOneShot(HealSound);
            Destroy(gameObject);            
        }
    }
}
