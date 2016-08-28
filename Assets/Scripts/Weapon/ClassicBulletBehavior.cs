using UnityEngine;
using System.Collections;

public class ClassicBulletBehavior : ProjectileBehavior {

	public ParticleSystem projectileTraineePrefab;


	public void Start() {
		ParticleSystem cloneParticleSystem = Instantiate (projectileTraineePrefab, gameObject.transform.position, Quaternion.identity) as ParticleSystem;
		cloneParticleSystem.transform.parent = transform;
		cloneParticleSystem.transform.rotation = Quaternion.Euler (0, 0, -90);
	}


	public void Update () {
		SraightMovement();
	}


	public void SraightMovement(){
		transform.Translate (Vector3.right * - Time.deltaTime * projectileSpeed);
	}
}
