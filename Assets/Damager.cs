using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {
	public int damage = 1;

	public float knockbackMagnitude = 500f;

	[SerializeField]
	public List<string> friendTags;

	void OnTriggerEnter2D(Collider2D other) {
		Damageable thing = other.GetComponent<Damageable> ();

		if (thing != null && !friendTags.Contains(other.tag)) {
			
			thing.TakeDamage (this, damage, knockbackMagnitude);
			thingInsideDomain = thing;
		}
	}

	Damageable thingInsideDomain;

	void OnTriggerExit(Collider2D other) {
		Damageable thing = other.GetComponent<Damageable> ();
		if (thing == thingInsideDomain)
			thingInsideDomain = null;
	}
}
