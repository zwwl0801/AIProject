using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSmMgr
{
    //状态堆栈
    //如果用列表来做的话，index为0的就表示栈顶
    List<UnitSMBase> _smList;
    Dictionary<UnitState, UnitSMBase> _smStateDict;

    public UnitSmMgr(UnitSMBase initSM, UnitState initState)
    {
        _smList = new List<UnitSMBase>();
        _smList.Add(initSM);

        _smStateDict = new Dictionary<UnitState, UnitSMBase>();
        _smStateDict[initState] = initSM;

        initSM.Enter();
    }

    public void RegisterSM(UnitSMBase sm, UnitState state)
    {
        _smStateDict[state] = sm;
    }

    public void ProcessEvent(UnitEvent evt)
    {
        _smList[0].ProcessEvent(evt);
        //先执行栈顶的状态机，根据状态返回的信息，决定下一个状态。
        switch (_smList[0].change)
        {
            case StateChange.Enter://加入新的栈顶元素
                //新状态一直是放到栈顶，
                _smList.Insert(0, _smStateDict[_smList[0].state]);
                _smList[0].Enter();
                break;

            case StateChange.Switch://替换栈顶元素
                //即先退出当前状态，再进入目标状态
                _smList[0].Exit();//栈顶元素退出
                _smList[0] = _smStateDict[_smList[0].state];//替换栈顶元素
                _smList[0].Enter();//栈顶元素进入
                break;

            case StateChange.Exit://栈顶元素退出
                _smList[0].Exit();//栈顶元素退出
                _smList.RemoveAt(0);//移除栈顶元素
                break;
        }

        if (0 == _smList.Count)
        {
            //状态机空了
            Debug.LogError("state machine is empty");
        }
    }
}