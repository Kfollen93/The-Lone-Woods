using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadWorld : MonoBehaviour
{
    private GameObject player;
    private CharacterController cc;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        cc = player.GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Can't move character controller using transform.position, to fix this I've turned the CC off, move the player, and then turn CC back on.
        cc.enabled = false;
        player.transform.localPosition = new Vector3(160f, 1f, 125f);
        cc.enabled = true;
        LevelManager.EnableScene(1);
    }
}
