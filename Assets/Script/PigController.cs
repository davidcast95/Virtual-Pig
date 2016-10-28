using UnityEngine;
using System.Collections;

public class PigController : MonoBehaviour {
	StateMachine stateMachine;
	Pig pig;
	NavMeshAgent agent;
	AudioSource sourceAudio;
	Transform player;
	// Use this for initialization
	void Start () {
		stateMachine = GetComponent<StateMachine> ();
		pig = GetComponent<Pig> ();
		agent = GetComponent<NavMeshAgent> ();
		sourceAudio = GetComponent<AudioSource> ();
		if (player == null)
			player = GameObject.Find ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		float volume = (20F - Vector3.Distance (transform.position, player.position)) / 20F;
		if (volume > 1F) {
			volume = 1F;
		} else if (volume < 0.01F)
			volume = 0.01F;


		sourceAudio.volume = volume;
		if (!pig.alive)
			sourceAudio.Stop ();

		if (stateMachine.state == State.Panic) {
			agent.speed = pig.run;
			pig.energy -= Time.deltaTime * 0.75F;
		}
		if (stateMachine.state == State.Wandering) {
			agent.speed = pig.walk;
			pig.energy -= Time.deltaTime * 0.1F;
		}
		if (stateMachine.state == State.GetFood) {
			agent.speed = pig.run;
			pig.energy -= Time.deltaTime * 0.3F;
		}
		if (stateMachine.state == State.Resting) {
			agent.speed = 0F;
			pig.energy += Time.deltaTime * 5F;
		}
		if (stateMachine.state == State.GetMud) {
			agent.speed = pig.run;
			pig.energy -= Time.deltaTime * 0.3F;
		}
		if (stateMachine.state == State.Playing) {
			agent.speed = 0F;
			pig.energy -= Time.deltaTime * 0.5F;
		}
		if (stateMachine.state == State.Fulled) {
			agent.speed = 0F;
			pig.energy -= Time.deltaTime * 10F;
		}
		if (stateMachine.state == State.SeekMud) {
			agent.speed = pig.run;
			pig.energy -= Time.deltaTime * 0.3F;
		}
	}
}
