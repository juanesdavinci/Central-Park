using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaodLeaderboard : MonoBehaviour
{
    private ApiTest api;
    
    // Start is called before the first frame update
    void Start()
    {
        api.GetLeaderboard();


        foreach (var usr in api.leaderboard)
        {
            print(usr);
        }
    }

}
