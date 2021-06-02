using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private bool worldHasBeenLoaded;

    // Public method for OnClick event
    public void LoadGame()
    {
        if (!worldHasBeenLoaded)
        {
            worldHasBeenLoaded = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            SceneManager.LoadScene(1);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            LevelManager.EnableScene(1); // Enable the scene if the game has already been loaded once.
        }
    }    
}
