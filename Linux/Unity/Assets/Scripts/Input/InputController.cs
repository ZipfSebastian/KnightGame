using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
	public virtual float GetHorizontal(){
		return 0;
	}

	public virtual float GetVertical(){
		return 0;
	}
}
