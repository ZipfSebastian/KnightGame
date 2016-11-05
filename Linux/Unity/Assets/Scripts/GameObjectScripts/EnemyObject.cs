using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Prime31;

public class EnemyObject : InputController
{
	public int id;
	private Vector2 nextPosition;

	void Start(){
	}

	public override float GetHorizontal ()
	{
		return nextPosition.x;
	}

	public override float GetVertical ()
	{
		return nextPosition.y;
	}

	public void MoveSmothTo(Vector2 newPosition){
		nextPosition = newPosition;
	}

	public Vector2 Extrapolate(Vector2 newPosition, float speed){
		return Vector2.Lerp (transform.position, newPosition, speed);
	}

}

