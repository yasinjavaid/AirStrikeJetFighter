using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingTextScript : MonoBehaviour {
	int TotalCharactersInDescription;
	public string DialogText;
	public GameObject SkipButton;
	int WritertedCharacter = 0;
	// Use this for initialization
	void OnEnable () {
		DialogText = GlobalVeriables.Instance.MissionObjective[GlobalVeriables.Instance.CurrentLevel];
		TotalCharactersInDescription = DialogText.Length;//DialogText.Length;
		InvokeRepeating("TypeWriter",.01f,.01f);
		//		GameManager.IsFirstClickOnScreen = true;
		gameObject.GetComponent<Text>().text = "";
	}


	public void TypeWriter()
	{

		gameObject.GetComponent<Text>().text = string.Concat(gameObject.GetComponent<Text>().text, DialogText[WritertedCharacter]);
		WritertedCharacter++;
		if (WritertedCharacter == TotalCharactersInDescription)
		{
			CancelInvoke("TypeWriter");
			SkipButton.SetActive (true);
			Time.timeScale = 0;
			//			GameManager.IsFirstClickOnScreen = false;

			//			if (LevelManager.Instance.currentLevel.mission == MissionTypes.EliteTraining && GameObject.FindGameObjectWithTag("Enemy") != null)
			//				GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyLife>().FireMissileInEliteTraining();
		}
	}
}
