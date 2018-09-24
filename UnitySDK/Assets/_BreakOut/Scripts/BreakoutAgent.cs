/*
 * 
 * Prototype 02:
 * - training the agent to learn how to follow the position of the ball.
 *  Almost instan
 *  Hmm.. Need to find a way to make it so that it won't drop the ball.
 * 
 * 
 * TODO:
 * - replace the magic numbers
 * - ...
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class BreakoutAgent : Agent
{
    [SerializeField]
    private Transform agentPaddle;

    [SerializeField]
    private Transform ball;

    public Transform ballPos;

    public Transform paddlePos;

    public Rigidbody rbBall;

    [SerializeField]
    private float ballXPos;

    [SerializeField]
    private float paddleXPos;


//    public Transform paddleStartPos;
    //public GameObject ball;
    //public Rigidbody rbBall;

	//private float distanceToTarget;

	public override void InitializeAgent()
	{
        // etc..
        rbBall = ball.GetComponent<Rigidbody>();    //NT: need?
	}

	/*
     * TODO:
     * - Figure out a way to normalize them -- perhaps by using Debug.Log
     */
	public override void CollectObservations()
    {

        //AddVectorObs(ballPos.position - paddlePos.position);        // the y distance from the ball to the agent

        AddVectorObs(ballPos.position.x);
        AddVectorObs(ballPos.position.y);

        AddVectorObs(paddlePos.position.x);

        //        AddVectorObs(rbBall.velocity);      // need?

        // whether the ball has touched the deadZone?
        // the num of blocks destroyed?
    }

    public override void AgentReset()
    {
        paddlePos.transform.position = new Vector3(0, 1.8f, 0); // paddleStartPos.position;

        rbBall.GetComponent<BallMovement>().ResetToInitialVelocity();

        //rbBall.angularVelocity = Vector3.zero;
        //rbBall.velocity = Vector3.zero;

        // place the ball slightly above the player
        ball.transform.position = new Vector3(Random.Range(-1.5f, 1.5f), 2.68f, 0) + gameObject.transform.position;
    }


    public override void AgentAction(float[] vectorAction, string textAction)
    {
        switch ((int)vectorAction[0])
        {
            case 1: // move the paddle to the left
                paddleXPos -= 0.1f;
                break;

            case 2: // move the paddle to the right
                paddleXPos += 0.1f;
                break;

            default:
                break;
        }

        paddlePos.position = new Vector3(paddleXPos, 1.8f, 0);

        if ((ball.transform.position.y - paddlePos.transform.position.y) < -0.1f)
        {
            SetReward(-1.0f);
            Done();
        }

        float difference = Mathf.Abs(paddleXPos - ballXPos);

        //Debug.Log(string.Format("dif: {0}, R:{1}, CR: {2}", difference, GetReward(), GetCumulativeReward()));


        if (paddlePos.position.y < 2.0)
        {
            if (difference < 0.5f)
            {
                AddReward(0.1f);
            }
        }
    }


}



/* I winged it */
///*
// * 
// * Prototype 01:
// * - training the agent to learn how to follow the position of the ball.
// *  Almost instan
// *  Hmm.. Need to find a way to make it so that it won't drop the ball.
// * 
// * 
// * TODO:
// * - replace the magic numbers
// * - ...
// */ 

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using MLAgents;

//public class BreakoutAgent : Agent
//{
//    [SerializeField]
//    private Transform agentPaddle;

//    [SerializeField]
//    private Transform targetBall;

//    public Transform ballPos;

//    public Transform paddlePos;

//    public Rigidbody rbBall;

//    [SerializeField]
//    private float ballXPos;

//    [SerializeField]
//    private float paddleXPos;

//    //private float distanceToTarget;

//    /*
//     * TODO:
//     * - Figure out a way to normalize them -- perhaps by using Debug.Log
//     */ 
//	public override void CollectObservations()
//	{
//        AddVectorObs(ballPos.position.x);
//        AddVectorObs(ballPos.position.y);

//        AddVectorObs(paddlePos.position.x);

////        AddVectorObs(rbBall.velocity);      // need?


//        // whether the ball has touched the deadZone?
//        // the num of blocks destroyed?
//	}

//	public override void AgentReset()
//	{
//        // the ball position
//        if (ballPos.position.y < 1.5) // TODO: fix this magic numbers
//        {
//            // reset
//            ballPos.position = new Vector3(0, 2.5f, 0);

//            rbBall.angularVelocity = Vector3.zero;
//            rbBall.velocity = Vector3.zero;

//        }

//        paddlePos.position = new Vector3(0, 1.8f, 0);



//        // the paddle position
//	}


//	public override void AgentAction(float[] vectorAction, string textAction)
//	{
//        switch ((int)vectorAction[0])
//        {
//            case 1: // move the paddle to the left
//                paddleXPos -= 0.1f;
//                break;

//            case 2: // move the paddle to the right
//                paddleXPos += 0.1f;
//                break;

//            default:
//                return;
//        }

//        paddlePos.position = new Vector3(paddleXPos, 1.8f, 0);


//        if (ballPos.position.y < 1.5)
//        {
//            SetReward(-1.0f);
//            Done();
//        }

//        float difference = Mathf.Abs(paddleXPos - ballXPos);

//        Debug.Log(string.Format("dif: {0}, R:{1}, CR: {2}", difference, GetReward(), GetCumulativeReward()));


//        if (paddlePos.position.y < 2.0)
//        {
//            if (difference < 0.5f)
//            {
//                AddReward(0.5f);
//            }
//        }
//	}


//}
