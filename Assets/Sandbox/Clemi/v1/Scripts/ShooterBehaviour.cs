using UnityEngine;
using System.Collections;

public class ShooterBehaviour : MonoBehaviour {

	private GameObject enemyTarget;
	private GameObject bullet;

	public GameObject classicBullet;
	public GameObject followingBullet;
	public bool canFollow;


	// Use this for initialization
	void Start () {
		enemyTarget = GameObject.FindGameObjectWithTag ("Player");

		// Placeholder for dynamic instantiation.
		canFollow = Random.Range (0, 2) >= 1 ? true : false;
	}


	// Update is called once per frame
	void Update () {
		if (canFollow && bullet != followingBullet) {
			Debug.Log ("INTO");
			bullet = followingBullet;
		} else {
			bullet = classicBullet;
		}
	
	}
}
