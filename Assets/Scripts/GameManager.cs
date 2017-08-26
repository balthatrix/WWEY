using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Editor-Visible Fields
	public static GameManager instance;
	[SerializeField]
	private GameObject heroPrefab;
	[SerializeField]
	private GameObject tutorialPrefab;
	[SerializeField]
	private SavePoint firstSave;
	[SerializeField]
	private List<SavePoint> allSavePoints;

	// Fields
	private SavePoint currentSave;
	private TutorialText currentText;

	// Methods
	public void SetSave(SavePoint save) {
		if (currentSave != save) {
			if (currentSave != null) {
				currentSave.DecorateAsNotCurrentSave ();
			}
			currentSave = save;
			currentSave.DecorateAsCurrentSave ();
		}
	}

	public void SpawnHero() {
		GameObject hero = Instantiate (heroPrefab);
		hero.transform.position = currentSave.transform.position;
	}

	public void SpawnText(string whatToSay) {
		GameObject text = Instantiate (tutorialPrefab);
		if (currentText != null) {
			currentText.CleanupText ();
		}
		currentText = text.GetComponent<TutorialText> ();
		currentText.Say (whatToSay);
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
		SpawnText ("Press ENTER to start!");
	}

	public void CheckInSavePoint(SavePoint p) {
		if (p.isFirst) {
			firstSave = p;
		}
		allSavePoints.Add (p);
	}


}
