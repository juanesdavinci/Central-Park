using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedMeterUI : MonoBehaviour
{
    public Text SpeedText;
    public float multiplier;
    
    public void setSpeed(float speed)
    {
        SpeedText.text = Mathf.FloorToInt(speed * multiplier).ToString();
    }
}
