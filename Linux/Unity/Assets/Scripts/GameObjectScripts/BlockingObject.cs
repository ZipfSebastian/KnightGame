using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlockingObject : MonoBehaviour {

	private int health;
	public int maxHealth;
	public Canvas worldCanvasTemplate;
	private Transform canvasHolder;
	private Slider slider;
	private Canvas canvasRef;
	public float positionOffset;

	// Use this for initialization
	void Start () {
		canvasHolder = GameObject.FindGameObjectWithTag ("WordCanvases").transform;
		health = maxHealth;
		canvasRef = Instantiate (worldCanvasTemplate).GetComponent<Canvas>();
		canvasRef.transform.SetParent (canvasHolder,true);
		slider = canvasRef.transform.FindChild ("HealthBar").GetComponent<Slider>();
		RefreshUI ();
	}

	void FixedUpdate(){
		Vector3 pos = this.transform.position;
		pos.y += positionOffset;
		canvasRef.transform.position = pos;
	}
	
	public void DamageIncome(int damage){
		//Debug.Log ("HIT");
		health -= damage;
		RefreshUI ();
	}

	public void RefreshUI(){
		
		slider.value = (float)((float)health / (float)maxHealth);

		if (health <= 0) {
			Destroy (canvasRef.gameObject);
			Destroy (this.gameObject);
		}
		if (health == maxHealth) {
			slider.gameObject.SetActive (false);
		} else {
			slider.gameObject.SetActive (true);
		}
	}
}
