using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    int defaultHealth = 10;
    int playerHealth;
    public GameObject Camera;

    [SerializeField]
    Transform groundDetectCenterPoint;

    [SerializeField]
    float GroundDetectRadius = 0.2f;

    [SerializeField]
    LayerMask CountsAsGround = 8;

    [SerializeField]
    public float Accel; //used so as not to have arbitrary numbers each time something needs to be called
                        //easily edited once, and referred to many times, later on
                        //making this public allows editing through unity inspector, rather than here
                        //but that editing is temporary unless you include the serialized field
                        //[SerializeField ]

    [SerializeField]
    public float jumpStrength = .05f;


    Rigidbody2D myRigidThing;

    private bool isOnGround;
    

	// Use this for initialization
	void Start ()
    {
        //Debug.Log("called from start.");
        playerHealth = defaultHealth;
        //below code teleports player
        //transform.position = new Vector3(-6,-3,0);


        myRigidThing = GetComponent<Rigidbody2D>();
        //do not use get component if there is more than one rigidbody per object
        //usually this is fine




	}
	
	// Update is called once per frame
	void Update ()
    {
        ////Debug.Log("You're a nerd.");

        //if (Input.GetKey(KeyCode.D)) 
        //{
        //    transform.Translate(new Vector3(0.05f,0f,0f));
        //}
        //else if (Input.GetKey(KeyCode.RightArrow))
        //    {
        //    transform.Translate(new Vector3(0.05f, 0f, 0f));
        //}

        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.Translate(new Vector3(-0.05f, 0f, 0f));
        //}
        //else if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.Translate(new Vector3(-0.05f, 0f, 0f));

        //}

        
        

        //get name of buttons and edit shit using input editor through unity


        //myRigidThing.velocity = new Vector2(myRigidThing.velocity.x, Accel * VerticalInput);


        //if (Input.GetButtonDown("Jump"))
        //{
        //    //transform.Translate(new Vector3(0f, .7f, 0f));
        //}

        //also, using getbutton or get button down is better than getkey and getkeydown


        ////transform.Translate(0.1f * HorizontalInput, 0,0);
        //transform is bad for movement(no physics), better for changing position of things. 
        //rigid body component of physics system is better for movement, so use that


        //velocity over rides physics every frame, therefore is less than optimal to use raw numbers
        //just use default value of physics as shown with "velocity.y"
        // Debug.Log("Horizontal Input: " + HorizontalInput);


        UpdateIsOnGround();
        HandleMovement();
        HandleJump();

       

    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    isOnGround = true;
    //}

    void UpdateIsOnGround()
    {
        Collider2D[] groundObjects = Physics2D.OverlapCircleAll(groundDetectCenterPoint.position, GroundDetectRadius, CountsAsGround);

        isOnGround = groundObjects.Length > 0;


    }


    private void HandleMovement()
    {

        float HorizontalInput = Input.GetAxis("Horizontal");
        myRigidThing.velocity = new Vector2(Accel * HorizontalInput, myRigidThing.velocity.y);
    }

    private void HandleJump()
    {
        float VerticalInput = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            myRigidThing.velocity = new Vector2(myRigidThing.velocity.x, jumpStrength);
            isOnGround = false;
        }


    }


}
