using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour {

	private IEnumerator lastActiveIEnumerator;

	// Methods
	public void DecorateAsCurrentSave() {
		if (lastActiveIEnumerator != null) {
			StopCoroutine (lastActiveIEnumerator);
		}
		lastActiveIEnumerator = SetSaveAnimation ();
		StartCoroutine (lastActiveIEnumerator);
	}

	private IEnumerator SetSaveAnimation() {
		GetComponent<SpriteRenderer> ().color = new Color (255, 0, 255, 255);
		yield return new WaitForSeconds (1f);
		GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255, 255);
	}

	// Mono-Behavior Methods
	void OnTriggerEnter2D (Collider2D other) {
		Debug.Log ("ENTER SAVE");
		GameManager.instance.SetSave (this);
	}
}
