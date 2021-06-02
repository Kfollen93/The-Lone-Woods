using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnQuestObjects : MonoBehaviour
{
    /* Regarding the IF checks - It's possible that the current quest index is == X, but that the player didn't actually accept the quest yet.
       Need to ensure the player has accepted the quest, so that the objects spawn only when player has accepted the quest */

    public QuestGiver questGiver;

    [Header("Rat Header")]
    [SerializeField] private GameObject[] rats;
    private bool ratsSpawned;
    private bool RatQuestIsActive => !ratsSpawned && questGiver.currentQuestIndex == 3 && questGiver.isQuestAccepted;

    [Header("Plant Collectable Header")]
    public CollectPlant collectPlantScript;
    [SerializeField] private GameObject collectablePlantGO;
    private bool PlantQuestIsActive => !collectPlantScript.isPlantOn && questGiver.currentQuestIndex == 4 && questGiver.isQuestAccepted;

    [Header("Wolves Header")]
    [SerializeField] private GameObject[] wolves;
    private bool wolvesSpawned;
    private bool WolfQuestIsActive => !wolvesSpawned && questGiver.currentQuestIndex == 5 && questGiver.isQuestAccepted;

    [Header("Boss Header")]
    [SerializeField] private GameObject bossBarrier;
    private bool bossBarrierRemoved;
    private bool BossQuestIsActive => !bossBarrierRemoved && questGiver.currentQuestIndex == 6 && questGiver.isQuestAccepted;

    private void Update()
    {
        SpawnRats();
        TurnOnCollectablePlant();
        SpawnWolves();
        RemoveBossBarrier();
    }

    private void SpawnRats()
    {
        if (RatQuestIsActive)
        {
            foreach (GameObject rat in rats)
            {
                if (!rat.activeSelf)
                {
                    rat.SetActive(true);
                }
            }
            ratsSpawned = true;
        }
    }

    private void TurnOnCollectablePlant()
    {
        if (PlantQuestIsActive)
        {
            collectablePlantGO.SetActive(true);
        }
    }

    private void SpawnWolves()
    {
        if (WolfQuestIsActive)
        {
            foreach (GameObject wolf in wolves)
            {
                if (!wolf.activeSelf)
                {
                    wolf.SetActive(true);
                }
            }
            wolvesSpawned = true;
        }
    }

    private void RemoveBossBarrier()
    {
        if (BossQuestIsActive)
        {
            bossBarrier.SetActive(false);
            bossBarrierRemoved = true;
        }
    }
}
