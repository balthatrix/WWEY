using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Editor-Visible Fields
	public static GameManager instance;
	[SerializeField]
	private GameObject heroPrefab;
	[SerializeField]
	private SavePoint firstSave;

	[SerializeField]
	private List<SavePoint> allSavePoints;

	// Fields
	private SavePoint currentSave;

	// Methods
	public void SetSave(SavePoint save) {
		currentSave = save;
		save.DecorateAsCurrentSave ();
	}

	public void SpawnHero() {
		GameObject hero = Instantiate (heroPrefab);
		hero.transform.position = currentSave.transform.position;
	}

	// Mono-Behavior Methods
	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (this);
		}
	}

	IEnumerator Start () {
		//Give time for all save points to check in.
		yield return new WaitForEndOfFrame ();
		SetSave (firstSave);
		SpawnHero ();
	}

	public void CheckInSavePoint(SavePoint p) {
		if (p.isFirst) {
			firstSave = p;
		}
		allSavePoints.Add (p);
	}


}
