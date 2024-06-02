using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> : MonoBehaviour
   where T : Enum
{
   public T state;
   protected StatesReference<T> _reference;
   public void Initialize(StatesReference<T> reference)
   {
      _reference = reference;
   }
   public abstract bool CanChangeToOther(ref T state);
   public virtual void Enter() { }
   public virtual void Exit() { }
}
