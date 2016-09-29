using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public GameObject box;
    public GameObject holder;
    public Camera mainCamera;
    public float lastClick;
    public Text childCount;
    public float timeOffset;
    private float deltaTime;
    private float childCountNum;

    public void OnClear()
    {
        foreach(Transform trans in holder.transform)
        {
            Destroy(trans.gameObject);
        }
    }

    private bool IsPointerOverUIObject(Touch t) {
        // Referencing this code for GraphicRaycaster https://gist.github.com/stramit/ead7ca1f432f3c0f181f
        // the ray cast appears to require only eventData.position.
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(t.position.x, t.position.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        if (Input.touchCount > 0 && !IsPointerOverUIObject(Input.touches[0]))
        {
            /*for(int i = 0; i < Input.touchCount; i++)
            {*/
            if (lastClick + timeOffset < Time.time)
            {
                lastClick = Time.time;
                Vector2 pos = mainCamera.ScreenToWorldPoint(Input.touches[0].position);
                GameObject boxObject = Instantiate(box);
                boxObject.GetComponent<Renderer>().material.color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
                //boxObject.GetComponent<SpriteRenderer>().color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
                boxObject.transform.SetParent(holder.transform, true);
                boxObject.transform.position = pos;
            }
            childCountNum = holder.transform.childCount;
            //}
        }
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            if (lastClick + timeOffset < Time.time)
            {
                lastClick = Time.time;
                Vector2 pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                GameObject boxObject = Instantiate(box);
                boxObject.GetComponent<Renderer>().material.color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
                //boxObject.GetComponent<SpriteRenderer>().color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);

                boxObject.transform.SetParent(holder.transform, true);
                boxObject.transform.position = pos;
            }
            childCountNum = holder.transform.childCount;
        }

#endif
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);

        childCount.text = childCountNum + "; " + text + " 3D";
    }

}
