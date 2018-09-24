/*
 * Code reference https://unity3d.college/2017/07/03/using-vector3-reflect-to-cheat-ball-bouncing-physics-in-unity/
 * 
 * TODO
 * - perhaps use object pooling
 *   for the blocks and the ball using a different script?
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField]
    [Tooltip("For debugging, adds velocity during OnEnable")]
    private Vector3 initialVelocity;

    [SerializeField]
    private float minVelocity = 5f;

    private Vector3 lastFrameVelocity;
    private Rigidbody rb;

	private void OnEnable()
	{
        rb = GetComponent<Rigidbody>();
        rb.velocity = initialVelocity;
	}

	private void Update()
	{
        lastFrameVelocity = rb.velocity;
	}


    public void ResetToInitialVelocity()
    {
        rb.velocity = initialVelocity;
    }


    private void OnCollisionEnter(Collision collision)
    {
        Bounce(collision.contacts[0].normal);

        // checks what it is colliding to
        if (collision.gameObject.tag == "Block")
        {
            Destroy(collision.gameObject);
        }
        //else if (collision.gameObject.tag == "GameOverWall")    // the bottom wall
        //{
        //    Destroy(gameObject);
        //}
    }

    private void Bounce(Vector3 collisionNormal)
    {
        var speed = lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);     // NT - m?

        rb.velocity = direction * Mathf.Max(speed, minVelocity);
    }

}