using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IState
{
   void OnEnter();
   void OnExit();
}

public class DecideUI : BaseAgent
{
   [SerializeField] private DecideUIReference _reference;
   private List<MonoTag<bool>> _tagList;

   private int currentIdx = 0;
   private MonoTag<bool> currentRunningTag = null;

   private void Awake()
   {
      Initialize();
      _reference.Initialize();
   }

   private void Update()
   {
      BaseUpdate();
      _reference.UpdateInput();
   }

   public override void Initialize()
   {
      _reference = GetComponent<DecideUIReference>();
      _reference.Initialize();


      _tagList = new List<MonoTag<bool>>();

      transform.Find("Tags")
         .GetComponents(_tagList);

      currentIdx = 0;
      currentRunningTag = _tagList[currentIdx];
   }


   public override void BaseUpdate()
   {
      _reference.UpdateInput();
      TagsUpdate();
   }

   private void TagsUpdate()
   {
      if (currentRunningTag.Current)
      {
         currentRunningTag.BaseUpdate();
      }
      else
      {
         currentIdx = (currentIdx + 1) % _tagList.Count;
         currentRunningTag = _tagList[currentIdx];
         currentRunningTag.Current = true;
      }
   }
}

