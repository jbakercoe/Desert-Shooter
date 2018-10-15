using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillText : MonoBehaviour {
    
    Text killText;
    
    int numKills;

	// Use this for initialization
	void Start ()
    {
        EnemyHealth.OnEnemyKilled += KillTextTick;
        killText = GetComponent<Text>();
        ResetKills();
    }

    public void ResetKills()
    {
        numKills = 0;
        killText.text = "Kills: " + numKills;
    }

    public void KillTextTick()
    {
        numKills++;
        killText.text = "Kills: " + numKills;
    }

}
