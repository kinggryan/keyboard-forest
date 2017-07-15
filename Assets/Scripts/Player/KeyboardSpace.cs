using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KeyboardSpaceEffectManager))]
[RequireComponent(typeof(PlantSpace))]
public class KeyboardSpace : MonoBehaviour {

	public string keyboardButton;
	public bool permanentlyCharged = false;

	KeyboardSpaceEffectManager effectManager;
    PlantSpace plant;

	private float chargeDelay = 0.8f;
	private float maxChargeTime = 2f;
	private float currentChargeTime = 0f;
    private bool charge;
	private GameObject energyWavePrefab;

	// Use this for initialization
	void Start () {
        plant = GetComponent<PlantSpace>();
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
            charge = true;
			effectManager.StartCharging ();
		}
        plant.Grow();
	}

	void ButtonReleased() {
		effectManager.ButtonReleased (currentChargeTime >= chargeDelay);
		if (permanentlyCharged || charge) {
			GameObject.Instantiate (energyWavePrefab, transform.position + Vector3.up, Quaternion.AngleAxis(90,Vector3.right));
		}
        charge = false;
	}

	void ButtonHeld() {
        if(charge)
        {
            currentChargeTime += Time.deltaTime;
            if (currentChargeTime >= chargeDelay)
            {
                effectManager.Charged();
            }
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
