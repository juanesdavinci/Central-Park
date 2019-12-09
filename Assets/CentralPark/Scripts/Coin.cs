using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public static Coin Instance;

    public int coins;
    public Text coinsText;
    
    
    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    public void AddCoin()
    {
        coins++;
        coinsText.text = "" + coins;
    }
    
}
