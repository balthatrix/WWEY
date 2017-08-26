using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

	// Editor-Visible Fields
	public static Hero currentHero;
	public Transform waist;
	public Transform shoulders;


	public float swingCooldown = 1f;

	public  SwordSwingEffect swordSwing;


	[SerializeField]
	private int damage;
	[SerializeField]
	private float speed;

	private bool lockMovement;


	void Start() {
		CameraFollow.instance.AttachToHero (this);

		swordSwing.OnSwingStart += () => {
			lockMovement = true;
		};

		swordSwing.OnSwingEnd += () => {
			lockMovement = false;
		};
	}
		
	void Update() {
		float x = Input.GetAxis ("Horizontal");
		float y = Input.GetAxis ("Vertical");




		Vector3 intendedDirection = new Vector3 (x, y, 0f);
		if (intendedDirection.sqrMagnitude > 1f) {
			intendedDirection = intendedDirection.normalized;
		}

		//should be a value between 0, 1
		FeetAnimator ().speed = intendedDirection.magnitude;


		transform.Translate (intendedDirection * Time.deltaTime * speed);

		if(Mathf.Abs(x) > 0f || Mathf.Abs(y) > 0f)
			waist.localRotation = Quaternion.Euler(0, 0, Util.ZDegFromDirection(x, y));

		shoulders.localRotation = Quaternion.Euler (0, 0, -Util.ZDegFromDirection (DirectionsToMouseInWorld()));

		if (Input.GetMouseButtonDown (0)) { //left click
			swordSwing.Swing();
		}
	}

	Vector2 DirectionsToMouseInWorld() {
		return Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
	}



	Animator FeetAnimator() {
		return waist.GetComponentInChildren<Animator> ();
	}

	Animator SwordAnimator() {
		return shoulders.GetComponentInChildren<Animator> ();
	}



}

public class Util {
	public static float ZDegFromDirection(Vector2 vect2) {
		return ZDegFromDirection (vect2.x, vect2.y);
	}

	public static float ZDegFromDirection(float x, float y) {
		return Mathf.Atan2(x, y) * Mathf.Rad2Deg;
	}
}
