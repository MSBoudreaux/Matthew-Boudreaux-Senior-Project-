using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public enum State
    {
        FreeMovement,
        Talking,
        Cutscene
    }
    public State myState;
    public GameObject self;

    //movement data
    public float speed = 12f;
    public CharacterController controller;
    public Camera myCam;
    Vector3 velocity;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float height = 1f;
    public float crouchSpeed = 0.5f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded = true;
    public bool isCrouched = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

        switch (myState)
        {

            case State.FreeMovement:

                isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

                if (Input.GetKey(KeyCode.LeftControl))
                {
                    isCrouched = true;
                }

                if (isGrounded && velocity.y < 0)
                {
                    velocity.y = -2f;
                }

                float x = Input.GetAxis("Horizontal");
                float z = Input.GetAxis("Vertical");

                if (isCrouched && isGrounded)
                {
                    //Debug.Log("crouching");
                    self.transform.localScale = new Vector3(1, height * 0.5f, 1);
                }

                Vector3 move = new Vector3();
                if (isCrouched)
                {
                    move = transform.right * x * crouchSpeed + transform.forward * z * crouchSpeed;
                }
                else
                {
                    move = transform.right * x + transform.forward * z;
                }
                controller.Move(move * speed * Time.deltaTime);




                if (Input.GetButtonDown("Jump") && isGrounded && !isCrouched)
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * gravity * -2);
                }

                velocity.y += gravity * Time.deltaTime;


                controller.Move(velocity * Time.deltaTime);


                if (!Input.GetKey((KeyCode.LeftControl)))
                {
                    isCrouched = false;
                    //Debug.Log("not crouch");
                    self.transform.localScale = new Vector3(1, height, 1);
                }

                break;


        }
    }


    public void SetState(State inState)
    {
        FindObjectOfType<FPSController>().myState = inState;
    }
    public static State GetState()
    {
        return FindObjectOfType<FPSController>().myState;
    }
}
