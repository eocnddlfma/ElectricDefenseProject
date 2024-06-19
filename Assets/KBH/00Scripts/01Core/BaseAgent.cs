using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBase
{
   void Initialize();
   void Update();
}

public interface INext<D>
{
   D Value { get; }
   void Set(D value);
}


public abstract class BaseAgent : MonoBehaviour, IBase
{
   public abstract void Initialize();
   public abstract void Update();
}
