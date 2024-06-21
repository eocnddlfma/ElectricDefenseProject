using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEndUI : MonoBehaviour
{
   private Transform _panelTrm;
   private CanvasGroup _canvasGroup;

   public TextMeshProUGUI _waveShowTextMesh;
   public Button _returnToMenuSceneBtn;
   public Button _restartBtn;

   private void Awake()
   {
      _panelTrm = transform.Find("Panel");
      _canvasGroup = _panelTrm.GetComponent<CanvasGroup>();

      _waveShowTextMesh = _panelTrm.transform.Find("Text")
         .GetComponent<TextMeshProUGUI>();

      _returnToMenuSceneBtn = _panelTrm.Find("Return").GetComponent<Button>();
      _restartBtn = _panelTrm.Find("Restart").GetComponent<Button>();
   }


   [ContextMenu("Show")]
   public void Show()
   {
      float transitionTime = 2f;

      _waveShowTextMesh.text
         = @$"Reached Wave
<size=150><b><color=green>{WaveManager.Instance._wave}</color></b></size>";

      DOTween.defaultTimeScaleIndependent = true;
      _canvasGroup.DOFade(1, transitionTime);
      Time.timeScale = 0.1f;

      _canvasGroup.interactable = true;
      _canvasGroup.blocksRaycasts = true;
   }


}
