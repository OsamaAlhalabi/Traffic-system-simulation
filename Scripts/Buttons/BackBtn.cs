using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBtn : MonoBehaviour
{
    public GameObject pasueMenu;
    public GameObject advancedSettingsMenu;
    public GameObject VarsMenuUI;
    public GameObject button;
    public void BacktoPauseMenu()
    {
        advancedSettingsMenu.SetActive(false);
        pasueMenu.SetActive(true) ;
    }
    public void BackToView()
    {
        VarsMenuUI.SetActive(false);
        button.SetActive(true);
        Time.timeScale = 1f;
    }
}
