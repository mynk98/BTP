using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VehicleButtons : MonoBehaviour
{
    [SerializeField] GameObject button;
    public GameObject vehicleCanvas;
    public GameObject crosshairCanvas;
    public List<CarController> vehicles;
    public VehicleEntryExit lastVehicleCheckpoint;
    

    private void Start()
    {
        vehicleCanvas.SetActive(false);
    }

    public void AddButtons(List<CarController> vehicles,VehicleEntryExit checkpoint)
    {
        //this.vehicles.Clear();
        this.vehicles = vehicles;
        lastVehicleCheckpoint = checkpoint;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for(int i = 0; i < vehicles.Count; i++)
        {
            GameObject newButton= Instantiate<GameObject>(button, transform);
            newButton.GetComponentInChildren<TMP_Text>().text = vehicles[i].type.ToString();
        }
    }

    public void CloseButton()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        vehicleCanvas.SetActive(false);
        lastVehicleCheckpoint.isCloseButtonPressed = true;


    }


}
