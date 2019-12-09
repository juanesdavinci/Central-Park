using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    public Animator anim;

    private float prevDir = 0;

    public void SetDirection(float turnDirection)
    {
        prevDir = Mathf.MoveTowards(prevDir, turnDirection, Time.deltaTime * 3);
        
        anim.SetFloat("direction", prevDir);
    }
    
    public void SetSpeed(float speed)
    {
        anim.SetFloat("speed", speed);
    }
}
