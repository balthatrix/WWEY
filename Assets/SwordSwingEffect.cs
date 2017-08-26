using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwingEffect : MonoBehaviour {


	//sword to rotate
	public Transform swordSprite;

	//container to manipulate position on...
	public Transform foreArm;

	public AnimationCurve swingRotationAnim;

	private float minZSwordRotation = -60f;
	private float maxZSwordRotation =  70f;

	public float duration;



	// Use this for initialization
	void Start () {
		StartCoroutine (Perform());
	}

	IEnumerator Perform() {
		
		float directionsToEnd = maxZSwordRotation - minZSwordRotation;
		foreArm.localRotation =  Quaternion.Euler (0, 0, minZSwordRotation);

		float time = 0f;
		while (time < duration) {
			float progress = time / duration;

			float currRatioOfComplete = swingRotationAnim.Evaluate (progress);
			float newRot = minZSwordRotation + (currRatioOfComplete * directionsToEnd);

			foreArm.localRotation =  Quaternion.Euler (0, 0, newRot);
			time += Time.deltaTime;
			yield return new WaitForEndOfFrame ();
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
