using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ravine : MonoBehaviour {

	IEnumerator Fall (GameObject faller) {
	}

	void OnTriggerEnter2D (Collider2D other) {

		if (other.gameObject.GetComponent<Hero> () != null) {
			if (other.gameObject.GetComponent<Hero>().isDashing) {
				return;
			}
		}

		Fall (other);
	}

}
