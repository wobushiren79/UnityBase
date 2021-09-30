using UnityEditor;
using UnityEngine;
using System.Collections;
using System;

public static class MonoExtension
{
    /// <summary>
    /// 延迟执行-秒
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="seconds"></param>
    /// <param name="action"></param>
    public static void DelayExecuteSeconds(this MonoBehaviour mono, float seconds, Action action)
    {
        mono.StartCoroutine(CoroutineForDelayExecuteSeconds(seconds, action));
    }

    /// <summary>
    /// 延迟执行-真实时间
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="seconds"></param>
    /// <param name="action"></param>
    public static void DelayExecuteSecondsRealtime(this MonoBehaviour mono, float seconds, Action action)
    {
        mono.StartCoroutine(CoroutineForDelayExecuteSecondsRealtime(seconds, action));
    }

    public static IEnumerator CoroutineForDelayExecuteSeconds(float timeWait, Action action)
    {
        yield return new WaitForSeconds(timeWait);
        action?.Invoke();
    }
    public static IEnumerator CoroutineForDelayExecuteSecondsRealtime(float timeWait, Action action)
    {
        yield return new WaitForSecondsRealtime(timeWait);
        action?.Invoke();
    }
}