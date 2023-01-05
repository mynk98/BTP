using UnityEngine;
using System.Collections;
public class CarController : MonoBehaviour
{
    public WheelCollider WheelFL;
    public WheelCollider WheelFR;
    public WheelCollider WheelRL;
    public WheelCollider WheelRR;
    public Transform WheelFLtrans;
    public Transform WheelFRtrans;
    public Transform WheelRLtrans;
    public Transform WheelRRtrans;
    public Vector3 eulertest;
    float maxFwdSpeed = 15;
    float maxBwdSpeed = 5;
    float gravity = 9.8f;
    private bool braked = false;
    private float maxBrakeTorque = 500;
    private Rigidbody rb;
    public Transform centreofmass;
    private float maxTorque = 1000;
    const float fixedMaxTorque=1000;

    public enum VehicleType
    {
        GarbageTruck,
        Car
    }

    public VehicleType type = VehicleType.GarbageTruck;
    public GameObject vehicleCamera;
    public VehicleSpot currentSpot;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centreofmass.transform.localPosition;
    }

    void FixedUpdate()
    {
        if (!braked)
        {
            WheelFL.brakeTorque = 0;
            WheelFR.brakeTorque = 0;
            WheelRL.brakeTorque = 0;
            WheelRR.brakeTorque = 0;
        }
        //speed of car, Car will move as you will provide the input to it.

        if (Player.state == Player.PlayerState.driving)
        {
            if (rb.velocity.magnitude >= maxFwdSpeed && Input.GetAxis("Vertical") > 0 ||
                rb.velocity.magnitude >= maxBwdSpeed && Input.GetAxis("Vertical") < 0)
            {
                maxTorque = 0;
            }
            else maxTorque = fixedMaxTorque;
           // print("speed=" + rb.velocity.magnitude + " torque: " + maxTorque);
            WheelRR.motorTorque = maxTorque * Input.GetAxis("Vertical");
            WheelRL.motorTorque = maxTorque * Input.GetAxis("Vertical");

            //changing car direction
            //Here we are changing the steer angle of the front tyres of the car so that we can change the car direction.
            WheelFL.steerAngle = 30 * (Input.GetAxis("Horizontal"));
            WheelFR.steerAngle = 30 * Input.GetAxis("Horizontal");
        }
       
    }
    void Update()
    {
        HandBrake();

        //for tyre rotate
        /*WheelFLtrans.Rotate(WheelFL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        WheelFRtrans.Rotate(WheelFR.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        WheelRLtrans.Rotate(WheelRL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        WheelRRtrans.Rotate(WheelRL.rpm / 60 * 360 * Time.deltaTime, 0, 0);*/
        //changing tyre direction
        /*Vector3 temp = WheelFLtrans.localEulerAngles;
        Vector3 temp1 = WheelFRtrans.localEulerAngles;*/
        /*temp.y = WheelFL.steerAngle - (WheelFLtrans.localEulerAngles.z);
        WheelFLtrans.localEulerAngles = temp;
        temp1.y = WheelFR.steerAngle - WheelFRtrans.localEulerAngles.z;
        WheelFRtrans.localEulerAngles = temp1;
        eulertest = WheelFLtrans.localEulerAngles;*/
    }
    void HandBrake()
    {
        //Debug.Log("brakes " + braked);
        if (Input.GetButton("Jump"))
        {
            braked = true;
        }
        else
        {
            braked = false;
        }
        if (braked)
        {

            WheelRL.brakeTorque = maxBrakeTorque * 20;//0000;
            WheelRR.brakeTorque = maxBrakeTorque * 20;//0000;
            WheelRL.motorTorque = 0;
            WheelRR.motorTorque = 0;
        }
    }
}