using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BrakeDistance : MonoBehaviour
{
    public GameObject ChangeButton;
    private float barkeDistance;

    public float GetBrakeDistance()
    {
        return this.barkeDistance;
    }

    public void SetMBrakeDistance(float distanceValue)
    {
        this.barkeDistance = distanceValue;
        GetComponent<Text>().text = Mathf.RoundToInt(distanceValue).ToString();
    }
    void Start()
    {
        SetMBrakeDistance(10.0f);
        GetComponent<Text>().text = barkeDistance.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (ChangeButton.GetComponent<CountChange>().GetON())
        {
            SetMBrakeDistance(barkeDistance);
            ChangeButton.GetComponent<CountChange>().SetON(false);
        }
    }
}
