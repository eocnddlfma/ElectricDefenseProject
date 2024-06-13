using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMoveInfo
{
   private Agent _owner;
   private Transform _trm;
   public void Initialize(Agent owner)
   {
      _owner = owner;
      _trm = _owner.transform;
   }

   [SerializeField] private float _speed = 3f;

   public void Move(Vector2 moveDir)
   {
      Debug.Log(moveDir);
      Vector3 newMovedir = new Vector3(moveDir.x, 0, moveDir.y);
      _trm.position += newMovedir * (Time.deltaTime * _speed);
   }
}
