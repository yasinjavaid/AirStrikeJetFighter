using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

public class ConsoliAds : MonoBehaviour
{
	private const string gameObjectName = "ConsoliAds";
	private static ConsoliAds _instance;
	private ConsoliAdsMediationWrapper consoliAdsMediation;
	bool isRTB = false;
	private bool userConsent = true;
	private bool isCOPPA;
	private bool initCalled = false;
	private int pendingBannerSceneIndex;

	public static event Action onInterstitialAdShownEvent;
	public static event Action onInterstitialAdClickedEvent;
    //public static event Action onInteractiveAdShownEvent;
    //public static event Action onInteractiveAdClickedEvent;
    public static event Action onVideoAdShownEvent;
	public static event Action onVideoAdClickedEvent;
    public static event Action onRewardedVideoAdShownEvent;
    public static event Action onRewardedVideoAdCompletedEvent;
    public static event Action onRewardedVideoAdClickEvent;
    public static event Action onPopupAdShownEvent;
	public static event Action<string> onNativeAdLoadedEvent;
	public static event Action<string> onNativeAdFailedToLoadEvent;
	public static event Action onConsoliAdsInitializationSuccess;

	[Header("ConsoliAds Version " + CAConstants.ConsoliAdsVersion)]
    public string userSignature;
    public string appName;
    public Platform platform = Platform.Google;
	public string bundleIdentifier;
	private string bundleVersion;

	public string moreAppsURL;
	public string rateUsURL;

	public string supportEmail;
	public ConsoliAdsShowAdMechanism showAdMechanism = ConsoliAdsShowAdMechanism.RoundRobin;

    public bool isHideAds = false;
    public bool ShowLog = true;
    public bool ChildDirected = false;
    public CAScene[] scenesArray;
	public CAInspectorAdNetworkIDs adIDList;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
		consoliAdsMediation = new ConsoliAdsMediationWrapper();
    }

	public void initialize(bool userConsent = true)
	{
		
		if (!initCalled)
		{
			initCalled = true;
			this.userConsent = userConsent;
			consoliAdsMediation.initializeWithProductName(platform ,appName,
			                                              bundleIdentifier,
			                                              gameObjectName,
			                                              userConsent);
		}
	}

    private ConsoliAds() { }

    public static ConsoliAds Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ConsoliAds>();
                if (_instance && _instance.gameObject)
                {
                    DontDestroyOnLoad(_instance.gameObject);
                }
            }
            return _instance;
        }
    }

	public void enableLog(bool value)
	{
		ShowLog = value;
		CALogManager.Instance.EnableLog (value);
	}

    public void setShowAdMechanism(ConsoliAdsShowAdMechanism type)
    {
        showAdMechanism = type;
    }

	public ConsoliAdsShowAdMechanism getShowAdMechanism()
	{
		return showAdMechanism;
	}
	
	public void setAutoMediation(bool isEnabled)
	{
		isRTB = isEnabled; 
	}

	public bool IsRTB()
	{
		return isRTB;
	}

	public bool getUserConsent()
	{
		return this.userConsent;
	}

	public string getGameObjectName()
	{
		return gameObjectName;
	}

	public bool isHideAd()
	{
		return isHideAds;
	}

    /*
	public bool IsInteractiveAvailable(int index)
	{
		return consoliAdsMediation.isInteractiveAvailable(index);
	}
    */
	
    public bool IsInterstitialAvailable(int index)
    {
		return consoliAdsMediation.isInterstitialAvailable(index);
    }

    public bool IsRewardedVideoAvailable(int index)
    {
		return consoliAdsMediation.isRewardedVideoAvailable(index);
    }

	public void LoadRewarded(int index)
	{
		consoliAdsMediation.loadRewarded(index);
	}

	public void ShowBanner(int index)
	{
		consoliAdsMediation.showBanner(index);
	}

    public void ShowBanner(int index, double x , double y)
    {
        consoliAdsMediation.showBanner(index, x, y);
    }

	public void ShowBanner(int index,AdSize adsize)
    {
		consoliAdsMediation.showBanner(index,adsize);
    }

	public void ShowBanner(int index, AdSize adsize, double x, double y)
    {
		consoliAdsMediation.showBanner(index, adsize, x, y);
    }

    public void HideBanner()
	{
		consoliAdsMediation.hideBanner();
	}

	public void ShowInterstitial(int index)
	{
		consoliAdsMediation.showInterstitial(index);
	}

    /*
	public void ShowInteractive(int index)
	{
		consoliAdsMediation.showInteractive(index);
	}
    */

	public void ShowRewardedVideo(int index)
    {
		consoliAdsMediation.showRewardedVideo(index);
    }
	
	public void ShowNativeAd(GameObject gmObject , int index)
	{
		RectTransform rectTransform = gmObject.GetComponent<RectTransform>();
		Rect rect = getGameObjectRect(gmObject, rectTransform, Camera.main);
		Canvas canvas = getCanvas(gmObject);

		if (canvas == null)
		{
			CALogManager.Instance.Log(CALogManager.LogType.ERROR, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Make sure ConsoliAds NativeAd Prefab is child of a Canvas");
			return;
		}

		float baseWidth = canvas.GetComponent<RectTransform>().rect.width;
		float baseHeight = canvas.GetComponent<RectTransform>().rect.height;

		consoliAdsMediation.showNativeAd(index, rect.x, rect.y , baseWidth, baseHeight);	
	}

	public void ShowIconAd(GameObject gameObject, int index)
	{

		RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
		Rect rect = getGameObjectRect(gameObject, rectTransform, Camera.main);

		Canvas canvas = getCanvas(gameObject);

		if (canvas == null)
		{
			CALogManager.Instance.Log(CALogManager.LogType.ERROR, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Make sure ConsoliAds IconAd Prefab is child of a Canvas");
			return;
		}


		float baseWidth = canvas.GetComponent<RectTransform>().rect.width;
		float baseHeight = canvas.GetComponent<RectTransform>().rect.height;

		int instanceID = gameObject.GetInstanceID();

		consoliAdsMediation.showIconAd(instanceID, index, rect.x, rect.y, baseWidth, baseHeight);	
	}

	private Rect getGameObjectRect(GameObject gObject, RectTransform rectTransform, Camera cam)
	{
		if (rectTransform == null)
		{
			return new Rect(0, 0, 0, 0);
		}

		Vector3[] worldCorners = new Vector3[4];
		Canvas canvas = getCanvas(gObject);

		rectTransform.GetWorldCorners(worldCorners);
		Vector3 gameObjectBottomLeft = worldCorners[0];
		Vector3 gameObjectTopRight = worldCorners[2];
		Vector3 cameraBottomLeft = cam.pixelRect.min;
		Vector3 cameraTopRight = cam.pixelRect.max;

		if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
		{
			gameObjectBottomLeft = cam.WorldToScreenPoint(gameObjectBottomLeft);
			gameObjectTopRight = cam.WorldToScreenPoint(gameObjectTopRight);
		}

		return new Rect(Mathf.Round(gameObjectBottomLeft.x),
			Mathf.Floor((cameraTopRight.y - gameObjectTopRight.y)),
			Mathf.Ceil((gameObjectTopRight.x - gameObjectBottomLeft.x)),
			Mathf.Round((gameObjectTopRight.y - gameObjectBottomLeft.y)));
	}

	private Canvas getCanvas(GameObject gmObject)
	{
		if (gmObject.GetComponent<Canvas>() != null)
		{
			return gmObject.GetComponent<Canvas>();
		}
		else
		{
			if (gmObject.transform.parent != null)
			{
				return getCanvas(gmObject.transform.parent.gameObject);
			}
		}
		return null;	
	}

	public void HideNative(int index)
	{
		consoliAdsMediation.hideNativeAd(index);
	}

	public void DestoryIconAd(GameObject gameObject, int index)
	{
		int instanceID = -1;

		if (gameObject != null){
			instanceID = gameObject.GetInstanceID();
		}
		consoliAdsMediation.destoryIconAd(instanceID, index);	
	}
	
    public void hideAllAds()
    {
		consoliAdsMediation.hideAllAds();
    }

	public void addAdmobTestDevice(string deviceID)
	{
		consoliAdsMediation.addAdmobTestDevice (deviceID);
	}
    
    private string GetStore()
    {
        switch (platform)
        {
            case Platform.Amazon:
                return "amazon";
            case Platform.Google:
                return "google";
            case Platform.Apple:
                return "apple";
            default:
                return null;
        }
    }

	public string MoreFunURL()
	{
		return moreAppsURL;
	}

	public string SupportEmail()
	{
		return supportEmail;
	}

	public string RateUsURL()
	{
		return rateUsURL;
	}

	public WWW postAppJson(WWW www)
    {
        StartCoroutine(WaitForRequest(www));

        // Do nothing until the response is complete.
        while (!www.isDone) { }

        // Deliver the result to the method that called this one.
        return www;
    }
    
	private IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
    }

	#region Native Callabcks

	public void onInterstitialAdShown(string empty)
	{
		if (onInterstitialAdShownEvent != null)
			onInterstitialAdShownEvent();
	}

	public void onInterstitialAdClicked(string empty)
	{
		if (onInterstitialAdClickedEvent != null)
			onInterstitialAdClickedEvent();
	}

	public void onVideoAdShown(string empty)
	{
		if (onVideoAdShownEvent != null)
			onVideoAdShownEvent();
	}

	public void onVideoAdClicked(string empty)
	{
		if (onVideoAdClickedEvent != null)
			onVideoAdClickedEvent();
	}

	public void onRewardedVideoAdShown(string empty)
	{
		if (onRewardedVideoAdShownEvent != null)
			onRewardedVideoAdShownEvent();
	}

	public void onRewardedVideoAdCompleted(string empty)
	{
		if (onRewardedVideoAdCompletedEvent != null)
			onRewardedVideoAdCompletedEvent();
	}

	public void onRewardedVideoAdClick(string empty)
	{
		if (onRewardedVideoAdClickEvent != null)
			onRewardedVideoAdClickEvent();
	}

	public void onPopupAdShown(string empty)
	{
		if (onPopupAdShownEvent != null)
			onPopupAdShownEvent();
	}

	public void onNativeAdLoaded(string adnetworkName)
	{ 
		if (onNativeAdLoadedEvent != null)
			onNativeAdLoadedEvent (adnetworkName);
	}

	public void onNativeAdFailedToLoad(string adnetworkName)
	{ 
		if (onNativeAdFailedToLoadEvent != null)
			onNativeAdFailedToLoadEvent (adnetworkName);
	}

    /*
	public void onInteractiveAdShown(string empty)
	{
		if (onInteractiveAdShownEvent != null)
			onInteractiveAdShownEvent();
	}

	public void onInteractiveAdClicked(string empty)
	{
		if (onInteractiveAdClickedEvent != null)
			onInteractiveAdClickedEvent();
	}
    */

	#endregion

	#region ConsoliadsInitialization

	public void didInitialize(string empty)
	{
		if (onConsoliAdsInitializationSuccess != null)
			onConsoliAdsInitializationSuccess();
	}

	#endregion

	#region Response from mediation

	public void onResponseRecieve(string response)
	{
		ServerConfig.Instance.setNativeMediationResponse (response , ConsoliAds.Instance );
	}

	#endregion

}