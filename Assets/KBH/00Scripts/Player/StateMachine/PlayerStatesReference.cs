using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatesReference : StatesReference<GameMode>
{
   [SerializeField] private UIUtil uiManager;
   public GameMode currentGameMode => uiManager.coreCanvas.currentMode;

   
}
