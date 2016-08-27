using UnityEngine;
using System.Collections;

public class Ennemies : MonoBehaviour {
	public GameObject prefabEnnemy;
	void Start () {
		InvokeRepeating ("spawn", 1, 1);
	}
	
	void spawn()
	{
		Instantiate (prefabEnnemy);

	}
}
