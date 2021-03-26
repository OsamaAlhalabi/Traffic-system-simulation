using UnityEngine;
using UnityEngine.UI;

public class CCS : MonoBehaviour
{
    public Text Field;
    int count;
    private GameObject[] carsNumber;

    public void Update()
    {
        carsNumber = GameObject.FindGameObjectsWithTag("Car");
        count = carsNumber.Length/2;
        Field.text = count.ToString();
    }

}
