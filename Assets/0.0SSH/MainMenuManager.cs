using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Canvas SettingCanvas;
    
    public void GameStart()
    {
        SceneManager.LoadScene("Scenes/Kbh");
    }

    public void OpenSetting()
    {
        SettingCanvas.enabled = true;
    }

    public void ExitProgram()
    {
        Application.Quit(0);
    }
}
