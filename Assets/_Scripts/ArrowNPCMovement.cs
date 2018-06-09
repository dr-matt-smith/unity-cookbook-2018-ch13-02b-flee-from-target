using UnityEngine;
using UnityEngine.AI;

public class ArrowNPCMovement : MonoBehaviour {
	public float runAwayDistance = 10;
	public GameObject targetGO;
	private NavMeshAgent navMeshAgent;
	
	void Start()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

	/*----------------------------------------------------------*/
	// if fleeing, then call 'PositionToFleeTowards()' to calculted locaiton to flee towards
	// away from 'targetGO'
	void Update()
	{
		Vector3 targetPosition = targetGO.transform.position;
		float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
//		if (distanceToTarget < runAwayDistance)
//		{
			FleeFromTarget(targetPosition);
//		}
	}
	
	private void FleeFromTarget(Vector3 targetPosition)
	{
		Vector3 destination = PositionToFleeTowards(targetPosition);
		HeadForDestintation(destination);
		
		// show yellow line from source to target
		UsefulFunctions.DebugRay(transform.position, destination, Color.yellow);
	}

	/*----------------------------------------------------------*/
	private void HeadForDestintation (Vector3 destinationPosition)
	{
		navMeshAgent.SetDestination (destinationPosition);
	}
	
	/*----------------------------------------------------------
	 * rotate in opposite direciton to where targetGO is
	 * set return position of point that is 'runAwayDistance' further away from 'targetGO'
	 * than current position
	 */
	private Vector3 PositionToFleeTowards(Vector3 targetPosition)
	{
		transform.rotation = Quaternion.LookRotation(transform.position - targetPosition);
		Vector3 runToPosition = targetPosition + (transform.forward * runAwayDistance);
		return runToPosition;
		
		/*
		 * code to ensure our run to position is a valid part of the NavMesh
		NavMeshHit hit;
		NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetAreaFromName("Default"));
		return hit.position;
		*/
	}

}
