using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour
{
    public string LevelName;
    public Image LoadingBarFill;
    float last;
    public bool IsCircular;
    public bool OnActive;
    public bool IsAlpha;
    float val = 0.0f;
    bool left, right;
    // Use this for initialization
    void OnEnable()
    {
        if (OnActive)
            StartCoroutine(LevelCoroutine(LevelName));
        right = true;
    }

    public IEnumerator LevelCoroutine(System.String nomScene)
    {
        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(3);  
        AsyncOperation async = SceneManager.LoadSceneAsync(nomScene);
        while (!async.isDone)
        {
            last = async.progress;
            if (last >= async.progress && !IsCircular && !IsAlpha)
                LoadingBarFill.fillAmount = async.progress / 0.9f; //Async progress returns always 0 here 
            if (IsCircular)
            {
                LoadingBarFill.gameObject.transform.eulerAngles -= new Vector3(0, 0, 3);//LoadingBarFill.gameObject.transform.eulerAngles.z - 5);
            }
            if (IsAlpha)
            {
                //LoadingBarFill.CrossFadeAlpha() 
                var color = LoadingBarFill.color;


                if (val <= 1.0f && right)
                {
                    val += 0.5f;
                    if (val == 1)
                    {
                        right = false;
                        left = true;
                    }
                    color.a = val;
                    LoadingBarFill.color = color;
                }
                else if (val >= 0.0f && left)
                {
                    val -= 0.5f;
                    if (val == 0)
                    {
                        right = true;
                        left = false;
                    }
                    color.a = val;
                    LoadingBarFill.color = color;
                }

            }
            yield return null;

        }
    }

}
