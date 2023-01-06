using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleButtonBehaviour : MonoBehaviour
{
    public void OnButtonClick()
    {
        int index = transform.GetSiblingIndex();
        VehicleButtons vehicleButtons = GetComponentInParent<VehicleButtons>();
        CarController currentVehicle = vehicleButtons.vehicles[index];
        vehicleButtons.lastVehicleCheckpoint.availableVehicles.Remove(currentVehicle);
        currentVehicle.enabled = true;
        currentVehicle.vehicleCamera.SetActive(true);
        currentVehicle.currentSpot.isFilled = false;
        vehicleButtons.vehicleCanvas.SetActive(false);
        vehicleButtons.crosshairCanvas.SetActive(false);
        GameObject player= GameObject.FindGameObjectWithTag("Player");
        Player.state = Player.PlayerState.driving;
        player.GetComponent<Player>().Cam.gameObject.SetActive(false);
        player.GetComponentInChildren<Camera>().transform.parent = currentVehicle.transform;
        player.SetActive(false);
        VehicleCheckpoints.get.ChangeColor();
        Player.DeactivateUIHelper();
    }
}
