using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HitAnimationController : MonoBehaviour {

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
    public void TakeHit()
    {
        anim.SetTrigger("Hit");
    }

}
