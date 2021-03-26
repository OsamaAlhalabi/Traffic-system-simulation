using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class CarsNumber : MonoBehaviour
{
    public GameObject changeButton;
    public GameObject slider;
    private int carsNumber;
    private GameObject[] allPaths;
    private CarWalkPath[] walkPaths;
    private int carsCount;
    public float GetCarsNumber()
    {
        return this.carsNumber;
    }


    void Start()
    {
        allPaths = GameObject.FindGameObjectsWithTag("TrafficLines");
        walkPaths = new CarWalkPath[allPaths.Length];
        for (int i = 0; i < allPaths.Length; i++)
        {
            walkPaths[i] = allPaths[i].GetComponent<CarWalkPath>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        carsCount = Int32.Parse(GetComponent<Text>().text);
        for (int i = 0; i < allPaths.Length; i++)
        {
            walkPaths[i].SetCarNumberOS(carsCount / allPaths.Length);
            Debug.Log("The new number is : " + carsCount / allPaths.Length);
        }
        Debug.Log("The PATH : " +allPaths.Length);
        if (changeButton.GetComponent<CountChange>().GetON())
        {
            foreach (CarWalkPath walkPath in walkPaths)
            {

                if (walkPath.par != null && walkPath.par.tag != "FCar")
                {
                    changeButton.GetComponent<CountChange>().SetON(false);
                    DestroyImmediate(walkPath.par);
                }

                if (walkPath.walkingPrefabs != null && walkPath.walkingPrefabs.Length > 0 && walkPath.walkingPrefabs[0] != null)
                {
                    walkPath.SpawnPeople();
                }

            }
            Camera.main.GetComponent<CinemachineBrain>().enabled = false;
            Camera.main.GetComponent<CameraManagment>().BackTOBasicPosition();
        }

        GameObject[] carsNumber = GameObject.FindGameObjectsWithTag("Car");
        int count = carsNumber.Length / 2;
        if (slider.GetComponent<Slider>().value != 0)
        {
            count = Mathf.RoundToInt(slider.GetComponent<Slider>().value);
        }
        GetComponent<Text>().text = count.ToString();
    }
}
