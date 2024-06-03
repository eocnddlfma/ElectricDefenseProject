using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Agent
{
   [SerializeField] private UIManager uiManager;
   [SerializeField] private StateMachine<GameMode> _stateMachine;

   [Header("Physics")]
   [SerializeField] private Rigidbody _rigidbodyCompo;
   [SerializeField] private float _speed = 3f; 

   private GameMode _previousState;

   private void Awake()
   {
      uiManager = FindAnyObjectByType<UIManager>();
      _stateMachine.Intialize(this, GameMode.View);
   }


   private void Update()
   {
      _stateMachine.Update();

      if(_previousState != _stateMachine.State)
      {
         OnStateChange(_previousState, _stateMachine.State);
      }
      else
      {
         UpdateGameMode(_stateMachine.State);
      }

      _previousState = _stateMachine.State;
   }

   private void UpdateGameMode(GameMode currentState)
   {

      if (currentState != GameMode.Shop
         && currentState != GameMode.Upgrade)
      {
         MoveAction();
      }

         switch (currentState)
      {
         case GameMode.View:
            break;
         case GameMode.Build:
            break;
         case GameMode.Upgrade:
            break;
         case GameMode.Shop:
            break;
      }
   }

   private void MoveAction()
   {
      Vector2 moveDir = InputUtil.moveDirection;
      Vector3 newMovedir = new Vector3(moveDir.x, 0, moveDir.y);

      transform.position += newMovedir * (Time.deltaTime * _speed);
   }

   private void OnStateChange(GameMode previousMode, GameMode currentState)
   {

      switch (currentState)
      {
         case GameMode.View:
            break;
         case GameMode.Build:
            break;
         case GameMode.Upgrade:
            break;
         case GameMode.Shop:
            break;
      }
   }
}
