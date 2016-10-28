using UnityEngine;
using System.Collections;

public class WalkPointController : MonoBehaviour {
	public float width,height;
	public Vector3 target;
	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		GenerateRandomWalkPoint ();
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void GenerateRandomWalkPoint() {
		float x = Random.Range (0F, width);
		float z = Random.Range (0F, height);
		target = new Vector3 (x, 0, z);
	}

	public void SetWalkPointToAgent() {
		GenerateRandomWalkPoint ();
		if (target != null && agent != null)
			agent.SetDestination (target);
		while (!agent.CalculatePath(target,agent.path)) {
			GenerateRandomWalkPoint();
			agent.SetDestination(target);
		}
	}

	public bool ReachDestination(float radius) {
		return agent.remainingDistance <= radius;
	}

	public void RunTo(Transform target) {
		agent.SetDestination (target.position);
	}

	public void Stop() {
		agent.Stop ();
	}

	public void Resume() {
		agent.Resume ();
	}

}
