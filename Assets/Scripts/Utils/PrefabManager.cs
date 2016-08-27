using UnityEngine;
using System.Collections;

public class PrefabManager : MonoBehaviour {

	public static PrefabManager instance = null;

	public GameObject letter;

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

}
