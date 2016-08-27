using UnityEngine;
using System.Collections;

public class Coins : MonoBehaviour {
	public GameObject prefabEnnemy;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("spawn", 0.5f, 3);
	}
	
	void spawn()
	{
		Instantiate (prefabEnnemy);

	}


}
