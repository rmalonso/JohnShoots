using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public float SecondsToDestroy;
    void Start()
    {
        Destroy(gameObject, SecondsToDestroy);
    }
}
