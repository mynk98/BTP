using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleCheckpoints : MonoBehaviour
{
    public enum RecycleType
    {
        paper,
        plastic,
        metal,
        glass
    }

    public RecycleType type;

    public bool isCloseButtonPressed = false;
    [SerializeField] GameObject binCanvas;
    [SerializeField] GameObject minimap;
    

    public static RecycleCheckpoints get;

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
            minimap.SetActive(false);
            binCanvas.SetActive(true);
            binCanvas.GetComponentInChildren<TMPro.TMP_Text>().text = "Select the bin whoose garbage you want to recycle";
            Player.state = Player.PlayerState.recycling;
            Player.ActivateUIHelper();
            Player.currentSelectedRecycleCheckpoint = this;
        }
    }

    public void CloseButton()
    {
        Player.DeactivateUIHelper();
        //libraryCanvas.SetActive(false);
        isCloseButtonPressed = true;
    }
}
