using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JohnScript : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float JumpForce = 3.5f;
    public float Speed = 1f;
    public AudioClip JumpSound;
    public AudioClip HitSound;
    public AudioClip DeathSound;
    public AudioClip ReloadSound;
    private GameObject HealthBar;
    public GameOverController GameOverController;

    private int Ammo;
    private int MaxAmmo = 6;
    public TMPro.TextMeshProUGUI AmmoCountText;


    public PauseController Pause;

    public Rigidbody2D Rb;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;
    public bool Stop;
    private float LastShoot;
    private float Health, MaxHP = 5f;

    void Start()
    {
        Stop = false;
        Ammo = MaxAmmo;
        AmmoCountText.text = Ammo.ToString();
        Health = MaxHP;
        SaveGame();
        HealthBar = GameObject.Find("HealthBar");
        Rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        GameOverController = GameObject.Find("GameOver").GetComponent<GameOverController>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        FlipCharacter();

        //Variables que controlan los estados del animator
        Animator.SetBool("running", Horizontal != 0.0f);
        Animator.SetBool("jumping", Grounded != true);

        //Control de cuando john toca el suelo o no mediante rayo que apunta hacia el suelo
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }else Grounded = false;

        if (Input.GetKeyDown(KeyCode.Space) && Grounded && !Stop)
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(JumpSound);
            Jump();
        }

        //Caída

        if(transform.position.y <= -0.7f && Health>0)
        {
            Hit();
            if (Health>0 && InfoGame.gameSaved)
            {
                LoadGame();
            }
        }

        //Recarga
        if (Ammo <= 0 || Input.GetKey(KeyCode.R))
        {
            Animator.SetBool("isReloading", true);
        }

        if (Input.GetKey(KeyCode.E) && Time.time > LastShoot + 0.5f && !Stop)
        {
            if(Ammo >= 1)
            {
                Shoot();
                Ammo--;
                AmmoCountText.text = Ammo.ToString();
                LastShoot = Time.time;
            }
        }
    }
    private void FixedUpdate()
    {
        if(Health > 0)
        {
            Movement();
        }        
    }

    //Corrección de sprite dependiendo de si se mueve a la derecha o la izquierda
    private void FlipCharacter()
    {
        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
    private void Movement()
    {
        Rb.velocity = new Vector2(Horizontal * Speed, Rb.velocity.y);        
    }
    private void Jump()
    {
        Rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.2f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }
    private void Reload()
    {
        Ammo = MaxAmmo;
        Camera.main.GetComponent<AudioSource>().PlayOneShot(ReloadSound);
        AmmoCountText.text = Ammo.ToString();
        Animator.SetBool("isReloading", false);
    }
    
    public void Hit()
    {
        Health = Health - 1f;
        if (Health >= 1) { StartCoroutine(Damager()); }
        HealthBar.SendMessage("UploadHealthBar");
        Camera.main.GetComponent<AudioSource>().PlayOneShot(HitSound);
        Animator.SetBool("isHurt", true); 
        
        if (Health == 0)
        {
            Die();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "MobilePlatform")
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
    public void Die()
    {
        Animator.SetBool("isDead", true);
    }
    public bool isAlive()
    {
        return (Health > 0);
    }
    public void JohnDeathSound()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(DeathSound);
    }
    public void Hurt()
    {
        Animator.SetBool("isHurt", false);
    }
    public void Heal()
    {
        Health = MaxHP;
        HealthBar.SendMessage("UploadHealthBar");
    }
    public float getHealth()
    {
        return Health / MaxHP;
    }

    public void SaveGame()
    {
        InfoGame.infoJohn.Position = transform.position;    
        InfoGame.gameSaved = true;
    }
    public void LoadGame()
    {
        transform.position = InfoGame.infoJohn.Position;
    }

    public void GameFinished()
    {
        GameOverController.GameOver();
    }
    IEnumerator Damager()
    {
        GetComponent<SpriteRenderer>().material = GetComponent<BlinkScript>().Blink;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().material = GetComponent<BlinkScript>().Origin;
    }

}
