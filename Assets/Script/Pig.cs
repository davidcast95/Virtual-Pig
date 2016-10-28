using UnityEngine;
using System.Collections;

public class Pig : MonoBehaviour {
	public float walk;
	public float run;
	public float energy;
	public bool alive = true;
	public Transform body;
	public float mass = 0.5F;
	// Use this for initialization
	void Start () {
		energy = Random.Range (10F, 20F);
		UpdateBody ();
	}

	void UpdateBody() {
		body.localScale = new Vector3 (mass, mass, mass);
		walk = 1F * mass;
		run = 2F * mass;
	}

	public void GetBigger() {
		mass += 0.02F;
		UpdateBody ();
	}
}
