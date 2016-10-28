using UnityEngine;
using System.Collections;

public class Detector : MonoBehaviour {
	StateMachine stateMachine;

	void OnTriggerStay(Collider other) {
		if (stateMachine.state == State.Wandering || stateMachine.state == State.Panic) {
			if (other.CompareTag ("Food")) {
				stateMachine.GetFood (other.transform);
			}
		}
		if (stateMachine.state == State.SeekMud) {
			if (other.CompareTag ("Mud")) {
				stateMachine.GetMud (other.transform);
			}
		}
	}

	// Use this for initialization
	void Start () {
		stateMachine = transform.parent.GetComponentInParent<StateMachine> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
