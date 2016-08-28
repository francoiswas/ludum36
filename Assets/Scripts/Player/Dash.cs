using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour {
	public float 			dashLength;
	public float 			dashTime;
	public LeanTweenType 	tweenType;
	public float 			dashWait;
	public GameObject		prefabTarget;

	private GameObject		target;
	private GameObject 		historyTarget;
	private	Vector3 		heading;
	private	Vector3 		destination;
	private Vector3			mousePosition;
	private Vector3 		historyPosition;
	private bool 			hasDroppedLetter;
//	private bool			isInvincible;
	private bool			isPlaying;

	private bool			canDash;


	private SpriteRenderer	sprite;


	// Use this for initialization
	void Start () {
		sprite = this.GetComponent<SpriteRenderer> ();
		isPlaying = true;
		canDash = true;
		target= Instantiate (prefabTarget);
		historyTarget= Instantiate (prefabTarget);
		historyTarget.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonDown (0)&&canDash) {
			DoDash ();
		}
		DrawTarget ();
	}

	void DrawTarget()
	{
		Vector3 mp  =Input.mousePosition;
		mp.z = -Camera.main.transform.position.z;
		Vector3 head = Camera.main.ScreenToWorldPoint (mp)-transform.position;
		Vector3 dest = transform.position+head.normalized*dashLength;
		target.transform.position = dest;
		historyPosition = dest;
	}

	void DoDash()
	{

//		heading = Camera.main.ScreenToWorldPoint (Input.mousePosition)-transform.position;
//		destination = (Vector2)transform.position+heading.normalized*dashLength;
//
//		LeanTween.move(this.gameObject, destination, dashTime).setEase(tweenType);
		canDash=false;
		mousePosition =Input.mousePosition;
		mousePosition.z = -Camera.main.transform.position.z;
		heading = Camera.main.ScreenToWorldPoint (mousePosition)-transform.position;
		destination = transform.position+heading.normalized*dashLength;
		historyTarget.transform.position = historyPosition;
		historyTarget.SetActive (true);
		LeanTween.move (this.gameObject, destination, dashTime).setEase (tweenType).setOnComplete (() => {
			historyTarget.SetActive(false);
		}
		)
		;

		if (dashWait > 0) {
			Invoke ("EnableDash", dashWait);
		} else {
			canDash = true;
		}

	}

	void EnableDash()
	{
		canDash = true;
	}


	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("COLLISION");
		if (other.tag == "Projectile") {
			//UI.instance.GameOver ();
			Debug.Log ("GAME OVER");
			Application.LoadLevel (Application.loadedLevel);
		}
			// Application.LoadLevel ("YOU ARE DEAD BITCH");}

//		if (!isInvincible && other.tag == "Projectile") {
//			if (!hasDroppedLetter) {
//				
//				DropLetter ();
//				isInvincible = true;
//
//				StartCoroutine (DoBlinks (6, 0.1f));
//		
//				//Invoke ("BecomeVincible", 0.5f);
//				Debug.Log ("BOOM");
//			} else if(isPlaying){
//				isPlaying = false;
//				UI.instance.GameOver ();
//				Debug.Log ("GAME OVER");
//			}
//
//		}
//
//		else if (!isInvincible&&other.tag == "Letter") {
//			hasDroppedLetter = false;
//			Destroy (other.gameObject);
//		}
	}
		

//	void DropLetter()
//	{
//		hasDroppedLetter = true;
//		Instantiate (PrefabManager.instance.letter,this.transform.position, Quaternion.identity);
//	}
//
//	IEnumerator DoBlinks(int numBlinks, float seconds) {
//		for (int i=0; i<numBlinks*2; i++) {
//
//				sprite.enabled = !sprite.enabled;
//			//wait for a bit
//			yield return new WaitForSeconds(seconds);
//		}
//
//		sprite.enabled=true;
//		isInvincible = false;
//	}
}
