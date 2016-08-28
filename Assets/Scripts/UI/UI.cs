using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UI : MonoBehaviour {

	public Image  	imageBackground;
	public Text 	textStory;

	public static UI instance = null;

	// Use this for initialization
	void Start () {
		StartGame ();
	}

	void StartGame()
	{
		textStory.text = "The fate of humanity is in your hand...";
		LeanTween.value(gameObject,1,0,2f).setOnUpdate (
			(float val) => {

				imageBackground.color=new Color(imageBackground.color.r,imageBackground.color.g,
					imageBackground.color.b,val);
			}
		);
		LeanTween.moveX (textStory.gameObject, -400, 1f).setEase (LeanTweenType.easeInOutBack).setDelay (2f);
	}

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

	public void GameOver()
	{
		textStory.text = "The world is doomed...";
		LeanTween.value (gameObject, 0, 1, 2f).setOnUpdate (
			(float val) => {

				imageBackground.color = new Color (imageBackground.color.r, imageBackground.color.g,
					imageBackground.color.b, val);
			}
		)
		;
		LeanTween.moveX (textStory.gameObject, 500, 1f).setEase (LeanTweenType.easeInOutBack).setDelay (2f).setOnComplete (() => {
			Application.LoadLevel(Application.loadedLevel);
		});

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
