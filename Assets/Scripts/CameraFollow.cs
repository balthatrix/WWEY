using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public static CameraFollow instance;

	private Hero hero;

	public void AttachToHero (Hero h) {
		hero = h;
	}

	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (this);
		}
	}

	// Update is called once per frame
	void Update () {
		if (hero != null) {
			transform.position = new Vector3 (hero.transform.position.x, hero.transform.position.y, transform.position.z);
		}
	}
}
