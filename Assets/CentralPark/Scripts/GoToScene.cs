﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    public void Scene(int i)
    {
        gotoSC(i);
    }
    
    

    public void gotoSC(int i )
    {
        SceneManager.LoadScene(i);
    }
}
