using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour {
	[Range (0, 10)]
	public float 			dashLength;
	[Range (0, 3)]
	public float 			dashTime;
	[Range (0, 3)]
	public float 			dashWait;
	[Range (0.001f, 1)]
	public float 			followSpeed;

	[Tooltip("Dash avec target")]
	public bool 			enableTargetDash;
	[Tooltip("Montre la destination du dash courant")]
	public bool 			enableDashHistory;
	[Tooltip("Le piaf regarde la souris")]
	public bool 			enableFollowMouse;
	[Tooltip("Le piaf suit la souris")]
	public bool				enableLookAtMouse;

	public LeanTweenType 	tweenType;
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

	private Camera 			cam;

	void Start () {
		cam = Camera.main;
		isPlaying = true;
		canDash = true;
		if (enableTargetDash) {
			target = Instantiate (prefabTarget);
		}

		if (enableDashHistory) {
			historyTarget = Instantiate (prefabTarget);
			historyTarget.SetActive (false);
		}

	}

	void Update () {
		if (canDash) {
			if(Input.GetMouseButtonDown (0) && enableTargetDash) {
				DoDash (true);
			}
			else if (Input.GetMouseButtonDown (0)) {
				DoDash (true);
			} else if (Input.GetMouseButtonDown (1)){
				DoDash (false);
			}
		}

		if (enableTargetDash) {
			DrawTarget ();
		}

		if (enableFollowMouse) {
			transform.position = Vector3.Lerp (transform.position, cam.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x,
				Input.mousePosition.y, -cam.transform.position.z)), followSpeed);
		}

		if (enableLookAtMouse) {
			Vector3 mp = Input.mousePosition;
			mp.z = -cam.transform.position.z;
			float AngleRad = Mathf.Atan2(cam.ScreenToWorldPoint (mp).y - transform.position.y, cam.ScreenToWorldPoint (mp).x - transform.position.x);
			float AngleDeg = (180 / Mathf.PI) * AngleRad;
			this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
		}

	}


	/// <summary>
	/// Dessine l'endroit vers lequel le piaf va aller
	/// </summary>
	void DrawTarget()
	{
		Vector3 mousePosition  =Input.mousePosition;
		mousePosition.z = -cam.transform.position.z;
		Vector3 head = cam.ScreenToWorldPoint (mousePosition)-transform.position;
		Vector3 dest = transform.position+head.normalized*dashLength;
		target.transform.position = dest;
		if (enableDashHistory) {
			historyPosition = dest;
		}

	}


	/// <summary>
	/// Fait un dash
	/// </summary>
	/// <param name="up">If set to <c>true</c> goes up.</param>
	void DoDash(bool up){

		canDash=false;
		mousePosition = Input.mousePosition;
		mousePosition.z = -cam.transform.position.z;
		heading = cam.ScreenToWorldPoint (mousePosition)-transform.position;

		if (!enableTargetDash) {
			heading.y = transform.position.y + (up?10:-10);
		}

		destination = transform.position+heading.normalized*dashLength;

		if (!enableTargetDash) {
			destination.x = transform.position.x;
		}
		if (enableDashHistory) {
			historyTarget.transform.position = historyPosition;
			historyTarget.SetActive (true);
		}
		LeanTween.move (this.gameObject, destination, dashTime).setEase (tweenType)
			.setOnComplete (() => {
				if(enableDashHistory){
					historyTarget.SetActive(false);
				}
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
