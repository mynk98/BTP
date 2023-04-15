using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Transform FacingCamera;

    void Start()
    {
        FacingCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void LateUpdate()
    {
        transform.forward = new Vector3(FacingCamera.forward.x, transform.forward.y, FacingCamera.forward.z);

        //Vector3 Rotation = transform.eulerAngles;
        //transform.LookAt(FacingCamera);
        //transform.eulerAngles = new Vector3(Rotation.x, transform.eulerAngles.y, Rotation.z);
    }
}
