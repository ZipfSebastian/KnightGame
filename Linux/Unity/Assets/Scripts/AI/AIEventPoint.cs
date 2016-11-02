using UnityEngine;
using System.Collections;
using System;

public class AIEventPoint : MonoBehaviour {

	public EventType eventType;
}

[Serializable]
public enum EventType{
	Left, Right, Jump, Down,Stop
}