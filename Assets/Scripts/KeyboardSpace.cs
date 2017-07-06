using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class KeyboardSpace : MonoBehaviour {

	public string keyboardButton;
	private SpriteRenderer spriteRenderer;
	private Color buttonPushedColor = new Color(1f,1f,1f,180f/255f);
	private Color buttonNotPushedColor;
	private float buttonPushedScale = 1.25f;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		buttonNotPushedColor = spriteRenderer.color;
	}
	
	// Update is called once per frame
	void Update () {
		float currentScale = transform.localScale.x;
		currentScale = Mathf.Lerp (currentScale, 1f, 15 * Time.deltaTime);
		transform.localScale = new Vector3 (currentScale, currentScale, currentScale);

		if (Input.GetKeyDown (keyboardButton)) {
			ButtonPushed ();
		} else if (Input.GetKeyUp (keyboardButton)) {
			ButtonReleased ();
		}
	}

	void ButtonPushed() {
		spriteRenderer.color = buttonPushedColor;
		transform.localScale = new Vector3 (buttonPushedScale, buttonPushedScale, buttonPushedScale);
	}

	void ButtonReleased() {
		spriteRenderer.color = buttonNotPushedColor;
	}
}
