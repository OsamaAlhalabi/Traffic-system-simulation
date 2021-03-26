using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class Starting : MonoBehaviour
{
    private GameObject[] allPaths;
    private CarWalkPath[] walkPaths;
    private GameObject[] cars;
    void Start()
    {
        allPaths = GameObject.FindGameObjectsWithTag("TrafficLines");
 

        walkPaths = new CarWalkPath[allPaths.Length];
        for (int i = 0; i < allPaths.Length; i++)
        {
            walkPaths[i] = allPaths[i].GetComponent<CarWalkPath>();
        }
        foreach (CarWalkPath walkPath in walkPaths)
        {

            if (walkPath.par != null && walkPath.par.tag != "FCar")
            {
                DestroyImmediate(walkPath.par);
            }

            if (walkPath.walkingPrefabs != null && walkPath.walkingPrefabs.Length > 0 && walkPath.walkingPrefabs[0] != null)
            {
                walkPath.SpawnPeople();
            }

        }

    }
    static Random rnd = new Random();
    private int off = 0;
    private void Update()
    {
        /*var fps = 1.0 / Time.deltaTime;

        cars = GameObject.FindGameObjectsWithTag("Car");
        List<GameObject> activeCars = new List<GameObject>();
        foreach(GameObject c in cars)
        {
            if (c.name != "Checking Box")
            {
                activeCars.Add(c);
            }
        }
        Debug.Log("xxxxxxxx" + activeCars.Count);
        if (activeCars.Count > 500)
        {
            for (int i = 0; i < 27; i++)
            {
                activeCars[rnd.Next(0, activeCars.Count)].SetActive(false);
                off++;
            }
               
        }
        if (off <= 500)
        {
            for (int i = 0; i < 20; i++)
            {
                int x = rnd.Next(0, activeCars.Count);
                if (!activeCars[x].activeSelf)
                {
                    activeCars[x].SetActive(true);
                }
                
            }
            off = 0;
        }
        int counter = 0;
        foreach(GameObject g in activeCars)
        {
            if (g.activeSelf)
            {
                counter++;
            }
        }
        Debug.Log("Current Cars number:  " + counter);*/
    }
}
