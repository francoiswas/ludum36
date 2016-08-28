using UnityEngine;
using System;
using System.Collections;
using RSG;

public class BombshellBehavior : Projectile {

	public float radiusExplosion;
	public float explosionDuration;

	private PromiseTimer promiseTimer = new PromiseTimer();


	public void Start () {
		Fire ();	
	}

	public void Update () {
//		StartCoroutine ("SraightMovement");

		promiseTimer.Update(Time.deltaTime);
	}

	public void Fire() {
		LeanTween
			.move (gameObject, target.transform.position, timeBeforeDestruction)
			.setOnComplete ( () => {
				LeanTween
					.scale (gameObject, new Vector3 (radiusExplosion, radiusExplosion, radiusExplosion), 0.1f)
					.setOnComplete( () => {
						LeanTween.delayedCall(explosionDuration, () => {
							Destruct();
						});
					});
			});
	}
}
