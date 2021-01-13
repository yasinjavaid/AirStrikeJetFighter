using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class CAScene { 
	public PlaceholderName placeholderName;
	private bool isSessionStart = false;

	public CAInterstitialMediationDetails interstitialAndVideoDetails;
	//public CAInteractiveMediationDetails interactiveDetails; 
    public CARewardedVideoMediationDetails rewardedVideoDetails;
    public CANativeMediationDetails nativeDetails;
    public CABannerMediationDetails bannerDetails; 
	public CAIconMediationDetails iconDetails; 

	public bool IsSessionStart
	{
		get
		{
			return isSessionStart;
		}
		set
		{
			isSessionStart = value;
		}
	}
}
