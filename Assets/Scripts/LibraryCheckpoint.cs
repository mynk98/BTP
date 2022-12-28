using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryCheckpoint : MonoBehaviour
{
    public bool isCloseButtonPressed;
    public GameObject libraryCanvas;

    private void OnTriggerEnter(Collider other)
    {
        isCloseButtonPressed = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (isCloseButtonPressed == true) return;

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) return;
        if (other.tag == "Player" && Player.state != Player.PlayerState.driving)
        {
            libraryCanvas.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    public void CloseButton()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        libraryCanvas.SetActive(false);
        isCloseButtonPressed = true;
    }
}
