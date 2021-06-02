using UnityEngine;

public class HideMouse : MonoBehaviour
{
    private void Update()
    {
        MouseHide();
    }

    private void MouseHide()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
