using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Debug
{
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Log(object msg) =>
         UnityEngine.Debug.Log(msg);
    public static void LogWarning(object msg) =>
         UnityEngine.Debug.LogWarning(msg);
    public static void LogError(object msg) =>
        UnityEngine.Debug.LogError(msg);
    public static void LogFormat(string msg, params object[] var) =>
        UnityEngine.Debug.LogFormat(msg, var);
}
