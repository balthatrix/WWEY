using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathGhostDone : MonoBehaviour {

	public Transform toFollow;

	//gets called from the animation event in the ghost animation
	public void DestroySelf() {

		GetComponent<Animator> ().speed = 0f;
//		Destroy (gameObject);
		StartCoroutine(FadeAway());
	}

	IEnumerator FadeAway() {
		SpriteRenderer rend = GetComponent<SpriteRenderer> ();
		float runningA = 1f;
		while (runningA > 0f) {
			rend.color = new Color (rend.color.r, rend.color.g, rend.color.b, runningA);
			transform.localScale = new Vector3 (2f - runningA, 2f - runningA, 1f);
			runningA -= Time.deltaTime * 2f;
			yield return new WaitForEndOfFrame ();
		}

		Destroy (gameObject);
	}

	void Update() {
		if (toFollow != null) {
			transform.position = toFollow.position;
		}
	}
}
