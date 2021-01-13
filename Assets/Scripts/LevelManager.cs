using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Text sec;
    public GameObject Player;
    public Texture TexBodyA, TexTaleA;
    public Texture TexBodyB, TexTaleB;
    public Texture TexBodyC, TexTaleC;
    public Material Body, Tale;

    //public GameObject[] LevelPickUps;

    //	public GameObject ControlFreakRef;
    //	public GameObject MiniMap;

    [Header("UI HUD")]
    public GameObject Hud;
    public Image PlayerHealthFill;
    public Text EnemyCounter;



    [Header("UI Dialogue")]
    public GameObject Dialogue;
    public Text ObjectiveText;
    public Text PlayerCoinsText;
    public GameObject PauseText;
    public GameObject CompleteText;
    public Text LevelRewards;
    public GameObject FailText;
    public GameObject MenuButton;
    public GameObject ResumeButton;
    public GameObject NextButton;
    public GameObject RetryButton;
    public GameObject Objective;
    public GameObject LoadingBar;
    bool isOutOfAmmp = false;

    // Use this for initialization
    void OnEnable()
    {
        //play.GetComponent<DamageManager> ().HP
//		foreach (var item in LevelsDestinations) {
//			item.SetActive (false);
//		}
//		LevelsDestinations[GlobalVeriables.Instance.CurrentLevel].SetActive(true);
//		Player.transform.position =LevelPlayerPoints[GlobalVeriables.Instance.CurrentLevel].transform.position;
//		Player.transform.rotation =LevelPlayerPoints[GlobalVeriables.Instance.CurrentLevel].transform.rotation;
//		GlobalVeriables.Instance.LevelsDestination = LevelsDestinations;

        PlayerDead.OnPlayerDie += PlayerDie;
        EnemyDead.OnEnemyDie += OnEnemyDown; 
        DamageManager.OnJetDamage += OnPlayerHit;
        //	Gun.OnShoot += ShowBullets;
        //YasinConsoliAds.onRewardedVideoAdCompletedEvent += OnRewardedVideoComplpete;
    }

    void Start()
    {

        LoadingBar.SetActive(false);
        GlobalVeriables.Instance.EnemiesDown = 0;
        GlobalVeriables.Instance.bullets = 6;
        Dialogue.SetActive(false);
        isOutOfAmmp = false;
        GlobalVeriables.Instance.IsGamePause = false;
        GlobalVeriables.Instance.IsLevelFail = false;
        GlobalVeriables.Instance.IsLevelComplete = false;
        GlobalVeriables.Instance.TimeCounter = GlobalVeriables.Instance.LevelsTime[GlobalVeriables.Instance.CurrentLevel];
//		foreach (var item in LevelPickUps) {
//			item.SetActive (false);
//		}
//		foreach (var item in LevelEnemies) {
//			item.SetActive (false);
//		}
        //	LevelPickUps [GlobalVeriables.Instance.CurrentLevel].SetActive (true);
        //LevelEnemies [GlobalVeriables.Instance.CurrentLevel].SetActive (true);
        GlobalVeriables.Instance.CurrentGun = 0;
        if (GlobalVeriables.Instance.CurrentGun == 0)
        {
            Body.mainTexture = TexBodyA;
            Tale.mainTexture = TexTaleA;
        }
        else if (GlobalVeriables.Instance.CurrentGun == 1)
        {
            Body.mainTexture = TexBodyB;
            Tale.mainTexture = TexTaleB;
        }
        else if (GlobalVeriables.Instance.CurrentGun == 2)
        {
            Body.mainTexture = TexBodyC;
            Tale.mainTexture = TexTaleC;
        }
        LoadingBar.SetActive(false);
        EnemyCounter.text = GlobalVeriables.Instance.EnemiesDown + "/" + GlobalVeriables.Instance.Enemies[GlobalVeriables.Instance.CurrentLevel];
        //ShowObjective ();
        //YasinConsoliAds.Instance.LoadRewarded(4);
    }

    void ShowObjective()
    {
        Objective.SetActive(true);
        Hud.SetActive(false);
        //	MiniMap.SetActive (false);
        //	ControlFreakRef.SetActive (false);
        //Time.timeScale = 0.0f;
//		if (Player.GetComponent<FPSInputControllerMobile> ()) {
//			Player.GetComponent<FPSInputControllerMobile> ().enabled = false;
//			Player.GetComponent<GunHanddle> ().Guns [GlobalVeriables.Instance.CurrentGun].gameObject.GetComponent<GunMecanim> ().enabled =false;
//		}

    }

    public void HideObjective()
    {
        Objective.SetActive(false);
        Hud.SetActive(true);
        //	MiniMap.SetActive (true);
        //Invoke ("ActiveCF2",0.5f);
//		if (Player.GetComponent<FPSInputControllerMobile> ()) {
//			Player.GetComponent<FPSInputControllerMobile> ().enabled = true;
//			Player.GetComponent<GunHanddle> ().Guns [GlobalVeriables.Instance.CurrentGun].gameObject.GetComponent<GunMecanim> ().enabled =true;
//		}
        Time.timeScale = 1.0f;
    }
    //	// Update is called once per frame
    void Update()
    {
        if (!GlobalVeriables.Instance.IsGamePause && !GlobalVeriables.Instance.IsLevelComplete
        && GlobalVeriables.Instance.TimeCounter >= 0.0f)
        {
            GlobalVeriables.Instance.TimeCounter -= Time.deltaTime;
            //	sec.text = (int)GlobalVeriables.Instance.TimeCounter + "";
        }
//		if (!GlobalVeriables.Instance.IsGamePause && !GlobalVeriables.Instance.IsLevelFail
//			&&GlobalVeriables.Instance.TimeCounter <= 0.0f) {
//			LevelFail ();
//		}


    }

    void LateUpdate()
    {
        if (GlobalVeriables.Instance.IsLevelFail || GlobalVeriables.Instance.IsLevelFail)
        {
           // Time.timeScale = 0;
        }
        //	TotalBullets.text = "";//+ PlayerWeapon.CurrentWeaponBehaviorComponent.ammo;
    }

    public void ShowBullets(int ammo)
    {
		
    }

    public void OnPlayerHit(int hp)
    {
        PlayerHealthFill.fillAmount = hp / 100;
    }

    public void OnEnemyDown()
    {
        GlobalVeriables.Instance.TimeCounter += 20;
        GlobalVeriables.Instance.EnemiesDown++;
        EnemyCounter.text = GlobalVeriables.Instance.EnemiesDown + "/" + GlobalVeriables.Instance.Enemies[GlobalVeriables.Instance.CurrentLevel];
        if (GlobalVeriables.Instance.EnemiesDown == GlobalVeriables.Instance.Enemies[GlobalVeriables.Instance.CurrentLevel])
        {
            Invoke("LevelComplete", 1f);//LevelComplete ();
        }
    }

    public void LevelComplete()
    {
        if (GlobalVeriables.Instance.CurrentLevel == GlobalVeriables.Instance.UnlockedLevels && GlobalVeriables.Instance.UnlockedLevels < 10)
        {
            GlobalVeriables.Instance.UnlockedLevels = GlobalVeriables.Instance.UnlockedLevels + 1;
            GlobalVeriables.Instance.SaveToFile();
        }
        GlobalVeriables.Instance.PlayerCoins = GlobalVeriables.Instance.PlayerCoins + GlobalVeriables.Instance.LevelReward[GlobalVeriables.Instance.CurrentLevel];
        GlobalVeriables.Instance.SaveToFile();
        Time.timeScale = 0; 
        //	ControlFreakRef.SetActive (false);
        Hud.SetActive(false);
        //	MiniMap.SetActive (false);
        ShowDialogue(2);
        Debug.Log("LevelComplete");
    }

    public void GamePause()
    {
        if (!GlobalVeriables.Instance.IsLevelComplete)
        {
            GlobalVeriables.Instance.IsGamePause = true;
            Time.timeScale = 0;
            //	ControlFreakRef.SetActive (false);
            Hud.SetActive(false);
            //	MiniMap.SetActive (false);
            ShowDialogue(1);
        }
    }

    public void GameResume()
    {
        if (GlobalVeriables.Instance.IsGamePause)
        {
//			if (Player.GetComponent<FPSInputControllerMobile> ()) {
//				Player.GetComponent<FPSInputControllerMobile> ().enabled = true;
//				Player.GetComponent<GunHanddle> ().Guns [GlobalVeriables.Instance.CurrentGun].gameObject.GetComponent<GunMecanim> ().enabled =true;
//			}
            GlobalVeriables.Instance.IsGamePause = false;
            Dialogue.SetActive(false);
            Time.timeScale = 1;
            Hud.SetActive(true);
            //	MiniMap.SetActive (true);
            Invoke("ActiveCF2", 0.5f);
        }
    }

    void ActiveCF2()
    {
        //	ControlFreakRef.SetActive (true);
    }

    public void PlayerDie()
    {
        Invoke("LevelFail", 0.5f);
    }

    public void LevelFail()
    {
        GlobalVeriables.Instance.IsLevelFail = true;
        Time.timeScale = 0.0f;
        Debug.Log("Levelfail");
        //	ControlFreakRef.SetActive (false);
        Hud.SetActive(false);
        //	MiniMap.SetActive (false);
        ShowDialogue(3);
    }

    public void ShowMenu()
    {

        Time.timeScale = 1.0f;
        LoadingBar.SetActive(true);
        StartCoroutine(LoadingBar.GetComponent<LoadingBar>().LevelCoroutine("Menu"));

    }

    public void Restart()
    {
        LoadingBar.SetActive(true);
        StartCoroutine(LoadingBar.GetComponent<LoadingBar>().LevelCoroutine("GameScene"));
	
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        if (GlobalVeriables.Instance.CurrentLevel < 10)
        {
            GlobalVeriables.Instance.CurrentLevel = GlobalVeriables.Instance.CurrentLevel + 1;
            LoadingBar.SetActive(true);
            StartCoroutine(LoadingBar.GetComponent<LoadingBar>().LevelCoroutine("GameScene"));
        }
    }

    public void ShowDialogue(int show)
    {
        LevelRewards.gameObject.SetActive(true);
        PlayerCoinsText.gameObject.SetActive(true);
        LevelRewards.text = "" + GlobalVeriables.Instance.LevelReward[GlobalVeriables.Instance.CurrentLevel];
        PlayerCoinsText.text = "Total Coins: " + GlobalVeriables.Instance.PlayerCoins + "";
//		if (Player.GetComponent<FPSInputControllerMobile> ()) {
//			Player.GetComponent<FPSInputControllerMobile> ().enabled = false;
//			Player.GetComponent<GunHanddle> ().Guns [GlobalVeriables.Instance.CurrentGun].gameObject.GetComponent<GunMecanim> ().enabled =false;
//		}
        Dialogue.SetActive(true);
        if (show == 1)
        { //Pause
            LevelRewards.gameObject.SetActive(false);
            PlayerCoinsText.gameObject.SetActive(false);
            PauseText.SetActive(true);
            CompleteText.SetActive(false);
            FailText.SetActive(false);
            ResumeButton.SetActive(true);
            MenuButton.SetActive(true);
            RetryButton.SetActive(true);
            NextButton.SetActive(false);

//			ObjectiveText.text = GlobalVeriables.Instance.MissionObjective [GlobalVeriables.Instance.CurrentLevel];


//			//YasinConsoliAds.Instance.LoadInterstitial(3);
            //YasinConsoliAds.Instance.ShowInterstitial(2);

        }
        else if (show == 2)
        { //Complete
            PauseText.SetActive(false);
            CompleteText.SetActive(true);
            FailText.SetActive(false);
            ResumeButton.SetActive(false);
            MenuButton.SetActive(true);
            RetryButton.SetActive(true);
            if (GlobalVeriables.Instance.CurrentLevel >= 9)
                NextButton.SetActive(false);
            ObjectiveText.text = GlobalVeriables.Instance.LevelCompleteObjective[GlobalVeriables.Instance.CurrentLevel];
            //	//YasinConsoliAds.Instance.LoadInterstitial(4);
            //YasinConsoliAds.Instance.ShowInterstitial(3);

        }
        else if (show == 3)
        { //fail
            PauseText.SetActive(false);
            CompleteText.SetActive(false);
            FailText.SetActive(true);
            ResumeButton.SetActive(false);
            MenuButton.SetActive(true);
            RetryButton.SetActive(true);
            NextButton.SetActive(false);
            if (isOutOfAmmp)
            {
                isOutOfAmmp = false;
                //	ObjectiveText.text = "You are out of ammo...";
            }
            else
            {
                //	ObjectiveText.text = GlobalVeriables.Instance.LevelFailObjective [GlobalVeriables.Instance.CurrentLevel];
            }

            //YasinConsoliAds.Instance.ShowInterstitial(4);

        }
    }

    public void SkipLevel()
    {
        //YasinConsoliAds.Instance.ShowRewardedVideo(4);
    }

    void OnRewardedVideoComplpete()
    {
        GlobalVeriables.Instance.CurrentLevel = GlobalVeriables.Instance.CurrentLevel + 1;
        LoadingBar.SetActive(true);
        //YasinConsoliAds.Instance.LoadRewarded(4);
        StartCoroutine(LoadingBar.GetComponent<LoadingBar>().LevelCoroutine("GameScene"));

    }

    void OnDisable()
    {
        PlayerDead.OnPlayerDie -= PlayerDie;
        EnemyDead.OnEnemyDie -= OnEnemyDown;
        DamageManager.OnJetDamage -= OnPlayerHit;
        //YasinConsoliAds.onRewardedVideoAdCompletedEvent -= OnRewardedVideoComplpete;
//
    }
}
