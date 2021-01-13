using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserConsentManager : MonoBehaviour
{
    public GameObject TermsPanel;
    public GameObject PrivacyButton, Background;
    public string admobTestDeviceID;

    bool userConsent = false;

    // Use this for initialization
    void Start()
    {
        //YasinConsoliAds.Instance.addAdmobTestDevice(admobTestDeviceID);

        //If First Scene Show PrivacyPolicy/ConsentScreen
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (PlayerPrefs.GetInt("TermsShown", 0) == 0)
            {
                TermsPanel.SetActive(true);
            }
            else
            {
                if (PlayerPrefs.GetInt("userConsent") == 0)
                {
                    userConsent = false;
                }
                else
                {
                    userConsent = true;
                }

                Debug.Log("***** Setting User Consent For Consoli: " + userConsent.ToString() + " *****");
                //Set User Consent
                //YasinConsoliAds.Instance.initialize(userConsent);
                //Load Next Scene 
                Invoke("LoadNextScene", 2.0f);
            }
        }
        //If AnyOther Scene show Privacy Button
        else
        {
            Background.SetActive(false);
            PrivacyButton.SetActive(true);

        }
    }
    public void OnClickYes()
    {
        PlayerPrefs.SetInt("userConsent", 1);
        PlayerPrefs.SetInt("TermsShown", 1);
        userConsent = true;
        Debug.Log("***** Setting User Consent For Consoli: " + userConsent.ToString() + " *****");
        //YasinConsoliAds.Instance.initialize(userConsent);
        TermsPanel.SetActive(false);


        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Invoke("LoadNextScene", 0.50f);
        }
    }


    public void OnClickNo()
    {
        PlayerPrefs.SetInt("userConsent", 0);
        PlayerPrefs.SetInt("TermsShown", 1);
        userConsent = false;
        Debug.Log("***** Setting User Consent For Consoli: " + userConsent.ToString() + " *****");
        //YasinConsoliAds.Instance.initialize(userConsent);
        TermsPanel.SetActive(false);


        if (SceneManager.GetActiveScene().buildIndex == 0)
            Invoke("LoadNextScene", 0.50f);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
