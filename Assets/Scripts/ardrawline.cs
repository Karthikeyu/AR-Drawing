using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ardrawline : MonoBehaviour
{
    public Camera camera;
    private List<Vector3> list = new List<Vector3>();
    public GameObject linePrefab;
    private GameObject currentLine;
    private Vector3 camPos;
    private Vector3 camDirection;
    private Quaternion camRotation;
    float spawnDistance = 1f;
    private LineRenderer lineRenderer;
    Vector3 spawnPos;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "false";
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            createLine();


        }
        else if (Input.GetMouseButton(0))
        {
            updateLine();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            list.Clear();
        }


    }

   

    private void updateLineCenter()
    {

        spawnPos = getSpawnPos();

        if(Vector3.Distance(spawnPos,list[list.Count-1] )> 0.01f)
        {

            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount-1, spawnPos);
            list.Add(spawnPos);
        }
    }

    private void createLineCenter()
    {
        spawnPos =  getSpawnPos();
        currentLine = Instantiate(linePrefab, spawnPos, camRotation);


        lineRenderer = currentLine.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, spawnPos);
        lineRenderer.SetPosition(1, getSpawnPos());

        list.Add(spawnPos);

    }

    private Vector3 getSpawnPos()
    {
        camPos = camera.transform.position;
        camDirection = camera.transform.forward;
        camRotation = camera.transform.rotation;
        spawnPos = camPos + (camDirection * spawnDistance);

        return spawnPos;
    }

 

    private void createLine()
    {
        var pos = computeScreenToWorld();

        currentLine = Instantiate(linePrefab, pos, camera.transform.rotation);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, pos);
        list.Add(pos);
        pos = computeScreenToWorld();
        lineRenderer.SetPosition(1, pos);
        list.Add(pos);
            
        text.text = "true";


    }

    private void updateLine()
    {
        var pos = computeScreenToWorld();
        if(Vector3.Distance(pos,list[list.Count-1]) >0.01f)
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos);
            list.Add(pos);
        }
    }

    private Vector3 computeScreenToWorld()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        return ray.GetPoint(1f);

    }
}
