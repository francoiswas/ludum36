using UnityEngine;
using System.Collections;

public class BulletBehavior : Projectile {
	
	// Variables pour Munition 
	public bool projectCanFollow;


	public void Update () {
		if(projectCanFollow){
			StartCoroutine ("FollowPlayer");
		}
		else SraightMovement();
	}


	public void SraightMovement(){
		transform.Translate (Vector3.forward * Time.deltaTime * projectileSpeed);
	}


	public IEnumerator FollowPlayer(){
		transform.LookAt (target.transform);
		transform.Translate (Vector3.forward * Time.deltaTime * projectileSpeed);

		yield return new WaitForSeconds (timeBeforeDestruction);
		Destruct ();
	}
}
