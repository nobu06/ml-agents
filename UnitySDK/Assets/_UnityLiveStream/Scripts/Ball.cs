using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float ballInitialVelocity = 600f;

    private Rigidbody rb;

    private bool ballInPlay;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody>();
	}
	
	/*
	 * Left it in Update instead of FixedUpdate b/c here, we're not repeatedly adding force.
	 * We're only doing this at one frame.
	 * If we're going to be adding force repeadly, then should use FixedUpdate instead.
	 */
	void Update () {
		
        if (Input.GetButtonDown("Fire1") && ballInPlay == false)
        {
            transform.parent = null;    // unparent the ball from the paddle
            ballInPlay = true;
            rb.isKinematic = false;
            rb.AddForce(new Vector3(ballInitialVelocity, ballInitialVelocity, 0));

        }
	}


}
