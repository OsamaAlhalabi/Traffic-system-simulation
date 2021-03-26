using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvSetBtn : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject advancedSettingsMenu;
  public void AdvancedSettingsOpen()
    {
        pauseMenu.SetActive(false);
        advancedSettingsMenu.SetActive(true);
    }
}
