using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StartEvent : UnityEvent { }

public class GameStarter : MonoBehaviour {

    public StartEvent OnGameStart = new StartEvent();

    void Start()
    {
        OnGameStart.Invoke();
    }

}
