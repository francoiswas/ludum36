using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour {

	private Color mouseOverColor = Color.blue;
	private Color originalColor = Color.yellow;

	private Vector2 fromPositionObject;
	private Vector2 fromPositionDash;
	private Vector2 toPosition;

	private float startMovingTime;
	private float speed = 7.5f;

	private bool go = false;


	void Start()
	{
		GetComponent<Renderer>().material.color = originalColor;
	}


	void Update()
	{
		if (go) {
			float timeSinceStarted = Time.time - startMovingTime;
			float percentageComplete = timeSinceStarted * speed;
			transform.position = Vector3.Lerp (fromPositionObject, toPosition, percentageComplete);
			if(percentageComplete >= 1.0f)
			{
				go = false;
			}
		}
	}


	void OnMouseDown()
	{
		Debug.Log ("OnMouseDown");
		fromPositionObject = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
	}


	void OnMouseDrag()
	{
		Vector2 curScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

		toPosition = Camera.main.ScreenToWorldPoint (curScreenPoint);
		Debug.DrawLine(transform.position, toPosition, Color.red);
	}


	void OnMouseUp()
	{
		go = true;
		startMovingTime = Time.time;
		fromPositionObject = transform.position;
	}

}
