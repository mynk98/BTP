using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleEntryExit : MonoBehaviour
{
    public List<CarController> availableVehicles;
    [SerializeField] GameObject vehicleCanvas;

    public static ParticleSystem particleSystem;
    public static VehicleEntryExit get;

    [SerializeField] Player player;

    private void Start()
    {
        //particleSystem = GetComponent<ParticleSystem>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        get = this;
    }



    private void OnTriggerStay (Collider other)
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) return;
        if (other.tag == "Player" && Player.state!=Player.PlayerState.driving && availableVehicles.Count>0)
        {
            vehicleCanvas.SetActive(true);
            vehicleCanvas.GetComponentInChildren<VehicleButtons>().AddButtons(availableVehicles,this);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        else if (other.tag=="Vehicle" && Player.state == Player.PlayerState.driving && other.GetComponent<Rigidbody>().velocity.magnitude <=1)
        {
            bool flag = false;
            VehicleSpot spot=transform.GetChild(1).GetComponent<VehicleSpot>();
            foreach(var vehicleSpot in GetComponentsInChildren<VehicleSpot>())
            {
                if (!vehicleSpot.isFilled)
                {
                    flag = true;
                    spot = vehicleSpot;
                    break;
                }
            }
            if (!flag) return;

            other.transform.position = spot.transform.position;
            spot.isFilled = true;
            CarController currentCar = other.GetComponent<CarController>();
            currentCar.GetComponent<Rigidbody>().velocity = Vector3.zero;
            currentCar.currentSpot = spot;
            currentCar.vehicleCamera.SetActive(false);
            currentCar.enabled = false;
            availableVehicles.Add(currentCar);

            player.Cam.gameObject.SetActive(true);
            player.transform.position = transform.GetChild(0).position;
            player.gameObject.SetActive(true);
            Player.state = Player.PlayerState.idle;
            vehicleCanvas.GetComponentInChildren<VehicleButtons>().crosshairCanvas.SetActive(true);
            VehicleCheckpoints.get.ChangeColor();

        }
    }

    /*public static void ChangeColor()
    {
        bool isEmptySpot = false;
        foreach (var vehicleSpot in get.GetComponentsInChildren<VehicleSpot>())
        {
            if (!vehicleSpot.isFilled)
            {
                isEmptySpot = true;
                break;
            }
        }

        var main = get.GetComponent<ParticleSystem>().main;
        if (Player.state == Player.PlayerState.driving)
        {

            if (isEmptySpot) main.startColor = Color.red;
            else main.startColor = Color.gray;

        }
        else
        {
            if(get.availableVehicles.Count>0) main.startColor = Color.blue;
            else main.startColor = Color.gray;

        }
    }*/


}
