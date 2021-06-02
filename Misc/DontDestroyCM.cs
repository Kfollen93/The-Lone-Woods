using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyCM : MonoBehaviour
{
    private static bool cmExists;

    private void Awake()
    {
        if (!cmExists)
        {
            cmExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
