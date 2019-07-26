using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LogicMath
{
	#region Rotate
    /// <summary>
    /// 计算偏移后的位置
    /// </summary>
    /// <param name="oldPos"></param>
    /// <param name="sin"></param>
    /// <param name="cos"></param>
    /// <returns></returns>
	public static Vector2 GetRotateNewPos (Vector2 oldPos, double sin, double cos)
	{
		double x = oldPos.x * cos - oldPos.y * sin;
		double y = oldPos.x * sin + oldPos.y * cos;

        Debug.Log("=4=init=======》" + "offsetDir.x = " + x + ",offsetDir.y = " + y + ",oldPos = " + oldPos + ", cos = " + cos + ",sin = " + sin);
        return new Vector2 ((float) x, (float) y);
	}

	#endregion
}