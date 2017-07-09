using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KeyboardSpaceEffectManager))]
public class KeyboardSpace : MonoBehaviour {

	public string keyboardButton;
	public bool permanentlyCharged = false;

	KeyboardSpaceEffectManager effectManager;

	private float chargeDelay = 0.8f;
	private float maxChargeTime = 2f;
	private float currentChargeTime = 0f;
	private GameObject energyWavePrefab;

	// Use this for initialization
	void Start () {
		effectManager = GetComponent<KeyboardSpaceEffectManager> ();
		effectManager.buttonHeldShakeDelay = chargeDelay;
		effectManager.buttonHeldShakeMaxTime = maxChargeTime;
		energyWavePrefab = (GameObject)Resources.Load ("EnergyWave");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (keyboardButton)) {
			ButtonPushed ();
		} else if (Input.GetKeyUp (keyboardButton)) {
			ButtonReleased ();
		} else if (Input.GetKey(keyboardButton)) {
			ButtonHeld();
		}
	}

	void ButtonPushed() {
		effectManager.ButtonPushed ();
		currentChargeTime = 0;
		if (ShouldStartCharging()) {
			effectManager.StartCharging ();
		}
	}

	void ButtonReleased() {
		effectManager.ButtonReleased (currentChargeTime >= chargeDelay);
		if (permanentlyCharged) {
			GameObject.Instantiate (energyWavePrefab, transform.position + Vector3.up, Quaternion.AngleAxis(90,Vector3.right));
		}
	}

	void ButtonHeld() {
		currentChargeTime += Time.deltaTime;
		if (currentChargeTime >= chargeDelay) {
			effectManager.Charged ();
		}
	}

	bool ShouldStartCharging() {
		foreach (var eWave in Object.FindObjectsOfType<EnergyWave>()) {
			if (eWave.IsPositionInWave (transform.position)) {
				return true;
			}
		}

		return false;
	}
}
