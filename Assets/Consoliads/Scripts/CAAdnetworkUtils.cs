using System;
using UnityEngine;
using System.Linq;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.IO.Compression;

public class CAAdnetworkUtils {
	
	private const string JSON_ADID = "adID";
	private const string kAssets = "Assets";
	public const string kPluginsPath = "Assets/Plugins";
	public const string kAndroidPluginsPath = kPluginsPath + "/Android/";
	public const string kIOSPluginsPath = kPluginsPath + "/iOS/";
	static List<string> mediationFileList = new List<string>();

	public static string CompressJson(string text)
	{
		byte[] buffer = Encoding.UTF8.GetBytes(text);

		var memoryStream = new MemoryStream();
		using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
		{
			gZipStream.Write(buffer, 0, buffer.Length);
		}

		memoryStream.Position = 0;

		var compressedData = new byte[memoryStream.Length];
		memoryStream.Read(compressedData, 0, compressedData.Length);

		return Convert.ToBase64String(compressedData);
	}

	public static string UnZip(string value1)
	{

		byte[] byteArray = System.Convert.FromBase64String(value1.Trim());
		string unzippedResponse = "";

		using (var mem = new MemoryStream())
		{
			mem.Write(new byte[] { 0x1f, 0x8b, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00 }, 0, 8);
			mem.Write(byteArray, 0, byteArray.Length);

			mem.Position = 0;

			using (var gzip = new GZipStream(mem, CompressionMode.Decompress))
			using (var reader = new StreamReader(gzip))
			{
				unzippedResponse = reader.ReadToEnd();
			}
		}
		return unzippedResponse;
	}

	static public string DecodeFrom64(string encodedData)
	{
		byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
		string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
		return returnValue;	
	}

	public static void populateIntegratedMediationSDKs(ConsoliAds sdkScript)
	{
		DirectoryInfo info = null;

		if (sdkScript.platform == Platform.Apple)
			info = new DirectoryInfo(AssetPathToAbsolutePath(kIOSPluginsPath));
		else
			info = new DirectoryInfo(AssetPathToAbsolutePath(kAndroidPluginsPath));

		FileInfo[] fileInfo = info.GetFiles();

		foreach (var file in fileInfo)
		{
			string fileName = file.Name.ToLower();

			if (fileName.Contains("-mediation") && !fileName.Contains(".meta"))
			{
				mediationFileList.Add(fileName);
				Debug.Log("" + file.Name);
			}
		}
	}
	
	public static ArrayList GetIntegratedAdNetworksList(ConsoliAds sdkScript, List<int> userSelectedAdNetworks)
	{
		mediationFileList.Clear ();
		int[] distinctAdnetwork = userSelectedAdNetworks.Distinct().ToArray();
		List<int> unavailableAdnetworks = new List<int>();

		populateIntegratedMediationSDKs(sdkScript);
		var strJson = new JSONClass();
		int eventCounter = 0;

		foreach (AdNetworkName value in Enum.GetValues(typeof(AdNetworkName)))
		{
			bool isAdavailable = false;
			switch (value)
			{
				case AdNetworkName.ADMOBINTERSTITIAL:
				case AdNetworkName.ADMOBREWARDEDVIDEO:
				case AdNetworkName.ADMOBBANNER:
				case AdNetworkName.ADMOBNATIVEAD:
					{
						switch (sdkScript.platform)
						{
							case Platform.Apple:
								if (mediationFileList.Contains("libadmob-mediation.a"))
								{
									isAdavailable = true;
								}
								break;
							case Platform.Google:
							case Platform.Amazon:
								if (mediationFileList.Contains("admob-mediation.aar"))
								{
									isAdavailable = true;
								}
								break;
						}

					}
					break;

				case AdNetworkName.CONSOLIADS:
				case AdNetworkName.CONSOLIADSREWARDEDVIDEO:
				case AdNetworkName.CONSOLIADSBANNER:
				case AdNetworkName.CONSOLIADSNATIVE:
				case AdNetworkName.CONSOLIADSICON:
					{
						switch (sdkScript.platform)
						{
							case Platform.Apple:
								if (mediationFileList.Contains("libconsoliads-mediation.a"))
								{
									isAdavailable = true;
								}
								break;
							case Platform.Google:
							case Platform.Amazon:
								if (mediationFileList.Contains("consoliads-mediation.aar"))
								{
									isAdavailable = true;
								}
								break;
	
						}
					}
					break;

				case AdNetworkName.UNITYADS:
				case AdNetworkName.UNITYADSREWARDEDVIDEO:
                case AdNetworkName.UNITYADSBANNER:
                {
						switch (sdkScript.platform)
						{
							case Platform.Apple:
								if (mediationFileList.Contains("libunityads-mediation.a"))
								{
									isAdavailable = true;
								}
								break;
							case Platform.Google:
							case Platform.Amazon:
								if (mediationFileList.Contains("unityads-mediation.aar"))
								{
									isAdavailable = true;
								}
								break;
						}
					}
					break;

				case AdNetworkName.IRONSOURCEINTERSTITIAL:
				case AdNetworkName.IRONSOURCEREWARDEDVIDEO:
                case AdNetworkName.IRONSOURCEBANNER:
                {
						switch (sdkScript.platform)
						{
							case Platform.Apple:
								if (mediationFileList.Contains("libironsource-mediation.a"))
								{
									isAdavailable = true;
								}
								break;
							case Platform.Google:
							case Platform.Amazon:
								if (mediationFileList.Contains("ironsource-mediation.aar"))
								{
									isAdavailable = true;
								}
								break;
						}
					}
					break;

				case AdNetworkName.APPLOVININTERSTITIAL:
				case AdNetworkName.APPLOVINREWARDEDVIDEO:
				case AdNetworkName.APPLOVINBANNER:
                case AdNetworkName.APPLOVINNATIVE:
                    {
                        switch (sdkScript.platform)
						{
							case Platform.Apple:
								if (mediationFileList.Contains("libapplovin-mediation.a"))
								{
									isAdavailable = true;
								}
								break;
							case Platform.Google:
							case Platform.Amazon:
								if (mediationFileList.Contains("applovin-mediation.aar"))
								{
									isAdavailable = true;
								}
								break;
						}
					}
					break;

				case AdNetworkName.CHARTBOOST:
				case AdNetworkName.CHARTBOOSTREWARDEDVIDEO:
					{
						switch (sdkScript.platform)
						{
							case Platform.Apple:
								if (mediationFileList.Contains("libchartboost-mediation.a"))
								{
									isAdavailable = true;
								}
								break;
							case Platform.Google:
							case Platform.Amazon:
								if (mediationFileList.Contains("chartboost-mediation.aar"))
								{
									isAdavailable = true;
								}
								break;
						}
					}
					break;

				case AdNetworkName.REVMOBFULLSCREEN:
				case AdNetworkName.REVMOBVIDEO:
				case AdNetworkName.REVMOBREWARDEDVIDEO:
					{
						switch (sdkScript.platform)
						{
							case Platform.Apple:
								if (mediationFileList.Contains("librevmob-mediation.a"))
								{
									isAdavailable = true;
								}
								break;
							case Platform.Google:
							case Platform.Amazon:
								if (mediationFileList.Contains("revmob-mediation.aar"))
								{
									isAdavailable = true;
								}
								break;
	
							}
					}
					break;

				case AdNetworkName.ADCOLONY:
				case AdNetworkName.ADCOLONYREWARDEDVIDEO:
					{
						switch (sdkScript.platform)
						{
							case Platform.Apple:
								if (mediationFileList.Contains("libadcolony-mediation.a"))
								{
									isAdavailable = true;
								}
								break;
							case Platform.Google:
							case Platform.Amazon:
								if (mediationFileList.Contains("adcolony-mediation.aar"))
								{
									isAdavailable = true;
								}
								break;
						}
					}
					break;

				case AdNetworkName.TAPJOYADS:
                case AdNetworkName.TAPJOYREWARDED:
                    {
                        switch (sdkScript.platform)
						{
							case Platform.Apple:
								if (mediationFileList.Contains("libtapjoy-mediation.a"))
								{
									isAdavailable = true;
								}
								break;
							case Platform.Google:
							case Platform.Amazon:
								if (mediationFileList.Contains("tapjoy-mediation.aar"))
								{
									isAdavailable = true;
								}
								break;
						}
					}
					break;

				case AdNetworkName.LEADBOLTINTERSTITIAL:
				case AdNetworkName.LEADBOLTREWARDEDVIDEO:
                case AdNetworkName.LEADBOLTNATIVE:
                    {
                        switch (sdkScript.platform)
						{
							case Platform.Apple:
								if (mediationFileList.Contains("libleadbolt-mediation.a"))
								{
									isAdavailable = true;
								}
								break;
							case Platform.Google:
							case Platform.Amazon:
								if (mediationFileList.Contains("leadbolt-mediation.aar"))
								{
									isAdavailable = true;
								}
								break;
						}
					}
					break;

				case AdNetworkName.HEYZAPINTERSTITIAL:
				case AdNetworkName.HEYZAPVIDEO:
                case AdNetworkName.HEYZAPBANNER:
                case AdNetworkName.HEYZAPREWARDEDVIDEO:
                    {
                        switch (sdkScript.platform)
						{
							case Platform.Apple:
								if (mediationFileList.Contains("libheyzap-mediation.a"))
								{
									isAdavailable = true;
								}
								break;
							case Platform.Google:
							case Platform.Amazon:
								if (mediationFileList.Contains("heyzap-mediation.aar"))
								{
									isAdavailable = true;
								}
								break;
						}
					}
					break;

				case AdNetworkName.VUNGLEADS:
                case AdNetworkName.VUNGLEREWARDED:
                    {
                        switch (sdkScript.platform)
						{
							case Platform.Apple:
								if (mediationFileList.Contains("libvungle-mediation.a"))
								{
									isAdavailable = true;
								}
								break;
							case Platform.Google:
							case Platform.Amazon:
								if (mediationFileList.Contains("vungle-mediation.aar"))
								{
									isAdavailable = true;
								}
								break;
						}
					}
					break;

				case AdNetworkName.MOBVISTAINTERSTITIAL:
				case AdNetworkName.MOBVISTAREWARDEDVIDEO:
					{
						switch (sdkScript.platform)
						{
							case Platform.Apple:
								if (mediationFileList.Contains("libmobvista-mediation.a"))
								{
									isAdavailable = true;
								}
								break;
							case Platform.Google:
							case Platform.Amazon:
								if (mediationFileList.Contains("mobvista-mediation.aar"))
								{
									isAdavailable = true;
								}
								break;
						}
					}
					break;

				case AdNetworkName.FACEBOOKINTERSTITIAL:
				case AdNetworkName.FACEBOOKREWARDEDVIDEO:
				case AdNetworkName.FACEBOOKBANNER:
				case AdNetworkName.FACEBOOKNATIVE:
					{
						switch (sdkScript.platform)
						{
							case Platform.Apple:
								if (mediationFileList.Contains("libfacebook-mediation.a"))
								{
									isAdavailable = true;
								}
								break;
							case Platform.Google:
							case Platform.Amazon:
								if (mediationFileList.Contains("facebook-mediation.aar"))
								{
									isAdavailable = true;
								}
								break;
						}
					}
					break;

				case AdNetworkName.STARTAPPINTERSTITIAL:
				case AdNetworkName.STARTAPPREWARDEDVIDEO:
                case AdNetworkName.STARTAPPBANNER:
                case AdNetworkName.STARTAPPNATIVE:
                    {
                        switch (sdkScript.platform)
						{
							case Platform.Apple:
								if (mediationFileList.Contains("libstartapp-mediation.a"))
								{
									isAdavailable = true;
								}
								break;
							case Platform.Google:
							case Platform.Amazon:
								if (mediationFileList.Contains("startapp-mediation.aar"))
								{
									isAdavailable = true;
								}
								break;
						}
					}
					break;

				case AdNetworkName.KIDOZINTERSTITIAL:
				case AdNetworkName.KIDOZREWARDEDVIDEO:
                case AdNetworkName.KIDOZBANNER:
                    {
                        switch (sdkScript.platform)
						{
							case Platform.Apple:
								if (mediationFileList.Contains("libkidoz-mediation.a"))
								{
									isAdavailable = true;
								}
								break;
							case Platform.Google:
							case Platform.Amazon:
								if (mediationFileList.Contains("kidoz-mediation.aar"))
								{
									isAdavailable = true;
								}
								break;
						}
					}
					break;

				case AdNetworkName.MOPUBINTERSTITIAL:
				case AdNetworkName.MOPUBREWARDEDVIDEO:
                case AdNetworkName.MOPUBBANNER:
                case AdNetworkName.MOPUBNATIVE:
                    {
                        switch (sdkScript.platform)
						{
							case Platform.Apple:
								if (mediationFileList.Contains("libmopub-mediation.a"))
								{
									isAdavailable = true;
								}
								break;
							case Platform.Google:
							case Platform.Amazon:
								if (mediationFileList.Contains("mopub-mediation.aar"))
								{
									isAdavailable = true;
								}
								break;
						}
					}
					break;

				case AdNetworkName.INMOBIINTERSTITIAL:
				case AdNetworkName.INMOBIREWARDEDVIDEO:
				case AdNetworkName.INMOBIBANNERAD:
                case AdNetworkName.INMOBINATIVE:
                    {
                        switch (sdkScript.platform)
						{
							case Platform.Apple:
								if (mediationFileList.Contains("libinmobi-mediation.a"))
								{
									isAdavailable = true;
								}
								break;
							case Platform.Google:
							case Platform.Amazon:
								if (mediationFileList.Contains("inmobi-mediation.aar"))
								{
									isAdavailable = true;
								}
								break;
						}
					}
					break;

				case AdNetworkName.MOBFOXINTERSTITIAL:
				case AdNetworkName.MOBFOXREWARDEDVIDEO:
				case AdNetworkName.MOBFOXBANNER:
                case AdNetworkName.MOBFOXNATIVE:
                    {
                        switch (sdkScript.platform)
						{
							case Platform.Apple:
								if (mediationFileList.Contains("libmobfox-mediation.a"))
								{
									isAdavailable = true;
								}
								break;
							case Platform.Google:
							case Platform.Amazon:
								if (mediationFileList.Contains("mobfox-mediation.aar"))
								{
									isAdavailable = true;
								}
								break;
						}
					}
					break;

				case AdNetworkName.OGURYINTERSTITIAL:
				case AdNetworkName.OGURYREWARDEDVIDEO:
					{
						switch (sdkScript.platform)
						{
							case Platform.Apple:
								if (mediationFileList.Contains("libogury-mediation.a"))
								{
									isAdavailable = true;
								}
								break;
							case Platform.Google:
							case Platform.Amazon:
								if (mediationFileList.Contains("ogury-mediation.aar"))
								{
									isAdavailable = true;
								}
								break;
						}
					}
					break;

				case AdNetworkName.MYTARGETBANNERAD:
				case AdNetworkName.MYTARGETINTERSTITIAL:
                case AdNetworkName.MYTARGETNATIVE:
                case AdNetworkName.MYTARGETREWARDED:
                    {
                        switch (sdkScript.platform)
						{
							case Platform.Apple:
								if (mediationFileList.Contains("libmytarget-mediation.a"))
								{
									isAdavailable = true;
								}
								break;
							case Platform.Google:
							case Platform.Amazon:
								if (mediationFileList.Contains("mytarget-mediation.aar"))
								{
									isAdavailable = true;
								}
								break;
						}
					}
					break;

			case AdNetworkName.MINTEGRALINTERSTITIAL:
			case AdNetworkName.MINTEGRALVIDEO:
			//case AdNetworkName.MINTEGRALINTERACTIVE:
			case AdNetworkName.MINTEGRALREWARDEDVIDEO:
			case AdNetworkName.MINTEGRALNATIVE:
				{
					switch (sdkScript.platform)
					{
					case Platform.Apple:
						if (mediationFileList.Contains("libmintegral-mediation.a"))
						{
							isAdavailable = true;
						}
						break;
					case Platform.Google:
					case Platform.Amazon:
						if (mediationFileList.Contains("mintegral-mediation.aar"))
						{
							isAdavailable = true;
						}
						break;
					}
				}
				break;
				/*
			case AdNetworkName.ATMOSPLAYINTERSTITIAL:
			case AdNetworkName.ATMOSPLAYREWARDEDVIDEO:
			case AdNetworkName.ATMOSPLAYNATIVE:
				{
					switch (sdkScript.platform)
					{
					case Platform.Apple:
						if (mediationFileList.Contains("libatmosplay-mediation.a"))
						{
							isAdavailable = true;
						}
						break;
					case Platform.Google:
					case Platform.Amazon:
						if (mediationFileList.Contains("atmosplay-mediation.aar"))
						{
							isAdavailable = true;
						}
						break;
					}
				}
				break;*/

			}

			if (isAdavailable)
			{
				strJson["appAdnetwork"][eventCounter][JSON_ADID].AsInt = (int)value;
				strJson["appAdnetwork"][eventCounter]["isAdavailable"].AsInt = 1;
				eventCounter++;
			}
			else {
				if (distinctAdnetwork.Contains((int)value))
				{
					unavailableAdnetworks.Add((int)value);
				}
			}
		}

		ArrayList arr = new ArrayList();
		arr.Add(unavailableAdnetworks);
		arr.Add(strJson);
		return arr;	
	}

	/*
	public static JSONClass setAdIDs(JSONClass strJson, ConsoliAds CAInstance)
	{
		int index = 0;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.ConsoliadsAppKey;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.ConsoliadsAppKey, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.ChartboostAppID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.ChartboostAppID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.ChartboostAppSignature;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.ChartboostAppSignature, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.AdmobAppID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.AdmobAppID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.AdmobBannerAdUnitID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.AdmobBannerAdUnitID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.AdmobInterstitialAdUnitID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.AdmobInterstitialAdUnitID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.AdmobRewardedVideoAdUnitID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.AdmobRewardedVideoAdUnitID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.AdmobNativeAdID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.AdmobNativeAdID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.LeadboltAppKey;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.LeadboltAppKey, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.HeyzapID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.HeyzapID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.RevmobMediaID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.RevmobMediaID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.UnityAdsID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.UnityAdsID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.AdColonyAppID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.AdColonyAppID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.AdColonyInterstitialZoneID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.AdColonyInterstitialZoneID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.AdColonyRewardedZoneID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.AdColonyRewardedZoneID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.SupersonicAppKey;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.SupersonicAppKey, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.AppLovinID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.AppLovinID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.VungleAdID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.VungleAdID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.TapJoyAdID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.TapJoyAdID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.TapJoyPlacement;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.TapJoyPlacement, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.MobVistaAPPID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.MobVistaAPPID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.MobVistaAppKey;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.MobVistaAppKey, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.MobVistaInterstitialID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.MobVistaInterstitialID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.MobVistaVideoID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.MobVistaVideoID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.FacebookBannerID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.FacebookBannerID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.FacebookInterstitialUnitID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.FacebookInterstitialUnitID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.FacebookRewardedUnitID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.FacebookRewardedUnitID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.FacebookNativeID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.FacebookNativeID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.StartAppDeveloperID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.StartAppDeveloperID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.StartAppApplicationID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.StartAppApplicationID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.KidozPubID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.KidozPubID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.KidozSecToken;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.KidozSecToken, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.MopubIntesrstialAdUnit;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.MopubIntesrstialAdUnit, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.MopubRewardedAdUnit;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.MopubRewardedAdUnit, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.VunglePlacementID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.VunglePlacementID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.InmobiAccountID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.InmobiAccountID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.InmobiInterstitialPlacement;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.InmobiInterstitialPlacement, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.InmobiRewardedVideoPlacement;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.InmobiRewardedVideoPlacement, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.InmobiBannerAdPlacement;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.InmobiBannerAdPlacement, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.MobfoxInterstitialAdUnit;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.MobfoxInterstitialAdUnit, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.MobfoxRewardedVideoAdUnit;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.MobfoxRewardedVideoAdUnit, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.MyTargetInterstitialSlotID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.MyTargetInterstitialSlotID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.MyTargetBannerAdSlotID;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.MyTargetBannerAdSlotID, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.OguryInterstitialAdUnit;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.OguryInterstitialAdUnit, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.OguryRewardedVideoAdUnit;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.OguryRewardedVideoAdUnit, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		strJson ["adIDs"][index]["OS"].AsInt = getPlateForm(CAInstance.platform);
		strJson ["adIDs"][index]["adValueType"].AsInt = (int)NetworkAdIDType.OguryAPIKey;
		strJson["adIDs"][index]["adValue"] = getAdNetworkKey(NetworkAdIDType.OguryAPIKey, CAInstance);
		strJson["adIDs"][index]["adID"] = "";
		index++;

		return strJson;
	}*/

	static int getPlateForm(Platform platform)
	{
		switch (platform)
		{
			case Platform.Google:
				return 41;
			case Platform.Apple:
				return 42;
			case Platform.Amazon:
				return 43;
		}
		return -1;
	}

	#region IntegratedAdsList

	public static string AssetPathToAbsolutePath(string _relativePath)
	{
		string _unrootedRelativePath = _relativePath.TrimStart('/');
		if (!_unrootedRelativePath.StartsWith(kAssets, StringComparison.Ordinal))
			return null;
		string _absolutePath = System.IO.Path.Combine(GetProjectPath(), _unrootedRelativePath);
		return _absolutePath;
	}

	public static string GetProjectPath()
	{
		return System.IO.Path.GetFullPath(Application.dataPath + @"/../");
	}

	public static bool isFileExists(string fileName, Platform platform)
	{
		string file = kAndroidPluginsPath + fileName;

		if (platform == Platform.Apple)
			file = kIOSPluginsPath + fileName;

		string _absolutePath = AssetPathToAbsolutePath(file);
		return System.IO.File.Exists(_absolutePath);
	}

	#endregion
}
