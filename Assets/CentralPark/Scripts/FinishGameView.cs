using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class FinishGameView : MonoBehaviour
{
    public Text scoreTxt;
    public Text timeTxt;
    public Text ResoultsTxt;

    public LapCounter LapCounter;
    public ApiTest ApiTest;
    public GameObject window;
    public GameObject infoWindow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        ResoultsTxt.text = LapCounter.gameManager.winningCar.playerNumber == 1 ? "Ganaste" : "Inténtalo nuevamente";
        
        var score = Coin.Instance.coins;
        scoreTxt.text = "" + score;

        var time = LapCounter.Timer;
        
        string minutes = "";
        if(Mathf.Floor(time / 60) > 0 && Mathf.Floor(time / 60) < 10)
            minutes = Mathf.Floor(time / 60).ToString("0");
		
        if(Mathf.Floor(time / 60) >  10)
            minutes = Mathf.Floor(time / 60).ToString("00");
		
        string seconds = Mathf.Floor(time % 60).ToString("00");
        string milliseconds = Mathf.Floor((time*1000) % 1000).ToString("000");


            if(Mathf.Floor(time / 60) == 0)
                timeTxt.text = seconds + ":" + milliseconds;
            else
                timeTxt.text = minutes + ":" + seconds + ":" + milliseconds;

        ApiTest.score = score;
        ApiTest.time = (int)time;
    }


    public void ShareScreenShot()
    {
        StartCoroutine(ReloadScreen());
    }

    IEnumerator ReloadScreen()
    {
        window.SetActive(false);
        infoWindow.SetActive(true);
        yield return new WaitForEndOfFrame();
        var filename = "Central_Park" + System.DateTime.Now.ToString("dd-MM-yyyy-HH.mm.ss") + ".png";
#if UNITY_EDITOR
        ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/" + filename);
#else
                ScreenCapture.CaptureScreenshot(filename);
            #endif
        var imagePath = "";
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {        
                string dirPath = Application.persistentDataPath;
                imagePath= dirPath + filename;
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
        
                AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaClass classUri = new AndroidJavaClass("android.net.Uri");
                AndroidJavaObject objIntent =
                new AndroidJavaObject("android.content.Intent", new object[2] { "android.intent.action.MEDIA_SCANNER_SCAN_FILE", classUri.CallStatic<AndroidJavaObject>("parse", "file://" + imagePath) });
                objActivity.Call("sendBroadcast", objIntent);
            }
            catch (Exception e)
            {
                
            }
                
#endif
        imagePath = Application.persistentDataPath + "/" + filename;
            
        yield return new WaitForSeconds(3);
        new NativeShare().AddFile(imagePath).Share();


        window.SetActive(true);
        infoWindow.SetActive(false);
    }
}
