using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class RollerAgent : Agent
{

    public Transform target;

    private Rigidbody rbAgent;

	private void Start()
	{
        rbAgent = GetComponent<Rigidbody>();
	}

	public override void AgentReset()
	{
        //base.AgentReset();

        if (transform.position.y < -1.0)
        {
            // the agent fell
            transform.position = Vector3.zero;
            rbAgent.angularVelocity = Vector3.zero;
            rbAgent.velocity = Vector3.zero;
        }
        else
        {
            // move the target to a new spot
            target.position = new Vector3(Random.value * 8 - 4,
                                         0.5f,
                                          Random.value * 8 - 4);

            //// NT: I think this works too
            //Vector3 newPos = target.position + new Vector3(Random.Range(-3f, 3f), 0.5f, Random.Range(-3f, 3f));
            //target.position = newPos;
        }
    }

	public override void CollectObservations()
	{
        //base.CollectObservations();

        // 
        Vector3 relativePosition = target.position - gameObject.transform.position;

        AddVectorObs(relativePosition.x / 5);       // NT: Q) why divide by 5?
        AddVectorObs(relativePosition.z / 5);

        // position of the agent itself within the confines of the floor
        AddVectorObs((transform.position.x + 5) / 5);
        AddVectorObs((transform.position.x - 5) / 5);
        AddVectorObs((transform.position.z + 5) / 5);
        AddVectorObs((transform.position.z - 5) / 5);

        // 
        AddVectorObs(rbAgent.velocity.x / 5);
        AddVectorObs(rbAgent.velocity.z / 5);

    }

    public float speed = 10f;
    private float previousDistance = float.MaxValue;

    // Q)is this the correct method?
	public override void AgentAction(float[] vectorAction, string textAction)
	{
        //base.AgentAction(vectorAction, textAction);

        // rewards
        float distanceToTarget = Vector3.Distance(gameObject.transform.position, target.position);

        // reached target
        if (distanceToTarget < 1.42f)      // NT: Q) why 1.42?
        {
            AddReward(1.0f);
            Done();
        }

        // time penalty
        AddReward(-0.05f);

        // fell of the platform
        if (transform.position.y < -1.0f)
        {
            AddReward(-1.0f);
            Done();
        }

        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.z = vectorAction[1];
        rbAgent.AddForce(controlSignal * speed);
	}



	//   public Transform targetInitialPos;
	//   public Transform agentInitialPos;


	//// a method that resets itself and the target when it succeeds or fails trying
	//private void ResetAgentAndTarget()
	//{
	//    target.transform.position = targetInitialPos.position;
	//    transform.position = agentInitialPos.position;
	//}

}
