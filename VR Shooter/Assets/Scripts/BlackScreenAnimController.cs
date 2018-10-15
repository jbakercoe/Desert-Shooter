using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BlackScreenAnimController : MonoBehaviour {

    Animator anim;

	void Start () {
        anim = GetComponent<Animator>();
	}

    public void PlayFadeIn()
    {
        anim.SetTrigger("Fade In");
    }

    public void PlayFadeOut()
    {
        anim.SetTrigger("Fade Out");
    }

}
