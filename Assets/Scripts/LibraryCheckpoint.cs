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
            Player.ActivateUIHelper();
            
        }
    }

    public void CloseButton()
    {
        Player.DeactivateUIHelper();
        libraryCanvas.SetActive(false);
        isCloseButtonPressed = true;
    }
}
