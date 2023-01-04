using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCheckpoints : MonoBehaviour
{
    public static VehicleCheckpoints get; 

    // Start is called before the first frame update
    void Start()
    {
        get = this;
        ChangeColor();
    }

    public void ChangeColor()
    {
        VehicleEntryExit[] checkpoints = GetComponentsInChildren<VehicleEntryExit>();

        foreach(VehicleEntryExit checkpoint in checkpoints)
        {
            bool isEmptySpot = false;
            foreach (var vehicleSpot in checkpoint.GetComponentsInChildren<VehicleSpot>())
            {
                if (!vehicleSpot.isFilled)
                {
                    isEmptySpot = true;
                    break;
                }
            }

            var main = checkpoint.GetComponent<ParticleSystem>().main;
            if (Player.state == Player.PlayerState.driving)
            {

                if (isEmptySpot) main.startColor = Color.red;
                else main.startColor = Color.gray;

            }
            else
            {
                if (checkpoint.availableVehicles.Count > 0) main.startColor = Color.blue;
                else main.startColor = Color.gray;

            }
        }

        
    }
}
