using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour {
	private Vector3 mousePosWorld;
	private Vector3 mousePosWorld2D;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
			Input.mousePosition.y, Camera.main.nearClipPlane)));

		transform.position = Vector3.Lerp (transform.position,Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
			Input.mousePosition.y, 10F)),0.01F);
		
	}
}
