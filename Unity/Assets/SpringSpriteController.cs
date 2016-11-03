using UnityEngine;
using System.Collections;

public class SpringSpriteController : MonoBehaviour {

    private LineRenderer lineRenderer;
    public Transform startPosition;
    public Transform endPosition;

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        lineRenderer.SetPosition(0, startPosition.position);
        lineRenderer.SetPosition(1, endPosition.position);
	}
}
