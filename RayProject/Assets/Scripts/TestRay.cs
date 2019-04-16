using UnityEngine;
using System.Collections;

public class TestRay : MonoBehaviour
{
    public Camera m_Camera;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("xxxxxxxxxxxxxxxxxxxxxx");

            Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);//从摄像机发出到点击坐标的射线
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                Debug.DrawLine(ray.origin, hitInfo.point);//划出射线，在scene视图中能看到由摄像机发射出的射线
                GameObject gameObj = hitInfo.collider.gameObject;
                if (gameObj.name.StartsWith("Cube") == true)//当射线碰撞目标的name包含Cube，执行拾取操作
                {
                    Debug.Log(gameObj.name);
                }
            }
        }
    }
}