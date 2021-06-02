using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    // Manage footsteps from level manager to turn off the sound if the player goes to the main menu
    public AudioSource audioSrc;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    // This is being called using Unity Animation Events
    public void Step()
    {
        audioSrc.volume = Random.Range(0.22f, 0.25f);
        audioSrc.pitch = Random.Range(1.0f, 1.0f);
        audioSrc.Play();
    }
}