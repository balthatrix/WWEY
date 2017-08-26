using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

	private int damage;
	private float speed;

	void Awake() {
		speed = 5;
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
