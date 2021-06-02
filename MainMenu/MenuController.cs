using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject grayBackground;
    private bool isMenuOpen;

    private void Start()
    {
        HideCursor();
    }

    private void Update()
    {
        if (!isMenuOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenu();
        }
        else if (isMenuOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMenu();
        }
    }

    private void OpenMenu()
    {
        DisplayCursor();
        isMenuOpen = true;
        grayBackground.SetActive(true);
    }

    private void CloseMenu()
    {
        HideCursor();
        isMenuOpen = false;
        grayBackground.SetActive(false);
    }

    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void DisplayCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}