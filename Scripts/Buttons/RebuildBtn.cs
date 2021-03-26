using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RebuildBtn : MonoBehaviour
{
    private bool rebuild = false;
    private GameObject[] allPaths;
    private CarWalkPath[] walkPaths;
    private int carsCount;
    public Text text;
    private GameObject BasicCarToFollow;
    public void Rebuild()
    {
        this.rebuild = true;
    }
    public void Rebuild(bool rb)
    {
        this.rebuild = rb;
    }
    public bool getRebuild()
    {
        return this.rebuild;
    }







    private void Start()
    {
        
        allPaths = GameObject.FindGameObjectsWithTag("TrafficLines");
        walkPaths = new CarWalkPath[allPaths.Length];
        Debug.Log("number of waaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaays: " + allPaths.Length);
        for(int i = 0; i < allPaths.Length; i++)
        {
            walkPaths[i] = allPaths[i].GetComponent<CarWalkPath>();
        }
        BasicCarToFollow = GameObject.FindGameObjectWithTag("FCar");
        
    }

    void Update()
    {
        /*carsCount = Int32.Parse(text.text);
        for (int i = 0; i < allPaths.Length; i++)
        {
            int x = carsCount / allPaths.Length;
            walkPaths[i].SetCarNumberOS(x);
            Debug.Log("each: " + x + " " + (1.0f / Time.deltaTime));
        }
        if (rebuild)
        {
            foreach(CarWalkPath walkPath in walkPaths)
            {
               
                if (walkPath.par != null && walkPath.par.tag!="FCar")
                {
                    rebuild = false;
                    DestroyImmediate(walkPath.par);
                }

                if (walkPath.walkingPrefabs != null && walkPath.walkingPrefabs.Length > 0 && walkPath.walkingPrefabs[0] != null)
                {
                    walkPath.SpawnPeople();
                }
               
            }

        }*/
    }
}
