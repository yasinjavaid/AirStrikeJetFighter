using UnityEngine;
using System.Collections;
using System;
using SimpleJSON;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.IO.Compression;
using System.Reflection;

public class ServerConfig
{
	private static ServerConfig _instance;
	//------------------------------------------------------------------------------
	private ServerConfig() { }
	//------------------------------------------------------------------------------
	public static ServerConfig Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new ServerConfig();
			}
			return _instance;
		}
	}

	public WWW networkCall(int url, string strJson)
	{
		NetworkLayer nl = GameObject.FindObjectOfType<NetworkLayer>();
		return nl.syncWithServer(url, strJson);
	}

	//called on configure server button click
	public string configureServer(ConsoliAds CAInstance, JSONClass allAdNetworks)
	{
		//creating App JSON
		var strJson = new JSONClass();
		string contentEncoding = "";
		strJson.Add(CAConstants.PACKAGE_KEY, CAInstance.bundleIdentifier.ToString());
		strJson.Add(CAConstants.TITLE_KEY, CAInstance.appName.ToString());
		strJson.Add(CAConstants.SDK_VERSION_KEY, CAConstants.ConsoliAdsVersion);
		strJson.Add(CAConstants.SDK_VERSION_ID_KEY, CAConstants.sdkVersionID);
		strJson.Add(CAConstants.SDK_VERSION, CAConstants.sdkVersionID);
		strJson.Add(CAConstants.USER_SIGNATURE_KEY, CAInstance.userSignature.ToString());
		strJson.Add(CAConstants.TOTAL_SEQUENCES_KEY, CAInstance.scenesArray.Length.ToString());

		if (CAInstance.getShowAdMechanism () == ConsoliAdsShowAdMechanism.Priority) 
		{
			strJson.Add (CAConstants.AD_SHOWN_MECHANISM_KEY, "priority");
		} 
		else 
		{
			strJson.Add (CAConstants.AD_SHOWN_MECHANISM_KEY, "round_robin");
		}

		if(CAInstance.platform == Platform.Google)
		{
			if (CAInstance.rateUsURL == "https://play.google.com/store/apps/details?id=")
			{
				strJson.Add(CAConstants.GP_RATEUS_URL_KEY, CAInstance.rateUsURL + CAInstance.bundleIdentifier);
			}
			else
			{
				strJson.Add(CAConstants.GP_RATEUS_URL_KEY, CAInstance.rateUsURL);
			}
		}
		else
		{
			strJson.Add(CAConstants.AS_RATEUS_URL_KEY, CAInstance.rateUsURL);
		}

		if (CAInstance.isHideAds)
			strJson[CAConstants.IS_HIDEAD_KEY].AsInt = 1;
		else
			strJson[CAConstants.IS_HIDEAD_KEY].AsInt = 0;

		if (CAInstance.ShowLog)
		{
			strJson[CAConstants.MEDIATION_LOG_KEY].AsInt = 1;
		}

		if (CAInstance.ChildDirected)
		{
			strJson[CAConstants.CHILED_DIRECTED_KEY].AsInt = 1;
		}
		else
		{
			strJson[CAConstants.CHILED_DIRECTED_KEY].AsInt = 0;
		}

		strJson[CAConstants.STORE_KEY].AsInt = (int)CAInstance.platform;

		for (int sequenceCounter = 0; sequenceCounter < CAInstance.scenesArray.Length; sequenceCounter++)
		{
			strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.SEQUENCE_TITLE_ID_KEY].AsInt = (int)CAInstance.scenesArray[sequenceCounter].placeholderName;
			strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.SKIPFIRST_KEY] = CAInstance.scenesArray[sequenceCounter].interstitialAndVideoDetails.skipFirst.ToString();
			strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.FAILOVERAD_ID_KEY].AsInt = (int)(AdNetworkName)CAInstance.scenesArray[sequenceCounter].interstitialAndVideoDetails.failOver;
			strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.REWARDED_SKIPFIRST_KEY] = CAInstance.scenesArray[sequenceCounter].rewardedVideoDetails.skipFirst.ToString();
			int rewardedFiloverID = (int)(AdNetworkName)CAInstance.scenesArray[sequenceCounter].rewardedVideoDetails.failOver;
			if (rewardedFiloverID != 0)
			{
				strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.REWARDED_FAILOVERAD_ID_KEY].AsInt = (int)(AdNetworkName)CAInstance.scenesArray[sequenceCounter].rewardedVideoDetails.failOver;
			}
			else
			{
				strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.REWARDED_FAILOVERAD_ID_KEY].AsInt = -1;
			}

            /*
			strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.INTERACTIVE_SKIPFIRST_KEY] = CAInstance.scenesArray[sequenceCounter].interactiveDetails.skipFirst.ToString();
			int interactiveFailoverID = (int)(AdNetworkName)CAInstance.scenesArray[sequenceCounter].interactiveDetails.failOver;
			if (interactiveFailoverID != 0)
			{
				strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.INTERACTIVE_FAILOVERAD_ID_KEY].AsInt = (int)(AdNetworkName)CAInstance.scenesArray[sequenceCounter].interactiveDetails.failOver;
			}
			else
			{
				strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.INTERACTIVE_FAILOVERAD_ID_KEY].AsInt = -1;
			}
            */

			for (int adCounter = 0; adCounter < CAInstance.scenesArray[sequenceCounter].interstitialAndVideoDetails.networkList.Length; adCounter++)
			{
				AdNetworkNameInterstitial ad = CAInstance.scenesArray[sequenceCounter].interstitialAndVideoDetails.networkList[adCounter];
				strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.INTERSTITIAL_VIDEO_KEY][adCounter][CAConstants.AD_ID_KEY].AsInt = (int)ad;
				strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.INTERSTITIAL_VIDEO_KEY][adCounter][CAConstants.AD_ORDER_KEY].AsInt = (adCounter + 1);
			}

			for (int adCounter = 0; adCounter < CAInstance.scenesArray[sequenceCounter].rewardedVideoDetails.networkList.Length; adCounter++)
			{
				AdNetworkNameRewardedVideo ad = CAInstance.scenesArray[sequenceCounter].rewardedVideoDetails.networkList[adCounter];

				if ((int)ad == 0)
					strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.REWARDEDVIDEO_KEY][adCounter][CAConstants.AD_ID_KEY].AsInt = -1;
				else
					strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.REWARDEDVIDEO_KEY][adCounter][CAConstants.AD_ID_KEY].AsInt = (int)ad;
				
				strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.REWARDEDVIDEO_KEY][adCounter][CAConstants.AD_ORDER_KEY].AsInt = (adCounter + 1);
			}

            /*
			for (int adCounter = 0; adCounter < CAInstance.scenesArray[sequenceCounter].interactiveDetails.networkList.Length; adCounter++)
			{
				AdNetworkNameInteractive ad = CAInstance.scenesArray[sequenceCounter].interactiveDetails.networkList[adCounter];

				if ((int)ad == 0)
					strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.INTERACTIVE_KEY][adCounter][CAConstants.AD_ID_KEY].AsInt = -1;
				else
					strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.INTERACTIVE_KEY][adCounter][CAConstants.AD_ID_KEY].AsInt = (int)ad;

				strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.INTERACTIVE_KEY][adCounter][CAConstants.AD_ORDER_KEY].AsInt = (adCounter + 1);
			}
            */

			strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.NATIVEAD_KEY][CAConstants.ENABLED_KEY].AsBool = CAInstance.scenesArray[sequenceCounter].nativeDetails.enabled;
			strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.NATIVEAD_KEY][CAConstants.AD_ID_KEY].AsInt = (int)CAInstance.scenesArray[sequenceCounter].nativeDetails.adType;
			strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.NATIVEAD_KEY][CAConstants.WIDTH_KEY].AsInt = CAInstance.scenesArray[sequenceCounter].nativeDetails.width;
			strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.NATIVEAD_KEY][CAConstants.HEIGHT_KEY].AsInt = CAInstance.scenesArray[sequenceCounter].nativeDetails.height;
			strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.NATIVEAD_KEY][CAConstants.POSITION_KEY].AsInt = (int)CAInstance.scenesArray[sequenceCounter].nativeDetails.position;

			strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.BANNERAD_KEY][CAConstants.ENABLED_KEY].AsBool = CAInstance.scenesArray[sequenceCounter].bannerDetails.enabled;
			strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.BANNERAD_KEY][CAConstants.AD_ID_KEY].AsInt = (int)CAInstance.scenesArray[sequenceCounter].bannerDetails.adType;
			strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.BANNERAD_KEY][CAConstants.SIZE_KEY].AsInt = (int)CAInstance.scenesArray[sequenceCounter].bannerDetails.size;
			strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.BANNERAD_KEY][CAConstants.POSITION_KEY].AsInt = (int)CAInstance.scenesArray[sequenceCounter].bannerDetails.position;

			strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.ICONAD_KEY][CAConstants.ENABLED_KEY].AsBool = CAInstance.scenesArray[sequenceCounter].iconDetails.enabled;
			strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.ICONAD_KEY][CAConstants.AD_ID_KEY].AsInt = (int)CAInstance.scenesArray[sequenceCounter].iconDetails.adType;
			strJson[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.ICONAD_KEY][CAConstants.SIZE_KEY].AsInt = (int)CAInstance.scenesArray[sequenceCounter].iconDetails.size;

		}

		strJson[CAConstants.APP_INTEGRATED_ADNETWORKS_KEY] = allAdNetworks[CAConstants.APP_INTEGRATED_ADNETWORKS_KEY];
		var compressedJson = CAAdnetworkUtils.CompressJson(strJson.ToString());

		WWW result = CAInstance.postAppJson(networkCall(0, compressedJson));

		if (!string.IsNullOrEmpty(result.error))
		{
			Debug.Log("Error: configureServer" + result.error.ToString());
		}
		else
		{
			Dictionary<string, string> headers = result.responseHeaders;

			if (headers.ContainsKey("Content-Encoding"))
			{
				contentEncoding = headers["Content-Encoding"];
			}
			else if (headers.ContainsKey("CONTENT-ENCODING"))
			{
				contentEncoding = headers["CONTENT-ENCODING"];
			}

			contentEncoding = contentEncoding.ToLower();
			string resultResponse = "";

			if (contentEncoding.Equals("gzip") || contentEncoding.Equals("binary/octet stream"))
			{
				try
				{
					resultResponse = CAAdnetworkUtils.UnZip(result.text);
				}
				catch (Exception exp)
				{
					resultResponse = "";
				}
			}
			else
			{
				resultResponse = result.text;
			}

			var responseArray = JSONNode.Parse(resultResponse);
			if (responseArray != null)
			{
				populateConsoliAdsParamsFromResponse(responseArray, CAInstance);
				if (responseArray[CAConstants.MESSAGE_KEY] != null)
				{
					return responseArray[CAConstants.MESSAGE_KEY];
				}
			}
		}
		return null;
	}

	public void setNativeMediationResponse(string resultResponse , ConsoliAds CAInstance)
	{
		var responseArray = JSONNode.Parse(resultResponse);
		if (responseArray != null) {
			populateConsoliAdsParamsFromResponse (responseArray, CAInstance);
		} 
		else 
		{	
			Debug.Log ("Error in mediation response...");
		}
	}

	private void populateConsoliAdsParamsFromResponse(JSONNode responseArray, ConsoliAds CAInstance)
	{		
		if (responseArray != null)
		{
			//checking to enable log
			if (responseArray[CAConstants.MEDIATION_MODE_KEY] != null)
			{
				if (responseArray[CAConstants.MEDIATION_MODE_KEY].ToString().ToLower().Contains("test"))
				{
					CAInstance.enableLog(true);
				}
				else
				{
					CAInstance.enableLog(false);
				}
			}
			else
			{
				CAInstance.enableLog(false);
			}

			if (responseArray[CAConstants.CHILED_DIRECTED_KEY] != null)
			{
				if (responseArray[CAConstants.CHILED_DIRECTED_KEY].AsInt == 1)
				{
					CAInstance.ChildDirected = true;
				}
				else
				{
					CAInstance.ChildDirected = false;
				}
			}

			if (responseArray[CAConstants.RTBKey] != null && responseArray[CAConstants.RTBKey].AsInt == 1)
			{
				CAInstance.setAutoMediation(true);
			}

			if (responseArray[CAConstants.SUPPORT_URL_KEY] != null)
			{
				CAInstance.supportEmail = responseArray[CAConstants.SUPPORT_URL_KEY];
			}

			CAInstance.moreAppsURL = "";
			CAInstance.rateUsURL = "";

			if (CAInstance.platform == Platform.Google) 
			{
				if (responseArray[CAConstants.GP_MOREAPPS_URL_KEY] != null)
				{
					CAInstance.moreAppsURL = responseArray[CAConstants.GP_MOREAPPS_URL_KEY];
				}
				if (responseArray[CAConstants.GP_RATEUS_URL_KEY] != null)
				{
					CAInstance.rateUsURL = responseArray[CAConstants.GP_RATEUS_URL_KEY];
				}
			}
			else if(CAInstance.platform == Platform.Apple)
			{
				if (responseArray[CAConstants.AS_MOREAPPS_URL_KEY] != null)
				{
					CAInstance.moreAppsURL = responseArray[CAConstants.AS_MOREAPPS_URL_KEY];
				}
				if (responseArray[CAConstants.AS_RATEUS_URL_KEY] != null)
				{
					CAInstance.rateUsURL = responseArray[CAConstants.AS_RATEUS_URL_KEY];
				}
			}

			//saving deviceID in the sharedPrefs
			if (responseArray[CAConstants.DEVICE_ID_KEY] != null)
			{
				PlayerPrefs.SetString("ConsoliAds_DeviceID", responseArray[CAConstants.DEVICE_ID_KEY]);
			}
			if (responseArray[CAConstants.API_APP_ID_KEY] != null)
			{
				PlayerPrefs.SetString("ConsoliAds_AppID", responseArray[CAConstants.API_APP_ID_KEY]);
			}
			if (responseArray[CAConstants.REGION_KEY] != null)
			{
				PlayerPrefs.SetString("ConsoliAds_Region", responseArray[CAConstants.REGION_KEY]);
			}
			//populating inspector
			if (responseArray[CAConstants.PACKAGE_KEY] != null)
			{
				CAInstance.bundleIdentifier = responseArray[CAConstants.PACKAGE_KEY];
			}
			if (responseArray[CAConstants.TITLE_KEY] != null)
			{
				CAInstance.appName = responseArray[CAConstants.TITLE_KEY];
			}

			if (responseArray[CAConstants.AD_SHOWN_MECHANISM_KEY] != null && responseArray[CAConstants.AD_SHOWN_MECHANISM_KEY].ToString().Contains("priority"))
			{
				CAInstance.setShowAdMechanism(ConsoliAdsShowAdMechanism.Priority);
			}

			int hideAllAds = PlayerPrefs.GetInt("consoliads_hide_all_ads", 0);
			if (hideAllAds == 1)
			{
				CAInstance.isHideAds = true;
			}
			else if (responseArray[CAConstants.IS_HIDEAD_KEY] != null && responseArray[CAConstants.IS_HIDEAD_KEY].AsInt == 1)
			{
				CAInstance.isHideAds = true;
			}
			else
			{
				CAInstance.isHideAds = false;
			}

			if (responseArray[CAConstants.SEQUENCES_KEY] != null)
			{
				//initializing ad sequences array to the size of the return JSON Array from server
				CAInstance.scenesArray = new CAScene[responseArray[CAConstants.SEQUENCES_KEY].Count];
				//populating ad sequences
				for (int sequenceCounter = 0; sequenceCounter < responseArray[CAConstants.SEQUENCES_KEY].Count; sequenceCounter++)
				{
					//initializing each array item  of ad sequence
					CAInstance.scenesArray[sequenceCounter] = new CAScene();
					CAInstance.scenesArray[sequenceCounter].interstitialAndVideoDetails = new CAInterstitialMediationDetails();
					//CAInstance.scenesArray[sequenceCounter].interactiveDetails = new CAInteractiveMediationDetails();
					CAInstance.scenesArray[sequenceCounter].rewardedVideoDetails = new CARewardedVideoMediationDetails();
					CAInstance.scenesArray[sequenceCounter].nativeDetails = new CANativeMediationDetails();
					CAInstance.scenesArray[sequenceCounter].bannerDetails = new CABannerMediationDetails();
					//populating sequence values
					if (responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.SKIPFIRST_KEY].AsInt == 1)
					{
						CAInstance.scenesArray[sequenceCounter].interstitialAndVideoDetails.skipFirst = true;
					}
					else
					{
						CAInstance.scenesArray[sequenceCounter].interstitialAndVideoDetails.skipFirst = false;
					}
					//populating sequence values
					if (responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.REWARDED_SKIPFIRST_KEY].AsInt == 1)
					{
						CAInstance.scenesArray[sequenceCounter].rewardedVideoDetails.skipFirst = true;
					}
					else
					{
						CAInstance.scenesArray[sequenceCounter].rewardedVideoDetails.skipFirst = false;
					}
                    /*
                    //populating sequence values
					if (responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.INTERACTIVE_SKIPFIRST_KEY].AsInt == 1)
					{
						CAInstance.scenesArray[sequenceCounter].interactiveDetails.skipFirst = true;
					}
					else
					{
						CAInstance.scenesArray[sequenceCounter].interactiveDetails.skipFirst = false;
					}
                    */

					CAInstance.scenesArray[sequenceCounter].interstitialAndVideoDetails.failOver = (AdNetworkNameInterstitial)responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.FAILOVERAD_ID_KEY].AsInt;
					CAInstance.scenesArray[sequenceCounter].rewardedVideoDetails.failOver = (AdNetworkNameRewardedVideo)responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.REWARDED_FAILOVERAD_ID_KEY].AsInt;
					//CAInstance.scenesArray[sequenceCounter].interactiveDetails.failOver = (AdNetworkNameInteractive)responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.INTERACTIVE_FAILOVERAD_ID_KEY].AsInt;
					CAInstance.scenesArray[sequenceCounter].placeholderName = (PlaceholderName)responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.SEQUENCE_TITLE_ID_KEY].AsInt;

					//initializing ad sequence's ads Array to the size received in JSON Array 
					CAInstance.scenesArray[sequenceCounter].interstitialAndVideoDetails.networkList = new AdNetworkNameInterstitial[responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.INTERSTITIAL_VIDEO_KEY].Count];
					//populating ad sequence's Ads Array
					for (int adCounter = 0; adCounter < responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.INTERSTITIAL_VIDEO_KEY].Count; adCounter++)
					{
						CAInstance.scenesArray[sequenceCounter].interstitialAndVideoDetails.networkList[adCounter] = (AdNetworkNameInterstitial)responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.INTERSTITIAL_VIDEO_KEY][adCounter][CAConstants.AD_ID_KEY].AsInt;

					}
					CAInstance.scenesArray[sequenceCounter].rewardedVideoDetails.networkList = new AdNetworkNameRewardedVideo[responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.REWARDEDVIDEO_KEY].Count];
					//populating ad sequence's Ads Array
					for (int adCounter = 0; adCounter < responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.REWARDEDVIDEO_KEY].Count; adCounter++)
					{
						CAInstance.scenesArray[sequenceCounter].rewardedVideoDetails.networkList[adCounter] = (AdNetworkNameRewardedVideo)responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.REWARDEDVIDEO_KEY][adCounter][CAConstants.AD_ID_KEY].AsInt;

					}

                    /*
					CAInstance.scenesArray[sequenceCounter].interactiveDetails.networkList = new AdNetworkNameInteractive[responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.INTERACTIVE_KEY].Count];
					//populating ad sequence's Ads Array
					for (int adCounter = 0; adCounter < responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.INTERACTIVE_KEY].Count; adCounter++)
					{
						CAInstance.scenesArray[sequenceCounter].interactiveDetails.networkList[adCounter] = (AdNetworkNameInteractive)responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.INTERACTIVE_KEY][adCounter][CAConstants.AD_ID_KEY].AsInt;

					}
                    */

					//populating native ad settings
					if (responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.NATIVEAD_KEY] != null)
					{
						CAInstance.scenesArray[sequenceCounter].nativeDetails = new CANativeMediationDetails();
						CAInstance.scenesArray[sequenceCounter].nativeDetails.adType = (AdNetworkNameNative)responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.NATIVEAD_KEY][CAConstants.AD_ID_KEY].AsInt;
						CAInstance.scenesArray[sequenceCounter].nativeDetails.enabled = responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.NATIVEAD_KEY]["enabled"].AsBool;
						CAInstance.scenesArray[sequenceCounter].nativeDetails.width = responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.NATIVEAD_KEY][CAConstants.WIDTH_KEY].AsInt;
						CAInstance.scenesArray[sequenceCounter].nativeDetails.height = responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.NATIVEAD_KEY][CAConstants.HEIGHT_KEY].AsInt;
						CAInstance.scenesArray[sequenceCounter].nativeDetails.position = (NativeAdPosition)responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.NATIVEAD_KEY][CAConstants.POSITION_KEY].AsInt;
					}
					else
					{
						CAInstance.scenesArray[sequenceCounter].nativeDetails = new CANativeMediationDetails();
						CAInstance.scenesArray[sequenceCounter].nativeDetails.enabled = false;
					}
					//populating native ad settings

					if (responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.BANNERAD_KEY] != null)
					{
						CAInstance.scenesArray[sequenceCounter].bannerDetails = new CABannerMediationDetails();
						CAInstance.scenesArray[sequenceCounter].bannerDetails.adType = (AdNetworkNameBanner)responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.BANNERAD_KEY][CAConstants.AD_ID_KEY].AsInt;
						CAInstance.scenesArray[sequenceCounter].bannerDetails.enabled = responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.BANNERAD_KEY]["enabled"].AsBool;
						CAInstance.scenesArray[sequenceCounter].bannerDetails.size = (BannerSize)responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.BANNERAD_KEY][CAConstants.SIZE_KEY].AsInt;
						CAInstance.scenesArray[sequenceCounter].bannerDetails.position = (BannerPosition)responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.BANNERAD_KEY][CAConstants.POSITION_KEY].AsInt;
					}
					else
					{
						CAInstance.scenesArray[sequenceCounter].bannerDetails = new CABannerMediationDetails();
						CAInstance.scenesArray[sequenceCounter].bannerDetails.enabled = false;
					}

					if (responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.ICONAD_KEY] != null)
					{
						CAInstance.scenesArray[sequenceCounter].iconDetails = new CAIconMediationDetails();
						CAInstance.scenesArray[sequenceCounter].iconDetails.enabled = responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.ICONAD_KEY]["enabled"].AsBool;
						CAInstance.scenesArray[sequenceCounter].iconDetails.adType = (AdNetworkNameIcon)responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.ICONAD_KEY][CAConstants.AD_ID_KEY].AsInt;
						CAInstance.scenesArray[sequenceCounter].iconDetails.size = (IconSize)responseArray[CAConstants.SEQUENCES_KEY][sequenceCounter][CAConstants.ICONAD_KEY][CAConstants.SIZE_KEY].AsInt;
					}
					else
					{
						CAInstance.scenesArray[sequenceCounter].iconDetails = new CAIconMediationDetails();
						CAInstance.scenesArray[sequenceCounter].iconDetails.enabled = false;
					}
				}
			}

			CAInstance.adIDList.clearInspectorAdIds (CAInstance.platform);

			if (responseArray[CAConstants.AD_IDS_KEY] != null)
			{
				//populating ad IDs
				for (int adIDCounter = 0; adIDCounter < responseArray[CAConstants.AD_IDS_KEY].Count; adIDCounter++)
				{
					NetworkAdIDType type = (NetworkAdIDType)responseArray[CAConstants.AD_IDS_KEY][adIDCounter][CAConstants.AD_ID_VALUE_TYPE_KEY].AsInt;
					Platform platform = (Platform)responseArray[CAConstants.AD_IDS_KEY][adIDCounter][CAConstants.AD_ID_PLATEFORM_TYPE_KEY].AsInt;
					String key = responseArray[CAConstants.AD_IDS_KEY][adIDCounter][CAConstants.AD_ID_VALUE_KEY];
					CAInstance.adIDList.setupAdIds (type,platform,key,CAInstance.platform);
				}
			}
		}
	}

	//called on configure server button click
	public JSONNode checkForUpdates(ConsoliAds CAInstance)
	{
		//creating App JSON
		var strJson = new JSONClass();
		string contentEncoding = "";
		strJson.Add(CAConstants.SDK_VERSION, CAConstants.sdkVersionID);

		var compressedJson = CAAdnetworkUtils.CompressJson(strJson.ToString());

		WWW result = CAInstance.postAppJson(networkCall(4, compressedJson));

		if (result.error != null)
		{
			Debug.Log("Error: checkForUpdates" + result.error.ToString());
		}
		else
		{
			Dictionary<string, string> headers = result.responseHeaders;

			if (headers.ContainsKey("Content-Encoding"))
			{
				contentEncoding = headers["Content-Encoding"];
			}

			contentEncoding = contentEncoding.ToLower();
			string resultResponse = "";

			if (contentEncoding.Equals("gzip") || contentEncoding.Equals("binary/octet stream"))
			{
				resultResponse = CAAdnetworkUtils.UnZip(result.text);
			}
			else
			{
				resultResponse = result.text;
			}

			var responseArray = JSONNode.Parse(resultResponse);
			if (responseArray != null && responseArray[CAConstants.MESSAGE_KEY] != null && responseArray["mediation_patch_adnetworks"] != null)
			{
				return responseArray;
			}
		}
		return null;
	}

}