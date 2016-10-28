using UnityEngine;
using System.Collections;

public class PigAnimator : MonoBehaviour {
	public Animation animate;
	StateMachine stateMachine;
	Pig pig;
	bool hasEaten;

	// Use this for initialization
	void Start () {
		stateMachine = GetComponentInParent<StateMachine>();
		pig = GetComponentInParent<Pig>();
	}

	// Update is called once per frame
	void Update () {
		if (!pig.alive)
			animate.enabled = false;
		else
			animate.CrossFade (stateMachine.animationState);
	}



}
