using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<ICleverUnit, Result>
{
    private readonly List<IState<ICleverUnit, Result>> subStates;
    public PatrolState()
    {
        //实例化后将子状态，放入到列表中
        subStates = new List<IState<ICleverUnit, Result>>()
            {
                new MoveToState(),
                new IdleState(),
            };
    }
    public Result Drive(Context<ICleverUnit> ctx)
    {
        //根据检查
        var unit = ctx.Self.GetNearestTarget();
        if (unit != null)
        {
            ctx.Self.LockTarget(unit);

            return Result.Success;
        }

        var nextStep = 0;
        if (ctx.Continuation != null)
        {
            // 检查没有结果，进入子状态
            var thisContinuation = ctx.Continuation;

            ctx.Continuation = thisContinuation.SubContinuation;
            //执行第一个子任务
            var ret = subStates[nextStep].Drive(ctx);

            if (ret == Result.Continue)
            {
                thisContinuation.SubContinuation = ctx.Continuation;
                ctx.Continuation = thisContinuation;

                return Result.Continue;
            }
            else if (ret == Result.Failure)
            {
                ctx.Continuation = null;

                return Result.Failure;
            }

            ctx.Continuation = null;
            nextStep = thisContinuation.NextStep + 1;
        }
        //遍历其他的子任务
        for (; nextStep < subStates.Count; nextStep++)
        {
            var ret = subStates[nextStep].Drive(ctx);
            if (ret == Result.Continue)
            {
                ctx.Continuation = new Continuation()
                {
                    SubContinuation = ctx.Continuation,
                    NextStep = nextStep,
                };

                return Result.Continue;
            }
            else if (ret == Result.Failure)
            {
                ctx.Continuation = null;

                return Result.Failure;
            }
        }

        ctx.Continuation = null;

        return Result.Success;
    }
}

public enum Result
{
    Success,
    Failure,
    Continue,
}