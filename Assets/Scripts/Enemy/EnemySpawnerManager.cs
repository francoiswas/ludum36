using UnityEngine;
using System.Collections;
using System.Reflection;

public class EnemySpawnerManager : MonoBehaviour {

	public GameObject P_Enemy;
	public GameObject P_DCA;
	[Range(0,50)]
	public int NB_ENEMIES_CLASSIC;
	[Range(0,0)]
	public int NB_ENEMIES_DCA;

	public bool forceSpawningOnRight;

	private string ClassicTag;
	private string DcaTag;


	// Use this for initialization
	void Start () {
		Camera cam = Camera.main;

		Vector3 test = cam.ViewportToWorldPoint(new Vector3(1, 1, -cam.transform.position.z));

		ClassicTag = "EnemyClassic";
		DcaTag = "EnemyDca";

		SpawnEnemies (P_Enemy, NB_ENEMIES_CLASSIC, ClassicTag);
		SpawnEnemies (P_DCA, NB_ENEMIES_DCA, DcaTag);
	}


	// Update is called once per frame
	void Update () {
		WatchInstance (P_Enemy, NB_ENEMIES_CLASSIC, ClassicTag);
		WatchInstance (P_DCA, NB_ENEMIES_DCA, DcaTag);
	}


	/// <summary>
	/// Spawn several enemies.
	/// </summary>
	/// <param name="prefab">Prefab.</param>
	/// <param name="nbInstance">Nb instance.</param>
	public void SpawnEnemies (GameObject prefab, int nbInstance, string tag) {
		for(int i = 0; i < nbInstance; i++){
			SpawnEnemy(prefab, tag);
		}
	}


	/// <summary>
	/// Spawn an enemy around a square.
	/// </summary>
	/// <param name="maxVertical">Max vertical.</param>
	/// <param name="maxHorizontal">Max horizontal.</param>
	void SpawnEnemy(GameObject prefab, string tag) {
		EnemyBehavior clone = (Instantiate (prefab) as GameObject).GetComponent<EnemyBehavior>();
		clone.transform.parent = transform;
		clone.tag = tag;

		bool spawnOnRight = forceSpawningOnRight;
		if (!spawnOnRight) {
			spawnOnRight = Random.Range (0, 2) >= 1 ? true : false;
		}

		Vector3 spawnPosition;
		GameObject[] targets;

		if (spawnOnRight) {
			spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(1.25f, Random.Range(0.0f, 1.0f), -Camera.main.transform.position.z));

			// Pickup a random left target and assign it to the clone.
			targets = GameObject.FindGameObjectsWithTag ("TargetLeft");
		}else{
			Vector3 rot = clone.transform.rotation.eulerAngles;
			rot = new Vector3(rot.x, rot.y + 180, rot.z);
			clone.transform.rotation = Quaternion.Euler(rot);

			spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(-0.25f, Random.Range(0.0f, 1.0f), -Camera.main.transform.position.z));
			targets = GameObject.FindGameObjectsWithTag ("TargetRight");
		}

		clone.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0);
		clone.targetMovment = targets[Random.Range (0, targets.Length - 1)];
	}


	/// <summary>
	/// Watch a specified instance and spawn or kill them.
	/// </summary>
	/// <param name="prefab">Prefab.</param>
	/// <param name="nbInstance">Nb instance.</param>
	public void WatchInstance(GameObject prefab, int nbInstance, string tag){
		int currentNbPrefab = GameObject.FindGameObjectsWithTag (tag).Length;

		if (currentNbPrefab < nbInstance) {
			SpawnEnemy(prefab, tag);
		} else if (currentNbPrefab > nbInstance) {
			KillEnemy (tag);
		}
	}


	/// <summary>
	/// Kill a random enemy.
	/// </summary>
	void KillEnemy(string tag){
		GameObject[] Enemies = GameObject.FindGameObjectsWithTag (tag);

		Destroy(Enemies[Random.Range (0, Enemies.Length - 1)]);
	}
}
