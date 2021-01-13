using UnityEngine;
using System; 
using System.Collections;

[Serializable]
public class CARewardedVideoMediationDetails
{
	private int count = 0;
	private bool isFirst = true;
    public bool skipFirst = false;
    public AdNetworkNameRewardedVideo[] networkList;
    public AdNetworkNameRewardedVideo failOver = AdNetworkNameRewardedVideo.EMPTY;

    public bool IsFirst
    {
        get
        {
            bool val = isFirst;
            isFirst = false;
            return val;
        }
    }


    public int Count
    {
        get
        {
            return count;
        }
        set
        {
            count = value;
        }
    }
}
