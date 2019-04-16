using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateChange
{
    None,
    Switch,
    Enter,
    Exit,
}
public class UnitSMBase
{
    public UnitState state;
    public StateChange change;

    float _deltaTime;
    protected float _checkTime;
    protected Unit _unit;
    private UnitEvent UnitEvent;
    public UnitSMBase(Unit unit)
    {
        _unit = unit;
        _deltaTime = 0;
    }

    public virtual void Enter()
    {
        _checkTime = 1;
    }

    public virtual void Exit()
    {
    }

    public void ProcessEvent(UnitEvent evt)
    {
        state = UnitState.None;
        change = StateChange.None;

        if (evt == UnitEvent.Update)
        {
            if (_deltaTime >= _checkTime)
            {
                _deltaTime = 0;
                evt = UnitEvent.UpdateFixTime;
            }
            else
            {
                _deltaTime += Time.deltaTime;
            }
        }

        DoProcess(evt);
    }

    protected virtual void DoProcess(UnitEvent evt)
    {
    }

    public virtual bool CanGo(UnitState unitState)
    {
        return true;
    }
}