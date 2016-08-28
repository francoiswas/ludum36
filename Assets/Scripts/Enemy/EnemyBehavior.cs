using UnityEngine;
using System;
using System.Collections;
using RSG;

public abstract class EnemyBehavior : MonoBehaviour {

	protected GameObject bullet;
	protected float timeElapsed;


	[Range (1, 10)]
	public int bulletPerBurst;
	[Range (0, 10)]
	public float bulletFreqency;
	[Range (0, 10)]
	public float burstFreqency;


	public virtual void Update() {
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
		//clone.transform.rotation = transform.rotation;

		resolve();
	}


}