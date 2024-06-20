using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BaseAgent와 BaseTag가 상속받고 있다.
/// 오브젝트라면 당연히 가져야 한다고 생각되는 것을
/// 갖는다. 
/// </summary>
public interface IBase
{
   void Initialize();
   void BaseUpdate();
}

/// <summary>
/// 데이터를 가지는 Tag에 대한 인터페이스이다. 
/// </summary>
/// <typeparam name="D">
/// 어떤 데이터 타입이든 올 수 있다. 
/// </typeparam>
public interface INext<D>
{
   D Current { get; set; }
}


/// <summary>
/// Base Agent는 반.드.시. Tag를 가지고 관리하되,
/// 다른 종속성을 가지지 않도록 구성해야 한다.
/// </summary>
public abstract class BaseAgent : MonoBehaviour, IBase
{
   /// <summary>
   /// 기본 세팅을 해주어야 한다. 
   /// </summary>
   public abstract void Initialize();

   /// <summary>
   /// 어떤 태크를 업데이트 시킬 것인지에 대한 기준을 써줘야 한다. 
   /// </summary>
   public abstract void BaseUpdate();
}

