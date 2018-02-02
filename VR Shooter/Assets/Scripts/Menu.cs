using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : Singleton<Menu> {

    private static bool isSurvivalMode;

    public static bool IsSurvivalMode
    {
        get { return isSurvivalMode; }
    }

    public void PlayClassicMode()
    {
        isSurvivalMode = false;
        SceneManager.UnloadSceneAsync("MainMenu");
        SceneManager.LoadSceneAsync("Desert");
    }

    public void PlaySurvivalMode()
    {
        isSurvivalMode = true;
        SceneManager.UnloadSceneAsync("MainMenu");
        SceneManager.LoadSceneAsync("Desert");

    }



}
