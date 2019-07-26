using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>移动核心</para>
/// <para></para>
/// <para>内部《集成》移动的主骨骼、副骨骼、肌肉。</para>
/// <para>主骨骼成一字长蛇阵移动</para>
/// <para>副骨骼依托主骨骼移动。当前副骨骼移动模式为跟随，可根据需求定制副骨骼移动策略。</para>
/// <para>肌肉完全依托副骨骼，根据副骨骼方向与位置进行偏移求得具体位置与方向信息</para>
/// 
/// </summary>
public partial class MoveAvatar
{
    /// <summary>
    /// 阵型支持最大人数
    /// </summary>
    const int c_nMaxActorNum = 6;

    /// <summary>
    /// 主骨骼间距
    /// </summary>
    private float interval;

    public int firstBone;
    public int boneNum;
    public List<MoveBone> boneList;

    public int viceNum;
    public List<MoveViceBone> viceBoneList;

    public int muscleNum;
    public List<MoveMuscle> muscleList;

    public int centerPosBone1;
    public int centerPosBone2;

    public MoveAvatar()
    {
        interval = 20;

        firstBone = 0;

        boneNum = c_nMaxActorNum;
        viceNum = c_nMaxActorNum;
        muscleNum = c_nMaxActorNum;

        boneList = new List<MoveBone>();
        viceBoneList = new List<MoveViceBone>();
        muscleList = new List<MoveMuscle>();
        //根据设置的阵营支持的最大人数，实例化，放到数组中进行管理
        for (int i = 0; i < c_nMaxActorNum; i++)
        {
            //移动主骨骼
            boneList.Add(new MoveBone());
            //移动副骨骼
            viceBoneList.Add(new MoveViceBone());
            //对于副骨骼进行的偏移，队员实际站立的位置
            muscleList.Add(new MoveMuscle());
        }
    }

    #region 移动更新骨骼、副骨骼、肌肉数据

    public void Update(float delta_s, float speed, Vector2 dir)
    {
        UpdateBone(delta_s, speed, dir);
        UpdateViceBone();
        UpdateMuscle();
    }

    /// <summary>
    /// 更新主骨骼层数据
    /// </summary>
    /// <param name="delta_s">时间，单位秒.</param>
    /// <param name="speed">速度.</param>
    /// <param name="dir">方向.</param>
    private void UpdateBone(float delta_s, float speed, Vector2 dir)
    {
        MoveBone bonePtr = GetBaseBone(firstBone);
        MoveBone beforeBonePtr = null;
        //主骨骼的位置
        bonePtr.pos = bonePtr.pos + dir * (speed * delta_s);
        //主骨骼的方向
        bonePtr.dir = dir;
        for (int i = firstBone + 1, imax = firstBone + boneNum; i < imax; i++)
        {
            //获取当前主骨骼
            bonePtr = GetBaseBone(i);
            //获取上一个主骨骼
            beforeBonePtr = GetBaseBone(i - 1);

            //求取上一个骨骼和当前骨骼的方向
            Vector2 fNDir = (beforeBonePtr.pos - bonePtr.pos).normalized;
            //根据和上一个主骨骼的间距，求得现在位置
            Debug.Log("=1=update=======》当前主骨骼是：" + i + "；上一个主骨骼是：" + (i - 1) + "；bonePtr.lengthWithBefore:" + bonePtr.lengthWithBefore);
            bonePtr.pos = beforeBonePtr.pos - fNDir * bonePtr.lengthWithBefore;
            bonePtr.dir = fNDir;
        }
    }

    private void UpdateViceBone()
    {
        MoveViceBone vicePtr = null;
        for (int i = 0; i < viceNum; i++)
        {
            vicePtr = GetViceBone(i);
            Debug.Log("=2=update=======》当前副骨骼是：" + i);
            SetViceBoneMoveDataWhenFollow(vicePtr);
        }
    }

    private void SetViceBoneMoveDataWhenFollow(MoveViceBone vicePtr)
    {
        Debug.Log("=2=update=======》前副骨骼追随的主骨骼是：" + vicePtr.baseBoneId);
        MoveBone bonePtr = GetBaseBone(vicePtr.baseBoneId);
        vicePtr.pos = bonePtr.pos;
        vicePtr.dir = bonePtr.dir;
    }

    private void UpdateMuscle()
    {
        MoveViceBone viceBonePtr = null;
        MoveMuscle musclePtr = null;
        for (int i = 0; i < muscleNum; i++)
        {
            musclePtr = GetMuscle(i);
            viceBonePtr = GetViceBone(musclePtr.viceBoneId);
            //获取主骨骼
            MoveBone baseBone = GetBaseBone(viceBonePtr.baseBoneId);
            musclePtr.dir = baseBone.dir;
            Vector2 offsetDir = LogicMath.GetRotateNewPos(baseBone.dir, musclePtr.sinWithBoneDir, musclePtr.cosWithBoneDir);
            Debug.Log("=2=update=======》肌肉追随的副骨骼是：" + viceBonePtr.baseBoneId);
            //计算队员的位置
            musclePtr.pos = baseBone.pos + offsetDir * musclePtr.deltaLength;
        }
    }

    #endregion

    /// <summary>
    /// 获取队伍中心点位置
    /// 通过设置代表中心点的两根骨骼计算得到
    /// 使用两根骨骼是为了在队伍有偶数排的情况下准确计算
    /// </summary>
    /// <returns>The center position.</returns>
    public Vector2 GetCenterPos()
    {
        if (centerPosBone1 != centerPosBone2)
        {
            return (GetBaseBone(centerPosBone1).pos + GetBaseBone(centerPosBone2).pos) * 0.5f;
        }
        else
        {
            return GetBaseBone(centerPosBone1).pos;
        }
    }
    /// <summary>
    /// 根据id号获取主骨骼
    /// </summary>
    /// <param name="baseBoneId"></param>
    /// <returns></returns>
    private MoveBone GetBaseBone(int baseBoneId)
    {
        return boneList[baseBoneId];
    }

    private MoveViceBone GetViceBone(int viceBoneId)
    {
        return viceBoneList[viceBoneId];
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="muscleId"></param>
    /// <returns></returns>
    private MoveMuscle GetMuscle(int muscleId)
    {
        //根据id获取每个位置
        return muscleList[muscleId];
    }
}