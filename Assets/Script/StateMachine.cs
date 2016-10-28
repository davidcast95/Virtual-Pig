using UnityEngine;
using System.Collections;

public enum State {Wandering,Rest,Resting,GetFood,Eating,Eat,SeekMud,GetMud,Playing,Fulled, Panic, Death};

public class StateMachine : MonoBehaviour {
	public State state;
	public string animationState;
	Pig pig;
	WalkPointController walkpoint;
	bool isFoodReachable = false;
	Transform food;
	WeatherController weather;
	public AudioClip[] sounds;


	// Use this for initialization
	void Start () {

		pig = GetComponent<Pig> ();
		walkpoint = GetComponent<WalkPointController> ();
		weather = GetComponentInParent<WeatherController> ();

		//initial state
		state = State.Wandering;
		if (weather.isRaining)
			state = State.Panic;
		if (weather.isHot)
			state = State.SeekMud;
		walkpoint.SetWalkPointToAgent ();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (transform.name + " : " + state);
		if (pig.alive) {
			if (state == State.Death) {
				animationState = "death";
				walkpoint.Stop ();
				StartCoroutine ("Death");
			}

			if (state == State.Playing) {
				if (weather.isRaining) {
					state = State.Wandering;
					walkpoint.Resume ();
				}
			}

			if (state == State.GetFood) {
				walkpoint.Resume ();
				animationState = "run";
				if (walkpoint.ReachDestination (1.05F * pig.mass)) {
					animationState = "eat";
					pig.energy += 10F;
					pig.GetBigger ();
					walkpoint.Stop ();
					state = State.Eating;
					StartCoroutine ("Eating");
				}
			}

			if (state == State.Panic) {
				walkpoint.Resume ();

				if (pig.energy >= 200F)
					state = State.Fulled;
				if (pig.energy < 0F) {
					state = State.Death;
				}
				if (!weather.isRaining)
					state = State.Wandering;
				animationState = "run";
				if (walkpoint.ReachDestination (0.5F)) {
					walkpoint.SetWalkPointToAgent ();
				}
			}
			if (state == State.Fulled) {
				animationState = "idle2";
				if (pig.energy <= 100F) {
					state = State.Wandering;
					walkpoint.Resume ();
				}
				if (weather.isRaining)
					state = State.Panic;
				if (weather.isHot)
					state = State.SeekMud;
			}
			if (state == State.Wandering) {
				walkpoint.Resume ();
				if (pig.energy >= 200F)
					state = State.Fulled;
				if (weather.isRaining)
					state = State.Panic;
				if (weather.isHot)
					state = State.SeekMud;
				if (pig.energy <= 30F) {
					state = State.Rest;
				}
				animationState = "walk";
				if (walkpoint.ReachDestination (0.5F)) {
					walkpoint.SetWalkPointToAgent ();
				}
			}
			if (state == State.SeekMud) {
				if (!weather.isHot)
					state = State.Wandering;
				animationState = "run";
				if (walkpoint.ReachDestination (0.5F)) {
					walkpoint.SetWalkPointToAgent ();
				}
			}

			if (state == State.GetMud) {
				if (walkpoint.ReachDestination (Random.Range(0F,1F))) {
					animationState = "idle1";
					walkpoint.Stop ();
					state = State.Playing;
					StartCoroutine ("Playing");
				}
			}
			if (state == State.Rest) {
				animationState = "idle2";
				walkpoint.Stop ();
				state = State.Resting;
				StartCoroutine ("Resting");
			}
		}
	}

	IEnumerator Eating() {
		GetComponent<AudioSource> ().clip = sounds [1];
		GetComponent<AudioSource> ().Play ();
		yield return new WaitForSeconds (2F);
		GetComponent<AudioSource> ().clip = sounds [0];
		GetComponent<AudioSource> ().Play ();
		if (pig.energy <= 200F) {
			state = State.Wandering;
			walkpoint.Resume ();
		}
		else
			state = State.Fulled;
		Destroy (food.parent.gameObject);
	}

	IEnumerator Playing() {
		float x = 2 * Random.Range (1.0F, 5.0F);
		yield return new WaitForSeconds (x);
		if (weather.isHot)
			state = State.GetMud;
		else {
			state = State.Wandering;
			walkpoint.Resume ();
		}
	}

	IEnumerator Resting() {
		yield return new WaitForSeconds (10F);
		if (weather.isRaining)
			state = State.Panic;
		else {
			state = State.Wandering;
			walkpoint.Resume ();
		}
	}

	IEnumerator Death() {
		yield return new WaitForSeconds (2.3F);
		pig.alive = false;
	}

	//By Trigger
	public void GetFood(Transform food) {
		//running to food
		animationState = "run";
		walkpoint.RunTo (food);
		state = State.GetFood;
		this.food = food;
	}
	public void GetMud(Transform mud) {
		animationState = "run";
		walkpoint.RunTo (mud);
		state = State.GetMud;
	}
}
