using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FollowCar : MonoBehaviour
{
    public static bool FollowOnRun = false;
    public Camera mainCamera;
    public GameObject FollowCarController;
    private Color OrigColor;
    public void Follow()
    {
          if (!FollowOnRun)
          {
          GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
          Debug.Log("number of cars currently: " + cars.Length);
          GameObject car=null;
               foreach (GameObject c in cars)
               {
                     if(c.name!="Checking Box" && c.name!= "PlayerCar")
                     {
                          car = c;
                           break;
                     }
               }
               Debug.Log("car: " + car.name);
               if (car != null)
               {
                  FollowCarController.GetComponent<CinemachineFreeLook>().m_LookAt = car.transform;
                  FollowCarController.GetComponent<CinemachineFreeLook>().m_Follow = car.transform;
                  car.GetComponent<CarAIController>().manipulate = true;

               }
              FollowOnRun = true;
              FollowCarController.SetActive(true);
              mainCamera.GetComponent<CinemachineBrain>().enabled = true;
              GetComponentInChildren<Text>().color = Color.red;
            mainCamera.GetComponent<CameraManagment>().enabled = false;
        }
          else
          {
              FollowOnRun = false;
              FollowCarController.SetActive(false);
              mainCamera.GetComponent<CinemachineBrain>().enabled = false;
              mainCamera.GetComponent<CameraManagment>().BackTOBasicPosition();
              GetComponentInChildren<Text>().color = OrigColor;
            mainCamera.GetComponent<CameraManagment>().enabled = true;

        }
    }
    public void Start()
    {
        OrigColor = GetComponentInChildren<Text>().color;
        
    }
    public void Update()
    {
        if (FollowCarController.GetComponent<CinemachineFreeLook>().m_Follow == null)
        {
            FollowOnRun = false;
              FollowCarController.SetActive(false);
              mainCamera.GetComponent<CinemachineBrain>().enabled = false;
              mainCamera.GetComponent<CameraManagment>().BackTOBasicPosition();
              GetComponentInChildren<Text>().color = OrigColor;
        }
    }
}
