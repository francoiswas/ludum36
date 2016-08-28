using UnityEngine;
using System.Collections;

public class EnemyTargetingMovement : MonoBehaviour {
	public float enemySpeed;
	public GameObject myTarget;
	private int mynumber;

	void Start(){
		enemySpeed /= 100;
	}

	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp (transform.position, myTarget.transform.position, enemySpeed);
}
}
