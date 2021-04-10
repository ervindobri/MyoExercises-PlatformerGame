using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class Timer : MonoBehaviour {

	private IEnumerator coroutine;
    private BoyController boyController;

    void Start () {
        Random.InitState((int)System.DateTime.Now.Ticks);
        boyController = GameObject.FindGameObjectWithTag("Player").GetComponent<BoyController>();
        //print("Jumpforce:" + boyController.jumpForce);
        // Start function WaitAndPrint as a coroutine.
        coroutine = WaitAndSet( 3.0f );
		StartCoroutine(coroutine);
	}

	// every 3 seconds perform the print()
	private IEnumerator WaitAndSet( float waitTime)
	{
        while (true)
		{
            float rJumpForce = Random.Range(8, 15);
            boyController.jumpForce = rJumpForce;
			//print("JumpForce generated " + rJumpForce);
            yield return new WaitForSeconds(waitTime);
            
		}
	}


}
