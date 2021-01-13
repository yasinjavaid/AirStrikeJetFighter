using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class CAInspectorAdNetworkIDs
{

	public AdmobIds Admob;
	public ChartboostIds Chartboost;
	public FacebookIds Facebook;
	public OguryIds Ogury;
	public MyTargetIds MyTarget;
	public KidozIds Kidoz;
	public MintegralIds Mintegral;
	//public AtmosplayIds Atmosplay;
	public string LeadboltAppKey;

    private string ConsoliadsAppKey;
    private string HeyzapID;
    private string RevmobMediaID;
    private string UnityadsAppID;
    private string AdcolonyAppID;
    private string AdcolonyInterstitialZoneID;
    private string AdcolonyRewardedZoneID;
    private string IronsourceAppKey;
    private string ApplovinID;
    private string VungleID;
    private string VunglePlacementName;
    private string TapJoyID;
    private string TapJoyPlacement;
    private string MobVistaAppID;
    private string MobVistaAppKey;
    private string MobVistaInterstitialID;
    private string MobVistaVideoID;
    private string unityAdsBannerPlacement;
    private string tapJoyRewardedPlacement;
    private string vungleRewardedPlacementID;
    private string InmobiAccountID;
    private string InmobiInterstitialPlacement;
    private string InmobiRewardedVideoPlacement;
    private string InmobiBannerAdPlacement;
    private string inmobiNativeAdPlacement;
    private string StartappDeveloperId;
    private string StartappApplicationId;
    private string MobfoxInterstitialAdUnit;
    private string MobfoxRewardedVideoAdUnit;
    private string MobfoxBannerAdUnit;
    private string MopubIntesrstialAdUnit;
    private string MopubRewardedAdUnit;
    private string mopubBannerAdUnit;
    private string mopubNativeAdUnit;

    [Serializable]
    public class OguryIds
    {
        public string OguryInterstitialAdUnit;
        public string OguryRewardedVideoAdUnit;
        public string OguryAPIKey;
    }

    [Serializable]
    public class MyTargetIds
    {
        public string MyTargetInterstitialSlotID;
        public string MyTargetBannerAdSlotID;
        public string myTargetNativeAdSlotID;
        public string myTargetRewardedAdSlotID;
    }

    [Serializable]
    public class ChartboostIds
    {
        public string ChartboostAppID;
        public string ChartboostAppSignature;
    }

    [Serializable]
    public class AdmobIds
    {
        public string AdmobAppID;
        public string AdmobBannerAdUnitID;
        public string AdmobInterstitialAdUnitID;
        public string AdmobRewardedVideoAdUnitID;
        public string AdmobNativeAdUnitID;
    }

    [Serializable]
    public class FacebookIds
    {
        public string FacebookBannerId;
        public string FacebookInterstitialUnitId;
        public string FacebookRewardedUnitId;
        public string FacebookNativeId;
    }

    [Serializable]
    public class KidozIds
    {
        public string KidozPubId;
        public string KidozSecToken;
    }

	[Serializable]
	public class MintegralIds
	{
		public string MintegralAppKey;
		public string MintegralAPPID;
		public string MintegralInterstitialID;
		public string MintegralRewardedID;
		public string MintegralVideoID;
		public string MintegralNativeID;
		//public string MintegralInteractiveID;
	}

	/*
	[Serializable]
	private class AtmosplayIds
	{
		public string AtmosplayAPPID;
		public string AtmosplayInterstitialAdUnitID;
		public string AtmosplayRewardedVideoAdUnitID;
		public string AtmosplayNativeAdUnitID;
	}*/

	public void setupAdIds(NetworkAdIDType type, Platform platform, String key ,Platform selectedPlatform )
	{
		if (selectedPlatform == platform) 
		{
            switch (type)
			{
			case NetworkAdIDType.ConsoliadsAppKey:
				this.ConsoliadsAppKey = key;
				break;
			case NetworkAdIDType.ChartboostAppID:
				this.Chartboost.ChartboostAppID = key;
				break;
			case NetworkAdIDType.ChartboostAppSignature:
				this.Chartboost.ChartboostAppSignature = key;
				break;
			case NetworkAdIDType.AdmobAppID:
				this.Admob.AdmobAppID = key;
				break;
			case NetworkAdIDType.AdmobBannerAdUnitID:
				this.Admob.AdmobBannerAdUnitID = key;
				break;
			case NetworkAdIDType.AdmobInterstitialAdUnitID:
				this.Admob.AdmobInterstitialAdUnitID = key;
				break;
			case NetworkAdIDType.AdmobRewardedVideoAdUnitID:
				this.Admob.AdmobRewardedVideoAdUnitID = key;
				break;
			case NetworkAdIDType.AdmobNativeAdID:
				this.Admob.AdmobNativeAdUnitID = key;
				break;
			case NetworkAdIDType.HeyzapID:
				this.HeyzapID = key;
				break;
			case NetworkAdIDType.RevmobMediaID:
				this.RevmobMediaID = key;
				break;
			case NetworkAdIDType.UnityAdsID:
				this.UnityadsAppID = key;
				break;
			case NetworkAdIDType.AdColonyAppID:
				this.AdcolonyAppID = key;
				break;
			case NetworkAdIDType.AdColonyInterstitialZoneID:
				this.AdcolonyInterstitialZoneID = key;
				break;
			case NetworkAdIDType.AdColonyRewardedZoneID:
				this.AdcolonyRewardedZoneID = key;
				break;
			case NetworkAdIDType.IronsourceAppKey:
				this.IronsourceAppKey = key;
				break;
			case NetworkAdIDType.AppLovinID:
				this.ApplovinID = key;
				break;
			case NetworkAdIDType.LeadboltAppKey:
				this.LeadboltAppKey = key;
				break;
			case NetworkAdIDType.VungleAdID:
				this.VungleID = key;
				break;
			case NetworkAdIDType.TapJoyAdID:
				this.TapJoyID = key;
				break;
			case NetworkAdIDType.TapJoyPlacement:
				this.TapJoyPlacement = key;
				break;
			case NetworkAdIDType.MobVistaAppKey:
				this.MobVistaAppKey = key;
				break;
			case NetworkAdIDType.MobVistaAPPID:
				this.MobVistaAppID = key;
				break;
			case NetworkAdIDType.MobVistaInterstitialID:
				this.MobVistaInterstitialID = key;
				break;
			case NetworkAdIDType.MobVistaVideoID:
				this.MobVistaVideoID = key;
				break;
			case NetworkAdIDType.FacebookBannerID:
				this.Facebook.FacebookBannerId = key;
				break;
			case NetworkAdIDType.FacebookNativeID:
				this.Facebook.FacebookNativeId = key;
				break;
			case NetworkAdIDType.FacebookInterstitialUnitID:
				this.Facebook.FacebookInterstitialUnitId = key;
				break;
			case NetworkAdIDType.FacebookRewardedUnitID:
				this.Facebook.FacebookRewardedUnitId = key;
				break;
			case NetworkAdIDType.StartAppDeveloperID:
				this.StartappDeveloperId = key;
				break;
			case NetworkAdIDType.StartAppApplicationID:
				this.StartappApplicationId = key;
				break;
			case NetworkAdIDType.KidozPubID:
				this.Kidoz.KidozPubId = key;
				break;
			case NetworkAdIDType.KidozSecToken:
				this.Kidoz.KidozSecToken = key;
				break;
			case NetworkAdIDType.MopubIntesrstialAdUnit:
				this.MopubIntesrstialAdUnit = key;
				break;
			case NetworkAdIDType.MopubRewardedAdUnit:
				this.MopubRewardedAdUnit = key;
				break;
			case NetworkAdIDType.VunglePlacementID:
				this.VunglePlacementName = key;
				break;
			case NetworkAdIDType.InmobiAccountID:
				this.InmobiAccountID = key;
				break;
			case NetworkAdIDType.InmobiInterstitialPlacement:
				this.InmobiInterstitialPlacement = key;
				break;
			case NetworkAdIDType.InmobiRewardedVideoPlacement:
				this.InmobiRewardedVideoPlacement = key;
				break;
			case NetworkAdIDType.InmobiBannerAdPlacement:
				this.InmobiBannerAdPlacement = key;
				break;
			case NetworkAdIDType.MobfoxInterstitialAdUnit:
				this.MobfoxInterstitialAdUnit = key;
				break;
			case NetworkAdIDType.MobfoxRewardedVideoAdUnit:
				this.MobfoxRewardedVideoAdUnit = key;
				break;
			case NetworkAdIDType.MobfoxBannerAdUnit:
				this.MobfoxBannerAdUnit = key;
				break;
			case NetworkAdIDType.MyTargetInterstitialSlotID:
				this.MyTarget.MyTargetInterstitialSlotID = key;
				break;
			case NetworkAdIDType.MyTargetBannerAdSlotID:
				this.MyTarget.MyTargetBannerAdSlotID = key;
				break;
			case NetworkAdIDType.OguryAPIKey:
				this.Ogury.OguryAPIKey = key;
				break;
			case NetworkAdIDType.OguryInterstitialAdUnit:
				this.Ogury.OguryInterstitialAdUnit = key;
				break;
			case NetworkAdIDType.OguryRewardedVideoAdUnit:
                this.Ogury.OguryRewardedVideoAdUnit = key;
				break;
            case NetworkAdIDType.UnityAdsBannerPlacement:
                this.unityAdsBannerPlacement = key;
                break;
            case NetworkAdIDType.MopubBannerAdUnit:
                this.mopubBannerAdUnit = key;
                break;
            case NetworkAdIDType.MopubNativeAdUnit:
                this.mopubNativeAdUnit = key;
                break;
            case NetworkAdIDType.InmobiNativeAdPlacement:
                this.inmobiNativeAdPlacement = key;
                break;
            case NetworkAdIDType.MyTargetNativeAdSlotID:
                this.MyTarget.myTargetNativeAdSlotID = key;
                break;
            case NetworkAdIDType.MyTargetRewardedAdSlotID:
                this.MyTarget.myTargetRewardedAdSlotID = key;
                break;
            case NetworkAdIDType.TapJoyRewardedPlacement:
                this.tapJoyRewardedPlacement = key;
                break;
            case NetworkAdIDType.VungleRewardedPlacementID:
                this.vungleRewardedPlacementID = key;
                break;

			}
		}
	}
   
	public void clearInspectorAdIds(Platform platform)
	{
		foreach (NetworkAdIDType value in Enum.GetValues(typeof(NetworkAdIDType))) 
		{
			setupAdIds (value,platform,"",platform);
		}
	}
}
