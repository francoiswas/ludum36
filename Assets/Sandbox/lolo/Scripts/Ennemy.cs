using UnityEngine;
using System.Collections;

public class Ennemy : MonoBehaviour {

	float speed, scale;

	void Start () {
		scale = Random.Range (0.5f, 1.5f);
		speed = Random.Range (0.5f, 3f);
		this.transform.localScale = new Vector2 (scale, scale);
		this.transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (1, Random.Range(0.1f,0.9f),10));
	}
	
	void Update () {
		this.gameObject.transform.Translate (new Vector2 (-0.1f*speed, 0));

		if (Camera.main.WorldToViewportPoint (this.transform.position).x<0) {

			World.instance.AddScore ();
			Destroy (this.gameObject);

		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		World.instance.GameOver ();
	}
}
