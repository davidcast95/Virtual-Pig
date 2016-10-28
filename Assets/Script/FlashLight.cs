using UnityEngine;
using System.Collections;

public class FlashLight : MonoBehaviour {

	public GameObject flashlight;
	bool On = false;

	void Update () {
		if (Input.GetKeyUp (KeyCode.F)) {
			On = !On;
		}
		flashlight.SetActive (On);
	}
}
