using UnityEngine;
using System.Collections;

public class ConsoliAdsNativeAd : MonoBehaviour {


	[Header("ConsoliAds NativeAd SceneIndex ")]
	public int sceneIndex = 0;

	void Start()
	{
		if (ConsoliAds.Instance != null) {
			ConsoliAds.Instance.ShowNativeAd (this.gameObject, sceneIndex);
		}
	}

	void OnDestroy()
	{
		if (ConsoliAds.Instance != null){
			ConsoliAds.Instance.HideNative(sceneIndex);
		}	
	}
}
