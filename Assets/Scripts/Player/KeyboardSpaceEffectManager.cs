using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(ObjectShakerV2))]
public class KeyboardSpaceEffectManager : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	private ObjectShakerV2 shaker;

	private bool buttonHeld;

	// Color Effects
	private Color buttonPushedColor = new Color(1f,1f,1f,180f/255f);
	private Color buttonChargedColor = new Color(1f,1f,0.5f,180f/255f);
	private Color buttonNotPushedColor;

	// Scaling Effects
	private Vector3 initialScale;
	private float buttonPushedScale = 1.25f;
	private float chargeBurstScale = 1.5f;
	private float restScaling = 1f;				//!< The scale to return to over time
	private float scalingFactor = 1f;

	// Shaking Effects
	public float buttonHeldShakeDelay;
	public float buttonHeldShakeMaxTime;
	private float buttonHeldShakeMaxAmount = 0.75f;

	private float buttonHeldTime;
	private GameObject startChargingEffectPrefab;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		shaker = GetComponent<ObjectShakerV2> ();
		buttonNotPushedColor = spriteRenderer.color;
		initialScale = transform.localScale;
		startChargingEffectPrefab = (GameObject)Resources.Load ("KeyHitEffect");
	}
	
	// Update is called once per frame
	void Update () {
		scalingFactor = Mathf.Lerp (scalingFactor, restScaling, 15 * Time.deltaTime);
		transform.localScale = scalingFactor*initialScale;

		if(buttonHeld)
			buttonHeldTime += Time.deltaTime;

		UpdateShaking ();
	}

	public void ButtonPushed() {
		spriteRenderer.color = buttonPushedColor;
		AddScalingImpulse (buttonPushedScale);
		buttonHeld = true;
		buttonHeldTime = 0f;
	}

	public void ButtonReleased(bool charged) {
		spriteRenderer.color = buttonNotPushedColor;
		buttonHeld = false;
		shaker.SetShake (0);
		if (charged) {
			AddScalingImpulse (chargeBurstScale);
		}
	}

	public void Charged() {
		spriteRenderer.color = buttonChargedColor;
	}

	public void StartCharging() {
		GameObject.Instantiate (startChargingEffectPrefab, transform.position + 0.1f * Vector3.up, transform.rotation);
	}

	void AddScalingImpulse(float additionalScale) {
		scalingFactor *= additionalScale;
		transform.localScale = scalingFactor * transform.localScale;
	}

	void UpdateShaking() {
		if (buttonHeld) {
			var shakeAmount = buttonHeldShakeMaxAmount*Mathf.Clamp ((buttonHeldTime - buttonHeldShakeDelay) / (buttonHeldShakeMaxTime - buttonHeldShakeDelay), 0, 1f);
			shaker.SetShake (shakeAmount);
		}
	}
}
