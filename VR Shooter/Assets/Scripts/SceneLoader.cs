using UnityEngine.SceneManagement;

public static class SceneLoader {

    /// <summary>
    /// Assumes scene 0 is the menu scene and scene 1 is the game
    /// </summary>
    
    public static void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public static void LoadMainMenu()
    {
        LoadScene(0);
    }

    public static void LoadGameScene()
    {
        LoadScene(1);
    }

}
