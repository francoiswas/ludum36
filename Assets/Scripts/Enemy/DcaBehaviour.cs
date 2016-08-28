﻿using UnityEngine;
using System;
using System.Collections;
using RSG;

public class DcaBehaviour : EnemyBehavior {

	private GameObject enemyTarget;
	public GameObject P_Bombshell;


	// Use this for initialization
	void Start () {
		enemyTarget = GameObject.FindGameObjectWithTag ("Player");
		bullet = P_Bombshell;
	}


	// Update is called once per frame
	public override void Update () {
		base.Update ();
		
		// Anim 
		transform.LookAt (enemyTarget.transform.position);
	}
}
