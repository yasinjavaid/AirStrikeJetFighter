using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GlobalVeriables : Singleton<GlobalVeriables> {
	public int UnlockedLevels;
	public int CurrentLevel = 0;
	public int PlayerCoins = 0;
	public List<int> LevelReward = new List<int> {1000,1300,1500,1700,2000,2500,3000,4000,5000,6000};
	public int Gun1Locked = 0;
	public int Gun2Locked = 0;
	public int Gun3Locked = 0;

	#region Gun values
	public List<int> GunPrice = new List<int> {0,4000,7000};
	public List<string> GunScope = new List<string> {"50%","72%","86%"};
	public List<string> GunStability = new List<string> {"70%","85%","98%"};
	public const int RifflePrice1 = 4000;
	public const int RifflePrice2 = 7000;

	#endregion


	public int EnemiesDown;
	public List<int> Enemies = new List<int> {2,3,3,3,3,3,3,4,4,4};
	public List<int> Guns = new List<int> {4,4,5,5,6,6,6,6,7,7};
	public List<int> LevelsTime = new List<int> {100,120,150,150,120,120,150,100,150,140};
	public List<int> LevelsAmmo = new List<int> {90,120,210,210,240,270,270,240,180,180};
	public float TimeCounter = 0.0f;
	public GameObject[] LevelsDestination;
	public int bullets = 6;
	public int CurrentGun = 0;

	#region Switches
	public bool IsLevelComplete = false;
	public bool IsGamePause = false;
	public bool IsLevelFail = false;

	#endregion

	#region consts
	#endregion
	// Use this for initialization
	void Start () {
		
	}
	public void LoadFromFile(){
		//UnlockedLevels = PreviewLabs.PlayerPrefs.GetInt ("UnlockedLevels",UnlockedLevels);
		UnlockedLevels = PlayerPrefs.GetInt ("UnlockedLevels");
		PlayerCoins = PlayerPrefs.GetInt ("PlayerCoins");
		Gun1Locked = PlayerPrefs.GetInt ("Gun1Locked");
		Gun2Locked = PlayerPrefs.GetInt ("Gun2Locked");
		Gun3Locked = PlayerPrefs.GetInt ("Gun3Locked");
	}
	public void SaveToFile(){
		//PreviewLabs.PlayerPrefs.SetInt ("UnlockedLevels",UnlockedLevels);
		PlayerPrefs.SetInt ("UnlockedLevels",UnlockedLevels);
		PlayerPrefs.SetInt ("PlayerCoins",PlayerCoins);
		PlayerPrefs.SetInt ("Gun1Locked",Gun1Locked);
		PlayerPrefs.SetInt ("Gun2Locked",Gun2Locked);
		PlayerPrefs.SetInt ("Gun3Locked",Gun3Locked);
	}


	#region Objectives
	public List<string> MissionObjective = new List<string>{ 
		"Welcome back Chief! \n Kill the agent in white shirt...",
		"Chief your next mission is to kill two secret service agents that are planning to assassin USA minister shoot them before they got away..",
		"Chief you are going good , Now shoot first man agent and then shoot women agent that are on live post of secret building. ",
		"Enemies are looking for our agent on road side kill them...",
		"Now enemies going to fly away shoot them all, but remember kill the pilot first...",
		"Hello Chief! \nTime to see your shooting skills kill secret service agents standing near black SUV and one on top of that building.",
		"Listen Chief! \nThree agents are planning, kill them on the spot... ",
		"Chief ! \nagents and cops planning to plant in the city shoot them before they take shelter of truck. ",
		"Chief ! \nEnemies Hijacked the police chooper and they are few minutes to escape kill them all '\\n' \nAlert! shoot the pilot first.",
		"Chief ! \nShoot the enemies they are going to assassin a agent in a bus kill them all. "
	};


	public List<string> LevelCompleteObjective = new List<string>{ 
		"Good shoot agent is expired you are clear to proced... ",
		"Good kill you just save USA minister... ",
		"weldone! you cleared the post...",
		"Good news our agent is save to move. ",
		"Good shoot you kill them all. ",
		"Good kill... \nGood kill... \nGood kill... ",
		"your are getting better and better.",
		"The General is immpressed. ",
		"Good you just save chooper from enemies. ",
		"you saved the agent and whole city your work is appriated..."
	};


	public List<string> LevelFailObjective = new List<string>{ 
		"Oh!  \nChief your target run to save house...",
		"You have to show good skills that you have, secret service agents managed to get escape... ",
		"You just missed it he escaped in the secret service builfing...",
		"He found shelter, improve your shooting skills... ",
		"Oh no pilot press the alarm button. ",
		"Agent managed to get in the car. . . shoot him first. try again.",
		"They are managed to get in the van and escape away...",
		"Oh! you are not going good they ran away and take shelter of truck...",
		"They escape. ",
		"they just killed our agent."
	};
	#endregion
}
