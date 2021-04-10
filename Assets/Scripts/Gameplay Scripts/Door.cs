using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    private Animator anim;


    void Start () {
        anim = GetComponent<Animator>();	
	}
	
	void Update () {
	    if ( GameplayManager.doorOpen )
        {
            anim.SetBool("Open", true);
            
        }	
	}
}
