using UnityEngine;
using System.Collections;

public class LightningBehavior : MonoBehaviour {

	private Color c;
	private float start;
	private float interval = 1f;

	// Use this for initialization
	void Start () {
//		c = GetComponent<Renderer>().material.color;
		start = Time.time;

//		GetComponent<Renderer>().material.color = Color.yellow;
	}
	
	// Update is called once per frame
	void Update () {
		collideFlash ();	
	}

	IEnumerator collideFlash() {
		GetComponent<Renderer>().material.color = Color.yellow;
		yield return new WaitForSeconds(0.5f);
		GetComponent<Renderer>().material.color = Color.red;
	}
}
