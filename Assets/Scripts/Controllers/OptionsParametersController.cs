using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsParametersController : MonoBehaviour
{
    private void Awake()
    {
        var noDestroyOptionsParameters = FindObjectsOfType<OptionsParametersController>();
        if (noDestroyOptionsParameters.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
