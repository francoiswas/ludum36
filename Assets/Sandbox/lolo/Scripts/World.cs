using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class World : MonoBehaviour {
	public static World instance = null;

	int score;

	public Text textScore;
	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GameOver()
	{
		score = 0;
		updateScore ();
		Ennemy[] ennemies=GameObject.FindObjectsOfType<Ennemy> ();
		for (int i = 0; i < ennemies.Length; i++) {
			Destroy (ennemies [i].gameObject);
		}
		Coin[] coins=GameObject.FindObjectsOfType<Coin> ();
		for (int i = 0; i < coins.Length; i++) {
			Destroy (coins [i].gameObject);
		}
	}


	public void AddScore()
	{
		score++;
		updateScore ();
	}

	void updateScore()
	{
		textScore.text = score.ToString ();
	}
}
