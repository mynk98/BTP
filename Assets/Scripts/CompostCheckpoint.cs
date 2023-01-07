using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostCheckpoint : MonoBehaviour
{
    public bool isCloseButtonPressed = false;
    [SerializeField] GameObject binCanvas;


    public static CompostCheckpoint get;

    // Start is called before the first frame update
    void Start()
    {
        get = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

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
            binCanvas.SetActive(true);
            binCanvas.GetComponentInChildren<TMPro.TMP_Text>().text = "Select the bin whoose garbage you want to Compost";
            Player.state = Player.PlayerState.composting;
            Player.ActivateUIHelper();
        }
    }
}
