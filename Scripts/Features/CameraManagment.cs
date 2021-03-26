using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraManagment : MonoBehaviour
{
    public Camera mainCamera;
    private Vector3 startingPosition;
    private Vector3 startingRotation;
    private List<Vector3> locs;
    private List<Vector3> rots;
    private int passingCounter;
    private int[,] availablePaths;
    private GameObject[] allPaths;
    public void BackTOBasicPosition()
    {
        mainCamera.transform.position = startingPosition;
        mainCamera.transform.eulerAngles = startingRotation;
        
    }
    private void Start()
    {
        locs = new List<Vector3>();
        rots = new List<Vector3>();
        availablePaths = new int[9, 9];

        allPaths = GameObject.FindGameObjectsWithTag("TrafficLines");
        for(int i=0;i<allPaths.Length;i++)
             Debug.Log("all paths are: " + allPaths[i].name);
        //First View.. 
        startingPosition = mainCamera.transform.position;
        startingRotation = mainCamera.transform.eulerAngles;
        locs.Add(startingPosition);
        rots.Add(startingRotation);

        //Second View..
        locs.Add(new Vector3(46.0f,28.0f,-315.0f));
        rots.Add(new Vector3(25.0f, 45.0f, 0.0f));

        //Third View
        locs.Add(new Vector3(-92.4f,20.1f,-162.4f));
        rots.Add(new Vector3(25.0f, 140.0f, 0.0f));


        //Fourth View..
        locs.Add(new Vector3(304.0f,112.0f,-266.0f));
        rots.Add(new Vector3(35.0f, -50.0f, 0.0f));
        passingCounter = locs.Count;

        //Fifth View..
        locs.Add(new Vector3(384.4f,40.1f,-7.1f));
        rots.Add(new Vector3(50.0f, -50.0f, 0.0f));
        passingCounter = locs.Count;

        for (int i = 0; i < 9; i++)
            for (int j = 0; j < 9; j++)
                availablePaths[i, j] = 0;
        //Filling the mask..

        availablePaths[1, 1] = 1;
        availablePaths[1, 6] = 1;
        availablePaths[1, 7] = 1;
        availablePaths[2, 1] = 1;
        availablePaths[5, 8] = 1;
        availablePaths[4, 7] = 1;
        availablePaths[3, 1] = 1;
        availablePaths[3, 2] = 1;
        availablePaths[3, 6] = 1;
        availablePaths[4, 1] = 1;
        availablePaths[4, 2] = 1;
        availablePaths[4, 6] = 1;
        availablePaths[5, 3] = 1;
        availablePaths[5, 2] = 1;
        availablePaths[5, 4] = 1;
        for (int i = 1; i < 9; i++)
        {
            if (availablePaths[1, i] != 1)
            {
                if (allPaths.Length >= i)
                {
                    allPaths[i - 1].transform.GetChild(1).gameObject.SetActive(false) ;

                }
            }
            else
            {
                if (allPaths.Length >= i)
                {
                    allPaths[i - 1].transform.GetChild(1).gameObject.SetActive(true);
                }
            }
        }
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.C) && passingCounter == 1)
        {
            mainCamera.transform.position = locs[passingCounter-1];
            mainCamera.transform.eulerAngles = rots[passingCounter-1];
            passingCounter=5;
            for(int i = 1; i < 9; i++)
            {
                if (availablePaths[1, i] != 1)
                {
                    if (allPaths.Length >= i)
                    {
                        allPaths[i - 1].transform.GetChild(1).gameObject.SetActive(false);

                    }
                }
                else
                {
                    if (allPaths.Length >= i)
                    {
                        allPaths[i - 1].transform.GetChild(1).gameObject.SetActive(true);
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.C) && passingCounter == 2)
        {
            mainCamera.transform.position = locs[passingCounter-1];
            mainCamera.transform.eulerAngles = rots[passingCounter-1];
            passingCounter--;
            for (int i = 1; i < 9; i++)
            {
                if (availablePaths[2, i] != 1)
                {
                    if (allPaths.Length >= i)
                    {
                        allPaths[i - 1].transform.GetChild(1).gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (allPaths.Length >= i)
                    {
                        allPaths[i - 1].transform.GetChild(1).gameObject.SetActive(true);
                    }
                }
            }

        }
        else if (Input.GetKeyDown(KeyCode.C) && passingCounter == 3)
        {
            mainCamera.transform.position = locs[passingCounter-1];
            mainCamera.transform.eulerAngles = rots[passingCounter-1];
            passingCounter--;
            for (int i = 1; i < 9; i++)
            {
                if (availablePaths[3, i] != 1)
                {
                    if (allPaths.Length >=i)
                    {
                        allPaths[i - 1].transform.GetChild(1).gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (allPaths.Length >= i)
                    {
                        allPaths[i - 1].transform.GetChild(1).gameObject.SetActive(true);
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.C) && passingCounter == 4)
        {
            mainCamera.transform.position = locs[passingCounter-1];
            mainCamera.transform.eulerAngles = rots[passingCounter-1];
            passingCounter--;
            for (int i = 1; i < 9; i++)
            {
                if (availablePaths[4, i] != 1)
                {
                    if (allPaths.Length >= i)
                    {
                        allPaths[i - 1].transform.GetChild(1).gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (allPaths.Length >= i)
                    {
                        allPaths[i - 1].transform.GetChild(1).gameObject.SetActive(true);
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.C) && passingCounter == 5)
        {
            mainCamera.transform.position = locs[passingCounter - 1];
            mainCamera.transform.eulerAngles = rots[passingCounter - 1];
            passingCounter--;
            for (int i = 1; i < 9; i++)
            {
                if (availablePaths[5, i] != 1)
                {
           
                    if (allPaths.Length >= i)
                    {
                        allPaths[i - 1].transform.GetChild(1).gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (allPaths.Length >= i)
                    {
                        allPaths[i - 1].transform.GetChild(1).gameObject.SetActive(true);
                    }
                }
            }
        }

    }
}
