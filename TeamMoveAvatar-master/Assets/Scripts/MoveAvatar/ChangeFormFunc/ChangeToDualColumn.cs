using UnityEngine;
using System.Collections;

public partial class MoveAvatar
{
    /// <summary>
    /// 两列纵队每个骨骼对应两个位置
    /// </summary>
    public void ChangeToDualColumn(int actorNum, Vector2 fleetCenterPos, Vector2 dir)
    {
        _InitCenterPosBone();
        _InitBone(actorNum, fleetCenterPos, dir);
        _InitViceBone(actorNum);
        _InitMuscle(actorNum);
    }

    /// <summary>
    /// 初始化确认中心位置的骨骼ID，
    /// </summary>
    private void _InitCenterPosBone()
    {
        centerPosBone1 = 1;
        centerPosBone2 = 1;
    }

    /// <summary>
    /// 初始化核心骨骼，使所有骨骼按照一字长蛇阵进行排列
    /// </summary>
    private void _InitBone(int actorNum, Vector2 enterPos, Vector2 dir)
    {
        firstBone = 0;
        boneNum = (actorNum + 1) / 2;//结果是 0，1
        //这里只是设置了第0个元素的位置和方向
        boneList[0].pos = enterPos;
        boneList[0].dir = dir;
        for (int i = 0; i < c_nMaxActorNum; i++)
        {
            boneList[i].lengthWithBefore = interval;
        }
        _InitBonePosInfo();
    }

    /// <summary>
    /// 初始化副骨骼，使队形保持两列纵队
    /// </summary>
    private void _InitViceBone(int actorNum)
    {
        viceNum = actorNum;
        for (int i = 0; i < c_nMaxActorNum; i++)
        {
            int index = i / 2;//主骨骼的id，要么是0，要么是1.

            Debug.Log("=2=init=======》当前主骨骼是：" + i + "；上一个主骨骼是：" + index);
            viceBoneList[i].SetBaseValue(index, GetBaseBone(index));
        }
    }

    /// <summary>
    /// 初始化肌肉，使每块肌肉绑定至每一块副骨骼上
    /// </summary>
    private void _InitMuscle(int actorNum)
    {
        muscleNum = actorNum;
        //主骨骼间距的一半
        float halfInterval = interval * 0.5f;
        for (int i = 0; i < c_nMaxActorNum; i++)
        {
            Debug.Log("=3=init=======》肌肉的 civeBoneId = " + i+ ", sinWithBoneDir = " + (i % 2 == 0 ? 1 : -1) + ", cosWithBoneDir = 0" + ", deltaLength = "  + halfInterval);
            muscleList[i].SetBaseValue(i, (i % 2 == 0 ? 1 : -1), 0, halfInterval);
        }
        _InitMusclePosInfo();
    }
}