using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using RSG;

public class EnemySpawnerManager : MonoBehaviour {

	public GameObject P_Enemy;
	public GameObject P_DCA;
	[Range(1, 6)]
	public int MIN_NB_ENEMIES_CLASSIC_PER_WAVE;
	[Range(1, 6)]
	public int MAX_NB_ENEMIES_CLASSIC_PER_WAVE;
	[Range(0.1f, 2f)]
	public float MIN_SPAWN_FREQUENCY;
	[Range(0.1f, 2f)]
	public float MAX_SPAWN_FREQUENCY;


	private int NB_ENEMIES_DCA = 0;

	public bool forceSpawningOnRight;

	private string ClassicTag;
	private string DcaTag;

	private List<GameObject> leftTargets;
	private List<GameObject> rightTargets;

	private List<GameObject> tmp_leftTargets;
	private List<GameObject> tmp_rightTargets;

	private int WAVE;


	// Use this for initialization
	void Start () {
		Camera cam = Camera.main;
		Vector3 test = cam.ViewportToWorldPoint(new Vector3(1, 1, -cam.transform.position.z));

		ClassicTag = "EnemyClassic";
		DcaTag = "EnemyDca";

		leftTargets = GameObject.FindGameObjectsWithTag ("TargetLeft").ToList();
		rightTargets = GameObject.FindGameObjectsWithTag ("TargetRight").ToList();

		WAVE = 0;

		newWave ();
	}


	// Update is called once per frame
	void Update () {
		if(transform.childCount == 0){
			newWave ();
		}
	}



	public void newWave(){
		WAVE++;
		Debug.Log ("WAVE " + WAVE);

		tmp_leftTargets = new List<GameObject>(leftTargets);
		tmp_rightTargets = new List<GameObject>(rightTargets);
		SpawnEnemies (P_Enemy, UnityEngine.Random.Range(MIN_NB_ENEMIES_CLASSIC_PER_WAVE, MAX_NB_ENEMIES_CLASSIC_PER_WAVE), ClassicTag);
		SpawnEnemies (P_DCA, NB_ENEMIES_DCA, DcaTag);

		Debug.Log (tmp_leftTargets);
	}


	/// <summary>
	/// Main shooting method.
	/// </summary>
	public virtual void SpawnEnemies(GameObject prefab, int nbInstance, string tag) {
		if(nbInstance>0) SpawnEnemy(prefab, nbInstance, tag);
	}


	/// <summary>
	/// Recursive shooting method. Instantiate nbBullet via a Coroutine.
	/// </summary>
	/// <param name="nbBullet">Nb bullet.</param>
	public virtual void SpawnEnemy(GameObject prefab, int nbInstance, string tag){
		Promise promise = new Promise((resolve, reject) => {
			nbInstance--;
			StartCoroutine(SpawnEnemyCoroutine(prefab, tag, resolve, reject));
		});

		promise.Done( () => {
			Debug.Log(nbInstance);
			if (nbInstance > 0) SpawnEnemy(prefab, nbInstance, tag);
		});
	}


	/// <summary>
	/// Launch the bullet coroutine, instantiate a bullet after a delay.
	/// </summary>
	/// <returns>The bullet coroutine.</returns>
	/// <param name="resolve">Resolve.</param>
	/// <param name="reject">Reject.</param>
	private IEnumerator SpawnEnemyCoroutine(GameObject prefab, string tag, Action resolve, Action<Exception> reject) {
		EnemyBehavior clone = (Instantiate (prefab) as GameObject).GetComponent<EnemyBehavior>();
		clone.transform.parent = transform;
		clone.tag = tag;

		bool spawnOnRight = forceSpawningOnRight;
		if (!spawnOnRight) {
			spawnOnRight = UnityEngine.Random.Range (0, 2) >= 1 ? true : false;
		}

		Vector3 spawnPosition;
		List<GameObject> targets;

		if (spawnOnRight) {
			//			spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(1.25f, Random.Range(0.0f, 1.0f), -Camera.main.transform.position.z));
			spawnPosition = rightTargets[UnityEngine.Random.Range(0, rightTargets.Count - 1)].transform.position;
			targets = tmp_leftTargets;
		}else{
			Vector3 rot = clone.transform.rotation.eulerAngles;
			rot = new Vector3(rot.x, rot.y + 180, rot.z);
			clone.transform.rotation = Quaternion.Euler(rot);

			//			spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(-0.25f, Random.Range(0.0f, 1.0f), -Camera.main.transform.position.z));
			spawnPosition = leftTargets[UnityEngine.Random.Range(0, leftTargets.Count - 1)].transform.position;
			targets = tmp_rightTargets;
		}

		clone.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0);
		int randomIndex = UnityEngine.Random.Range (0, targets.Count - 1);
		GameObject t = targets [randomIndex];
		clone.targetMovment = t;
		targets.Remove (t);

		yield return new WaitForSeconds(UnityEngine.Random.Range(MIN_SPAWN_FREQUENCY, MAX_SPAWN_FREQUENCY));
		resolve();
	}
		


	/// <summary>
	/// Kill a random enemy.
	/// </summary>
	void KillEnemy(string tag){
		Debug.Log ("KillEnemy " + tag);
		GameObject[] Enemies = GameObject.FindGameObjectsWithTag (tag);

		Destroy(Enemies[UnityEngine.Random.Range (0, Enemies.Length - 1)]);
	}
}
