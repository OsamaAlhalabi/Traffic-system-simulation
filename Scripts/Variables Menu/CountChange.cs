using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountChange : MonoBehaviour
{
    private bool on;
    public void SetON(bool value)
    {
        this.on = value;
    }
    public bool GetON()
    {
        return on;
    }
    public void Press()
    {
        if (!on)
            on = true;
        else
            on = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
