using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    [SerializeField] private GameObject cutSceneCam;
    [SerializeField] private GameObject credits;
    private CharacterController cc;
    private GameObject player;
    private Animator playerAnim;
    private PlayerMovement playerMoveScript;
    private bool triggeredScene;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        cc = player.GetComponent<CharacterController>();
        playerAnim = player.GetComponent<Animator>();
        playerMoveScript = player.GetComponent<PlayerMovement>();
        playerMoveScript.enabled = true;
    }

    private void Update()
    {
        if (triggeredScene)
        {
            cc.Move(new Vector3(1, 0, 1) * Time.deltaTime * 2f); // Move player straight ahead
            playerAnim.SetBool("isWalking", true);
            playerAnim.SetBool("isRunning", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggeredScene = true;
            playerMoveScript.enabled = false;
            cutSceneCam.SetActive(true);
            credits.SetActive(true);

            // Update and complete last quest
            UiCounter.Instance.killCount++;
            UiCounter.Instance.UpdateUIKills();
        }
    }
}
