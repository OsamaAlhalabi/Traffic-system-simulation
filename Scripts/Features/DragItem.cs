using UnityEngine;
using UnityEngine.UI;
public class DragItem : MonoBehaviour{

    private Vector3 mOffset;
    private float mZCoord;
    private static bool editable =false;
    private Color OrgColor;
    private GameObject parentGameObject;
    public void EnableEdit()
    {
        if (!editable)
        {
            editable = true;
            //GetComponentInChildren<Text>().color = Color.red;
        }

        else
        {
            editable = false;
           // GetComponentInChildren<Text>().color = OrgColor;
        }
        Debug.Log("Drag Something! " + editable);
    }

    public void OnMouseDown(){
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        if (gameObject.transform.parent != null && gameObject.transform.parent.tag == "Piece")
        {
            parentGameObject = gameObject.transform.parent.gameObject;
            mOffset = parentGameObject.transform.position - GetMouseAsWorldPoint();
        }
        else
        {
            mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
        }  
            
    }

    private Vector3 GetMouseAsWorldPoint()  {
         Vector3 mousePoint = Input.mousePosition;
         mousePoint.z = mZCoord;
         return Camera.main.ScreenToWorldPoint(mousePoint);
    }



    public void OnMouseDrag(){
        Debug.Log("Editable is : " + editable);
        if (editable)
        {
            if (parentGameObject != null)
                parentGameObject.transform.position = GetMouseAsWorldPoint() + mOffset;
            else
                transform.position = GetMouseAsWorldPoint() + mOffset;
        }
    }
    public void Start()
    {
       // editable = true;
        parentGameObject = null;
        if (gameObject.layer == 5)
             OrgColor =GetComponentInChildren<Text>().color;
    }
}