using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Privacy : MonoBehaviour {

	public GameObject Dialog;
	private void Awake()
	{
		if (PlayerPrefs.GetInt("PlayFirst") != 0)
		{
			Dialog.SetActive(false);
            if (PlayerPrefs.GetInt("PersonalAds") == 1)
            {
                ConsoliAds.Instance.initialize(true);
            }
            else {
                ConsoliAds.Instance.initialize(false);
            }


            Invoke("NextLevel", 5);
        }
	}

	public void No()
	{
		PlayerPrefs.SetInt("PlayFirst" , 1);
		PlayerPrefs.SetInt("PersonalAds", 0);
        ConsoliAds.Instance.initialize(false);
        Invoke("NextLevel",5);
	}
	public void PrivacyPolicyLink()
	{
		Application.OpenURL("http://lbzzztech.com/Privacy-Policy.html");
	}
	public void Ok()
	{
		PlayerPrefs.SetInt("PlayFirst", 1);
		PlayerPrefs.SetInt("PersonalAds", 1);
        ConsoliAds.Instance.initialize(true);
        Invoke("NextLevel", 5);

    }
	 void NextLevel()
	{
		SceneManager.LoadSceneAsync(1);
	}
}

