using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using Prime31;
using UnityStandardAssets.CrossPlatformInput;

public class KnightController : MonoBehaviour {
    
    public float runSpeed = 8f;
    public float groundDamping = 20f; // how fast do we change direction? higher means faster
    public float inAirDamping = 5f;
    public float jumpHeight = 3f;

    [HideInInspector]
    private float normalizedHorizontalSpeed = 0;
    private Vector3 _velocity;
    public float gravity = 20.0F;
    public Animator _animator;
    public float pushPower;
    public Text fpsText;
    CharacterController2D _controller;
    public Transform armTransform;
    private Quaternion defaultArmRotation;
    private float vSpeed;
    public float fallAnimationLimit;
	public InputController inputController;

    void Awake() {
		inputController = GetComponent<InputController> ();
        _controller = GetComponent<CharacterController2D>();
        _controller.onControllerCollidedEvent += OnControllerColliderHit;
        defaultArmRotation = armTransform.rotation;
    }

    void LateUpdate() {
        float axis = CrossPlatformInputManager.GetAxis("Arm");
        if (axis != 0) {
            armTransform.rotation = defaultArmRotation;

            armTransform.Rotate(new Vector3(0, 0, axis * 80 + 60));
        }
    }

    void Update() {
        if (_controller.isGrounded)
            _velocity.y = 0;

		if (inputController.GetHorizontal() >0) {
            normalizedHorizontalSpeed = 1;
            if (transform.localScale.x < 0f)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            /*
            if (_controller.isGrounded)
                _animator.Play(Animator.StringToHash("Run"));*/
        }
		else if (inputController.GetHorizontal()<0) {
            normalizedHorizontalSpeed = -1;
            if (transform.localScale.x > 0f)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

            /*if (_controller.isGrounded)
                _animator.Play(Animator.StringToHash("Run"));*/
        }
        else {
            normalizedHorizontalSpeed = 0;

           /* if (_controller.isGrounded)
                _animator.Play(Animator.StringToHash("Idle"));*/
        }


        // we can only jump whilst grounded
		if (_controller.isGrounded && inputController.GetVertical()>0.1f) {
            _velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
            _animator.Play(Animator.StringToHash("Armature|jump"));
        }


        // apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
        var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
        _velocity.x = Mathf.Lerp(_velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor);

        // apply gravity before moving
        _velocity.y += gravity * Time.deltaTime;

        // if holding down bump up our movement amount and turn off one way platform detection for a frame.
        // this lets us jump down through one way platforms
		if (_controller.isGrounded && inputController.GetVertical() < -0.1f) {
            _velocity.y *= 20f;
            _controller.ignoreOneWayPlatformsThisFrame = true;
        }

        _controller.move(_velocity * Time.deltaTime);

        // grab our current _velocity to use as a base for all calculations
        _velocity = _controller.velocity;

        if(Math.Abs(_controller.velocity.x)>0.03f) {
            _animator.SetFloat("Speed", Math.Abs(_controller.velocity.x));
        }
        else {
            _animator.SetFloat("Speed", 0);
        }
        if(_controller.velocity.y < fallAnimationLimit) {
            _animator.SetBool("InAir", true);
        }else {
            _animator.SetBool("InAir", false);
        }
    }

    void OnControllerColliderHit(RaycastHit2D hit) {
		if (hit.rigidbody != null && !hit.rigidbody.isKinematic && !hit.transform.GetComponent<AIInputController>())
            hit.rigidbody.AddForce(hit.normal * pushPower);
    }
}
