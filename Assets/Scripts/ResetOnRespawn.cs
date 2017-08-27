using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnRespawn : MonoBehaviour {

	[SerializeField]
	private EnemyID eyeDee;
	private Vector3 startPosition;

	public enum EnemyID {
		TREE,
		ROCK,
		SAVE
	}

	void Reset(){
		GameObject reset = null;

		switch (eyeDee) {
		case (EnemyID.ROCK):
			reset = Instantiate (GameManager.instance.rockEnemyFab);
			break;
		case (EnemyID.TREE):
			reset = Instantiate (GameManager.instance.treeEnemyFab);
			break;
		case (EnemyID.SAVE):
			reset = Instantiate (GameManager.instance.saveEnemyFab);
			break;
		}

		if (reset != null) {
			reset.transform.position = startPosition;
			Destroy (this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		GameManager.instance.OnHeroRespawn += Reset;
	}

	void OnDestroy () {
		GameManager.instance.OnHeroRespawn -= Reset;
	}

}
