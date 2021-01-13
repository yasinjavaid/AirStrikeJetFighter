using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LbzzzAdsCalling : MonoBehaviour
{
    [Range(0, 2)]
    public int index;
    private Image Icon;
    // Use this for initialization
    IEnumerator Start()
    {
        Icon = gameObject.GetComponent<Image>();
        StartPos:
        if (index == 0)
        {
            if (LbzzzAds.Instance0.sprite == null)
            {
                yield return new WaitForSeconds(2);
                goto StartPos;
            }
            Icon.sprite = LbzzzAds.Instance0.sprite;
        }
        
        if (index == 1)
            Icon.sprite = LbzzzAds.Instance1.sprite;
        if (index == 2)
            Icon.sprite = LbzzzAds.Instance2.sprite;
    }
	
    // Update is called once per frame
    void Update()
    {
	
    }

    public void OpenUrl()
    {
        if (index == 0)
        {
            if (LbzzzAds.Instance0.link == "https://play.google.com/store/apps/details?id=com.lethal.us.army.fps.shooting")
            {
                
            }
            else
            {
                Application.OpenURL(LbzzzAds.Instance0.link);
            }
        }
        else if (index == 1)
        {
            if (LbzzzAds.Instance0.link == "https://play.google.com/store/apps/details?id=com.lethal.us.army.fps.shooting")
            {

            }
            else
            {
                Application.OpenURL(LbzzzAds.Instance1.link);
            }
        }
        else if (index == 2)
        {
            if (LbzzzAds.Instance0.link == "https://play.google.com/store/apps/details?id=com.lethal.us.army.fps.shooting")
            {

            }
            else
            {
                Application.OpenURL(LbzzzAds.Instance2.link);
            }
        }

    }
}
