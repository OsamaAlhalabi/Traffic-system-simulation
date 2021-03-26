using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGround : MonoBehaviour
{
    public GameObject plane;

    private Vector3 startingPosition;
    private Vector3 nextPosition;
    public void Spawn()
    {
        startingPosition = plane.transform.position;
        nextPosition = new Vector3(0, 0, 0);
        for (int i = 0; i < 110; i++)
        {
            for(int j = 0; j < 70; j++)
            {
                nextPosition.x = startingPosition.x + (Vector3.right.x * i*10);
                nextPosition.z = startingPosition.z + (Vector3.forward.z * j*10);
                GameObject sp = Instantiate(plane, nextPosition, Quaternion.identity);
                sp.transform.SetParent(GameObject.FindGameObjectWithTag("Plane").transform, false);
            }
        }
    }

    void Start()
    {
        Spawn();
    }

}
