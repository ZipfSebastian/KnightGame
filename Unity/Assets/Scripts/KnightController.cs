
using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System;

public class KnightController : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;         // For determining which way the player is currently facing.
    [HideInInspector]
    public bool jump = false;				// Condition for whether the player should jump.
    private const int MOVEMENT_RIGHT = 1;
    private const int MOVEMENT_LEFT = 2;

    public float moveForce = 365f;          // Amount of force added to move the player left and right.
    public float maxSpeed = 5f;             // The fastest the player can travel in the x axis.
    public AudioClip[] jumpClips;           // Array of clips for when the player jumps.
    public float jumpForce = 1000f;         // Amount of force added when the player jumps.
    public AudioClip[] taunts;              // Array of clips for when the player taunts.
    public float tauntProbability = 50f;    // Chance of a taunt happening.
    public float tauntDelay = 1f;           // Delay for when the taunt should happen.

    private int tauntIndex;
    public Transform rightCheck;
    public Animator anim;                  // Reference to the player's animator component.

    private int movement = 0;
    private string session;
    private Vector3 lastPosition;
    public bool multiplayer;
    public bool grounded;
    private Rigidbody characterRigidBody;
	public float distToGround;
    public float animLength;
    private bool canJump = true;
    private bool jumpBegin = false;
    private bool inAir = false;
    private Collider coliderRef;
    public float velocityY;
    public float yLimit;
    public Transform armTransform;
    public Vector3 roatation;
    private Quaternion defaultArmRotation;
    public Vector3 velocity;

    void Awake()
    {
        // Setting up references.
        //groundCheck = transform.Find("groundCheck");
        //rightCheck = transform.Find("rightCheck");
        lastPosition = transform.position;
        characterRigidBody = GetComponent<Rigidbody>();
        coliderRef = GetComponent<Collider>();
		distToGround = coliderRef.bounds.extents.y;
        defaultArmRotation = armTransform.rotation;

        //anim = GetComponent<Animator>();
    }

    bool IsGrounded(){
        if((characterRigidBody.velocity.y < -yLimit || characterRigidBody.velocity.y > yLimit)) {
            return false;
        }else{
            return Physics.CheckCapsule(coliderRef.bounds.center, new Vector3(coliderRef.bounds.center.x, coliderRef.bounds.min.y - distToGround, coliderRef.bounds.center.z), distToGround);
        }
    }

void Update()
    {

    }
    /*
	void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.tag == "Ground")
		{
			grounded = true;
		}
	}

	//consider when character is jumping .. it will exit collision.
	void OnCollisionExit2D(Collision2D collision){
		if(collision.gameObject.tag == "Ground")
		{
			grounded = false;
		}
	}*/

    void LateUpdate() {
        float axis = CrossPlatformInputManager.GetAxis("Arm");
        if (axis!= 0) {
            armTransform.rotation = defaultArmRotation;

            armTransform.Rotate(new Vector3(0,0,axis*80+60));
        }
    }


    void FixedUpdate()
    {
        //groundCheck.GetComponent<Collider>().
        bool rightCollusion = rightCheck.GetComponent<RightCollider>().collision;
            //false;
        grounded = IsGrounded();
        velocityY = characterRigidBody.velocity.y;
        if (!grounded) {
            anim.SetBool("InAir", true);
            canJump = false;
            inAir = true;
        }
        if (grounded) {
            anim.SetBool("InAir", false);
            if (inAir) {
                jumpBegin = false;
                inAir = false;
            }
            canJump = true;
            
        }
#if !UNITY_STANDALONE
        if ((CrossPlatformInputManager.GetAxis("Vertical") > 0.7 || CrossPlatformInputManager.GetButtonDown("Jump")) && grounded && canJump && !jumpBegin) { //&& !rightCollusion)
#else
        if ((CrossPlatformInputManager.GetAxis("Vertical") > 0.1 || CrossPlatformInputManager.GetButtonDown("Jump")) && grounded && canJump && !jumpBegin) { //&& !rightCollusion) {
#endif
            jump = true;
            canJump = false;
            jumpBegin = true;
            //characterRigidBody.constraints = RigidbodyConstraints2D.None;
        }
        else
        {
            jump = false;
            //characterRigidBody.constraints = RigidbodyConstraints2D.FreezePositionY;
        }

        float h = 0;
        // Cache the horizontal input.



        if (CrossPlatformInputManager.GetAxis("Horizontal") < 0 && (grounded || !rightCollusion || movement == MOVEMENT_RIGHT))
        {
            h = CrossPlatformInputManager.GetAxis("Horizontal");
            movement = MOVEMENT_LEFT;
            characterRigidBody.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
            float charSpeed = Mathf.Abs(characterRigidBody.velocity.x);
            if(charSpeed > 0.2)
                anim.SetFloat("Speed", charSpeed);

        }
        else if (CrossPlatformInputManager.GetAxis("Horizontal") > 0 && (grounded || !rightCollusion || movement == MOVEMENT_LEFT))
        {
            h = CrossPlatformInputManager.GetAxis("Horizontal");
            movement = MOVEMENT_RIGHT;
            characterRigidBody.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
            float charSpeed = Mathf.Abs(characterRigidBody.velocity.x);
            if (charSpeed > 0.2)
                anim.SetFloat("Speed", charSpeed);
        }
        else
        {
			h = 0;
            characterRigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
            anim.SetFloat("Speed", 0);
        }


        //http://answers.unity3d.com/questions/1033875/2d-platformer-horizontal-max-speed.html

        // The Speed animator parameter is set to the absolute value of the horizontal input.

        // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        if (!rightCollusion) {
            if (h * characterRigidBody.velocity.x < maxSpeed)
                // ... add a force to the player.
                characterRigidBody.AddForce(Vector2.right * h * moveForce);

            // If the player's horizontal velocity is greater than the maxSpeed...
            if (Mathf.Abs(characterRigidBody.velocity.x) > maxSpeed)
                // ... set the player's velocity to the maxSpeed in the x axis.
                characterRigidBody.velocity = new Vector2(Mathf.Sign(characterRigidBody.velocity.x) * maxSpeed, characterRigidBody.velocity.y);
            velocity = characterRigidBody.velocity;
        }
        // If the input is moving the player right and the player is facing left...
        if (h > 0 && !facingRight)
            // ... flip the player.
            Flip();
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (h < 0 && facingRight)
            // ... flip the player.
            Flip();

        // If the player should jump...
        if (jump)
        {
            // Set the Jump animator trigger parameter.
            jump = false;
            StartCoroutine(Jump());
            
        }
    }
    
    private IEnumerator Jump() {
        if (grounded) {
            anim.SetTrigger("Jump");

            yield return new WaitForSeconds(animLength);
            // Play a random jump audio clip.
            //int i = Random.Range(0, jumpClips.Length);
            //AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

            // Add a vertical force to the player.
            //characterRigidBody.velocity = new Vector2(0, 0);
            characterRigidBody.AddForce(new Vector2(0f, jumpForce));
        }
        // Make sure the player can't jump again until the jump conditions from Update are satisfied.
        
    }

    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


}