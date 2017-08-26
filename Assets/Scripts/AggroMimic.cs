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

	// Properties
	public bool IsActive {
		get { return isActive; }
		set { isActive = value; }
	}

	// Methods
	private Vector3 GetDirectionToChase() {
		if (heroToChase != null) {
			Vector3 dirToHero = heroToChase.transform.position - transform.position;
			dirToHero = dirToHero.normalized;
			return dirToHero;
		} else {
			return new Vector3 (0, 0, 0);
		}
	}	

	// Mono-Behavior Methods
	void OnTriggerEnter2D (Collider2D other) {
		Debug.Log ("ENTER");
		if (other.GetComponent<Hero>() != null) {
			isActive = true;
			heroToChase = other.GetComponent<Hero> ();
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		Debug.Log ("EXIT");
		if (other.GetComponent<Hero>() != null) {
			isActive = false;
			heroToChase = null;
		}
	}

	void Update () {
		if (isActive) {
			transform.position = Vector3.MoveTowards (transform.position, heroToChase.transform.position, speed * Time.deltaTime);
		}
	}
}
