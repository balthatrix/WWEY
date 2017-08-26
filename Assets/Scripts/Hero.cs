using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

	// Editor-Visible Fields
	public static Hero currentHero;

	private int damage;
	private float speed;

	// Mono-Behavior Methods
	void Awake() {
	}

	void Start() {
		CameraFollow.instance.AttachToHero (this);
	}
		
	void Update() {
		float x = Input.GetAxis ("Horizontal");
		float y = Input.GetAxis ("Vertical");

		transform.Translate (new Vector3 (x, y, 0) * Time.deltaTime * speed);
	}

}
