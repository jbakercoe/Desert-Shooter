using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponColliderController : MonoBehaviour {

    [SerializeField]
    SphereCollider rightHand;
    [SerializeField]
    SphereCollider leftHand;

    // Use this for initialization
    void Start ()
    {
        leftHand.enabled = false;
        rightHand.enabled = false;
    }

    public void EnableRightHand()
    {
        rightHand.enabled = true;
    }

    public void EnableLeftHand()
    {
        leftHand.enabled = true;
    }

    public void DisableRightHand()
    {
        rightHand.enabled = false;
    }

    public void DisableLeftHand()
    {
        leftHand.enabled = false;
    }
}
