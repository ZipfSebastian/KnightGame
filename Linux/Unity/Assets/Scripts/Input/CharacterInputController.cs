using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using Prime31;

public class CharacterInputController : InputController {

	private float lastAttackTime;
	public float attackSpeed = 0.5f;
	private float lastAttackTime2;
	public float attackSpeed2 = 2f;
	public Image attackButtonImage2;
	public int attackDamage = 10;
	public float hitDistance = 1.0f;
	public Image attackButtonImage;
	//private CharacterController2D characterController;
	public ThrowingDagger throwingDagger;
	public float daggerSpeed = 1.0f;
	public int daggerDamage = 10;
	public float daggerYoffset = 0.7f;
	public OnlineGameController onlineGameController;
	public float lastSendTime;
	public float sendTime;

	void Start(){
		//characterController = GetComponent<CharacterController2D> ();
		onlineGameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<OnlineGameController>();
	}

	public override float GetHorizontal ()
	{
		return CrossPlatformInputManager.GetAxis ("Horizontal");
	}

	public override float GetVertical ()
	{
		return CrossPlatformInputManager.GetAxis ("Vertical");
	}

	public void OnAttackClick(){
		
		if (Time.time > lastAttackTime + attackSpeed) {
			lastAttackTime = Time.time;
			RaycastHit2D[] hitedObjects = new RaycastHit2D[0];
			if (transform.localScale.x > 0) 
				hitedObjects = Physics2D.RaycastAll (transform.position, Vector2.right);
			else
				hitedObjects = Physics2D.RaycastAll (transform.position, Vector2.left);
			foreach (RaycastHit2D hitedObject in hitedObjects) {
				if (hitedObject.transform != this.transform 
					&& hitedObject.transform.GetComponent<AIInputController>()
					&& hitedObject.distance < hitDistance) {
					hitedObject.transform.GetComponent<BlockingObject> ().DamageIncome (attackDamage);
					break;
				}
			}


		}

	}

	public void OnAttack2Click(){
		if (Time.time > lastAttackTime2 + attackSpeed2) {
			lastAttackTime2 = Time.time;
			ThrowingDagger daggerInstance = Instantiate (throwingDagger.gameObject).GetComponent<ThrowingDagger> ();
			daggerInstance.transform.position = new Vector3 (transform.position.x
				, transform.position.y + daggerYoffset, daggerInstance.transform.position.z);

			if (transform.localScale.x > 0)
				daggerInstance.Init (daggerSpeed, transform, daggerDamage);
			else
				daggerInstance.Init (-daggerSpeed, transform, daggerDamage);
		}
	}

	void FixedUpdate(){
		float remainingTime = (lastAttackTime + attackSpeed) - Time.time;
		attackButtonImage.fillAmount = 1-(remainingTime/attackSpeed);
		remainingTime = (lastAttackTime2 + attackSpeed2) - Time.time;
		attackButtonImage2.fillAmount = 1-(remainingTime/attackSpeed2);
		if (onlineGameController != null) {
			if (Time.time > lastSendTime + sendTime) {
				lastSendTime = Time.time;
				MoveRequest moveRequest = new MoveRequest ();
				moveRequest.session = onlineGameController.session;
				Vector2 movement = new Vector2 (CrossPlatformInputManager.GetAxis ("Horizontal")
					, CrossPlatformInputManager.GetAxis ("Vertical"));
				moveRequest.newPosition = transform.position;
				moveRequest.moveDirection = movement;
				moveRequest.type = CommunicationTypes.MOVE_REQUEST;
				onlineGameController.SendMessage(JsonUtility.ToJson(moveRequest));
			}
		}
	}
}
