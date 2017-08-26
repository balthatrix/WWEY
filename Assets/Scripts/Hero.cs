using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour, HasMovement {

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

	public void LockMovement() {
		lockMovement = true;
	}
	public void UnlockMovement() {
		lockMovement = false;
	}

	void Start() {
		CameraFollow.instance.AttachToHero (this);

		swordSwing.OnSwingStart += () => {
			LockMovement();
		};

		swordSwing.OnSwingEnd += () => {
			StartCoroutine(DelayUnlockMovement());
		};
	}

	IEnumerator DelayUnlockMovement() {
		yield return new WaitForSeconds (.1f);
		UnlockMovement ();
	}
		
	void Update() {
		CheckForMovementAndRotation ();

		if (Input.GetMouseButtonDown (0)) { //left click
			SwingSword();
		}
	}

	void CheckForMovementAndRotation() {
		if (lockMovement) {
//			Debug.Log ("movement is locked!");
			return;
		}
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
	}

	void SwingSword() {
		if (swordCooling || lockMovement)
			return;
		swordSwing.Swing();
		StartCoroutine(DoSwordCooldown ());
	}

	bool swordCooling = false;
	IEnumerator DoSwordCooldown() {
		swordCooling = true;
		yield return new WaitForSeconds (swingCooldown);
		swordCooling = false;
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
