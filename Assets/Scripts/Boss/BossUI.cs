using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    public GameObject BossHealth;
    public BossScript Boss;
    public GameObject Walls;
    public static BossUI instance;
    // Start is called before the first frame update
    void Start()
    {
        BossHealth.SetActive(false);
        Walls.SetActive(false);
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void BossActivator()
    {
        BossHealth.SetActive(true);
        Walls.SetActive(true);
    }
    public void BossDeactivator()
    {
        BossHealth.SetActive(false);
        Walls.SetActive(false);
    }
}
