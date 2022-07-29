using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public enum State
    {
        FreeMovement,
        Talking,
        Menu,
        Cutscene
    }
    public enum ActionState
    {
        Null,
        Attack,
        Block,
        Parry,
        UseItem
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

    public PlayerAnimator myAnim;

    //movement data
    public float speed = 12f;
    public CharacterController controller;
    public Camera myCam;
    Vector3 velocity;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
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

    //inventory data
    public PlayerInventory inventory;
    public GameObject inventoryUI;

    //temp attack control data
    public PlayerStats myStats;
    public float attackTime;
    public float parryTime;
    public Coroutine c;

    /* use to implement I-frames later
    float iFrames = 0.5f;
    public bool isIFramed;
    */



    // Start is called before the first frame update
    void Start()
    {
        myAnim = myStats.myAnim;
    }

    // Update is called once per frame
    void Update()
    {

        switch (myState)
        {

            case State.FreeMovement:

                UpdateStats();

                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    Debug.Log("Menu Opened");
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    myState = State.Menu;
                    inventoryUI.SetActive(true);
                    break;
                }

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
                        if (Input.GetMouseButtonDown(0) && myStats.equippedWeapon.amount != 0)
                        {
                            Debug.Log("Starting Attack");
                            myAction = ActionState.Attack;
                            myAnim.AttackAnimStart();
                            c = StartCoroutine(attackWait(attackTime));


                        }
                        else if (Input.GetMouseButton(1) && myStats.equippedShield.amount != 0)
                        {
                            myAction = ActionState.Parry;
                            c = StartCoroutine(parryWait(parryTime));
                            myAnim.ParryAnimStart();
                            Debug.Log("Starting to block");

                        }

                        else if (Input.GetKeyDown("q"))
                        {
                            if (myStats.consumableObject.IsConsumable && myStats.consumableObject.healValue != 0)
                            {
                                myAction = ActionState.UseItem;
                                myStats.UseItem(myStats.equippedConsumable);
                                c = StartCoroutine(itemWait(2.0f));
                                Debug.Log("Using Item");
                            }
                        }

                        break;

                    case ActionState.Attack:
                        break;

                    case ActionState.Parry:

                        if (Input.GetMouseButtonUp(1))
                        {
                            Debug.Log("Ending block early");
                            myAnim.BlockAnimEnd();
                            StopCoroutine(c);
                            c = null;
                            myAction = ActionState.Null;

                        }
                        break;

                    case ActionState.Block:
                        if (Input.GetMouseButtonUp(1))
                        {
                            Debug.Log("Ending block");
                            myAnim.BlockAnimEnd();
                            myAction = ActionState.Null;
                        }
                        break;
                    case ActionState.UseItem:
                        
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
                    
                    TriggerScript trigger = interactable.gameObject.GetComponentInParent<TriggerScript>();

                    if (trigger.requiresKey && !trigger.isUnlocked)
                    {
                        bool keyFound = false;

                        for (int i = 0; i < inventory.inventory.ConsumableInventory.Count; i++)
                        {
                            if (inventory.inventory.ConsumableInventory[i].ID == 8 && inventory.inventory.ConsumableInventory[i].amount != 0)
                            {
                                myStats.UseItem(inventory.inventory.ConsumableInventory[i].item);
                                trigger.InteractTrigger();
                                TriggerDialogue dialogue = interactable.gameObject.GetComponentInParent<TriggerDialogue>();
                                dialogue.TimedDialogueTrigger(1);
                                keyFound = true;
                            }
                        }
                        if (!keyFound)
                        {
                            TriggerDialogue dialogue = interactable.gameObject.GetComponentInParent<TriggerDialogue>();
                            dialogue.TimedDialogueTrigger(0);

                        }
                    }
                    else
                    {
                        trigger.InteractTrigger();
                    }


                }
                else if (Input.GetKeyDown("e") && myInteractType == InteractType.Pickup)
                {
                    var tempItem = interactable.GetComponentInParent<GroundItem>();

                        inventory.inventory.AddItem(new Item(tempItem.item));
                        Destroy(interactable.GetComponent<Transform>().root.gameObject);
                    
                }


                break;

            case State.Talking:

                if (Input.GetKeyDown("e"))

                {
                    Debug.Log("Display next sentence.");
                    FindObjectOfType<DialogueManager>().DisplayNextSentence();

                }

                break;

            case State.Menu:
                //Open menu and do menu things here

                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    myState = State.FreeMovement;
                    inventoryUI.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    Debug.Log("Menu Closed");
                    
                }

                break;
        }

    }

    private void OnApplicationQuit()
    {
        inventory.inventory.WeaponInventory.Clear();
        inventory.inventory.ArmorInventory.Clear();
        inventory.inventory.HeadwearInventory.Clear();
        inventory.inventory.ShieldInventory.Clear();
        inventory.inventory.ConsumableInventory.Clear();
        inventory.inventory.savedInventory.Clear();
        Destroy(GameObject.Find("WepViewModel"));
        Destroy(GameObject.Find("ShieldViewModel"));


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

    public void UpdateStats()
    {
        attackTime = myStats.attackSpeed;
        parryTime = myStats.parryLength;
    }

    public void TakeDamage(EnemyHitbox inHit)
    {
        int incDamage = inHit.damage;

        if (inHit.isStressCausing)
        {
            incDamage = (int)(incDamage * ((100 - myStats.StressResist) / 100f));
        }
        else
        {
            incDamage = (int)(incDamage * ((100 - myStats.Defense) / 100f));
        }
        Debug.Log("Incoming Damage Before Shield" + incDamage);


        if (myAction == ActionState.Block)
        {
            incDamage = (int)(incDamage * ((100 - myStats.BlockRating) / 100f));
        }
        Debug.Log("Final Incoming Damage" + incDamage);

        if (inHit.isStressCausing)
        {
            myStats.AddStress(-incDamage);
        }
        else
        {
            myStats.AddHealth(-incDamage);
        }
    }


    public IEnumerator attackWait(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Attack done");
        myAnim.AttackAnimEnd();
        myAction = ActionState.Null;
        c = null;
    }

    public IEnumerator parryWait(float time)
    {
        yield return new WaitForSeconds(time);
        if (Input.GetMouseButton(1))
        {
            myAction = ActionState.Block;
            myAnim.BlockAnimStart();
        }
        else
        {
            myAction = ActionState.Null;
            myAnim.BlockAnimEnd();
        }
        Debug.Log("Parry done");
        c = null;
    }

    public IEnumerator itemWait(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Using item done");
        myAction = ActionState.Null;
        c = null;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("eHitbox"))
        {

            EnemyHitbox inHit = other.GetComponent<EnemyHitbox>();
            if(myAction == ActionState.Parry)
            {
                myAnim.ParryTriggered();
                inHit.transform.root.GetComponent<EnemyController>().ParryStun();
                return;
            }
            else
            {
                TakeDamage(inHit);
            }

        }






        else if (other.CompareTag("TriggerVolume")) 
        {
            other.GetComponent<TriggerScript>().InteractTrigger();
        }
    }


}
