using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    public void Scene(int i)
    {
       Invoke(nameof(this.gotoSC), 2f);
    }
    
    

    public void gotoSC()
    {
        SceneManager.LoadScene(2);
    }
}
