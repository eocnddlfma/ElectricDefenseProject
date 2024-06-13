using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class GameManager : MonoSingleton<GameManager>
{
   // 맵 생성 구현 (어떤 위치에서 시작할지를 정해야 한다. )
   // Player, Building 생성 구현
   // Enemy들 생성 구현

   [SerializeField] private GameLogic _gameLogic;

   private void Awake()
   {
      IDOTweenInit dotweenInit
         = DOTween.Init(true, true, LogBehaviour.Verbose);
      dotweenInit.SetCapacity(50, 100);
   }

   private void Start()
   {
      _gameLogic.Initialize(this, null, parent : null);
      _gameLogic.Run();
      _gameLogic.Enable(-1);
   }



}
