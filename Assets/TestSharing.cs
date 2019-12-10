using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TestSharing : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SaveAndShareAndroid());
    }
        IEnumerator SaveAndShareAndroid()
        {
            yield return new WaitForEndOfFrame();
            var filename = "Central_Park" + System.DateTime.Now.ToString("dd-MM-yyyy-HH.mm.ss") + ".png";
            text.text = "1. " + filename + "\n";
            #if UNITY_EDITOR
                ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/" + filename);
            #else
                ScreenCapture.CaptureScreenshot(filename);
            #endif
            var imagePath = "";
            text.text += "2.  Screenshot complete\n";
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {        
                string dirPath = Application.persistentDataPath  ;//"/mnt/sdcard/DCIM/CentralPark/" ;
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
                text.text += "error: " + e.Message;
                
            }
                
#endif
                imagePath = Application.persistentDataPath + "/" + filename;
            
                text.text += "3. " + imagePath;
                yield return new WaitForSeconds(3);
                new NativeShare().AddFile(imagePath).Share();


        }
}
