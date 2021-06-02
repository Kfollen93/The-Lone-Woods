using UnityEngine;

public class LoadHome : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerSpawnPoint.enteredHomeForFirstTime = true;
        LevelManager.EnableScene(2);
    }
}