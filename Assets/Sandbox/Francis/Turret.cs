using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {
	private GameObject enemyTarget;
	public GameObject projectilePrefab;
	public GameObject autoProjectilePrefab;

	IEnumerator FireProjectile(){
		GameObject projectile = Instantiate (projectilePrefab, transform.position, Quaternion.identity) as GameObject;
		projectile.transform.rotation = transform.rotation;
		yield return new WaitForSeconds(1F);
	}

	IEnumerator FireAutoProjectile(){
		GameObject projectile = Instantiate (autoProjectilePrefab, transform.position, Quaternion.identity) as GameObject;
		projectile.transform.rotation = transform.rotation;
		yield return new WaitForSeconds(1F);
	}

	void Start () {
		enemyTarget = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		AimTarget ();

		if (Input.GetKeyDown ("space")) {
			StartCoroutine ("FireProjectile");
		} else if (Input.GetKeyDown ("c")) {
			StartCoroutine ("FireAutoProjectile");
		} 

	}
		

	void AimTarget(){
		transform.LookAt (enemyTarget.transform.position);

	}
}
