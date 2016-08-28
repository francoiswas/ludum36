using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour {
	public float projectileSpeed;
	public float projectilSize;
	public Material projectileMaterial;

	// Variables pour Munition 
	public float timeBeforeDestruction;
	public GameObject target;


	public virtual void Awake(){
		target = GameObject.FindGameObjectWithTag ("Player");
		SetProjectilSize (projectilSize);
	}


	public virtual void SetProjectilSize(float size) {
		transform.localScale = new Vector3(size, size, size);
	}


	public virtual void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			Debug.LogWarning ("PLAYER HIT");
			Destruct ();
		}
	}


	public virtual void OnBecameInvisible() {
		Destruct ();
	}


	public virtual void Destruct() {
		Destroy (gameObject);
	}
}
