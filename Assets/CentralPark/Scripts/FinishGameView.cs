using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishGameView : MonoBehaviour
{
    public Text scoreTxt;
    public Text timeTxt;
    public Text ResoultsTxt;

    public LapCounter LapCounter;
    public ApiTest ApiTest;
    
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
}
