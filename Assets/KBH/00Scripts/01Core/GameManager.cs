using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class GameManager : MonoSingleton<GameManager>
{
   // �� ���� ���� (� ��ġ���� ���������� ���ؾ� �Ѵ�. )
   // Player, Building ���� ����
   // Enemy�� ���� ����

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
