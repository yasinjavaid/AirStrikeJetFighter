using UnityEngine;
using System; 
using System.Collections;

[Serializable]
public class CABannerMediationDetails {

    public Boolean enabled;
	public AdNetworkNameBanner adType = AdNetworkNameBanner.NONE;

    public BannerSize size = BannerSize.SmartBanner;
	public BannerPosition position = BannerPosition.Top;
}
