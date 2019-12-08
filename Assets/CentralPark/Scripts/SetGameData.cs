using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
           PlayerPrefs.SetString("Which_GameMode", "Arcade");
           PlayerPrefs.SetInt ("HowManyPlayers", 1) ;
           PlayerPrefs.SetInt ("DifficultyChoise", 1) ;
           PlayerPrefs.SetInt ("Player_0_CarLastSelection", 0) ;
           PlayerPrefs.SetInt ("Player_1_CarLastSelection", 0) ;
           PlayerPrefs.SetInt ("Player_2_CarLastSelection", 0) ;
           PlayerPrefs.SetInt ("Player_3_CarLastSelection", 0);
    }
}
