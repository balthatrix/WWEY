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

	// Properties
	public bool IsActive {
		get { return isActive; }
		set { isActive = value; }
	}

	// Methods
		
	// Mono-Behavior Methods
	void OnTriggerEnter2D (Collider2D other) {
		if (other.GetComponent<Hero>() != null) {
			isActive = true;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.GetComponent<Hero>() != null) {
			isActive = false;
		}
	}

	void Update () {
		if (isActive) {
			transform.Translate (new Vector3 (1, 1, 0) * Time.deltaTime * speed);
		}
	}
}
