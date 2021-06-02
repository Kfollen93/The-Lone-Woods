using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPlant : MonoBehaviour
{
    public QuestGiver questGiver;
    public bool isPlantOn; // Game is started with plant turned off, the RespawnQuestObject.cs script turns plant on.

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKey("f"))
            {
                UiCounter.Instance.killCount++;
                UiCounter.Instance.UpdateUIKills();
                gameObject.SetActive(false);

                // Stop plant from turning back on after collecting from RespawnQuestObject.cs script.
                isPlantOn = true;
            }
        }
    }
}
