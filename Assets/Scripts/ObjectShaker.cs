using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShaker : MonoBehaviour {

	Vector3 localRestPosition;
	Vector3 currentVelocity;
	float shakeTension = 125f;
	float shakeDamper = 25f;
	float shakeImpulseForce = 7f;

	float shakeFrequency = 0.15f;
	float shakeTimer = 0f;

	// Use this for initialization
	void Start () {
		localRestPosition = transform.position - transform.parent.position;
	}
	
	// Update is called once per frame
	void Update () {
		PullTowardsRestPosition ();

		shakeTimer -= Time.deltaTime;
		if (shakeTimer < 0) {
			shakeTimer += shakeFrequency;
			ShakeInDirection ();
		}
	}

	void ShakeInDirection() {
		var flatShakeDir = shakeImpulseForce * Random.insideUnitCircle.normalized;
		currentVelocity += new Vector3 (flatShakeDir.x, 0, flatShakeDir.y);
	}

	void PullTowardsRestPosition() {
		var tensionForce = shakeTension*(localRestPosition - (transform.position - transform.parent.position));
		currentVelocity += tensionForce * Time.deltaTime;
		currentVelocity = Vector3.MoveTowards (currentVelocity, Vector3.zero, shakeDamper * Time.deltaTime);
		transform.position += currentVelocity*Time.deltaTime;
	}
}
