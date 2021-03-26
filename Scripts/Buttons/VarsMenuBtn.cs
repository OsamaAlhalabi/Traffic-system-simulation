using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VarsMenuBtn : MonoBehaviour
{
    public GameObject VarsMenuUI;
    public GameObject button;
    private static bool opened = false;
    public void VarsMenu()
    {
        if (!opened)
        {
            opened = true;
            VarsMenuUI.SetActive(true);
            button.SetActive(false);
            Time.timeScale = 0.0f;
        }
        else
        {
            opened = false;
            Time.timeScale = 1f;
            VarsMenuUI.SetActive(false);
            button.SetActive(true);
        }
    }
}
