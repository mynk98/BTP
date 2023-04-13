using UnityEngine;

namespace XEntity.Demo
{ 
    //This script includes the movement logic for the demo player.
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        //The speed the player will move at.
        public float speed = 5;

        private Rigidbody rb;
        private Vector2 input;
        public Transform Cam;
        Vector3 Movement;

        private void Awake() 
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update() 
        {
            float Horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            float Vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

            Movement = Cam.transform.right * Horizontal + Cam.transform.forward * Vertical;
            //input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed;
        }

        private void FixedUpdate() 
        {
            rb.velocity = Movement;
        }
    }
}
