using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LaodLeaderboard : MonoBehaviour
{
    
    [Header("Leadeboard")]
    public int limit = 10;
    public List<ApiUser> leaderboard;
    public RectTransform group;
    

    public GameObject userPrefab;    
    // Start is called before the first frame update
    void Start()
    {
        GetLeaderboard();
    }
    
    public void GetLeaderboard()
    {
        CentralParkApiService.Instance.GetLeaderboard(this.limit, delegate (ApiLeaderboardResponse response)
        {
            //Success response
            this.leaderboard = response.leaderboard;
            this.PrintLeaderboard();

        }, delegate (Exception e)
        {
            Debug.LogError(e);
            //Do something.
            //This is triggered when HTTP request faild. That's also mean when don't have itnernet connection
        });
    }

    public void PrintLeaderboard()
    {
        foreach (var usr in leaderboard)
        {
            var g = Instantiate(userPrefab, @group);
            g.transform.Find("txtName").GetComponent<Text>().text = usr.username;
            g.transform.Find("txtScore").GetComponent<Text>().text = usr.score.ToString();

            var time = usr.timePlayed;
            string minutes = "";
            if(Mathf.Floor(time / 60) > 0 && Mathf.Floor(time / 60) < 10)
                minutes = Mathf.Floor(time / 60).ToString("0");
		
            if(Mathf.Floor(time / 60) >  10)
                minutes = Mathf.Floor(time / 60).ToString("00");
		
            string seconds = Mathf.Floor(time % 60).ToString("00");
            string milliseconds = Mathf.Floor((time*1000) % 1000).ToString("000");

            g.transform.Find("txtTime").GetComponent<Text>().text = minutes + ":" + seconds + ":" + milliseconds;
            
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    
}
