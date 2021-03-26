using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoveSpeed : MonoBehaviour
{
    public GameObject ChangeButton;
    private float moveSpeed;

    public float GetMoveSpeed()
    { 
        return Random.Range(1.0f, moveSpeed);
        //return moveSpeed;

    }

    public void SetMoveSpeed(float speedValue)
    {
        this.moveSpeed = speedValue;
        GetComponent<Text>().text = Mathf.RoundToInt(speedValue).ToString();
    }

    void Start()
    {
        SetMoveSpeed(12.0f);
        GetComponent<Text>().text = moveSpeed.ToString();
    }
    void Update()
    {
        if (ChangeButton.GetComponent<CountChange>().GetON())
        {
            SetMoveSpeed(moveSpeed);
            ChangeButton.GetComponent<CountChange>().SetON(false);
        }
    }
}
