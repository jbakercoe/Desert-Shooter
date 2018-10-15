using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class LoadSceneEvent : UnityEvent { }

public class MenuInputReader : MonoBehaviour {

    public LoadSceneEvent OnLoadStart = new LoadSceneEvent();

    public void PlayGame()
    {
        OnLoadStart.Invoke();
        SceneLoader.LoadGameScene();
    }

}
