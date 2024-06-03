using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoSingleton<GameManager>
{
   
   private void Awake()
   {
      IDOTweenInit dotweenInit = DOTween.Init(true, true, LogBehaviour.Verbose);
      dotweenInit.SetCapacity(50, 100);
   }

   

   
}
