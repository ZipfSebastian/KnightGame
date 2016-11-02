using System;
using UnityEngine;
using UnityEngine.UI;
using Prime31;

public class CharacterUIController : MonoBehaviour{

	private int health;
	public int maxHealth;
	public Slider slider;
	public Text healthText;
	private CharacterController2D characterController;
	public Button attack1;
	public Button attack2;
	private Animator animator;
	private KnightController knightController;

	// Use this for initialization
	void Start () {
		knightController = GetComponent<KnightController> ();
		animator = knightController._animator;
		characterController = GetComponent<CharacterController2D> ();
		health = maxHealth;
		RefreshUI ();
	}

	public void DamageIncome(int damage){
		health -= damage;
		RefreshUI ();
	}

	public void RefreshUI(){
		//Debug.Log ("asd");
		slider.value = (float)((float)health / (float)maxHealth);
		healthText.text = health + " / " + maxHealth;
		if (health <= 0) {
			animator.SetFloat ("Speed", 0);
			animator.SetBool("InAir", false);

			characterController.velocity = Vector3.zero;
			health = 0;
			characterController.enabled = false;
			foreach (Collider2D colider in this.GetComponentsInChildren<Collider2D>()) {
				colider.enabled = false;
			}
			GetComponent<Collider2D> ().enabled = false;
			GetComponent<Rigidbody2D> ().isKinematic = true;
			GetComponent<KnightController> ().enabled = false;
			attack1.interactable = false;
			attack2.interactable = false;
		}
	}
}

