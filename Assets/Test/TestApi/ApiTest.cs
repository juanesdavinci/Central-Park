using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApiTest : MonoBehaviour
{

    [Header("Add user")]
    public string username = "userTest";
    public int score = 10;
    public ApiUser userSaved;

    [Header("Leadeboard")]
    public int limit = 10;
    public List<ApiUser> leaderboard;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            this.SaveUser();

        if (Input.GetKeyDown(KeyCode.L))
            this.GetLeaderboard();
    }

    private void SaveUser()
    {
        CentralParkApiService.Instance.AddUser(this.username, this.score, delegate (ApiUserResponse response)
        {
            //Success response
            this.userSaved = response.user;

        }, delegate (Exception e)
        {
            Debug.LogError(e);
            //Do something.
            //This is triggered when HTTP request faild. That's also mean when don't have itnernet connection
        });
    }

    private void GetLeaderboard()
    {
        CentralParkApiService.Instance.GetLeaderboard(this.limit, delegate (ApiLeaderboardResponse response)
        {
            //Success response
            this.leaderboard = response.leaderboard;

        }, delegate (Exception e)
        {
            Debug.LogError(e);
            //Do something.
            //This is triggered when HTTP request faild. That's also mean when don't have itnernet connection
        });
    }
}
