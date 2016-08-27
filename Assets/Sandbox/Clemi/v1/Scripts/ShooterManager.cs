using UnityEngine;
using System.Collections;

public class ShooterManager : MonoBehaviour {

	public GameObject P_Enemy;
	[Range(0,50)]
	public int NB_ENEMIES;

	private float camHeight;
	private float camWidth;

	// Use this for initialization
	void Start () {
		Camera cam = Camera.main;
		camHeight = 2f * cam.orthographicSize;
		camWidth = camHeight * cam.aspect;

		Debug.Log ("NB_ENEMIES " + NB_ENEMIES);
		for(int i = 0; i < NB_ENEMIES; i++){
			SpawnEnemy(camHeight, camWidth);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.childCount < NB_ENEMIES) {
			SpawnEnemy(camHeight, camWidth);
		} else if (transform.childCount > NB_ENEMIES) {
			KillEnemy ();
		}
	}


	void SpawnEnemy(float maxVertical, float maxHorizontal) {
		GameObject clone = Instantiate (P_Enemy) as GameObject;
		clone.transform.parent = transform;

		bool xFixed = Random.Range (0, 2) >= 1 ? true : false;

		float[] xFixedPossibilities = new float[] { -camWidth / 2f, camWidth / 2f};
		float[] yFixedPossibilities = new float[] { -camHeight / 2f, camHeight / 2f};
		Vector2 spawnPosition;

		if (xFixed) {
			// use random on Y axis
			spawnPosition = new Vector2(
				xFixedPossibilities[Random.Range( 0, xFixedPossibilities.Length)],
				Random.Range(-camHeight / 2f, camHeight / 2f)
			);
		}else{
			// use random on X axis
			spawnPosition = new Vector2(
				Random.Range(-camWidth / 2f, camWidth / 2f),
				yFixedPossibilities[Random.Range( 0, yFixedPossibilities.Length)]
			);
		}

		clone.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 1);
	}


	void KillEnemy(){
		Destroy(transform.GetChild (Random.Range (0, transform.childCount - 1)).gameObject);
	}
}
