using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class LineCreator : MonoBehaviour {


  [SerializeField]  private List<GameObject> Lines = null; 
  [SerializeField]  private Material lineMaterial;
  [SerializeField]  private float lineWidth;
 
    private float depth = 5f;  
    private GameObject gameObjectLine;
    private Vector3 lineEndPoint;
    private Vector3? lineStartPoint;
    private new Camera camera;
   
            

    private void Start()
    {
        camera = GetComponent<Camera>();
       
    }
    private void Update()
    {
        if (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                lineStartPoint = GetMouseCameraPoint();
            }
            if (Input.GetMouseButtonUp(0))
            {

                if (Lines.Count < 5)

                {
                    if (!lineStartPoint.HasValue)
                    {
                        return;
                    }

                    lineEndPoint = GetMouseCameraPoint();
                    gameObjectLine = new GameObject();

                    var lineRenderer = gameObjectLine.AddComponent<LineRenderer>();
                    var edgeCol = gameObjectLine.AddComponent<EdgeCollider2D>();

                    lineRenderer.material = lineMaterial;
                    lineRenderer.SetPositions(new Vector3[] { lineStartPoint.Value, lineEndPoint });
                    lineRenderer.startWidth = lineWidth;
                    lineRenderer.endWidth = lineWidth;
                    edgeCol.points = (new Vector2[] { lineStartPoint.Value, lineEndPoint });

                    lineStartPoint = null;
                    Lines.Add(gameObjectLine);

                }
                StartCoroutine("LineQuantityControl");
            }
        }
    }
    private Vector3 GetMouseCameraPoint()
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        return ray.origin+ray.direction*depth;
    }
    private IEnumerator LineQuantityControl()
    {
        yield return new WaitForSeconds(3f);
    
        Destroy(Lines[0]);
        Lines.RemoveAt(0);
    }
   
}