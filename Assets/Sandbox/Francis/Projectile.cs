using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float projectileSpeed;
	public float projectilSize;
	public Material projectileMaterial;

	// Variables pour Munition 
	public bool projectCanFollow;
	public float timeBeforeDestruction;
	public GameObject projectileTarget;


	IEnumerator FollowPlayer(){
		transform.LookAt (projectileTarget.transform);
		transform.Translate (Vector3.forward * Time.deltaTime * projectileSpeed);
	
		yield return new WaitForSeconds (timeBeforeDestruction);
		Destroy (gameObject);
	
	}
		
	void Awake(){
		projectileTarget = GameObject.FindGameObjectWithTag ("Player");
		SetProjectilSize ();
	}

	void Update () {
		if(projectCanFollow){
			StartCoroutine ("FollowPlayer");
		}
		else SraightMovement();
	}

	void SetProjectilSize(){
		// Set up SIZE
		transform.localScale = new Vector3(projectilSize,projectilSize,projectilSize);
	}

	void SraightMovement(){
		transform.Translate (Vector3.left * Time.deltaTime * projectileSpeed);
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			Debug.Log ("Joueur touché");
			Destroy (gameObject);
		}
	}
}
