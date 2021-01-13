using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;


public class LbzzzAds : MonoBehaviour {
    // For Mechanic Game Studio
   // string url = "http://lbzzztech.com/GameData/";
    // For Superhero Action Games
    //string url = "http://lbzzztech.com/GameDataSHAG/";
    // For Lethal Game Studio
    string url = "http://lbzzztech.com/GameDataLGS/";
    // For IOS
    //string url = "http://lbzzztech.com/GameDataIOS/";

    string TextFileName = "GameList.html";
    string LinkFileName = "GameLink.html";
    string ImageExtension = ".png";
    string FullText,FullLink;
    int GameIndex=0;
    public static bool InternetConnection = false;

    public  struct Instance0
    {
        public static string Name { get; set; }
        public static Sprite sprite { get; set; }
        public static string link { get; set; }
    }
    public struct Instance1
    {
        public static string Name { get; set; }
        public static Sprite sprite { get; set; }
        public static string link { get; set; }
    }
    public struct Instance2
    {
        public static string Name { get; set; }
        public static Sprite sprite { get; set; }
        public static string link { get; set; }
    }
	void Awake(){
		DontDestroyOnLoad(this.gameObject);
	}
    IEnumerator Start()
    {
       
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
            InternetConnection = false;
        }
        else
        {
            InternetConnection = true;
            ////////////////////////// Fetch Provided Index Name//////////////////////////////
            string TextUrl = url + TextFileName;
            using (WWW www = new WWW(TextUrl))
            {
                yield return www;
                FullText = www.text;
                string[] NamesArray = FullText.Split(
                                        new[] { "\r\n", "\r", "\n" },
                                        StringSplitOptions.None);
                // Debug.Log("FirstNameLoaded"+ DateTime.Now.ToLongTimeString());
                Instance0.Name = NamesArray[0];
                Instance1.Name = NamesArray[1];
                Instance2.Name = NamesArray[2];
            }

            ////////////////////////// Fetch Provided Index Image//////////////////////////////
            {
                Texture2D tex;
                tex = new Texture2D(185, 185, TextureFormat.DXT1, false);
                string ImageName = Instance0.Name;
                ImageName = Regex.Replace(ImageName, @"\s+", "");
                string ImageUrl = url + ImageName + ImageExtension;
                using (WWW www = new WWW(ImageUrl))
                {
                    yield return www;
                    www.LoadImageIntoTexture(tex);
                    Debug.Log("First Image Loaded " + DateTime.Now);
                    Instance0.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height),
                        new Vector2(0.5f, 0.5f));
                }
            }
            ////////////////////////// Fetch Provided Index Link//////////////////////////////
            string LinkUrl = url + LinkFileName;
            using (WWW www = new WWW(LinkUrl))
            {
                yield return www;
                FullLink = www.text;
                string[] LinksArray = FullLink.Split(
                                        new[] { "\r\n", "\r", "\n" },
                                        StringSplitOptions.None);
                // Debug.Log("First Link Loaded "+ DateTime.Now);
                Instance0.link = LinksArray[0];
                Instance1.link = LinksArray[1];
                Instance2.link = LinksArray[2];
            }
            {
                Texture2D tex1;
                tex1 = new Texture2D(185, 185, TextureFormat.DXT1, false);
                string ImageName1 = Instance1.Name;
                ImageName1 = Regex.Replace(ImageName1, @"\s+", "");
                string ImageUrl1 = url + ImageName1 + ImageExtension;
                using (WWW www = new WWW(ImageUrl1))
                {
                    yield return www;
                    www.LoadImageIntoTexture(tex1);
                    Debug.Log("Two Image Loaded " + DateTime.Now);
                    Instance1.sprite = Sprite.Create(tex1, new Rect(0, 0, tex1.width, tex1.height),
                        new Vector2(0.5f, 0.5f));
                }
            }
            {
                Texture2D tex1;
                tex1 = new Texture2D(185, 185, TextureFormat.DXT1, false);
                string ImageName1 = Instance2.Name;
                ImageName1 = Regex.Replace(ImageName1, @"\s+", "");
                string ImageUrl1 = url + ImageName1 + ImageExtension;
                Debug.Log(ImageUrl1);
                using (WWW www = new WWW(ImageUrl1))
                {
                    yield return www;
                    www.LoadImageIntoTexture(tex1);
                    Debug.Log("Three Image Loaded " + DateTime.Now);
                    Instance2.sprite = Sprite.Create(tex1, new Rect(0, 0, tex1.width, tex1.height),
                        new Vector2(0.5f, 0.5f));
                }
            }
        }
    }
}
