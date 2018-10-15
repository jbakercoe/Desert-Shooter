using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour {

    /// <summary>
    /// This script belongs on the object with an Audio Source that will be playing the sound
    /// </summary>

    [SerializeField]
    AudioClip sound;

    AudioSource source;
    
	void Awake () {
        source = GetComponent<AudioSource>();
	}
	
    public void PlaySound()
    {
        source.PlayOneShot(sound);
    }

}
