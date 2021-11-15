using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    Rigidbody2D Rb;

    public float MaxHP, CurrentHealth;
    public Image BossBar;
    public float TimeToShoot, CountDown;
    public float TimeToTeleport, CountDownTP;

    public AudioClip HitSound;
    public AudioClip Attack;
    public AudioClip BossDeath;

    public Transform InitialPos;
    public Transform[] Spots;
    public GameObject BossAttack;
    JohnScript John;
    Animator Animator;

    private AudioSource BossMusic;    

    WinController WinController;
    void Start()
    {
        BossMusic = GetComponent<AudioSource>();
        BossMusic.Play();
        Animator = GetComponent<Animator>();
        John = FindObjectOfType<JohnScript>();
        CurrentHealth = MaxHP;
        transform.position = InitialPos.position;
        CountDown = TimeToShoot;
        CountDownTP = TimeToTeleport;
        WinController = GameObject.Find("MissionCompleted").GetComponent<WinController>();
    }
    private void Update()
    {
        CountDowns();
        if (John.getHealth() == 0)
        {
            BossMusic.Stop();
        }
    }
    public void CountDowns()
    {
        CountDown -= Time.deltaTime;
        CountDownTP -= Time.deltaTime;
        if (CountDown <= 0)
        {
            Animator.SetBool("IsShooting", true);
            CountDown = TimeToShoot;            
        }
        if (CountDownTP <= 0)
        {
            CountDownTP = TimeToTeleport;
            Teleport();
        }
    }

    public void ShootPlayer()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Attack);
        var Mouth = new Vector3(transform.position.x, transform.position.y-0.2f,transform.position.z);
        GameObject spell = Instantiate(BossAttack, Mouth, Quaternion.identity);
    }

    public void StopShooting()
    {
        Animator.SetBool("IsShooting", false);
    }

    public void Teleport()
    {
        var InitialPosition = Random.Range(0, Spots.Length);
        transform.position = Spots[InitialPosition].position;
    }
    public void Hit()
    {
        CurrentHealth--;
        UpdateHealthBarBoss();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(HitSound);
        if (CurrentHealth >= 1) { StartCoroutine(Damager()); }
        if (CurrentHealth <= 0)
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(BossDeath);
            Animator.SetBool("IsDead", true);
        }
    }
    public void UpdateHealthBarBoss()
    {
        var amount = CurrentHealth / MaxHP;
        BossBar.transform.localScale = new Vector2(amount, 1);
    }
    public void BossScale()
    {
        if (transform.position.x > John.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else { transform.localScale = new Vector3(1, 1, 1); }
    }

    public void Death()
    {
        BossUI.instance.BossDeactivator();
        WinController.MissionCompleted();
        Destroy(gameObject);
    }
    IEnumerator Damager()
    {
        GetComponent<SpriteRenderer>().material = GetComponent<BlinkScript>().Blink;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().material = GetComponent<BlinkScript>().Origin;
    }
}
