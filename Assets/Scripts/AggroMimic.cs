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

	public float getUpDelay = 0f;


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
	private void GetUp() {
		if (OnGotUp != null) {
			OnGotUp ();
		}
	}

	private void SitDown() {
		if (OnSatDown != null) {
			OnSatDown ();
		}
	}

	// Mono-Behavior Methods
	void ThingEntered (Collider2D other) {
//		Debug.Log ("ENTER");
		if (other.GetComponent<Hero>() != null) {
			
			heroToChase = other.GetComponent<Hero> ();
			StartCoroutine (DelayGetUp ());
		}
	}

	bool delayingGetup = false;
	IEnumerator DelayGetUp() {
		delayingGetup = true;
		yield return new WaitForSeconds (getUpDelay);
		if (heroToChase != null)
			GetUp ();

		delayingGetup = false;
	}

	void ThingExited (Collider2D other) {
//		Debug.Log ("EXIT");
		if (other.GetComponent<Hero>() != null) {
			SitDown ();
			heroToChase = null;
		}
	}

	void Update () {
		if (CurrentlyChasing()) {
			transform.position = Vector3.MoveTowards (transform.position, heroToChase.transform.position, speed * Time.deltaTime);
			transform.localRotation = Quaternion.Euler (0, 0, -Util.ZDegFromDirection (heroToChase.transform.position - transform.position)  + 180f);
		}
	}

	public bool CurrentlyChasing() {
		return heroToChase != null && !movementLocked && !delayingGetup;
	}

	public delegate void GotUpAction();
	public event GotUpAction OnGotUp;
	public delegate void SitDownAction();
	public event SitDownAction OnSatDown;
}
