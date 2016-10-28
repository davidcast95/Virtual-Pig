using UnityEngine;
using System.Collections;

public class PigCreator : MonoBehaviour {

	public Transform pig;

	void Update () {
		if (Input.GetKeyUp(KeyCode.P)) {
			Vector3 personPosition = transform.position;
			Vector3 personDirection = transform.forward;

			float offsetForward = 2F;

			pig.position = new Vector3(personPosition.x,6F,personPosition.z) + personDirection * offsetForward;
			Transform result = Instantiate (pig);
			result.parent = GameObject.Find ("GameController").transform;
		}
	}
}
