using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OnlineGameController : Reciver
{
	public Transform characterTransform;
	public List<EnemyObject> enemyList = new List<EnemyObject> ();

	public override void OnRecive (string data)
	{
			
	}
}

