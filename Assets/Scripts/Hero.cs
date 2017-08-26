using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

	// Editor-Visible Fields
	public static Hero currentHero;
	public Transform waist;

	[SerializeField]
	private int damage;
	[SerializeField]
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

		waist.rotation = Quaternion.Euler(0, 0, Util.ZDegFromDirection(x, y));


	}





}

public class Util {
	public static float ZDegFromDirection(Vector2 vect2) {
		return ZDegFromDirection (vect2.x, vect2.y);
	}

	public static float ZDegFromDirection(float x, float y) {
		return Mathf.Atan2(x, y) * Mathf.Rad2Deg - 90;
	}
}
