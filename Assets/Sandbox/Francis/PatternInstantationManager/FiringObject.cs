﻿using UnityEngine;
using System;
using System.Collections;
using RSG;

public class FiringObject : MonoBehaviour {

	private GameObject enemyTarget;
	private GameObject bullet;
	private float timeElapsed;

	public GameObject classicBullet;
	public ParticleSystem projectileTraineePrefab;
	public ParticleSystem projectileFlashPrefab;
	public GameObject followingBullet;
	public bool canFollow;

	[Range (1, 10)]
	public int bulletPerBurst = 3;
	[Range (0, 10)]
	public float bulletFreqency = 1f;
	[Range (0, 10)]
	public float burstFreqency = 3f;

	public void DebugLogSMTH (){
		Debug.Log("next");
	}

	// Use this for initialization
	void Start () {

		Debug.Log ("FiringClass Instantiated");
		enemyTarget = GameObject.FindGameObjectWithTag ("Player");
		// Placeholder for dynamic instantiation.
		canFollow = UnityEngine.Random.Range (0, 2) >= 1 ? true : false;
	}


	// Update is called once per frame
	void Update () {

		// Select weapon
		if (canFollow && bullet != followingBullet) {
			Debug.Log ("PROJECTILE AUTO");
			bullet = followingBullet;
		} else if (!canFollow && bullet != classicBullet){
			Debug.Log ("PROJECTILE CLASSIC");
			bullet = classicBullet;
		}

		// Burst Frequency
		timeElapsed += Time.deltaTime;
		if (timeElapsed >= burstFreqency) {
			timeElapsed = 0;
			Shoot ();
		}

		// Anim 

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
                                         
	// METHODE MODIFIE POUR LE PATTERN MANAGER
	private IEnumerator LaunchBulletCoroutine(Action resolve, Action<Exception> reject) {
		yield return new WaitForSeconds(bulletFreqency);

		GameObject clone = Instantiate (bullet, transform.position, Quaternion.identity) as GameObject;
		ParticleSystem cloneTraineeParticleSystem = Instantiate (projectileTraineePrefab, clone.transform.position, Quaternion.identity) as ParticleSystem;
		ParticleSystem cloneFlashParticleSystem = Instantiate (projectileFlashPrefab, clone.transform.position, Quaternion.identity) as ParticleSystem;
		cloneFlashParticleSystem.transform.parent = clone.transform;
		cloneFlashParticleSystem.transform.rotation = Quaternion.Euler (0, 90,0);
		cloneTraineeParticleSystem.transform.parent = clone.transform;
		cloneTraineeParticleSystem.transform.rotation = Quaternion.Euler (0, 90,0);
		Debug.Log (cloneTraineeParticleSystem.transform.rotation);

		resolve();
	}
}
