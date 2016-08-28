using UnityEngine;
using System.Collections;

public class PatternManager : MonoBehaviour {
	// Pattern actuellement en jeu
	public GameObject[] PatternsList;
	private Vector3[] PatternSpawnPositions;
	public int currentLevel =0;
	public int spawnedEnemiesCount;

	void Start () {
		// Reference a La classe
		InstantiatePatternPrefabAndTarget (currentLevel);
	}


	// Fait apparaître le pattern suivant dans la liste
	public void InstantiatePatternPrefabAndTarget(int currentLevel){
		GameObject PatternToInstantiate = PatternsList [currentLevel].gameObject as GameObject;
		Instantiate (PatternToInstantiate,PatternToInstantiate.transform.position,Quaternion.identity);



		Debug.Log ("Instantiation du Prefab du pattern choisi réussie");

		// Maintenant il faut fair avancer leur enfant target

	}
		
	private void RandomPatternSelection(){

	}
		
}
