using UnityEngine;
using System; 
using System.Collections;

[Serializable]
public class CANativeMediationDetails {

    public bool enabled;
    public AdNetworkNameNative adType = AdNetworkNameNative.ADMOBNATIVEAD;

	public NativeAdPosition position;
    public int width;
    public int height;
}
