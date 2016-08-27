using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	void Start () {
		this.transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (1, Random.Range(0.1f,0.9f),10));
	}
	
	void Update () {
		this.gameObject.transform.Translate (new Vector2 (-0.1f, 0));
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Destroy (this.gameObject);
	}
}
