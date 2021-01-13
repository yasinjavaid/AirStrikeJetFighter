using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using SimpleJSON;
using System;

[CustomEditor(typeof(ConsoliAds))]
public class ConsoliAdsEditor : Editor
{
	string missMatchStr = "";
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ConsoliAds sdkScript = (ConsoliAds)target;

        if (GUILayout.Button("Configure Server"))
        {
			String applicationIdentifier = "";
			#if UNITY_5_6_OR_NEWER
			applicationIdentifier = PlayerSettings.applicationIdentifier;
			#else
			applicationIdentifier = PlayerSettings.bundleIdentifier;
			#endif
	
            string result = null;
            string errorMsg = "", warnings = "";
            if (sdkScript.userSignature == "")
            {
                errorMsg += "User Signature cannot be empty!\n";
            }

            if (sdkScript.appName == "")
            {
                errorMsg += "Product Name cannot be empty!\n";
            }
            if (sdkScript.bundleIdentifier == "")
            {
                errorMsg += "Bundle Identifier cannot be empty!";
            }
            if (Platform.IsDefined(typeof(Platform), sdkScript.platform) == false)
            {
                errorMsg += "Store cannot be empty!";
            }
			if (sdkScript.platform != Platform.Apple && EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android ) 
			{
				errorMsg += "Platform does not match with your Target Platform!\n";
			}
			else if (sdkScript.platform == Platform.Apple && EditorUserBuildSettings.activeBuildTarget != BuildTarget.iOS)
			{
				errorMsg += "Platform does not match with your Target Platform!\n";
			}
			if (applicationIdentifier != sdkScript.bundleIdentifier)
            {
                warnings += "Bundle Indentifier does not match with your application's bundle indentifier!\n";
            }

            if (errorMsg != "")
            {
                EditorUtility.DisplayDialog("Error", errorMsg, "Ok");
            }
            else {

				JSONClass allAdNetworks = validateAllScenesAdNetwork(sdkScript);
				if (missMatchStr != "") {
					EditorUtility.DisplayDialog("Alert", "Following Ad Networks are not integrated, either remove them from scenes or integrate them in " +
						"ConsoliAds before clicking \"Configure Server\" \nFor more info Kindly see the documentation on how to add or remove AdNetworks from ConsoliAdsPlugin " + "\n" + missMatchStr, "Ok");
					return;
				}
                if (warnings != "")
                {
                    bool dialogResult = EditorUtility.DisplayDialog("Warning", warnings, "Continue", "Cancel");
                    if (dialogResult)
                    {
						result = ServerConfig.Instance.configureServer(sdkScript,allAdNetworks);
                    }
                    else {
                        EditorUtility.DisplayDialog("Success", "ConsoliAds synchronization was canceled", "Ok");
                    }
                }
                else {

					result = ServerConfig.Instance.configureServer(sdkScript,allAdNetworks);
                }
            }
            if (result != null)
            {
                //popup that show the response message from server
                EditorUtility.DisplayDialog("Configure server", result, "Ok");
            }
        }

        if (GUILayout.Button("Goto Consoli Ads"))
        {
			Help.BrowseURL("https://portal.consoliads.com");
        }

		if (GUILayout.Button("Check for Updates"))
        {
			CheckForUpdate(sdkScript);
        }

		if (GUILayout.Button("Download Jar Resolver"))
        {
			Help.BrowseURL("https://github.com/googlesamples/unity-jar-resolver/releases");
        }
	}

	public void CheckForUpdate(ConsoliAds sdkScript)
	{
		JSONNode response = ServerConfig.Instance.checkForUpdates(sdkScript);
		if(response != null)
			DesignWindow.OpenWindow(response);
		else
			EditorUtility.DisplayDialog("Check for Updates", "No New Update found", "Ok");
	}

	public JSONClass validateAllScenesAdNetwork(ConsoliAds CAInstance)
	{
		List<int> userSelectedAdNetworks = new List<int>();

		for (int sequenceCounter = 0; sequenceCounter < CAInstance.scenesArray.Length; sequenceCounter++)
		{
			for (int adCounter = 0; adCounter < CAInstance.scenesArray[sequenceCounter].interstitialAndVideoDetails.networkList.Length; adCounter++)
			{
				AdNetworkNameInterstitial ad = CAInstance.scenesArray[sequenceCounter].interstitialAndVideoDetails.networkList[adCounter];
				if (ad != AdNetworkNameInterstitial.EMPTY)
				{
					userSelectedAdNetworks.Add((int)ad);
				}
			}

			for (int adCounter = 0; adCounter < CAInstance.scenesArray[sequenceCounter].rewardedVideoDetails.networkList.Length; adCounter++)
			{
				AdNetworkNameRewardedVideo ad = CAInstance.scenesArray[sequenceCounter].rewardedVideoDetails.networkList[adCounter];
				if (ad != AdNetworkNameRewardedVideo.EMPTY && (int)ad != 0)
				{
					userSelectedAdNetworks.Add((int)ad);
				}
			}

			AdNetworkName failOverInterstitial = (AdNetworkName)CAInstance.scenesArray[sequenceCounter].interstitialAndVideoDetails.failOver;
			if (failOverInterstitial != AdNetworkName.EMPTY)
			{
				userSelectedAdNetworks.Add((int)failOverInterstitial);
			}

			AdNetworkName failOverRewarded = (AdNetworkName)CAInstance.scenesArray[sequenceCounter].rewardedVideoDetails.failOver;
			if (failOverRewarded != AdNetworkName.EMPTY && (int)failOverRewarded != 0)
			{
				userSelectedAdNetworks.Add((int)failOverRewarded);
			}

		}

		ArrayList arraylist = CAAdnetworkUtils.GetIntegratedAdNetworksList(CAInstance, userSelectedAdNetworks);
		List<int> mismatchedAdnetwork = (List<int>)arraylist[0];

		missMatchStr = "";
		foreach (int element in mismatchedAdnetwork) {
			missMatchStr = missMatchStr + "\n" + (AdNetworkName)element;
		}
		JSONClass allAdNetworks = (JSONClass)arraylist[1];
		return allAdNetworks;
	}
}

