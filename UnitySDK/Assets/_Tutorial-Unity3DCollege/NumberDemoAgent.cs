/*
 * 
 * TODO:
 * - perhaps modify the way you give rewards and penalty - positive reinforcement!
 *      - give it a reward when the number gets closer to the target
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using UnityEngine.UI;

public class NumberDemoAgent : Agent
{

    [SerializeField]
    private Transform agentSphere;

    [SerializeField]
    private Transform targetCube;

    [SerializeField]
    private float currentNumber;

    [SerializeField]
    private float targetNumber;

    [SerializeField]
    private Text text;

    int solved;     // count of what has been successfully solved

    private float distanceToTarget;     // to give rewards for getting closer to the target


    public override void CollectObservations()
    {
        AddVectorObs(currentNumber);
        AddVectorObs(targetNumber);
    }

    // after every completion -- fails by going out of range or succeeds
    public override void AgentReset()
    {
        targetNumber = Random.Range(-1f, 1f);
        currentNumber = 0f;

        distanceToTarget = Mathf.Abs(currentNumber - targetNumber);

        // for visualization
        targetCube.position = new Vector3(targetNumber, 0f, 0f);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        if (text != null)
        {
            text.text = string.Format("C:{0} / T:{1} [{2}]", currentNumber, targetNumber, solved);
        }

        //Debug.Log("Agent action[0]: " + vectorAction[0]);      // NT: added

        switch ((int)vectorAction[0])
        {
            // NT: the case val is different from the tutorial.
            case 1:                     // NT: changed it to case: 1, instead of 0 b/c by default it returned a 0 and the value started decrementing
                //decrement
                currentNumber -= 0.01f;
                break;

            case 2:
                currentNumber += 0.01f;
                break;

            default:
                return;

        }

        // for visualization
        agentSphere.position = new Vector3(currentNumber, 0f, 0f);

        if (currentNumber < -1.2f || currentNumber > 1.2f)
        {

            SetReward(-1.0f);
            Done();
        }


        float difference = Mathf.Abs(targetNumber - currentNumber);

        Debug.Log(string.Format("dif: {0}, ToTarg:{1}, R:{2}, CR: {3}", difference, distanceToTarget, GetReward(), GetCumulativeReward()));

        // add reward for getting closer to the target
        if (difference < distanceToTarget)
        {
            AddReward(0.05f);
            distanceToTarget = difference;
        }

        if (difference <= 0.01f)
        {
            solved++;
            AddReward(1.0f);
            Done();

        }

    }

}


///* didn't really work well on training - the computer didn't know what to really do  */
///*
// * 
// * TODO:
// * - perhaps modify the way you give rewards and penalty - positive reinforcement!
// *      - give it a reward when the number gets closer to the target
// * 
// */ 
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using MLAgents;
//using UnityEngine.UI;

//public class NumberDemoAgent : Agent {

//    [SerializeField]
//    private float currentNumber;

//    [SerializeField]
//    private float targetNumber;

//    [SerializeField]
//    private Text text;

//    int solved;     // count of what has been successfully solved

//    //private float distanceToTarget;


//    public override void CollectObservations()
//    {
//        AddVectorObs(currentNumber);
//        AddVectorObs(targetNumber);
//    }

//    // after every completion -- fails by going out of range or succeeds
//    public override void AgentReset()
//    {
//        targetNumber = Random.Range(-1f, 1f);
//        currentNumber = 0f;

//    }

//    public override void AgentAction(float[] vectorAction, string textAction)
//    {
//        if (text != null)
//        {
//            text.text = string.Format("C:{0} / T:{1} [{2}]", currentNumber, targetNumber, solved);
//        }

//        //Debug.Log("Agent action[0]: " + vectorAction[0]);      // NT: added
//        //Debug.Log("Agent action[1]: " + vectorAction[1]);      // NT: 

//        switch((int)vectorAction[0])
//        {
//            // NT: the case val is different from the tutorial.
//            case 1:                     // NT: changed it to case: 1, instead of 0 b/c by default it returned a 0 and the value started decrementing
//                //decrement
//                currentNumber -= 0.01f;
//                break;

//            case 2:
//                currentNumber += 0.01f;
//                break;

//            default:
//                return;

//        }

//        if (currentNumber < -1.2f || currentNumber > 1.2f)
//        {
            
//            SetReward(-1.0f);
//            Done();
//        }

//        // time penalty
//        AddReward(-0.05f);      // NT: added to see what happens

//        float difference = Mathf.Abs(targetNumber - currentNumber);
//        if (difference <= 0.01f)
//        {
//            solved++;
//            SetReward(1.0f);
//            Done();

//        }
            
//    }

//}



//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using MLAgents;
//using UnityEngine.UI;

//public class NumberDemoAgent : Agent {

//    [SerializeField]
//    private float currentNumber;

//    [SerializeField]
//    private float targetNumber;

//    [SerializeField]
//    private Text text;

//    int solved;     // count of what has been successfully solved


//	public override void CollectObservations()
//	{
//        AddVectorObs(currentNumber);
//        AddVectorObs(targetNumber);
//	}

//    // after every completion -- fails by going out of range or succeeds
//	public override void AgentReset()
//	{
//        targetNumber = Random.Range(-1f, 1f);
//        currentNumber = 0f;
//	}

//	public override void AgentAction(float[] vectorAction, string textAction)
//	{
//        if (text != null)
//        {
//            text.text = string.Format("C:{0} / T:{1} [{2}]", currentNumber, targetNumber, solved);
//        }

//        Debug.Log("Agent action[0]: " + vectorAction[0]);      // NT: added
//        //Debug.Log("Agent action[1]: " + vectorAction[1]);      // NT: 

//        switch((int)vectorAction[0])
//        {
//            case 1:                     // NT: changed it to case: 1, instead of 0 b/c by default it returned a 0 and the value started decrementing
//                //decrement
//                currentNumber -= 0.01f;
//                break;

//            case 2:
//                currentNumber += 0.01f;
//                break;

//            default:
//                return;
//            //case 0:
//            //    //decrement;
//            //    currentNumber -= 0.01f;
//            //    break;

//            //case 1:
//            //    currentNumber += 0.01f;
//            //    break;

//            //default:
//                //return;
//        }

//        if (currentNumber < -1.2f || currentNumber > 1.2f)
//        {
            
//            AddReward(-1.0f);
//            Done();
//        }

//        float difference = Mathf.Abs(targetNumber - currentNumber);
//        if (difference <= 0.01f)
//        {
//            solved++;
//            AddReward(1.0f);
//            Done();

//        }
            
//	}

//}
