using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Prime31;

public class EnemyObject : InputController
{
	public int id;
	private Vector2 nextPosition;
	private Vector2 moveDirection;
	public float correctionLimit = 1.0f;

	void Start(){
	}

	public override float GetHorizontal ()
	{
		//Debug.Log (moveDirection.x);
		return moveDirection.x;
	}

	public override float GetVertical ()
	{
		float jump = moveDirection.y;
		moveDirection.y = 0;
		return jump;

	}

	public void MoveSmothTo(Vector2 newPosition, Vector2 moveDirection){
		nextPosition = newPosition;
		this.moveDirection = moveDirection;

		if(correctionLimit <= Vector2.Distance(newPosition, transform.position)){
			transform.position = newPosition;
		}
	}

	public Vector2 Extrapolate(Vector2 newPosition, float speed){
		return Vector2.Lerp (transform.position, newPosition, speed);
	}

}

