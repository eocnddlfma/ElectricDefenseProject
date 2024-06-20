using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EventExecuteCondition : byte
{
   OnEnter,
   OnReturn
}

public class EventUIBlock : DecideUIBlock
{
   public EventExecuteCondition executeType;
   public UnityEvent blockCallback;
}
