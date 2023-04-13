using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegregationCheckpoint : MonoBehaviour
{
    public static bool isCloseButtonPressed = false;
    public GameObject binCanvas;
    [SerializeField] GameObject minimap;

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
            minimap.SetActive(false);
            binCanvas.SetActive(true);
            binCanvas.GetComponentInChildren<TMPro.TMP_Text>().text = "Select the bin whoose garbage you want to segregate";
            Player.state = Player.PlayerState.segregating;
            Player.ActivateUIHelper();
        }
    }
}
