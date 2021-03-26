using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popopapa : MonoBehaviour
{
    private GameObject[] pointsObject;
    private List<GameObject> points;
    private Vector3 searchVec1;
    private Vector3 searchVec2;
    private GameObject startingNode;
    private GameObject endingNode;
    void Start()
    {
        pointsObject = GameObject.FindGameObjectsWithTag("Points");
        points = new List<GameObject>();
        Debug.Log("FIRST STAGE!");
        foreach(GameObject go in pointsObject)
        {
            foreach (Transform child in go.transform)
            {
                if (null == child)
                    continue;
                points.Add(child.gameObject);
            }

        }
        Debug.Log("the number of all points in the simulation is:" + points.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse is down");

            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                if (hitInfo.transform.gameObject.tag != "Car")
                {

                    searchVec1 = hitInfo.point;
                    searchVec2 = hitInfo.point;
                    Debug.Log("fofo" + searchVec1);
                    searchVec1.x = searchVec1.x + 10;
                    searchVec1.z = searchVec1.z + 10;
                    searchVec2.x = searchVec2.x - 10;
                    searchVec2.z = searchVec2.z - 10;
                }
               
            }
            
            else
            {
                Debug.Log("No hit");
            }
            Debug.Log("Mouse is down");
        }
        if (startingNode == null)
        {
            foreach (GameObject point in points)
            {
                if (point.transform.position.x < searchVec1.x && point.transform.position.x > searchVec2.x)
                {
                    if (point.transform.position.z < searchVec1.z && point.transform.position.z > searchVec2.z)
                    {
                        startingNode = point;
                    }
                }
            }
        }
        if(endingNode == null)
        {
            foreach (GameObject point in points)
            {
                if (point.transform.position.x < searchVec1.x && point.transform.position.x > searchVec2.x)
                {
                    if (point.transform.position.z < searchVec1.z && point.transform.position.z > searchVec2.z)
                    {
                        startingNode = point;
                        Debug.Log("aaaa  sss ");
                    }
                }
            }
        }
        if (endingNode != null && startingNode != null)
        {
            GameObject fPath = startingNode.transform.parent.transform.parent.gameObject;
            GameObject sPath = endingNode.transform.parent.transform.parent.gameObject;
            Debug.Log("aaaa fPath: " + fPath.name + " " + sPath.name);
            if(fPath == sPath)
            {
                Debug.Log("aaaa There is a com!");
            }
            else
            {
                Debug.Log("aaaa there is no com!");
                startingNode = null;
                endingNode = null;
            }
        }
        if (startingNode != null && endingNode!=null)
        {
            Debug.Log("aaaa we found a perfect node: " + startingNode.transform.name + " " + endingNode.name);
        }
    }
}
