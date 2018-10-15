using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InputEvent : UnityEvent { }

public class InputReader : MonoBehaviour {

    public InputEvent OnInputEnter = new InputEvent();
    
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            OnInputEnter.Invoke();
        }	
	}
}
