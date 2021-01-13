using UnityEngine;
using System.Collections;
using UnityEditor;

public class CASettings : Editor
{

    [MenuItem("ConsoliAds/Uninstall" , false , 1)]
    public static void Uninstall()
    {
        CAUninstallPlugin.UninstallConsoliAdsPlugin();
    }
    [MenuItem("ConsoliAds/Documentation", false, 2)]
    public static void OpenDocumentation()
    {
        string url = "http://www.consoliads.com/admin/documentation";
        Application.OpenURL(url);
    }
}
