using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBuilder : MonoBehaviour
{
   public GameObject buildPrefab;

   

   private void Update()
   {
      if (Input.GetButtonDown("Fire1"))
      {
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         RaycastHit hit;
         if(Physics.Raycast(ray, out hit))
         {
            Instantiate(buildPrefab, hit.point, Quaternion.identity);
         }

      }
   }


}
