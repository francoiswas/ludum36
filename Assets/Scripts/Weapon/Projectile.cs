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
		transform.Translate (Vector3.forward * Time.deltaTime * projectileSpeed);
	}


	IEnumerator FollowPlayer(){
		transform.LookAt (projectileTarget.transform);
		transform.Translate (Vector3.forward * Time.deltaTime * projectileSpeed);

		yield return new WaitForSeconds (timeBeforeDestruction);
		Destruct ();
	}


	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			Debug.Log ("Joueur touché");
			Destruct ();
		}
	}


	void OnBecameInvisible() {
		Debug.Log ("CIAO");
		Destruct ();
	}


	public void Destruct() {
		Destroy (gameObject);
	}
}
