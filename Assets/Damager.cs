using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {
	public int damage = 1;

	void OnTriggerEnter2D(Collider2D other) {
		Damageable thing = other.GetComponent<Damageable> ();
		if (thing != null) {
			thing.TakeDamage (damage);
		}
	}
}
