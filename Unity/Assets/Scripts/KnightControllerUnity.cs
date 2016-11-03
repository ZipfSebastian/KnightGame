using UnityEngine;
using System.Collections;

public class RobotController : MonoBehaviour {
    //This will be our maximum speed as we will always be multiplying by 1
    public float maxSpeed = 2f;
    //a boolean value to represent whether we are facing left or not
    bool facingLeft = true;
    //a value to represent our Animator
    Animator anim;
    //to check ground and to have a jumpforce we can change in the editor
    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float jumpForce = 700f;
    private Rigidbody2D rigidbody2Dref;

    // Use this for initialization
    void Start() {
        //set anim to our animator

    }


    void FixedUpdate() {
        //set our vSpeed
        anim.SetFloat("vSpeed", rigidbody2Dref.velocity.y);
        //set our grounded bool
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        //set ground in our Animator to match grounded
        anim.SetBool("Ground", grounded);


        float move = Input.GetAxis("Horizontal");//Gives us of one if we are moving via the arrow keys
                                                 //move our Players rigidbody
        rigidbody2Dref.velocity = new Vector3(move * maxSpeed, rigidbody2Dref.velocity.y);
        //set our speed
        anim.SetFloat("Speed", Mathf.Abs(move));
        //if we are moving left but not facing left flip, and vice versa
        if (move < 0 && !facingLeft) {

            Flip();
        }
        else if (move > 0 && facingLeft) {
            Flip();
        }
    }

    void Update() {
        //if we are on the ground and the space bar was pressed, change our ground state and add an upward force
        if (grounded && Input.GetKeyDown(KeyCode.Space)) {
            anim.SetBool("Ground", false);
            rigidbody2Dref.AddForce(new Vector2(0, jumpForce));
        }


    }

    //flip if needed
    void Flip() {
        facingLeft = !facingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}