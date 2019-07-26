using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTest : MonoBehaviour
{
    public float speed = 1;
    public float rotateSpeed = 10;
    public Transform dirTrans;
    public Transform dir_1;
    public Transform dir_2;
    /// <summary>
    /// 代表着队员
    /// </summary>
    public List<Transform> actors;

    private MoveAvatar avatar;
    /// <summary>
    /// 通过世界空间的位置计算 矢量
    /// </summary>
    private Vector2 MoveDir
    {
        get
        {
            Vector2 dir = dir_2.position - dir_1.position;
            return dir.normalized;
        }
    }

    private void Start()
    {
        avatar = new MoveAvatar();
        avatar.ChangeToDualColumn(actors.Count, Vector2.one, MoveDir);
        SetPos();
    }

    private void Update()
    {
        avatar.Update(Time.deltaTime, speed, MoveDir);
        SetPos();
    }

    /// <summary>
    /// 设置了指针 旋转就是设置了指针的旋转
    /// </summary>
    private void OnGUI()
    {
        if (GUILayout.RepeatButton("Left"))
        {
            dirTrans.localEulerAngles += new Vector3(0, 0, rotateSpeed * Time.deltaTime);
        }
        if (GUILayout.RepeatButton("Right"))
        {
            dirTrans.localEulerAngles += new Vector3(0, 0, -rotateSpeed * Time.deltaTime);
        }
    }
    /// <summary>
    /// 设置队员的位置
    /// </summary>
    private void SetPos()
    {
        //遍历所有的队员，设置它的位置
        for (int i = 0; i < actors.Count && i < avatar.muscleList.Count; i++)
        {
            actors[i].localPosition = avatar.muscleList[i].pos;
        }
    }
}
