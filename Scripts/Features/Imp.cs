using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp : CinemachineFreeLook
{
    public void SetFollowOS(GameObject car)
    {
        m_Follow = car.transform;
    }
    public void SetLookATOS(GameObject car)
    {
        m_LookAt = car.transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Follow" + m_Follow + " " + m_LookAt);
    }
}
