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

//		canFollow = true;
	}


	// Update is called once per frame
	void Update () {
		if (canFollow && bullet != followingBullet) {
			Debug.Log ("PROJECTILE AUTO");
			bullet = followingBullet;
		} else if (!canFollow && bullet != classicBullet){
			Debug.Log ("PROJECTILE CLASSIC");
			bullet = classicBullet;
		}
	
		if (Input.GetMouseButtonDown (0)) {
			shoot ();
		}
	}


	void shoot(){
		GameObject clone = Instantiate (bullet, transform.position, Quaternion.identity) as GameObject;
		clone.transform.rotation = transform.rotation;
	}
}
