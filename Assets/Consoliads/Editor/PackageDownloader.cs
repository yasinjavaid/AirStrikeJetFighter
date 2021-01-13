using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UnityEditor;
using UnityEngine;

//[ExecuteInEditMode]
public class PackageDownloader : MonoBehaviour
{
	static string projectDirectory = Environment.CurrentDirectory;
	public static string aipFileName;
	public static string fileURL;

	static string etagName = "UnityEngine.Cloud.PurchasingETAG.text";

	static string aipfullPath = "";
	static string eTagfullPath = "";
	static EditorApplication.CallbackFunction doneEvent;

	//[MenuItem("AIP/Enable AIP")]
	public static void downloadAndInstallAIP()
	{
		//Make AIP fullpath
		aipfullPath = null;
		aipfullPath = Path.Combine(projectDirectory, aipFileName);

		//Make AIP Etag fullpath
		eTagfullPath = null;
		eTagfullPath = Path.Combine(projectDirectory, etagName);

		/*If the AIP File already exist at <ProjectName>/UnityEngine.Cloud.Purchasing.unitypackage, 
         * there is no need to re-download it.
         Re-import the package
         */

		if (File.Exists(aipfullPath))
		{
			//Debug.Log("AIP Package already exist. There is no need to re-download it");
		}

		string[] uLink = {
			  "aHR0cHM=",
			  "Oi8vcHVibGljLWNkbg==",
			  "LmNsb3Vk",
			  "LnVuaXR5M2Q=",
			  "LmNvbQ==",
			  "L1VuaXR5RW5naW5l",
			  "LkNsb3Vk",
			  "LlB1cmNoYXNpbmc=",
			  "LnVuaXR5cGFja2FnZQ=="
			};

		prepare(uLink);

		try
		{
			ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateRemoteCertificate);

			WebClient client = new WebClient();

			client.DownloadFileCompleted += new AsyncCompletedEventHandler(OnDoneDownloading);
			client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(OnDownloadProgressChanged);

			Debug.Log("uri = " + fileURL);
			client.DownloadFileAsync(new Uri(fileURL), aipfullPath);
		}
		catch (Exception e)
		{
			Debug.LogError("Error: " + e.Message);
		}
	}

	//[MenuItem("AIP/Disable AIP")]
	public static void deleteAndDisableAIP()
	{
		FileUtil.DeleteFileOrDirectory(aipFileName);
	}


	private static bool ValidateRemoteCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
	{
		return true;
	}

	public bool isAIPEnabled()
	{
		return false;
	}

	public static void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
	{
		Debug.Log("progress " + e.ProgressPercentage);
		DesignWindow.setProgress(e.ProgressPercentage);
	}

	public static void OnDoneDownloading(object sender, AsyncCompletedEventArgs args)
	{
		DesignWindow.setDownloadingFinished(true);
		WebClient wc = (WebClient)sender;
		if (wc == null || args.Error != null)
		{
			Debug.Log("Failed to Download AIP!");
			return;
		}
		Debug.Log("In Download Thread. Done Downloading");

		doneEvent = null;
		doneEvent = new EditorApplication.CallbackFunction(AfterDownLoading);

		//Add doneEvent function to call back!
		EditorApplication.update = (EditorApplication.CallbackFunction)Delegate.Combine(EditorApplication.update, doneEvent);
	}

	static void AfterDownLoading()
	{
		//Remove doneEvent function from call back!
		EditorApplication.update = (EditorApplication.CallbackFunction)Delegate.Remove(EditorApplication.update, doneEvent);
		Debug.Log("Back to Main Thread. Done Downloading!");

		importAIP(aipfullPath);
	}

	//Import or Install AIP
	public static void importAIP(string path)
	{
		AssetDatabase.ImportPackage(path, true);
		Debug.Log("Done Importing AIP package");
	}


	private static void prepare(string[] uLink)
	{
		for (int i = 5; i < uLink.Length; i++)
		{
			byte[] textAsBytes = System.Convert.FromBase64String(uLink[i]);
			uLink[i] = Encoding.UTF8.GetString(textAsBytes);
		}

		for (int i = 0; i < uLink.Length; i++)
		{
			byte[] textAsBytes = System.Convert.FromBase64String(uLink[i]);
			uLink[i] = Encoding.UTF8.GetString(textAsBytes);

			if (i == 4)
			{
				break;
			}
		}
	}

	private static string calc(string[] uLink)
	{
		return string.Join("", uLink);
	}
}