using UnityEngine;

public class SteeringBtn : MonoBehaviour
{
    public Camera mainCamera;
    private Vector3 mapCamPos;
    private Vector3 mapCamRot;
    private Vector3 cameraLoc;
    private Vector3 cameraRot;

   
    public void Map(bool activate)
    {
        if (activate)
        {
            mainCamera.transform.position = mapCamPos;
            mainCamera.transform.eulerAngles = mapCamRot;
        }
        else
        {
            mainCamera.transform.position = cameraLoc;
            mainCamera.transform.eulerAngles = cameraRot;
        }
    }
    public void SelectPoint()
    {
       /* if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse is down");

            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                if (hitInfo.transform.gameObject.tag == "Construction")
                {
                    Debug.Log("It's working!");
                }
                else
                {
                    Debug.Log("nopz");
                }
            }
            else
            {
                Debug.Log("No hit");
            }
            Debug.Log("Mouse is down");
        }*/
    }

    void Update()
    {
        SelectPoint();
    }

}
