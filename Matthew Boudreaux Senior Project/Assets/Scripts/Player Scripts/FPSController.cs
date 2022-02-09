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
    public enum ActionState
    {
        Null,
        Attack,
        Block
    }
    public enum InteractType
    {
        Dialogue,
        Pickup,
        Interactable,
        Null
    }
    public State myState;
    public ActionState myAction;
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
    
    //interaction data
    public GameObject interactable;
    public InteractType myInteractType;

    //temp attack control data
    public float attackTime = 1.0f;
    public float parryTime = 1.0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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

                //Handle action input

                switch (myAction)
                {
                    case ActionState.Null:
                        if (Input.GetMouseButtonDown(0))
                        {
                            myAction = ActionState.Attack;
                            StartAttack();

                        }
                        else if (Input.GetMouseButton(1))
                        {
                            myAction = ActionState.Block;
                            Debug.Log("Starting to block");

                        }

                        break;

                    case ActionState.Attack:

                        break;

                    case ActionState.Block:
                        if (Input.GetMouseButtonUp(1))
                        {
                            Debug.Log("Ending block");
                            myAction = ActionState.Null;
                        }
                        break;

                }


                LookForward();
                //Debug.Log("interactable: " + myInteractType);

                if (Input.GetKeyDown("e") && myInteractType == InteractType.Dialogue)
                {
                    SetState(State.Talking);
                    TriggerDialogue dialogue = interactable.gameObject.GetComponentInParent<TriggerDialogue>();
                    dialogue.DialogueTrigger();
                }
                else if (Input.GetKeyDown("e") && myInteractType == InteractType.Interactable)
                {
                    //Set case for if Interactable can trigger a cutscene here:
                    ButtonScript button = interactable.gameObject.GetComponentInParent<ButtonScript>();
                    button.InteractTrigger();
                }
                else if (Input.GetKeyDown("e") && myInteractType == InteractType.Pickup)
                {

                }


                break;

            case State.Talking:

                if (Input.GetKeyDown("e"))

                {
                    Debug.Log("Display next sentence.");
                    FindObjectOfType<DialogueManager>().DisplayNextSentence();

                }

                break;


        }

    }

    void FixedUpdate()
    {

    }
    private GameObject LookForward()
    {
        Vector3 start = myCam.transform.position;
        Vector3 forward = myCam.transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(start, forward, out hit))
        {

            if (hit.collider.gameObject.tag == "Dialogue")
            {
                interactable = hit.collider.gameObject;
                myInteractType = InteractType.Dialogue;
            }
            else if(hit.collider.gameObject.tag == "Pickup")
            {
                interactable = hit.collider.gameObject;
                myInteractType = InteractType.Pickup;
            }
            else if(hit.collider.gameObject.tag == "Interactable")
            {
                interactable = hit.collider.gameObject;
                myInteractType = InteractType.Interactable;
            }
            else
            {
                interactable = null;
                myInteractType = InteractType.Null;
            }
        }
        else
        {
            interactable = null;
            myInteractType = InteractType.Null;
        }

        return interactable;
    }



    public void SetState(State inState)
    {
        FindObjectOfType<FPSController>().myState = inState;
    }
    public static State GetState()
    {
        return FindObjectOfType<FPSController>().myState;
    }
    public void StartAttack()
    {
        //Put stuff that happens on attack here
        Debug.Log("Starting Attack");
        StartCoroutine(attackWait(attackTime));

    }

    public IEnumerator attackWait(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Attack done");
        myAction = ActionState.Null;

    }
}
