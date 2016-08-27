using UnityEngine;
using System.Collections;

public class Storm : MonoBehaviour {

	public float interval = 0.5f;
	public GameObject P_Lightning;

	private bool shooting = false;
	private float firedTime;

	float screenWidth;

	// Use this for initialization
	void Start () {
		firedTime = Time.time;

		Camera cam = Camera.main;
		float height = 2f * cam.orthographicSize;
		screenWidth = height * cam.aspect;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - firedTime >= interval) {
			shooting = true;
		}

		if (shooting) {
			GameObject clone = Instantiate (P_Lightning) as GameObject;
			clone.transform.parent = this.transform;
			clone.transform.position = this.transform.position + clone.transform.localScale * 2;
			clone.transform.position = new Vector2 (Random.Range (-screenWidth / 2.5f, screenWidth / 2.5f), clone.transform.position.y);
			shooting = false;
			firedTime = Time.time;
		}
	}
}
