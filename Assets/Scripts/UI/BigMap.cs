using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMap : MonoBehaviour
{
    public GameObject minimap;
    public GameObject bigmap;

    public float zoomSpeed = 10f;
    public float minZoom = 50f;
    public float maxZoom = 500f;

    public Camera cam;

    public float panSpeed = 2f;         // Speed of camera movement
    public float minX = -350;          // Minimum X value for panning
    public float maxX = 500;           // Maximum X value for panning
    public float minZ = -600f;          // Minimum Z value for panning
    public float maxZ = 350;           // Maximum Z value for panning

    private bool isPanning;            // Flag to indicate whether panning is in progress
    private Vector3 lastMousePosition; // Last position of the mouse


    private void Start()
    {
        minimap.SetActive(true);
        bigmap.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !bigmap.activeInHierarchy)
        {
            Player.ActivateUIHelper();
            minimap.SetActive(false);
            bigmap.SetActive(true);
            cam.orthographicSize = maxZoom;

        }
        else if (Input.GetKeyDown(KeyCode.M) && bigmap.activeInHierarchy)
        {
            Player.DeactivateUIHelper();
            minimap.SetActive(true);
            bigmap.SetActive(false);
        }

        if (bigmap.activeInHierarchy)
        {
            float zoom = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            float size = cam.orthographicSize - zoom;
            size = Mathf.Clamp(size, minZoom, maxZoom);
            cam.orthographicSize = size;


            // Check if left mouse button is pressed
            if (Input.GetMouseButtonDown(0))
            {
                isPanning = true;
                lastMousePosition = Input.mousePosition;
            }

            // Check if left mouse button is released
            if (Input.GetMouseButtonUp(0))
            {
                isPanning = false;
            }

            // If panning is in progress
            if (isPanning)
            {
                print("panning");
                // Calculate the distance moved by the mouse
                Vector3 delta = Input.mousePosition - lastMousePosition;

                // Calculate the new position of the camera based on the distance moved
                Vector3 newPosition = cam.transform.position - new Vector3(delta.x, 0f, delta.y) * panSpeed * Time.unscaledDeltaTime;

                print(newPosition);

                // Clamp the new position to the minimum and maximum X and Z values
                newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
                newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

                // Set the new position of the camera
                cam.transform.position = newPosition;

                // Update the last mouse position
                lastMousePosition = Input.mousePosition;
            }
        }
    }
}
