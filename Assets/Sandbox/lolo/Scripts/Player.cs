using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	ConstantForce2D constantForce;
	void Start () {
		constantForce = this.GetComponent<ConstantForce2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (force.force.y);
		if (Input.GetMouseButtonDown (0)) {
			constantForce.force = Vector2.zero;
		}
		else if(Input.GetMouseButtonUp (0)){
			constantForce.force = Vector2.up *20;
		}
	}
}
