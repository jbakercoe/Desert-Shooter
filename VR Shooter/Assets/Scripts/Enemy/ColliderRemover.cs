using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRemover : MonoBehaviour {

    new Collider collider;

	// Use this for initialization
	void Start () {
        collider = GetComponent<Collider>();
        if(collider == null)
        {
            print("I an't got no collider");
        }		
	}

    public void RemoveCollider()
    {
        collider.enabled = false;
    }
    
}
