using UnityEngine;
using System;
using System.Collections;
using RSG;

public class ClassicShooterBehaviour : EnemyBehavior {

	private GameObject enemyTarget;

	public GameObject classicBullet;


	// Use this for initialization
	void Start () {
		enemyTarget = GameObject.FindGameObjectWithTag ("Player");
		bullet = classicBullet;
	}


	// Update is called once per frame
	public override void Update () {
		base.Update ();

		// Anim 
//		transform.LookAt (enemyTarget.transform.position);
	}
}
