using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {
	public int damage = 1;

	public float knockbackMagnitude = 20f;

	public Transform optionalCenterOfMass;

	[SerializeField]
	public List<string> friendTags;

	void OnTriggerEnter2D(Collider2D other) {
		Damageable thing = other.GetComponent<Damageable> ();

		if (thing != null && !friendTags.Contains(other.tag)) {
			
			thing.TakeDamage (this, damage, knockbackMagnitude);
			thingInsideDomain = thing;
		}
	}

	//this could be used to damage the thing inside the radius every once in a while. 
	Damageable thingInsideDomain;

	void OnTriggerExit2D(Collider2D other) {
		Damageable thing = other.GetComponent<Damageable> ();
		if (thing == thingInsideDomain)
			thingInsideDomain = null;
	}
}
