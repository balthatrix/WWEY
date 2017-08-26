using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour {

	private IEnumerator lastActiveIEnumerator;

	public bool isFirst = false;

	void Start() {
		GameManager.instance.CheckInSavePoint (this);
	}

	// Methods
	public void DecorateAsCurrentSave() {
		if (lastActiveIEnumerator != null) {
			StopCoroutine (lastActiveIEnumerator);
		}
		lastActiveIEnumerator = SetSaveAnimation ();
		StartCoroutine (lastActiveIEnumerator);
	}

	public void DecorateAsNotCurrentSave() {
		if (lastActiveIEnumerator != null) {
			StopCoroutine (lastActiveIEnumerator);
		}
		lastActiveIEnumerator = UnsetSaveAnimation ();
		StartCoroutine (lastActiveIEnumerator);
	}

	private IEnumerator SetSaveAnimation() {
		GetComponent<SpriteRenderer> ().color = new Color (255, 0, 255, 255);
		yield return new WaitForSeconds (0.75f);
		GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255, 255);
	}

	private IEnumerator UnsetSaveAnimation() {
		GetComponent<SpriteRenderer> ().color = new Color (0, 255, 255, 255);
		yield return new WaitForSeconds (0.5f);
		GetComponent<SpriteRenderer> ().color = new Color (0, 0, 0, 255);
	}

	// Mono-Behavior Methods
	void OnTriggerEnter2D (Collider2D other) {
		GameManager.instance.SetSave (this);
	}
}
