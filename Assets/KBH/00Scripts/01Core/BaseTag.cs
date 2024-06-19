using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTag<D> : IBase, INext<D>
{
   protected D value;
   public D Value => value;

   public abstract void Initialize();
   public abstract void Set(D value);
   public abstract void Update();
}
