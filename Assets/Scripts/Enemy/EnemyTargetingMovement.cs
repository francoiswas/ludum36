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
<<<<<<< HEAD:Assets/Sandbox/Francis/Scripts/EnemyTargetingMovement.cs
		transform.position = Vector3.Lerp (transform.position, myTarget.transform.position, enemySpeed);
=======
		transform.position = Vector3.Lerp (transform.position,myTarget.transform.position,enemySpeed);

>>>>>>> 653fa39c7a2bf0babadaef75d66ade9ec3ce86d9:Assets/Scripts/Enemy/EnemyTargetingMovement.cs
}
}
