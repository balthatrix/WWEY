using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {


	public enum PickupType {
		SWORD,
		DASHING_BOOTS,
		POISON_HEALTH,
		HEALTH,
		ANNKE
	}


	public PickupType myType;
	void OnTriggerEnter2D(Collider2D other) {
		PickupReceiver h = other.GetComponent<PickupReceiver> ();
		if (h != null) {
			h.DoPickup (this);

			Destroy (gameObject);
		}
	}

}
