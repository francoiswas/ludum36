using UnityEngine;
using System;
using System.Collections;
using RSG;

public abstract class EnemyBehavior : MonoBehaviour {

	protected GameObject bullet;
	protected float timeElapsed;

	public GameObject targetMovment;
	public float speed;
	[Range (1, 10)]
	public int bulletPerBurst;
	[Range (0, 10)]
	public float bulletFreqency;
	[Range (0, 10)]
	public float burstFreqency;


	public virtual void Awake () {
		speed /= 500;
	}

	public virtual void Update() {
		timeElapsed += Time.deltaTime;
		if (timeElapsed >= burstFreqency) {
			timeElapsed = 0;
			Shoot ();
		}

		Move ();
	}


	/// <summary>
	/// Main shooting method.
	/// </summary>
	public virtual void Shoot() {
		Shoot (bulletPerBurst);
	}


	/// <summary>
	/// Recursive shooting method. Instantiate nbBullet via a Coroutine.
	/// </summary>
	/// <param name="nbBullet">Nb bullet.</param>
	public virtual void Shoot(int nbBullet){
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



	public virtual void Move() {
		transform.position = Vector3.Lerp (transform.position,targetMovment.transform.position, speed);
//		transform.LookAt (targetMovment.transform.position);
	}
}