using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ApiTest : MonoBehaviour
{

    [Header("Add user")] public InputField username;
    public string usrname;
    public int score = 10;
    public int time = 10;
    public ApiUser userSaved;

    public Image loading;
    public Text errorText;
    private bool sending;

    [Header("Leadeboard")]
    public int limit = 10;
    public List<ApiUser> leaderboard;

    void Start()
    {
        
    }

    private void Update()
    {
        if (sending)
        {
            var rot = loading.transform.rotation;
            rot.eulerAngles = Vector3.forward * Time.deltaTime * 10;
            loading.transform.rotation = rot;
        }
    }

    public void SaveUser()
    {
        usrname = this.username.text;
        loading.gameObject.SetActive(true);
        sending = true;
        CentralParkApiService.Instance.AddUser(this.usrname, this.score, this.time, delegate (ApiUserResponse response)
        {
            //Success response
            this.userSaved = response.user;
            this.loading.gameObject.SetActive(true);
            SceneManager.LoadScene(3);

        }, delegate (Exception e)
        {
            Debug.LogError(e);
            loading.gameObject.SetActive(true);
            errorText.gameObject.SetActive(true);
            errorText.text = "No se pudo conectar a la base de datos inténtelo de nuevo más tarde";
            //Do something.
            //This is triggered when HTTP request faild. That's also mean when don't have itnernet connection
        });
    }

    public void GetLeaderboard()
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
