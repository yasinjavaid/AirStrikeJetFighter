using UnityEngine;
using System.Collections;

#if UNITY_EDITOR 
using UnityEditor;
using System.IO;

public class CAUninstallPlugin
{
	protected CAUninstallPlugin(){ }

    #region Constants

    private const string kUninstallAlertTitle = "Uninstall - ConsoliAds";
    private const string kUninstallAlertMessage = "Backup before doing this step to preserve changes done in this plugin. This deletes files only related to ConsoliAds plugin. Do you want to proceed?";

    public const string kAssets = "Assets";
    public const string kPluginsPath = "Assets/Plugins";
    public const string kAndroidPluginsPath = kPluginsPath + "/Android";
    public const string kIOSPluginsPath = kPluginsPath + "/iOS";

    private static string[] kPluginFolders = {

            kIOSPluginsPath                         +   "/ConsoliAdsResources.bundle",
            kAssets                                 +   "/Consoliads/Scripts",
            kAssets                                 +   "/Consoliads",
            kAssets                                 +   "/Unity.IO.Compression",
            kAssets                                 +   "/Editor/AutoUpdate",
        };

    private static string[] kPluginFiles = {

        kAssets                                 +   "/Consoliads/ConsoliAds.prefab",
        kAssets                                 +   "/Editor/Editor/BuildScript.cs",
        kAssets                                 +   "/Editor/CABuildPostprocessor.cs",
        kAssets                                 +   "/Consoliads/Editor/ConsoliAdsEditor.cs",

        kPluginsPath                            +   "/ConsoliDLL.dll",
        kPluginsPath                            +   "/ConsoliAdsSample.cs",
        kPluginsPath                            +   "/ConsoliAdsSample.cs",
        kPluginsPath                            +   "/ConsoliAdsSample.unity",

		kAndroidPluginsPath                     +   "/android.arch.core.common-1.0.0.jar",
		kAndroidPluginsPath                     +   "/android.arch.lifecycle.common-1.0.0.jar",
		kAndroidPluginsPath                     +   "/AndroidManifest.xml",
		kAndroidPluginsPath                     +   "/com.android.support.support-annotations-26.1.0.jar",
		kAndroidPluginsPath                     +   "/gson-2.8.5.jar",
		kAndroidPluginsPath                     +   "/android.arch.lifecycle.runtime-1.0.0.aar",
		kAndroidPluginsPath                     +   "/cardview-v7-23.0.0.aar",
		kAndroidPluginsPath                     +   "/com.android.support.support-compat-26.1.0.aar",
		kAndroidPluginsPath                     +   "/com.android.support.support-core-ui-26.1.0.aar",
		kAndroidPluginsPath                     +   "/com.android.support.support-core-utils-26.1.0.aar",
		kAndroidPluginsPath                     +   "/com.android.support.support-fragment-26.1.0.aar",
		kAndroidPluginsPath                     +   "/com.android.support.support-media-compat-26.1.0.aar",
		kAndroidPluginsPath                     +   "/com.android.support.support-v4-26.1.0.aar",
		kAndroidPluginsPath                     +   "/consoliads-mediation.aar",
		kAndroidPluginsPath                     +   "/consoliadsmediation.aar",
		kAndroidPluginsPath                     +   "/consoliadsplugin.aar",
		kAndroidPluginsPath                     +   "/loaderlibrary.aar",
		kAndroidPluginsPath                     +   "/play-services-ads-15.0.1.aar",
		kAndroidPluginsPath                     +   "/play-services-ads-base-15.0.1.aar",
		kAndroidPluginsPath                     +   "/play-services-ads-identifier-15.0.1.aar",
		kAndroidPluginsPath                     +   "/play-services-ads-lite-15.0.1.aar",
		kAndroidPluginsPath                     +   "/play-services-basement-15.0.1.aar",
		kAndroidPluginsPath                     +   "/play-services-gass-15.0.1.aar",
            
            kIOSPluginsPath                     +   "/ConsoliAdsIconAdSizes.h",
            kIOSPluginsPath                     +   "/ConsoliAdsMediationUnityManager.h",
            kIOSPluginsPath                     +   "/ConsoliAdsMediationUnityManager.mm",
            kIOSPluginsPath                     +   "/ConsoliAdsMediationUnityPlugin.h",
            kIOSPluginsPath                     +   "/ConsoliAdUnityPlugin.h",
            kIOSPluginsPath                     +   "/ConsoliAdUnityPluginManager.h",
            kIOSPluginsPath                     +   "/ConsoliAdUnityPluginManager.mm",
            kIOSPluginsPath                     +   "/ConsoliSDKAdsDelegate.h",
            kIOSPluginsPath                     +   "/NativeAdView.xib",
            kIOSPluginsPath                     +   "/ConsoliAdIOSPlugin.h",
            kIOSPluginsPath                     +   "/ConsoliAdsBannerTypes.h",
            kIOSPluginsPath                     +   "/libConsoliAd.a",
            kIOSPluginsPath                     +   "/libconsoliads-mediation.a",
            kIOSPluginsPath                     +   "/libConsoliMediation.a",
            kIOSPluginsPath                     +   "/ConsoliAdsMediationDelegate.h",
            kIOSPluginsPath                     +   "/IconAdView.h",
            kIOSPluginsPath                     +   "/IconAdBase.h",
            kIOSPluginsPath                     +   "/NativeAdBase.h",


        };

    #endregion

    #region Methods

    public static void UninstallConsoliAdsPlugin()
    {
        bool _startUninstall = EditorUtility.DisplayDialog(kUninstallAlertTitle, kUninstallAlertMessage, "Uninstall", "Cancel");

        if (_startUninstall)
        {

            foreach (string _eachFILE in kPluginFiles)
            {
                string _absolutePath = AssetPathToAbsolutePath(_eachFILE);

                if (File.Exists(_absolutePath))
                {
                    Delete(_absolutePath);

                    // Delete meta files.
                    if (File.Exists(_absolutePath + ".meta"))
                    {
                        Delete(_absolutePath + ".meta");
                    }
                }
            }

            foreach (string _eachFolder in kPluginFolders)
            {
                string _absolutePath = AssetPathToAbsolutePath(_eachFolder);

                if (Directory.Exists(_absolutePath))
                {
                    Directory.Delete(_absolutePath, true);

                    // Delete meta files.
                    if (File.Exists(_absolutePath + ".meta"))
                    {
                        Delete(_absolutePath + ".meta");
                    }
                }
            }
            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("ConsoliAds",
                                        "Uninstall successful!",
                                        "Ok");
        }
    }

    public static bool Unistall(string uninstallAlertTitle, string[] pluginFiles, string[] pluginFolders)
    {
        bool _startUninstall = EditorUtility.DisplayDialog(uninstallAlertTitle, kUninstallAlertMessage, "Uninstall", "Cancel");

        if (_startUninstall)
        {

            foreach (string _eachFILE in pluginFiles)
            {
                string _absolutePath = AssetPathToAbsolutePath(_eachFILE);

                if (File.Exists(_absolutePath))
                {
                    Delete(_absolutePath);

                    // Delete meta files.
                    if (File.Exists(_absolutePath + ".meta"))
                    {
                        Delete(_absolutePath + ".meta");
                    }
                }
            }

            foreach (string _eachFolder in pluginFolders)
            {
                string _absolutePath = AssetPathToAbsolutePath(_eachFolder);

                if (Directory.Exists(_absolutePath))
                {
                    Directory.Delete(_absolutePath, true);

                    // Delete meta files.
                    if (File.Exists(_absolutePath + ".meta"))
                    {
                        Delete(_absolutePath + ".meta");
                    }
                }
            }
            AssetDatabase.Refresh();
            return true;
        }
        return false;
    }

    #endregion
    public static string AssetPathToAbsolutePath(string _relativePath)
    {
        string _unrootedRelativePath = _relativePath.TrimStart('/');

        if (!_unrootedRelativePath.StartsWith(kAssets))
            return null;

        string _absolutePath = Path.Combine(GetProjectPath(), _unrootedRelativePath);

        // Return absolute path to asset
        return _absolutePath;
    }
    public static string GetProjectPath()
    {
        return Path.GetFullPath(Application.dataPath + @"/../");
    }
    public static void Delete(string _filePath)
    {
#if (UNITY_WEBPLAYER || UNITY_WEBGL)
			Debug.LogError("[CPFileOperations] File operations are not supported.");
#else
        File.Delete(_filePath);
#endif
    }

}

#endif