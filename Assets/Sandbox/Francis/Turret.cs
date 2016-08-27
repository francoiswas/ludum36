using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {
	public GameObject enemyTarget;
	public GameObject projectilePrefab;

	IEnumerator FireProjectile(){
		GameObject projectile = Instantiate (projectilePrefab, transform.position, Quaternion.identity) as GameObject;
		projectile.transform.rotation = transform.rotation;
		yield return new WaitForSeconds(1F);
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		AimTarget ();

		if(Input.GetKeyDown("space")){
			StartCoroutine ("FireProjectile");
		}

	}
		

	void AimTarget(){
		transform.LookAt (enemyTarget.transform.position);
	}
}
