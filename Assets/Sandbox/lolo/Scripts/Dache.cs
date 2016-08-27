using UnityEngine;
using System.Collections;

public class Dache : MonoBehaviour {
	public float 			dashLength;
	public float 			dashTime;
	public LeanTweenType 	tweenType;

	Vector2 heading;
	Vector2 destination;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonDown (0)) {
			Dash ();
		}
	}

	void Dash()
	{
		if (LeanTween.isTweening ())
			return;

		heading = Camera.main.ScreenToWorldPoint (Input.mousePosition)-transform.position;
		destination = (Vector2)transform.position+heading.normalized*dashLength;

		LeanTween.move(this.gameObject, destination, dashTime).setEase(tweenType);


	}
}
