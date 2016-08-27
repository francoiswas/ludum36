using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour {
	public float 			dashLength;
	public float 			dashTime;
	public LeanTweenType 	tweenType;

	private	Vector2 		heading;
	private	Vector2 		destination;
	private bool 			hasDroppedLetter;
	private bool			isInvincible;

	private SpriteRenderer	sprite;


	// Use this for initialization
	void Start () {
		sprite = this.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonDown (0)) {
			DoDash ();
		}
	}

	void DoDash()
	{
		if (LeanTween.isTweening ())
			return;

		heading = Camera.main.ScreenToWorldPoint (Input.mousePosition)-transform.position;
		destination = (Vector2)transform.position+heading.normalized*dashLength;

		LeanTween.move(this.gameObject, destination, dashTime).setEase(tweenType);


	}


	void OnTriggerEnter2D(Collider2D other) {
		if (!isInvincible && other.tag == "Projectile") {
			if (!hasDroppedLetter) {
				
				DropLetter ();
				isInvincible = true;

				StartCoroutine (DoBlinks (10, 0.1f));
		
				//Invoke ("BecomeVincible", 0.5f);
				Debug.Log ("BOOM");
			} else {
				Debug.Log ("GAME OVER");
			}
		} else if (other.tag == "Letter") {
			hasDroppedLetter = false;
			Destroy (other.gameObject);
		}
	}
		

	void DropLetter()
	{
		hasDroppedLetter = true;
		Instantiate (PrefabManager.instance.letter,this.transform.position, Quaternion.identity);
	}

	IEnumerator DoBlinks(int numBlinks, float seconds) {
		for (int i=0; i<numBlinks*2; i++) {

				sprite.enabled = !sprite.enabled;
			//wait for a bit
			yield return new WaitForSeconds(seconds);
		}

		//make sure renderer is enabled when we exit
		sprite.enabled=true;
		isInvincible = false;
	}
}
