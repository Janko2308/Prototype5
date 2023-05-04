using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorTrail : MonoBehaviour
{
    public Color trailColor = new Color(1, 0, 0.38f);
    public float distanceFromCamera = 5;
    public float startWidth = 0.1f;
    public float endWidth = 0f;
    public float trailTime = 0.25f;

    Transform trailTransform;
    Camera Camera;
    // Start is called before the first frame update
    void Start()
    {
        Camera = GetComponent<Camera>();
        GameObject trailObj = new GameObject("Mouse Trail");
        trailTransform = trailObj.transform;
        TrailRenderer trailRenderer = trailObj.AddComponent<TrailRenderer>();
        trailRenderer.time = trailTime;
        trailRenderer.startWidth = startWidth;
        trailRenderer.endWidth = endWidth;
        trailRenderer.numCapVertices = 10;
        trailRenderer.material = new Material(Shader.Find("Sprites/Default"));
        trailRenderer.material.color = trailColor;

        
    }

    // Update is called once per frame
    void Update()
    {
        MoveTrailToCursor();
    }

    public void MoveTrailToCursor()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = distanceFromCamera;
        trailTransform.position = Camera.ScreenToWorldPoint(mousePos);
    }
}
