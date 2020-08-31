
using System.Collections.Generic;
using UnityEngine;

public class drawLine : MonoBehaviour
{
    public Camera camera;
    private LineRenderer ren;
    private Vector3 mousePosition;
    private double distance;
    private List<Vector3> list = new List<Vector3>();
    public GameObject linePrefab;
    private GameObject currentLine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
           
            Debug.Log("Mouse pressed");
            createLine();
           
            
        }
        else if(Input.GetMouseButton(0))
        {
            mousePosition = Input.mousePosition;
            mousePosition.z = 10;
            mousePosition = camera.ScreenToWorldPoint(mousePosition);
            distance = Vector3.Distance(mousePosition, list[list.Count-1]);
            if(distance > 0.01)
            {
                updateLine(mousePosition);
           
            }
        }else if(Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse Released");
            list.Clear();
        }
    }


    void createLine()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        ren = currentLine.GetComponent<LineRenderer>();
        mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 10;
        ren.SetPosition(0, mousePosition);
        list.Add(mousePosition);
        mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 10;
        ren.SetPosition(1, mousePosition);
        list.Add(mousePosition);

    }

    void updateLine(Vector3 position)
    {
        list.Add(position);

        ren.positionCount++;
        ren.SetPosition(ren.positionCount - 1, position);
    }
}
