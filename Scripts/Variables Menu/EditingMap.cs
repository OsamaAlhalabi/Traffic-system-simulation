using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EditingMap : MonoBehaviour
{
    private bool on = false;
    private DragItem[] allScripts;
    private Color OrigColor;
    public void EditMap()
    {
        if (!on)
        {
            on = true;
            GetComponentInChildren<Text>().color = Color.red;
            foreach (DragItem script in allScripts)
            {
                script.enabled = true;
            }
        }
            
        else
        {
            on = false;
            GetComponentInChildren<Text>().color = OrigColor;
            foreach (DragItem script in allScripts)
            {
                script.enabled = false;
            }
        }
            

    }
    private void Start()
    {
        OrigColor = GetComponentInChildren<Text>().color;
        allScripts = FindObjectsOfType<DragItem>();
        foreach (DragItem script in allScripts)
        {
            script.enabled = false;
        }

    }

    void Update()
    {
  
    }
}
