﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class Ball3DAgent : Agent
{
    [Header("Specific to Ball3D")]
    public GameObject ball;
    private Rigidbody ballRb;

    public override void InitializeAgent()
    {
        ballRb = ball.GetComponent<Rigidbody>();
    }

    public override void CollectObservations()
    {
        AddVectorObs(gameObject.transform.rotation.z);
        AddVectorObs(gameObject.transform.rotation.x);
        AddVectorObs(ball.transform.position - gameObject.transform.position);
        AddVectorObs(ballRb.velocity);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        
        if (brain.brainParameters.vectorActionSpaceType == SpaceType.continuous)
        {
            var actionZ = 2f * Mathf.Clamp(vectorAction[0], -1f, 1f);
            var actionX = 2f * Mathf.Clamp(vectorAction[1], -1f, 1f);

            ////NT: added
            //Debug.Log(string.Format("v[0]: {0}, Z:{1}, v[1]:{2}, X:{3}, rotZ: {4}, rotX: {5}",
                                    //vectorAction[0], actionZ, vectorAction[1], actionX,
                                    //gameObject.transform.rotation.z, gameObject.transform.rotation.x));


            if ((gameObject.transform.rotation.z < 0.25f && actionZ > 0f) ||
                (gameObject.transform.rotation.z > -0.25f && actionZ < 0f))
            {
                gameObject.transform.Rotate(new Vector3(0, 0, 1), actionZ);
                //Debug.Log("rotating z");
            }

            if ((gameObject.transform.rotation.x < 0.25f && actionX > 0f) ||
                (gameObject.transform.rotation.x > -0.25f && actionX < 0f))
            {
                gameObject.transform.Rotate(new Vector3(1, 0, 0), actionX);
                //Debug.Log("rotating x");
            }
        }
        if ((ball.transform.position.y - gameObject.transform.position.y) < -2f ||
            Mathf.Abs(ball.transform.position.x - gameObject.transform.position.x) > 3f ||
            Mathf.Abs(ball.transform.position.z - gameObject.transform.position.z) > 3f)
        {
            Done();
            SetReward(-1f);

        }
        else
        {
            SetReward(0.1f);
        }
    }

    public override void AgentReset()
    {
        gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        gameObject.transform.Rotate(new Vector3(1, 0, 0), Random.Range(-10f, 10f));
        gameObject.transform.Rotate(new Vector3(0, 0, 1), Random.Range(-10f, 10f));
        ballRb.velocity = new Vector3(0f, 0f, 0f);
        ball.transform.position = new Vector3(Random.Range(-1.5f, 1.5f), 4f, Random.Range(-1.5f, 1.5f))
                                      + gameObject.transform.position;

    }

}
