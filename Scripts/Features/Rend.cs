using System.Collections.Generic;
using UnityEngine;

public class Rend : MonoBehaviour
{

      void OnBecameInvisible()
      {
        enabled = false;
        gameObject.SetActive(false);
      }

      void OnBecameVisible()
      {
        enabled = true;
      }
      

}
