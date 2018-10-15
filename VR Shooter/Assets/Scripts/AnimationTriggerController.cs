using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationTriggerController : MonoBehaviour {

    [SerializeField]
    string triggerName;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        anim.SetTrigger(triggerName);
    }

}
