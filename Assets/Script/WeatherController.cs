using UnityEngine;
using System.Collections;

public class WeatherController : MonoBehaviour {
	public Material[] skybox;
	int indexSkybox = 0;
	float skyboxRotation = 0F;
	public GameObject sun;
	public DigitalRuby.RainMaker.RainScript rain;
	public float chanceOfRain = 0.2F;
	public bool isRaining = false;
	public bool isHot = false;
	public float chanceOfHot = 0.3F;
	float maxHot;
	bool hotReached;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyUp(KeyCode.H)) {
			isHot = true;
			maxHot = Random.Range (1F, 2F);
			hotReached = false;
		}

		sun.transform.Rotate(new Vector3(Time.deltaTime * 0.3F,0,0));

		if (sun.transform.localRotation.eulerAngles.x > 60F && sun.transform.localRotation.eulerAngles.x < 120F) {
			RenderSettings.skybox = skybox [0];
		} else {
			RenderSettings.skybox = skybox [1];
		}
		if (sun.transform.localRotation.eulerAngles.x > 180F)
			sun.transform.rotation = Quaternion.Euler(new Vector3(0F,60F,0F));
		
		RenderSettings.skybox.SetFloat ("_Rotation", skyboxRotation);
		skyboxRotation += 0.1F * Time.deltaTime;

		if (!isRaining && !isHot) {
			if (sun.transform.localRotation.eulerAngles.x > 60 && Random.Range (0F, 10000F) <= chanceOfHot * 100F) {
				maxHot = Random.Range (1F, 2F);
				hotReached = false;
				isHot = true;
			} else
				isHot = false;
		}

		if (isHot && hotReached) {
			if (sun.GetComponent<Light> ().intensity > 1F) {
				sun.GetComponent<Light> ().intensity -= Time.deltaTime * Random.Range (0.01F, 0.1F);
			} else {
				isHot = false;
				hotReached = false;
			}
		}

		if (isHot && !hotReached) {
			if (sun.GetComponent<Light> ().intensity < maxHot) {
				sun.GetComponent<Light> ().intensity += Time.deltaTime * 0.1F; 
			} else
				hotReached = true;
		} else if (!isHot) {
			if (sun.GetComponent<Light> ().intensity > 1F) {
				sun.GetComponent<Light> ().intensity -= Time.deltaTime * 0.1F;
			}
		}
		if (isRaining && !isHot) {
			if (sun.GetComponent<Light> ().intensity > 0.5F) {
				sun.GetComponent<Light> ().intensity -= Time.deltaTime * 0.1F;
			}
			if (rain.RainIntensity - Time.deltaTime * 0.01F > 0)
				rain.RainIntensity -= Time.deltaTime * 0.01F;
			else {
				rain.RainIntensity = 0F;
				isRaining = false;
			}
		}
		else if (!isRaining && !isHot) {
			if (sun.GetComponent<Light> ().intensity < 1F) {
				sun.GetComponent<Light> ().intensity += Time.deltaTime * 0.1F;
			}
			if (Random.Range (0F, 10000F) <= chanceOfRain * 100F && sun.transform.localRotation.eulerAngles.x < 60) {
				isRaining = true;
				rain.RainIntensity = Random.Range (0F, 1F);
			} else {
				isRaining = false;
			}
		}
	}


}
