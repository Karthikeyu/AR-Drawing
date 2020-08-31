using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawLineNew : MonoBehaviour
{
    public GameObject line;
    public GameObject currentLine;
    public LineRenderer lineRenderer;
    private List<Vector2> fingerPos = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            createLine();
        }
        if(Input.GetMouseButton(0))
        {
            Vector2 tempPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector2.Distance(tempPos, fingerPos[fingerPos.Count-1]) > 0.1f)
                
            {
                updateLine(tempPos);
            }
        }
    }

    void createLine()
    {
        currentLine = Instantiate(line, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        fingerPos.Clear();

        fingerPos.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPos.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, fingerPos[0]);
        lineRenderer.SetPosition(1, fingerPos[1]);

    }

    void updateLine(Vector2 fingerPosNew)
    {
        fingerPos.Add(fingerPosNew);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, fingerPosNew);
    }
}
