using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Agent
{
   public Transform visualTrm;
   public float rotateSpeed = 360;
   public Vector3 rotateAxis = new Vector3(0,0,1);

   public Coroutine rotateRoutine;
   public UIUtil uis;
   public GameEndUI gameEndUI;

   public override void Awake()
   {
      base.Awake();
      visualTrm = transform.Find("Visual");
      rotateRoutine = StartCoroutine(VisualRotateRoutine());
   }

   private IEnumerator VisualRotateRoutine()
   {
      while(true)
      {
         visualTrm.Rotate(rotateAxis, rotateSpeed * Time.deltaTime);
         yield return null;
      }
   }

   public override void Die()
   {
      base.Die();
      uis.coreCanvas.gameObject.SetActive(false);
      uis.buildCanvas.gameObject.SetActive(false);
      uis.viewCanvas.gameObject.SetActive(false);
      gameEndUI.Show();
   }
}
