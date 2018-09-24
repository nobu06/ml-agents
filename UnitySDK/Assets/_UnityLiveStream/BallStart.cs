using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStart : MonoBehaviour {

    private Rigidbody rbBall;
    public float ballSpeed = 300f;

	// Use this for initialization
	void Start() {
        rbBall = GetComponent<Rigidbody>();
        rbBall.AddForce(new Vector3(ballSpeed, ballSpeed, 0));


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
