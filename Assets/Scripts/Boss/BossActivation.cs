using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivation : MonoBehaviour
{
    public JohnScript John;
    public CameraController Cam;
    public GameObject Boss;

    private void Start()
    {
        Boss.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("John"))
        {
            BossUI.instance.BossActivator();
            Camera.main.GetComponent<AudioSource>().Stop();
            StartCoroutine(WaitForBoss());
            Boss.SetActive(true);
        }
    }

    IEnumerator WaitForBoss()
    {
        John.Speed = 0f;
        John.Stop = true;
        Cam.BossFight = true;
        yield return new WaitForSeconds(2f);
        John.Speed = 1f;
        John.Stop = false;
        Destroy(gameObject);
    }
}
