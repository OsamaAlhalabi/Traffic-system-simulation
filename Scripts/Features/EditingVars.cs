using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditingVars : MonoBehaviour
{
    private GameObject[] allgameObjects;
    private List<GameObject> allCars;
    private CarAIController[] carsScripts;
    private float speed;


    public float GetUserSpeed()
    {
        return this.speed;
    }
    public void SetSpeed(float value)
    {
        this.speed = value;
    }
    void Start()
    {
        speed = 150;
        allgameObjects = GameObject.FindGameObjectsWithTag("Car");
        allCars = new List<GameObject>();
        foreach(GameObject car in allgameObjects)
        {
            if(car.name =="Checking Box")
            {
                allCars.Add(car.transform.parent.gameObject);
            }
        }
        carsScripts = new CarAIController[allCars.Count];
        for(int i = 0; i < allCars.Count; i++)
        {
            carsScripts[i] = allCars[i].GetComponent<CarAIController>();
        }
        Debug.Log("allgameObjects: " + allgameObjects.Length + "carsScripts: " + carsScripts.Length);
    }
    public void Speed(float value)
    {
        /*  Debug.Log("Pressed! " + carsScripts.Length);
          foreach(CarAIController script in carsScripts)
          {
              Debug.Log("the scrpt is  : " + script);
              script.SetTheSpeedOs(speed);
             // Debug.Log("Speedeljsdnvkjsdbkuebkd : " + script.GetSpeedOs());
          }*/
        SetSpeed(value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
