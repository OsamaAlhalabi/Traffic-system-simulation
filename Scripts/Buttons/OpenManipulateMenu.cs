using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenManipulateMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject mainpulateUI;
    public void Switch()
    {
        pauseMenuUI.SetActive(false);
        mainpulateUI.SetActive(true);
    }
    public void BackBtn()
    {
        pauseMenuUI.SetActive(true);
        mainpulateUI.SetActive(false);
    }
    void Update()
    {
        
    }
}
