using UnityEngine;
using System.Collections;

public class UpAndDownAvion : MonoBehaviour {

	IEnumerator ChangeY(){
		yield return new WaitForSeconds (3F);
		LeanTween.moveY (gameObject, -2F, 0.2F);
	}

	// Use this for initialization
	void Start () {
		StartCoroutine ("ChangeY");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
