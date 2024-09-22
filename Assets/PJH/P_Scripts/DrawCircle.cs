using System.Collections;
using System.Collections.Generic;
using System.Data;
//using Unity.Android.Types;
using UnityEngine;

public class DrawCircle : MonoBehaviour // 삼각함수로 원그리기
{
    public float radius = 5f;
    public int segments = 100;
    public LineRenderer lineRenderer;



    void Start()
    {
        
    }

    void Update()
    {
        
    }


    void Draw()
    {
        lineRenderer.positionCount = segments + 1;
        // 현재 각도 초기화
        float angle = 0f;
        float angleStep = 360f / segments;

        for(int i = 0; i < segments; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, y, 0f));

            angle += angleStep;
        }
    }


}
