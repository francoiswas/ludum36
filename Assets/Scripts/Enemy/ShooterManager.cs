using UnityEngine;
using System.Collections;
using System.Reflection;

public class ShooterManager : MonoBehaviour {

	public GameObject P_Enemy;
	public GameObject P_DCA;
	[Range(0,50)]
	public int NB_ENEMIES_CLASSIC;
	[Range(0,50)]
	public int NB_ENEMIES_DCA;

	private float camHeight;
	private float camWidth;

	private string ClassicTag;
	private string DcaTag;


	// Use this for initialization
	void Start () {
		Camera cam = Camera.main;
		camHeight = 2f * cam.orthographicSize;
		camWidth = camHeight * cam.aspect;

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
			SpawnEnemy(prefab, camHeight, camWidth, tag);
		}
	}


	/// <summary>
	/// Spawn an enemy around a square.
	/// </summary>
	/// <param name="maxVertical">Max vertical.</param>
	/// <param name="maxHorizontal">Max horizontal.</param>
	void SpawnEnemy(GameObject prefab, float maxVertical, float maxHorizontal, string tag) {
		GameObject clone = Instantiate (prefab) as GameObject;
		clone.transform.parent = transform;
		clone.tag = tag;

		bool xFixed = Random.Range (0, 2) >= 1 ? true : false;

		float[] xFixedPossibilities = new float[] { -camWidth / 2f, camWidth / 2f};
		float[] yFixedPossibilities = new float[] { -camHeight / 2f, camHeight / 2f};
		Vector2 spawnPosition;

		if (xFixed) {
			// use random on Y axis
			spawnPosition = new Vector2(
				xFixedPossibilities[Random.Range( 0, xFixedPossibilities.Length)],
				Random.Range(-camHeight / 2f, camHeight / 2f)
			);
		}else{
			// use random on X axis
			spawnPosition = new Vector2(
				Random.Range(-camWidth / 2f, camWidth / 2f),
				yFixedPossibilities[Random.Range( 0, yFixedPossibilities.Length)]
			);
		}

		clone.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0);
	}


	/// <summary>
	/// Watch a specified instance and spawn or kill them.
	/// </summary>
	/// <param name="prefab">Prefab.</param>
	/// <param name="nbInstance">Nb instance.</param>
	public void WatchInstance(GameObject prefab, int nbInstance, string tag){
		int currentNbPrefab = GameObject.FindGameObjectsWithTag (tag).Length;

		if (currentNbPrefab < nbInstance) {
			SpawnEnemy(prefab, camHeight, camWidth, tag);
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
