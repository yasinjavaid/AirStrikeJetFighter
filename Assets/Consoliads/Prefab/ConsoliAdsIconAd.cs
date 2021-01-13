using UnityEngine;
using System.Collections;

public class ConsoliAdsIconAd : MonoBehaviour {

	// Use this for initialization

	[Header("ConsoliAds IconAd SceneIndex ")]
	public int sceneIndex = 0;

	void Start () {
		if (ConsoliAds.Instance != null) {
			ConsoliAds.Instance.ShowIconAd (this.gameObject, sceneIndex);
		}
	}
		
	void OnDestroy()
	{
		if (ConsoliAds.Instance != null)
		{
			ConsoliAds.Instance.DestoryIconAd(this.gameObject, sceneIndex);
		}
	}
}
