using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HasMovement {
	void LockMovement();
	void UnlockMovement();
}

public class AggroMimic : MonoBehaviour, HasMovement {

	[SerializeField]
	private float speed;

	private Hero heroToChase;
	private IEnumerator lastActiveIEnumerator;

	public TriggerListener aggroDomain;


	void Start() {
		aggroDomain.OnTriggerEntered += ThingEntered;
		aggroDomain.OnTriggerExited += ThingExited;
	}

	bool movementLocked;
	public void LockMovement() {
		movementLocked = true;
	}
	public void UnlockMovement() {
		movementLocked = false;
	}


	// Methods
	private IEnumerator GetUp() {
		GetComponent<SpriteRenderer> ().color = new Color (255, 255, 0, 255);
		yield return new WaitForSeconds (0.4f);

		GetComponent<SpriteRenderer> ().color = new Color (255, 0, 0, 255);
	}

	private IEnumerator SitDown() {
		GetComponent<SpriteRenderer> ().color = new Color (0, 0, 255, 255);

		yield return new WaitForSeconds (0.4f);
		GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255, 255);
	}

	// Mono-Behavior Methods
	void ThingEntered (Collider2D other) {
		Debug.Log ("ENTER");
		if (other.GetComponent<Hero>() != null) {
			heroToChase = other.GetComponent<Hero> ();

			if (lastActiveIEnumerator != null) {
				StopCoroutine (lastActiveIEnumerator);
			}
			lastActiveIEnumerator = GetUp ();
			StartCoroutine (lastActiveIEnumerator);
		}
	}

	void ThingExited (Collider2D other) {
		Debug.Log ("EXIT");
		if (other.GetComponent<Hero>() != null) {
			if (lastActiveIEnumerator != null) {
				StopCoroutine (lastActiveIEnumerator);
			}
			lastActiveIEnumerator = SitDown ();
			StartCoroutine (lastActiveIEnumerator);

			heroToChase = null;
		}
	}

	void Update () {
		if (heroToChase != null && !movementLocked) {
			transform.position = Vector3.MoveTowards (transform.position, heroToChase.transform.position, speed * Time.deltaTime);
		}
	}
}
