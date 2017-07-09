using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShakerV2 : MonoBehaviour {

	Vector3 localRestPosition;
	Vector3 currentVelocity;
	float shakeImpulseForce = 0f;

	float shakeFrequency = 0.1f;
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
		transform.position = transform.parent.position + localRestPosition + new Vector3 (flatShakeDir.x, 0, flatShakeDir.y);
	}

	void PullTowardsRestPosition() {
		transform.position = Vector3.Lerp (transform.position, transform.parent.position + localRestPosition, 15f * Time.deltaTime);
	}

	public void SetShake (float force) {
		shakeImpulseForce = force;
	}
}
