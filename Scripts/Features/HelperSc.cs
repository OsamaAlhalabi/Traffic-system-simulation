using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperSc : MonoBehaviour
{
    public GameObject SpeedValue;
    public GameObject BarkeValue;
    private float speed;
    private float distance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed = SpeedValue.GetComponent<MoveSpeed>().GetMoveSpeed();
        distance = BarkeValue.GetComponent<BrakeDistance>().GetBrakeDistance();
    }
    public float GetTheSpeed()
    {
        return speed;
    }
    public float GetSafty()
    {
        return distance;
    }
}
