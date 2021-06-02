using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;
    private int isWalkingHash;
    private int isRunningHash;

    private void Start()
    {
        anim = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    private void Update()
    {
        bool isWalking = anim.GetBool(isWalkingHash);
        bool isRunning = anim.GetBool(isRunningHash);
        bool forwardPressed = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d");
        bool runPressed = Input.GetKey("left shift");

        if (!isWalking && forwardPressed && !Input.GetMouseButton(1))
        {
            anim.SetBool(isWalkingHash, true);
        }

        if (isWalking && forwardPressed && Input.GetMouseButton(1))
        {
            anim.SetBool(isWalkingHash, false);
        }

        if (isWalking & !forwardPressed)
        {
            anim.SetBool(isWalkingHash, false);
        }

        if (!isRunning && (forwardPressed && runPressed) && !Input.GetMouseButton(1))
        {
            anim.SetBool(isRunningHash, true);
        }

        if (isRunning && (forwardPressed || runPressed) && Input.GetMouseButton(1))
        {
            anim.SetBool(isRunningHash, false);
        }

        if (isRunning && (!forwardPressed || !runPressed))
        {
            anim.SetBool(isRunningHash, false);
        }

 
        if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
        {
            anim.SetBool("isShooting", true);
        }
        else
        {
            anim.SetBool("isShooting", false);
        }
    }
}
