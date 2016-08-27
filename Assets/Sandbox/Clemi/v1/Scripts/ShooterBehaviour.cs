using UnityEngine;
using System;
using System.Collections;
using RSG;

public class ShooterBehaviour : MonoBehaviour {

	private GameObject enemyTarget;
	private GameObject bullet;
	private float timeElapsed;

	public GameObject classicBullet;
	public GameObject followingBullet;
	public bool canFollow;

	[Range (1, 10)]
	public int bulletPerBurst = 3;
	[Range (0, 10)]
	public float bulletFreqency = 1f;
	[Range (0, 10)]
	public float burstFreqency = 3f;


	// Use this for initialization
	void Start () {
		enemyTarget = GameObject.FindGameObjectWithTag ("Player");

		// Placeholder for dynamic instantiation.
		canFollow = UnityEngine.Random.Range (0, 2) >= 1 ? true : false;
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

		timeElapsed += Time.deltaTime;
		if (timeElapsed >= burstFreqency) {
			timeElapsed = 0;
			Shoot ();
		}
	}

	/// <summary>
	/// Main shooting method.
	/// </summary>
	void Shoot() {
		Shoot (bulletPerBurst);
	}


	/// <summary>
	/// Recursive shooting method. Instantiate nbBullet via a Coroutine.
	/// </summary>
	/// <param name="nbBullet">Nb bullet.</param>
	void Shoot(int nbBullet){
		Promise promise = new Promise((resolve, reject) => {
			nbBullet--;
			StartCoroutine(LaunchBulletCoroutine(resolve, reject));
		});

		promise.Done( () => {
			if (nbBullet > 0) Shoot(nbBullet);
		});
	}


	/// <summary>
	/// Launch the bullet coroutine, instantiate a bullet after a delay.
	/// </summary>
	/// <returns>The bullet coroutine.</returns>
	/// <param name="resolve">Resolve.</param>
	/// <param name="reject">Reject.</param>
	private IEnumerator LaunchBulletCoroutine(Action resolve, Action<Exception> reject) {
		yield return new WaitForSeconds(bulletFreqency);

		GameObject clone = Instantiate (bullet, transform.position, Quaternion.identity) as GameObject;
		clone.transform.rotation = transform.rotation;

		resolve();
	}
}
