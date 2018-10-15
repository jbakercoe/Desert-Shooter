using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FinishWaveEvent : UnityEvent { }

public class KillCounter : MonoBehaviour {

    public FinishWaveEvent OnWaveComplete = new FinishWaveEvent();
    
    int killCount = 0;
    int waveLimit;

    void Start()
    {
        EnemyHealth.OnEnemyKilled += RegisterKilledEnemy;
    }

    public void RegisterKilledEnemy()
    {
        killCount++;
        if(killCount >= waveLimit)
        {
            print("Finished the wave!");
            OnWaveComplete.Invoke();
        }
    }

    public void SetNewWave(int waveCount)
    {
        waveLimit = waveCount;
    }

    public void ResetKills()
    {
        killCount = 0;
    }

}
