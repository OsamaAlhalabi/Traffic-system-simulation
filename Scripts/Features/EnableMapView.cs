using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnableMapView : MonoBehaviour
{
    public GameObject back;
    public GameObject PauseMenu;
    
    public void ShowMap()
    {
            Camera.main.transform.position = new Vector3(107.0f, 1010.0f, 0.0f);
            Camera.main.transform.eulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
            PauseMenu.SetActive(false);
            back.SetActive(true);
    }
    public void Back()
    {
        Camera.main.GetComponent<CameraManagment>().BackTOBasicPosition();
        back.SetActive(false);
        PauseMenu.SetActive(true);
        Time.timeScale = 1f;
    }
}
