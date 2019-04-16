using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGizmos : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;//为随后绘制的gizmos设置颜色。
        //Gizmos.DrawWireSphere(transform.position, .25f);//使用center和radius参数，绘制一个线框球体。
        Gizmos.DrawLine(startPoint.position, endPoint.position);
    }
}
