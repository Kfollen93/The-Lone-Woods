using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickUp : MonoBehaviour
{
    public GameObject gun; // Accessed from ShootGun and ShootShotty scripts.
    public GameObject shotty; // Accessed from ShootGun and ShootShotty scripts.
    private GameObject spine;
    [SerializeField] private GameObject pressFText;
    [SerializeField] private QuestGiver questGiver;
    public bool rifleWasPickedUp;
    public bool shottyWasPickedUp;
    private bool rifleIsActive = true;
    private bool rifleIsOnTable;
    private bool shotgunIsOnTable;
    private bool rifleSpawned;
    private bool shotgunSpawned;
    private bool SwitchToShotgun => Input.GetKeyDown("q") && !Input.GetMouseButton(1) && rifleIsActive;
    private bool SwitchToRifle => Input.GetKeyDown("q") && !Input.GetMouseButton(1) && !rifleIsActive;
    private bool SpawnRifle => questGiver.currentQuestIndex == 1 && !rifleSpawned && questGiver.isQuestAccepted;
    private bool SpawnShotgun => questGiver.currentQuestIndex == 2 && !shotgunSpawned && questGiver.isQuestAccepted;

    private void Start()
    {
        spine = GameObject.FindWithTag("Spine");
    }

    private void Update()
    {
        // Implement weapon switching with "Q"
        if (rifleWasPickedUp && shottyWasPickedUp)
        {
            if (SwitchToShotgun)
            {
                rifleIsActive = false;
                gun.SetActive(false);
                shotty.SetActive(true);
            }
            else if (SwitchToRifle)
            {
                shotty.SetActive(false);
                rifleIsActive = true;
                gun.SetActive(true);
            }
        }

        // Guns will appear on quest giver table.
        if (SpawnRifle)
        {
            rifleSpawned = true;
            rifleIsOnTable = true;
            gun.SetActive(true);
            pressFText.SetActive(true);
        }

        if (SpawnShotgun)
        {
            shotgunSpawned = true;
            shotgunIsOnTable = true;
            shotty.SetActive(true);
            pressFText.SetActive(true);
        }   
    }

    private void OnTriggerStay(Collider other)
    {
        // Position rifle on player model
        if (other.CompareTag("Player") && Input.GetKey("f") && !shottyWasPickedUp && rifleIsOnTable)
        {
            rifleWasPickedUp = true;
            rifleIsActive = true;
            pressFText.SetActive(false);
            gun.transform.SetParent(spine.transform);
            gun.transform.localPosition = new Vector3(0f, 0.0059f, -0.0070f);
            gun.transform.localRotation = Quaternion.Euler(-33f, -86f, -99f);
        }

        // Position shotgun on player model
        if (other.CompareTag("Player") && Input.GetKey("f") && rifleWasPickedUp && shotgunIsOnTable)
        {
            gun.SetActive(false);
            rifleIsActive = false;
            shottyWasPickedUp = true;
            pressFText.SetActive(false);
            shotty.transform.SetParent(spine.transform);
            shotty.transform.localPosition = new Vector3(0.00415f, -0.00103f, -0.00695f);
            shotty.transform.localRotation = Quaternion.Euler(-33f, 86f, -89.15f);
        }
    }
}
