using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class CountdownEvent : UnityEvent { }

[RequireComponent(typeof(Text))]
public class CountdownText : MonoBehaviour {

    public CountdownEvent OnFinishCountdown = new CountdownEvent();

    Text text;
    int counter;

    [SerializeField]
    string message;

	void Awake () {
        text = GetComponent<Text>();
	}

    public void StartCountdown()
    {
        counter = 3;
        StartCoroutine(Countdown());
    }
	
    IEnumerator Countdown()
    {
        while (counter > 0)
        {
            text.text = message + ": " + counter;
            counter--;
            yield return new WaitForSeconds(1f);
        }
        text.text = "";
        OnFinishCountdown.Invoke();
    }

}
