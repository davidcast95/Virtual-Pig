using UnityEngine;
using System.Collections;

public class FoodCreator : MonoBehaviour {
	public Transform food;
	bool isAble = true;

	// Use this for initialization
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("EyeRadius")) isAble = false;
	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag ("EyeRadius"))
			isAble = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Vector3 personPosition = transform.position;
			Vector3 personDirection = transform.forward;

			float offsetForward = 2F;

			food.position = personPosition + personDirection * offsetForward;
			Instantiate (food);
		}
	}
}
