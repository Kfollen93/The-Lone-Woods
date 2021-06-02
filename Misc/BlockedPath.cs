using UnityEngine;

public class BlockedPath : MonoBehaviour
{
    [SerializeField] private GameObject blockedPathIMG;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            blockedPathIMG.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            blockedPathIMG.SetActive(false);
        }
    }
}
