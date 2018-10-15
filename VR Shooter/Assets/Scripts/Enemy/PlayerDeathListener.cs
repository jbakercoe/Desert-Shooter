using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameOverEvent : UnityEvent { }

public class PlayerDeathListener : MonoBehaviour {

    public GameOverEvent OnGameOver = new GameOverEvent();

    [SerializeField]
    PlayerHealth playerHealth;

    //List<>

	// Use this for initialization
	void Start () {
        playerHealth.OnPlayerDie.AddListener(DoITTT);
	}	

    public void DoITTT()
    {
        OnGameOver.Invoke();
    }

}
