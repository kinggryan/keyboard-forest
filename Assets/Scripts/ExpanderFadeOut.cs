using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ExpanderFadeOut : MonoBehaviour {

	float lerpRate = 15f;
	float maxScale = 2f;

	private SpriteRenderer srenderer;

	// Use this for initialization
	void Start () {
		srenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = Vector3.Lerp (transform.localScale, maxScale * new Vector3 (1, 1, 1), lerpRate * Time.deltaTime);
		var targetColor = srenderer.color;
		targetColor.a = Mathf.Lerp (targetColor.a, 0, lerpRate * Time.deltaTime);
		srenderer.color = targetColor;
		if (srenderer.color.a < 0.01f) {
			GameObject.Destroy (gameObject);
		}
	}
}
