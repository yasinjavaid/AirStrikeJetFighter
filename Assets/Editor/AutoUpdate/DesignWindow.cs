using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SimpleJSON;

public class DesignWindow : EditorWindow {
	
	Texture2D headerTexture;
	Texture2D footerTexture;

	Texture2D[] infoTextures;

	readonly Color headerColor = Color.gray;
	readonly Color infoColor = Color.gray;

	Rect headerRect;
	Rect footerRect;

	Rect[] infoRects;

	List<string> adNetworkTitles = new List<string>();
	string patchURL;

	#region DemoProgressBar
	readonly float secs = 5.0f;
	readonly float startValue = 1.0f;
	public static float progress = 0.0f;
	public static bool isFinished = false;
	#endregion

	public static void OpenWindow(JSONNode response)
	{
		DesignWindow window = (DesignWindow)EditorWindow.GetWindow(typeof(DesignWindow), true, "ConsoliAds Update");

		if (window == null)
        {
			Debug.Log("_instance is null ");
			return;
		}
		window.minSize = new Vector2 (400,400);
		window.maxSize = new Vector2 (800,800);
		window.populateResponse(response);
		window.InitTextures();
		window.Show();
	}

	public static void setProgress(float p)
	{
		progress = p;
	}

	public static void setDownloadingFinished(bool finshed)
	{
		isFinished = finshed;
	}

	public bool populateResponse(JSONNode response)
	{
		adNetworkTitles = new List<string>();
		string adNetworks = response["mediation_patch_adnetworks"];
		Debug.Log("adNetworks " + adNetworks);
		string[] addNetowrkList = adNetworks.Split(","[0]);
		for (int sequenceCounter = 0; sequenceCounter < addNetowrkList.Length; sequenceCounter++)
		{
			adNetworkTitles.Add(addNetowrkList[sequenceCounter]);
			Debug.Log(adNetworkTitles[sequenceCounter]);
		}

		patchURL = response["patch_url"];//"https://s3-us-west-2.amazonaws.com/elasticbeanstalk-us-west-2-013548244107/sdk/v.2.0.1/Installable_Plugins/leadbolt.unitypackage";//response["patch_url"]
		PackageDownloader.aipFileName = patchURL.Substring(patchURL.LastIndexOf("/") + 1);
		PackageDownloader.fileURL = patchURL;

		return true;
	}

	void InitTextures()
	{
		infoTextures = new Texture2D[this.adNetworkTitles.Count];
		infoRects = new Rect[this.adNetworkTitles.Count];

		headerTexture = new Texture2D (1, 1);
		headerTexture.SetPixel (0,0,headerColor);
		headerTexture.Apply ();

		for (int i = 0; i < infoTextures.Length; i++) 
		{
			infoTextures [i] = new Texture2D (1,1);
			infoTextures [i].SetPixel (0, 0, infoColor);
			infoTextures [i].Apply ();
		}

		footerTexture = new Texture2D (1, 1);
		footerTexture.SetPixel (0,0,headerColor);
		footerTexture.Apply ();
	}

	void OnGUI()
	{
		DrawLayouts ();
		DrawHeader ();
		DrawProgressBar();
	}

	void DrawProgressBar()
	{
		if (progress > 0.0 && !isFinished )
		{
			EditorUtility.DisplayProgressBar ("Downloading Patch", "Downloading in Progress...", progress / 100f);
		}
		else
		{
			if (isFinished)
			{
				
				EditorUtility.ClearProgressBar();
			}
		}
	}

	void DrawLayouts()
	{
		headerRect.x = 0;
		headerRect.y = 0;
		headerRect.width = Screen.width;
		headerRect.height = 30;
		GUI.DrawTexture (headerRect,headerTexture);

		float h = 3;
		for (int i = 0; i < infoTextures.Length; i++) 
		{
			infoRects [i].x = 0;
			infoRects [i].y = 35 + h;
			infoRects [i].width = Screen.width;
			infoRects [i].height = 30;
			GUI.DrawTexture (infoRects [i],infoTextures[i]);
			h = infoRects [i].y;
			GUILayout.BeginArea (infoRects[i]);
			GUILayout.BeginHorizontal ();
			GUILayout.Label (adNetworkTitles[i].ToUpper(),EditorStyles.boldLabel);
			GUILayout.EndHorizontal ();
			GUILayout.EndArea ();
		}

		footerRect.x = 0;
		footerRect.y = 60 + h;
		footerRect.width = Screen.width;
		footerRect.height = 25;
		GUI.DrawTexture (footerRect,footerTexture);

		GUILayout.BeginArea (footerRect);
		GUILayout.BeginHorizontal ();

		if (GUILayout.Button ("Download Patch")) 
		{
			try
			{
				PackageDownloader.downloadAndInstallAIP();
				EditorUtility.DisplayProgressBar ("Downloading Patch", "Downloading in Progress...", 0.1f / 100f);
			}
			catch (System.Exception exp)
			{
				EditorUtility.ClearProgressBar ();	
			}
		}

		if (GUILayout.Button ("Adnetwork plugins")) {
			Application.OpenURL("https://portal.consoliads.com/download/adnetworksdk"); 
		}

		if (GUILayout.Button ("Cancel")) {
			EditorUtility.ClearProgressBar ();
			this.Close();
		}

		GUILayout.EndHorizontal ();
		GUILayout.EndArea ();
	}

	void DrawHeader()
	{
		GUILayout.BeginArea (headerRect);
		GUILayout.Label ("Click download patch to enable the following available new adnetworks",EditorStyles.boldLabel);
		GUILayout.EndArea ();
	}

	void OnInspectorUpdate()
	{
		Repaint ();
	}

}
