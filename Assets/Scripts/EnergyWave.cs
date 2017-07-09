using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyWave : MonoBehaviour {

	float growthRate = 7f;
	float radiusAtNormalScale = 2f;
	float lifespan = 10f;

	float keypressDistanceAllowed = 4f;

	// Use this for initialization
	void Start () {
		transform.localScale = 0.05f * new Vector3 (1, 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale += growthRate * Time.deltaTime * new Vector3 (1, 1, 1);
		lifespan -= Time.deltaTime;
		if (lifespan <= 0) {
			GameObject.Destroy (gameObject);
		}
	}

	public bool IsPositionInWave(Vector3 position) {
		// Find distance
		Debug.Log("Position : " + (position - transform.position).magnitude + " and " + radiusAtNormalScale * transform.localScale.x);
		if (Mathf.Abs ((position - transform.position).magnitude - radiusAtNormalScale * transform.localScale.x) <= keypressDistanceAllowed) {
			return true;
		}

		return false;
	}
}
