using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveFiveRight : MonoBehaviour
{
    // lerp를 쓰지 않고 정확히 5 만큼 움직이기
    // p = p0 + vt
    float speed = 0.1f;
    float finalSpeed;
    float currentTime = 0;

    // Update는 주기가 조금씩 차이나므로
    private void FixedUpdate()
    {
        //finalSpeed = speed * currentTime <= 5.0f ? 
        if (speed*currentTime < 5.0f)
        {
            currentTime += Time.deltaTime;
            transform.position += new Vector3 (speed * currentTime, 0, 0);
        }
        else if (speed * currentTime > 5.0f)
        {

        }
    }
}
