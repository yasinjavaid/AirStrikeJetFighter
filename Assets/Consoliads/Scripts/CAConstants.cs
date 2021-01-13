using UnityEngine;
using System.Collections;

public static class CAConstants
{
	public const string REQUEST_LOADED = "loaded";
	public const string REQUEST_FAILED = "failed";
	public const string ConsoliAdsVersion = "4.10.5";
	public const string sdkVersionID = "2105";
	public static string networkErrorMsg = "Sorry! There was a Network Error";
	public const float LOAD_THRESHOLD = 5.0f;

	public const string ADAPP_KEY = "appKey";
	public const string ADAPP_ID = "appID";
	public const string ADUNIT_ID = "adUnitID";
	public const string EXTRA_ID_1 = "extraID_1";

	public const string ADNETWORKID_Hash_Key = "adID";
	public const string FAILOVER_HASH_KEY = "failOverAdID";
	public const string RTBKey = "isRtb";
	public const string SEQUENCES_KEY = "sequences";
	public const string INTERSTITIAL_VIDEO_KEY = "interstitialAndVideo";
	//public const string INTERACTIVE_KEY = "interactive";
	public const string REWARDEDVIDEO_KEY = "rewardedVideo";
	public const string NATIVEAD_KEY = "native";
	public const string BANNERAD_KEY = "banner";
	public const string ICONAD_KEY = "icon";
	public const string SKIPFIRST_KEY = "isFirstSkip";
	public const string REWARDED_SKIPFIRST_KEY = "isFirstSkipRewardedVideo";
	//public const string INTERACTIVE_SKIPFIRST_KEY = "isFirstSkipInteractive";
	public const string FAILOVERAD_ID_KEY = "failOverAdID";
	public const string REWARDED_FAILOVERAD_ID_KEY = "failOverAdIDRewardedVideo";
	//public const string INTERACTIVE_FAILOVERAD_ID_KEY = "failOverAdIDInteractive";
	public const string SEQUENCE_TITLE_ID_KEY = "seqTitleID";
	public const string AD_ID_KEY = "adID";
	public const string AD_ORDER_KEY = "adOrder";
	public const string AD_IDS_KEY = "adIDs";
	public const string MEDIATION_MODE_KEY = "mediationMode";
	public const string MEDIATION_LOG_KEY = "mediationLog";
	public const string CHILED_DIRECTED_KEY = "childDirected";
	public const string SUPPORT_URL_KEY = "supportURL";
	public const string GP_MOREAPPS_URL_KEY = "gpMoreAppsURL";
	public const string AS_MOREAPPS_URL_KEY = "asMoreAppsURL";
	public const string GP_RATEUS_URL_KEY = "gpRateUsURL";
	public const string AS_RATEUS_URL_KEY = "asRateUsURL";
	public const string DEVICE_ID_KEY = "deviceID";
	public const string AD_SHOWN_MECHANISM_KEY = "adsQueueType";
	public const string API_APP_ID_KEY = "appID";
	public const string REGION_KEY = "region";
	public const string PACKAGE_KEY = "package";
	public const string TITLE_KEY = "title";
	public const string IS_HIDEAD_KEY = "isHideAds";

	public const string SDK_VERSION_KEY = "gssdkVersion";
	public const string SDK_VERSION_ID_KEY = "sdkVersionID";
	public const string SDK_VERSION = "sdkVersion";
	public const string USER_SIGNATURE_KEY = "userSignature";
	public const string TOTAL_SEQUENCES_KEY = "totalSequences";
	public const string STORE_KEY = "store";

	public const string ENABLED_KEY = "enabled";
	public const string WIDTH_KEY = "width";
	public const string POSITION_KEY = "position";
	public const string HEIGHT_KEY = "height";
	public const string SIZE_KEY = "size";
	public const string APP_INTEGRATED_ADNETWORKS_KEY= "appAdnetwork";

	public const string AD_ID_VALUE_TYPE_KEY = "adValueType";
	public const string AD_ID_PLATEFORM_TYPE_KEY = "OS";
	public const string AD_ID_VALUE_KEY = "adValue";
	public const string MESSAGE_KEY = "message";
}

public static class LogMessages
{
	public const string NO_INTERNET = "Internet connectivity not found";
	public const string CONSOLIADS_NOT_INITIALIZED = "ConsoliAds not initialized";
	public const string HIDE_ADS_ENABLED = "Hide Ads enabled";
	public const string METHOD_START = "Start";
	public const string METHOD_END = "End";
	public const string SKIPFIRST_ENABLED = "SkipFirst enabled";
	public const string NATIVE_NOT_CONFIGURED = "Native Ad not configured";
	public const string CONSOLIADS_LOG_STATUS = "ConsoliAds logs";
	public const string CONSOLIADS_COPPA_STATUS = "ConsoliAds Child Directed";
	public const string STOPWATCH_TIME_LAPSED = "***** TIME LAPSED *****";
	public const string STOPWATCH_START = "***** STOPWATCH START *****";
}

public class AdSize
{
	public int width { get; set; }
	public int height { get; set; }
}

public enum PlaceholderName
{
	None = -1,
    MainMenu = 1,
    SelectionScene = 2,
    FinalScene = 3,
    OnSuccess = 4,
    OnFailure = 5,
    OnPause = 6,
    StoreScene = 7,
    Gameplay = 8,
    MidScene1 = 9,
    MidScene2 = 10,
    MidScene3 = 11,
    AppExit = 12,
    LoadingScene1 = 13,
    LoadingScene2 = 14,
	onReward = 15
 
};
public enum AdNetworkName
{
    EMPTY = -1,
    ADMOBINTERSTITIAL = 0,
    LEADBOLTINTERSTITIAL = 2,
    LEADBOLTREWARDEDVIDEO = 4,
    CHARTBOOST = 5,
    CHARTBOOSTMOREAPPS = 6,
    HEYZAPINTERSTITIAL = 7,
    REVMOBFULLSCREEN = 8,
    ADCOLONY = 9,
    REVMOBVIDEO = 11,
    CONSOLIADS = 19,
    UNITYADS = 10,
    IRONSOURCEINTERSTITIAL = 12,
    IRONSOURCEOFFERWALL = 14,
    IRONSOURCEREWARDEDVIDEO = 15,
    APPLOVININTERSTITIAL = 18,
    APPLOVINREWARDEDVIDEO = 20,
    ADMOBREWARDEDVIDEO = 21,
    HEYZAPVIDEO = 22,
    REVMOBREWARDEDVIDEO = 24,
    CHARTBOOSTREWARDEDVIDEO = 25,
    UNITYADSREWARDEDVIDEO = 26,
    ADMOBNATIVEAD = 27,
    ADMOBBANNER = 28,
	VUNGLEADS = 29,
	TAPJOYADS = 30,
	MOBVISTAINTERSTITIAL = 31,
	MOBVISTAREWARDEDVIDEO = 32,
	FACEBOOKINTERSTITIAL = 33,
	FACEBOOKREWARDEDVIDEO = 34,
    CONSOLIADSREWARDEDVIDEO = 35,
	STARTAPPINTERSTITIAL = 36,
	STARTAPPREWARDEDVIDEO = 37,
	KIDOZINTERSTITIAL = 38,
	KIDOZREWARDEDVIDEO = 39,
	MOPUBINTERSTITIAL = 40,
	MOPUBREWARDEDVIDEO = 41,
	APPLOVINBANNER = 42,
	FACEBOOKBANNER = 43,
	FACEBOOKNATIVE = 44,
	ADCOLONYREWARDEDVIDEO = 46,
	INMOBIINTERSTITIAL = 47,
	INMOBIREWARDEDVIDEO = 48,
	INMOBIBANNERAD = 49,
	MOBFOXINTERSTITIAL = 50,
	MOBFOXREWARDEDVIDEO = 51,
	MYTARGETINTERSTITIAL = 52,
	MYTARGETBANNERAD = 53,
	OGURYINTERSTITIAL = 54,
	OGURYREWARDEDVIDEO = 55,
	MOBFOXBANNER = 56,
	CONSOLIADSBANNER = 57,
	CONSOLIADSNATIVE = 58,
	CONSOLIADSICON = 59,
    UNITYADSBANNER = 68,
    APPLOVINNATIVE = 69,
    IRONSOURCEBANNER = 70,
    HEYZAPREWARDEDVIDEO = 71,
    HEYZAPBANNER = 72,
    KIDOZBANNER = 73,
    LEADBOLTNATIVE = 74,
    STARTAPPNATIVE = 75,
    STARTAPPBANNER = 76,
    MOPUBBANNER = 77,
    MOPUBNATIVE = 78,
    INMOBINATIVE = 79,
    MYTARGETNATIVE = 80,
    MYTARGETREWARDED = 81,
    MOBFOXNATIVE = 82,
    TAPJOYREWARDED = 83,
    VUNGLEREWARDED = 84,
	MINTEGRALINTERSTITIAL = 60,
	MINTEGRALREWARDEDVIDEO = 61,
	MINTEGRALVIDEO = 62,
	MINTEGRALNATIVE = 63
	//MINTEGRALINTERACTIVE = 64,
	//ATMOSPLAYINTERSTITIAL = 65,
	//ATMOSPLAYREWARDEDVIDEO = 66,
	//ATMOSPLAYNATIVE = 67
};
public enum AdNetworkNameInterstitial
{
    EMPTY = -1,
    ADMOBINTERSTITIAL = 0,
    LEADBOLTINTERSTITIAL = 2,
    CHARTBOOST = 5,
    CHARTBOOSTMOREAPPS = 6,
    HEYZAPINTERSTITIAL = 7,
    REVMOBFULLSCREEN = 8,
    ADCOLONY = 9,
    REVMOBVIDEO = 11,
    CONSOLIADS = 19,
    UNITYADS = 10,
    IRONSOURCEINTERSTITIAL = 12,
    IRONSOURCEOFFERWALL = 14,
    APPLOVININTERSTITIAL = 18,
    HEYZAPVIDEO = 22,
	VUNGLEADS = 29,
	TAPJOYADS = 30,
	MOBVISTAINTERSTITIAL = 31,
	FACEBOOKINTERSTITIAL = 33,
	STARTAPPINTERSTITIAL = 36,
	KIDOZINTERSTITIAL = 38,
	MOPUBINTERSTITIAL = 40,
	INMOBIINTERSTITIAL = 47,
	MOBFOXINTERSTITIAL = 50,
	MYTARGETINTERSTITIAL = 52,
	OGURYINTERSTITIAL = 54,
	MINTEGRALINTERSTITIAL = 60,
	MINTEGRALVIDEO = 62,
	//ATMOSPLAYINTERSTITIAL = 65
};
public enum AdNetworkNameRewardedVideo
{
    EMPTY = -1,
    LEADBOLTREWARDEDVIDEO = 4,
    IRONSOURCEREWARDEDVIDEO = 15,
    APPLOVINREWARDEDVIDEO = 20,
    ADMOBREWARDEDVIDEO = 21,
    REVMOBREWARDEDVIDEO = 24,
    CHARTBOOSTREWARDEDVIDEO = 25,
    UNITYADSREWARDEDVIDEO = 26,
	MOBVISTAREWARDEDVIDEO = 32,
	FACEBOOKREWARDEDVIDEO = 34,
    CONSOLIADSREWARDEDVIDEO = 35,
	STARTAPPREWARDEDVIDEO = 37,
	KIDOZREWARDEDVIDEO = 39,
	MOPUBREWARDEDVIDEO = 41,
	ADCOLONYREWARDEDVIDEO = 46,
	INMOBIREWARDEDVIDEO = 48,
	MOBFOXREWARDEDVIDEO = 51,
	OGURYREWARDEDVIDEO = 55,
    HEYZAPREWARDEDVIDEO = 71,
    MYTARGETREWARDED = 81,
    TAPJOYREWARDED = 83,
    VUNGLEREWARDED = 84,
	MINTEGRALREWARDEDVIDEO = 61,
	//ATMOSPLAYREWARDEDVIDEO = 66
};

/*
public enum AdNetworkNameInteractive
{
	EMPTY = -1,
	MINTEGRALINTERACTIVE = 64
}
*/

public enum AdNetworkNameNative
{
	EMPTY = -1,
    ADMOBNATIVEAD = 27,
	FACEBOOKNATIVE = 44,
	CONSOLIADSNATIVE = 58,
    APPLOVINNATIVE = 69,
    LEADBOLTNATIVE = 74,
    STARTAPPNATIVE = 75,
    MOPUBNATIVE = 78,
    INMOBINATIVE = 79,
    MYTARGETNATIVE = 80,
    MOBFOXNATIVE = 82,
	MINTEGRALNATIVE = 63,
	//ATMOSPLAYNATIVE = 67
}

public enum AdNetworkNameBanner
{
    NONE = -1,
    ADMOBBANNER = 28,
	APPLOVINBANNER = 42,
	FACEBOOKBANNER = 43,
	INMOBIBANNERAD = 49,
	MYTARGETBANNERAD = 53,
	MOBFOXBANNER = 56,
	CONSOLIADSBANNER = 57,
    UNITYADSBANNER = 68,
    IRONSOURCEBANNER = 70,
    HEYZAPBANNER = 72,
    KIDOZBANNER = 73,
    STARTAPPBANNER = 76,
    MOPUBBANNER = 77
}

public enum AdNetworkNameIcon
{
	NONE = -1,
	CONSOLIADSICON = 59
}

public enum Platform
{
    Google = 41,
    Apple = 42,
    Amazon = 43
}

public enum BannerSize
{
    Banner = 1, //320x50
    MediumRectangle = 2,    //300x250
    IABBanner = 3,  //468x60
    Leaderboard = 4, //728x90
    SmartBanner = 5,
    LargeBanner = 6 //320x100
}

public enum IconSize
{
	Small = 0,
	Medium = 1,
	Large = 2
}

public enum BannerPosition
{
	Top = 0,
	Bottom = 1,
	TopLeft = 2,
	TopRight = 3,
	BottomLeft = 4,
	BottomRight = 5,
	Center = 6
}

public enum NativeAdPosition
{
	Top = 0,
	Bottom = 1,
	TopLeft = 2,
	TopRight = 3,
	BottomLeft = 4,
	BottomRight = 5,
	Center = 6
}

public enum ConsoliAdsShowAdMechanism
{
    RoundRobin = 1,
    Priority = 2
}

public enum NetworkAdIDType
{
    AdmobBannerAdUnitID = 1,
    AdmobInterstitialAdUnitID = 2,
    LeadboltAppKey = 3,
    RevmobMediaID = 4,
    HeyzapID = 5,
    ChartboostAppID = 6,
    ChartboostAppSignature = 7,
    AdColonyAppID = 8,
    AdColonyInterstitialZoneID = 9,
    UnityAdsID = 10,
    IronsourceAppKey = 11,
    GoogleAnalyticsTrackingCode = 12,
    FlurryAnalyticsAppKey = 13,
    AppLovinID = 14,
    OguryInterstitialAPIKey = 15,
    ConsoliadsAppKey = 16,
    AdmobRewardedVideoAdUnitID = 17,
    AdmobAppID = 18,
    AdmobNativeAdID = 19,
	VungleAdID = 20,
	TapJoyAdID = 21,
	TapJoyPlacement = 22,
	MobVistaAppKey = 23,
	MobVistaAPPID = 24,
	MobVistaInterstitialID = 25,
	MobVistaVideoID = 26,
	FacebookBannerID = 36,
	FacebookNativeID = 37,
	FacebookInterstitialUnitID = 27,
	FacebookRewardedUnitID = 28,
	StartAppDeveloperID = 29,
	StartAppApplicationID = 30,
	KidozPubID = 31,
	KidozSecToken = 32,
	MopubIntesrstialAdUnit = 33,
	MopubRewardedAdUnit = 34,
	VunglePlacementID = 35,
	AdColonyRewardedZoneID = 38,
	InmobiAccountID = 39,
	InmobiInterstitialPlacement = 40,
	InmobiRewardedVideoPlacement = 41,
	InmobiBannerAdPlacement = 42,
	MobfoxInterstitialAdUnit = 43,
	MobfoxRewardedVideoAdUnit = 44,
	MyTargetInterstitialSlotID = 45,
	MyTargetBannerAdSlotID = 46,
	OguryAPIKey = 47,
	OguryInterstitialAdUnit = 48,
	OguryRewardedVideoAdUnit = 49,
	MobfoxBannerAdUnit = 50,
    UnityAdsBannerPlacement = 62,
    MopubBannerAdUnit = 63,
    MopubNativeAdUnit = 64,
    InmobiNativeAdPlacement = 65,
    MyTargetNativeAdSlotID = 66,
    MyTargetRewardedAdSlotID = 67,
    TapJoyRewardedPlacement = 68,
    VungleRewardedPlacementID = 69,
	MintegralAppKey = 51,
	MintegralAPPID = 52,
	MintegralInterstitialID = 53,
	MintegralRewardedID = 54,
	MintegralVideoID = 55,
	MintegralNativeID = 56,
	//MintegralInteractiveID = 57,
	//AtmosplayAPPID = 58,
	//AtmosplayInterstitialAdUnitID = 59,
	//AtmosplayRewardedVideoAdUnitID = 60 ,
	//AtmosplayNativeAdUnitID = 61
}

public enum CAAnalytics
{
    FireBaseAnalytics = 1,
    GoogleAnalytics = 2,
    FlurryAnalytics = 3
}

public enum AdFormat
{
	INTERSTITIAL = 1,
	VIDEO = 2,
	REWARDED = 3,
	BANNER = 4,
	POPUP = 5,
	OFFERWALL = 6,
	NATIVE = 7,
	ICON = 8
}

public enum RequestState
{
	Idle = 1,
	Requested = 2,
	Completed = 3,
	Failed = 4
}


