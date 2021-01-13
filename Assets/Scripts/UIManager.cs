using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;



public class UIManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject LevelSelection;
    public GameObject TutorialScreen;
    public GameObject GunsSelectionScreen;
    public GameObject LoadingScreen;
    public GameObject[] Locks;
    public GameObject GunObject;

    public GameObject[] guns;

    public Texture[] TexBody, TexTale;

    public Material Body, Tale;

    public GameObject BuyButton, GunPrice;
    public Text GunPriceText;
    public Text GunScopeText;
    public Text GunStabilityText;
    public Text PlayerCoinsText;

    public Sprite[] OtherGamesIcons;
    public string[] OtherGamesPackageNames;
    public Image MarketingButton;

    int gunCurrent = 0;

    public Text time;
    private DateTime TimeCounter;
    float TotalSeconds;
    // Use this for initialization
    void OnEnable()
    {
        //YasinConsoliAds.onRewardedVideoAdCompletedEvent += OnRewardedVideoComplpete;
    }

    void Start()
    {
      // PlayerPrefs.DeleteAll();
        GlobalVeriables.Instance.LoadFromFile();
        MainMenu.SetActive(true);
        LevelSelection.SetActive(false);
        LoadingScreen.SetActive(false);
        GunsSelectionScreen.SetActive(false);
        GunObject.SetActive(false);
//		GlobalVeriables.Instance.UnlockedLevels = 9;
//		GlobalVeriables.Instance.SaveToFile ();
        ////YasinConsoliAds.Instance.LoadInterstitial(0);
        //YasinConsoliAds.Instance.ShowInterstitial(0);
        GlobalVeriables.Instance.Gun1Locked = 1;
        GlobalVeriables.Instance.SaveToFile();

        if (PlayerPrefs.GetInt("MainmenuMarketingIndex") >= OtherGamesIcons.Length)
            PlayerPrefs.SetInt("MainmenuMarketingIndex", 0);

        if (!PlayerPrefs.HasKey("Has24Hour"))
        {
            TotalSeconds = 84600f;
            PlayerPrefs.SetInt("Has24Hour", (int)TotalSeconds);
            PlayerPrefs.SetString("DateTime", DateTime.Now.ToString());
        }
        TotalSeconds = (int)PlayerPrefs.GetInt("Has24Hour");
        TimeSpan tt;
        tt = DateTime.Now.Subtract(DateTime.Parse(PlayerPrefs.GetString("DateTime")));
        TotalSeconds = TotalSeconds - (float)tt.TotalSeconds;
        //YasinConsoliAds.Instance.LoadRewarded(1);
    }

    TimeSpan t;
    bool setunlock = false;

    void  FixedUpdate()
    {
        if (setunlock)
        {
            return;
        }
        TotalSeconds = TotalSeconds - Time.deltaTime;
        t = TimeSpan.FromSeconds(TotalSeconds);

        string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s", 
                            t.Hours, 
                            t.Minutes, 
                            t.Seconds);
        if (TotalSeconds <= 0)
        {
            time.color = new Color32(0, 0, 0, 0);
        }
        time.text = "Get Unlock Levels in = " + answer;
        if (TotalSeconds <= 0.0f && !setunlock)
        {
            setunlock = true;

            GlobalVeriables.Instance.UnlockedLevels = 9;
            GlobalVeriables.Instance.SaveToFile();
        }

    }

    public void OtherGames()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + OtherGamesPackageNames[PlayerPrefs.GetInt("MainmenuMarketingIndex") - 1]);
    }
    //	// Update is called once per frame
    //	void Update () {
    //
    //	}
    public void ShowMainMenu()
    {
        MainMenu.SetActive(true);
        LevelSelection.SetActive(false);
        GunsSelectionScreen.SetActive(false);
    }

    public void HideMainMenu()
    {
        MainMenu.SetActive(false);
    }

    public void ShowTutorial()
    {
        TutorialScreen.SetActive(true);
        MainMenu.SetActive(false);
        GunsSelectionScreen.SetActive(false);
    }

    public void HideTutorial()
    {
        TutorialScreen.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void ShowLevelSelection()
    {
        LevelSelection.SetActive(true);
        GunsSelectionScreen.SetActive(false);
        GunObject.SetActive(false);
        for (int i = 0; i <= GlobalVeriables.Instance.UnlockedLevels; i++)
        {
            Locks[i].SetActive(false);
        }
    }

    public void HidLevelSelection()
    {
        LevelSelection.SetActive(false);
    }

    public void PlayLevel(int level)
    {

        if (level <= GlobalVeriables.Instance.UnlockedLevels)
        {
            GlobalVeriables.Instance.CurrentLevel = level;
            ShowGunSelection();
        }
    }

    public void RateUS()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.lethal.jet.fighters.combat.war.planes");
    }

    public void MoreFun()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Lethal+Action+Games");
    }

    public void ShowGunSelection()
    {
        GunObject.SetActive(true);
        GunsSelectionScreen.SetActive(true);
        LevelSelection.SetActive(false);
        MainMenu.SetActive(false);
//		foreach (var item in guns) {
//			item.SetActive (false);
//		}
        PlayerCoinsText.text = "" + GlobalVeriables.Instance.PlayerCoins;
        gunCurrent = 0;
        //guns [gunCurrent].SetActive (true);
        Body.mainTexture = TexBody[gunCurrent];
        Tale.mainTexture = TexTale[gunCurrent];
        GunPriceText.text = "" + GlobalVeriables.Instance.GunPrice[gunCurrent];
        GunScopeText.text = "" + GlobalVeriables.Instance.GunScope[gunCurrent];
        GunStabilityText.text = "" + GlobalVeriables.Instance.GunStability[gunCurrent];
        if (gunCurrent == 0 && GlobalVeriables.Instance.Gun1Locked != 0)
        {
            BuyButton.SetActive(false);
            GunPrice.SetActive(false);
        } 
        ////YasinConsoliAds.Instance.LoadInterstitial (2);
        //YasinConsoliAds.Instance.ShowInterstitial(1);
    }

    public void GunLeft()
    {


        if (gunCurrent > 0)
        {
//			foreach (var item in guns) {
//				item.SetActive (false);
//			}
            gunCurrent--;
            //	guns [gunCurrent].SetActive (true);
            Body.mainTexture = TexBody[gunCurrent];
            Tale.mainTexture = TexTale[gunCurrent];
            GunPriceText.text = "" + GlobalVeriables.Instance.GunPrice[gunCurrent];
            GunScopeText.text = "" + GlobalVeriables.Instance.GunScope[gunCurrent];
            GunStabilityText.text = "" + GlobalVeriables.Instance.GunStability[gunCurrent];

            if (gunCurrent == 0 && GlobalVeriables.Instance.Gun1Locked == 0)
            {
                BuyButton.SetActive(true);
                GunPrice.SetActive(true);
                return;
            }
            else if (gunCurrent == 0 && GlobalVeriables.Instance.Gun1Locked != 0)
            {
                BuyButton.SetActive(false);
                GunPrice.SetActive(false);

            }
            if (gunCurrent == 1 && GlobalVeriables.Instance.Gun2Locked == 0)
            {
                BuyButton.SetActive(true);
                GunPrice.SetActive(true);

            }
            else if (gunCurrent == 1 && GlobalVeriables.Instance.Gun2Locked != 0)
            {
                BuyButton.SetActive(false);
                GunPrice.SetActive(false);

            }
            if (gunCurrent == 2 && GlobalVeriables.Instance.Gun3Locked == 0)
            {
                BuyButton.SetActive(true);
                GunPrice.SetActive(true);

            }
            else if (gunCurrent == 2 && GlobalVeriables.Instance.Gun3Locked != 0)
            {
                BuyButton.SetActive(false);
                GunPrice.SetActive(false);

            }

        }
    }

    public void GunRight()
    {
		
        if (gunCurrent < guns.Length - 1)
        {
////			foreach (var item in guns) {
////				item.SetActive (false);
////			}
            gunCurrent++;
            //guns [gunCurrent].SetActive (true);
            Body.mainTexture = TexBody[gunCurrent];
            Tale.mainTexture = TexTale[gunCurrent];
            GunPriceText.text = "" + GlobalVeriables.Instance.GunPrice[gunCurrent];
            GunScopeText.text = "" + GlobalVeriables.Instance.GunScope[gunCurrent];
            GunStabilityText.text = "" + GlobalVeriables.Instance.GunStability[gunCurrent];

            if (gunCurrent == 0 && GlobalVeriables.Instance.Gun1Locked == 0)
            {
                BuyButton.SetActive(true);
                GunPrice.SetActive(true);
                return;
            }
            else if (gunCurrent == 0 && GlobalVeriables.Instance.Gun1Locked != 0)
            {
                BuyButton.SetActive(false);
                GunPrice.SetActive(false);

            }
            if (gunCurrent == 1 && GlobalVeriables.Instance.Gun2Locked == 0)
            {
                BuyButton.SetActive(true);
                GunPrice.SetActive(true);

            }
            else if (gunCurrent == 1 && GlobalVeriables.Instance.Gun2Locked != 0)
            {
                BuyButton.SetActive(false);
                GunPrice.SetActive(false);

            }
            if (gunCurrent == 2 && GlobalVeriables.Instance.Gun3Locked == 0)
            {
                BuyButton.SetActive(true);
                GunPrice.SetActive(true);

            }
            else if (gunCurrent == 2 && GlobalVeriables.Instance.Gun3Locked != 0)
            {
                BuyButton.SetActive(false);
                GunPrice.SetActive(false);
			
            }
        }
    }

    public void BuyGun()
    {
        if (gunCurrent == 1 && GlobalVeriables.Instance.PlayerCoins >= GlobalVeriables.Instance.GunPrice[gunCurrent])
        {
            GlobalVeriables.Instance.Gun2Locked = 1;
            GlobalVeriables.Instance.PlayerCoins = GlobalVeriables.Instance.PlayerCoins - GlobalVeriables.Instance.GunPrice[gunCurrent];
            GunPrice.SetActive(false);
            BuyButton.SetActive(false);
            PlayerCoinsText.text = GlobalVeriables.Instance.PlayerCoins + "";
            GlobalVeriables.Instance.SaveToFile();
        }
        else if (gunCurrent == 2 && GlobalVeriables.Instance.PlayerCoins >= GlobalVeriables.Instance.GunPrice[gunCurrent])
        {
            GlobalVeriables.Instance.Gun3Locked = 1;
            GlobalVeriables.Instance.PlayerCoins = GlobalVeriables.Instance.PlayerCoins - GlobalVeriables.Instance.GunPrice[gunCurrent];
            GunPrice.SetActive(false);
            BuyButton.SetActive(false);
            PlayerCoinsText.text = GlobalVeriables.Instance.PlayerCoins + "";
            GlobalVeriables.Instance.SaveToFile();
        }
    }

    public void Play()
    {
        GunObject.SetActive(false);
        GunsSelectionScreen.SetActive(false);
        GlobalVeriables.Instance.CurrentGun = gunCurrent;
        LoadingScreen.SetActive(true);
        StartCoroutine(LoadingScreen.GetComponent<LoadingBar>().LevelCoroutine("GameScene"));
    }

    public void MoreCoins()
    {
		
        //YasinConsoliAds.Instance.ShowRewardedVideo(1);
    }

    void OnRewardedVideoComplpete()
    {
        GlobalVeriables.Instance.PlayerCoins = GlobalVeriables.Instance.PlayerCoins + 200;
        GlobalVeriables.Instance.SaveToFile();
        PlayerCoinsText.text = GlobalVeriables.Instance.PlayerCoins + "";


    }

    void OnDisable()
    {
        //YasinConsoliAds.onRewardedVideoAdCompletedEvent -= OnRewardedVideoComplpete;

    }
}
