﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour {

	[SerializeField]
	[TextArea(5, 5)]
	private string text;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<Hero>() != null) {
			GameManager.instance.SpawnText (text);
		}
	}

}
