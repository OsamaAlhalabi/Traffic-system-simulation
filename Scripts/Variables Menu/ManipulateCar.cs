using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ManipulateCar : MonoBehaviour
{
    public GameObject speedS;
    public GameObject brackingS;
    public GameObject decS;
    public GameObject incS;

    private float sp;
    private float br;
    private float de;
    private float ce;
    public float GetMSpeed()
    {
        return this.sp;
    }
    public float GetMBracking()
    {
        return this.br;
    }
    public float GetDecM()
    {
        return this.de;
    }
    public float GetIncM()
    {
        return this.ce;
    }
    void Update()
    {
        sp = speedS.GetComponent<Slider>().value;
        br = brackingS.GetComponent<Slider>().value;
        de = decS.GetComponent<Slider>().value;
        ce = incS.GetComponent<Slider>().value;
        Debug.Log("bitch  2 " + sp);
    }


}
