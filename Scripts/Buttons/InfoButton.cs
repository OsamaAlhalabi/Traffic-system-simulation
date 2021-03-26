using System;
using UnityEngine;
using UnityEngine.UI;
public class InfoButton : MonoBehaviour
{
    public Text Field;
    public GameObject rebuild;

    public void Update()
    {
        if (Int32.Parse(Field.text) != 0)
        {
            rebuild.SetActive(true);
        }
        else
            rebuild.SetActive(false);
    }
    public void UpdateCC(float value)
    {
        Field.text = Mathf.RoundToInt(value).ToString();
    }
}
