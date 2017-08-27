using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ravine : MonoBehaviour {

	IEnumerator Fall (GameObject rootObjectFaller) {
		//remove death effects....
		yield return null;
	}

	private Hero player;

	void OnTriggerEnter2D (Collider2D other) {
		Damageable dmg = other.gameObject.GetComponent<Damageable> ();
		if (dmg != null) {
			Hero h = dmg.GetComponent<Hero> ();
			if (h != null) {
				player = h;
			} else {
				Fall (dmg.rootObject);
			}
		}

	}

	void OnTriggerExit2D(Collider2D other) {
		Damageable dmg = other.gameObject.GetComponent<Damageable> ();
		if (dmg != null) {
			Hero h = dmg.GetComponent<Hero> ();
			if (h != null) {
				player = null;
			}
//			damageable = 
		}

	}

	public void Update() {
		if(player != null && !player.isDashing) {
			Fall (player.GetComponent<Damageable> ().rootObject);
		}

	}
}
