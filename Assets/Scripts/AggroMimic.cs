using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroMimic : MonoBehaviour {

	// Editor-Visible Fields
	[SerializeField]
	private int damage;
	[SerializeField]
	private float speed;
	[SerializeField]
	private float aggroRadius;
	[SerializeField]
	private float giveUpRadius;

	// Fields
	private bool isActive;
	private Hero heroToChase;
	private IEnumerator lastActiveIEnumerator;

	// Properties
	public bool IsActive {
		get { return isActive; }
		set { isActive = value; }
	}

	// Methods
	private IEnumerator GetUp() {
		Debug.Log ("1");
		GetComponent<SpriteRenderer> ().color = new Color (255, 255, 0, 255);
		yield return new WaitForSeconds (0.4f);
		isActive = true;
		GetComponent<SpriteRenderer> ().color = new Color (255, 0, 0, 255);
		Debug.Log ("2");
	}

	private IEnumerator SitDown() {
		Debug.Log ("3");
		GetComponent<SpriteRenderer> ().color = new Color (0, 0, 255, 255);
		isActive = false;
		yield return new WaitForSeconds (0.4f);
		GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255, 255);
		Debug.Log ("4");
	}

	// Mono-Behavior Methods
	void OnTriggerEnter2D (Collider2D other) {
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

	void OnTriggerExit2D (Collider2D other) {
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
		if (isActive) {
			transform.position = Vector3.MoveTowards (transform.position, heroToChase.transform.position, speed * Time.deltaTime);
		}
	}
}
