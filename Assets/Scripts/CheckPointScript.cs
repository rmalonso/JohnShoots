using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    private Animator Animator;
    public AudioClip CheckPointSound;
    public bool green = false;

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        JohnScript john = collision.GetComponent<JohnScript>();
        if (john != null)
        {
            john.SaveGame();
            Animator.SetBool("IsGreen", true);
            if (!green) Camera.main.GetComponent<AudioSource>().PlayOneShot(CheckPointSound);
            green = true;
        }   
    }
}
