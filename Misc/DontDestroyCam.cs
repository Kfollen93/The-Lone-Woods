using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyCam : MonoBehaviour
{
    private static bool cameraExists;

    private void Awake()
    {
        if (!cameraExists)
        {
            cameraExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
