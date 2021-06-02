using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideHouseText : MonoBehaviour
{
    [SerializeField] private GameObject insideHouseIMG;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            insideHouseIMG.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            insideHouseIMG.SetActive(false);
        }
    }
}
