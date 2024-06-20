using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 기본 테그입니다. 일반적인 경우에는 이것을 상속받은 친구가
/// 게임의 내부 기능을 구현하게 될 것입니다. 
/// </summary>
/// <typeparam name="D">
/// 이 오브젝트가 가지게 될 데이터의 타입을 정의합니다. 
/// </typeparam>
public abstract class BaseTag<D> : IBase, INext<D>
{
   public D Current { get; set; }

   public abstract void Initialize();
   public abstract void BaseUpdate();
}

public abstract class MonoTag<D> : MonoBehaviour, IBase, INext<D>
{
   [field:SerializeField] public D Current { get; set; }

   public abstract void Initialize();
   public abstract void BaseUpdate();
}