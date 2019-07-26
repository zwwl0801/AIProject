using UnityEngine;
using System.Collections;

public partial class MoveAvatar
{
    private void _InitBonePosInfo()
    {
        Vector2 dir = GetBaseBone(0).dir;
        for (int i = 1; i < boneList.Count; i++)
        {
            boneList[i].pos = boneList[i - 1].pos - dir * boneList[i].lengthWithBefore;

            Debug.Log("baseid = " + i + ",pos = " + boneList[i].pos +
                "ǰbaseid = " + (i - 1) + ",pos = " + boneList[i-1].pos +
                "dir = " + dir + ",boneList[i].lengthWithBefore = " + boneList[i].lengthWithBefore);
            boneList[i].dir = dir;
        }
    }

    private void _InitMusclePosInfo()
    {
        MoveMuscle musclePtr = null;
        MoveViceBone viceBonePtr = null;
        for (int i = 0; i < c_nMaxActorNum; i++)
        {
            musclePtr = GetMuscle(i);
            //��ȡ������
            viceBonePtr = GetViceBone(musclePtr.viceBoneId);
            Debug.Log("=4=init=======>>" + " musclePtr.viceBoneId = " + musclePtr.viceBoneId);
            //����
            musclePtr.dir = viceBonePtr.dir;
            Vector2 offsetDir = LogicMath.GetRotateNewPos(viceBonePtr.dir, musclePtr.sinWithBoneDir, musclePtr.cosWithBoneDir);
            //����λ��
            musclePtr.pos = viceBonePtr.pos + offsetDir * musclePtr.deltaLength;

            Debug.Log("=4=init=======>>" + " musclePtr.pos = " + musclePtr.pos + ", viceBonePtr.pos = " + viceBonePtr.pos + ", musclePtr.dir = " + musclePtr.dir + ",offsetDir = "+ offsetDir + ",MusclePos = " + musclePtr.pos);
        }
    }
}