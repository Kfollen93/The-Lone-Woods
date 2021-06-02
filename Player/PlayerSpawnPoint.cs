using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    private GameObject player;
    public static bool enteredHomeForFirstTime;

    private void OnEnable()
    {
        if (enteredHomeForFirstTime)
        {
            player = GameObject.FindWithTag("Player");
            CharacterController cc = player.GetComponent<CharacterController>();
            cc.enabled = false;
            player.transform.position = transform.position;
            cc.enabled = true;
            Debug.Log("PLAYER ENTERED HOME IS TRUE");
        }
    }
}

/* The Character Controller does NOT change its position, therefore, the fix is to disable the controller momentarily,
   move the player, and then re-enable the controller.  This took a while to figure out, and it's something to remember! */