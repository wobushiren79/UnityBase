using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Test : BaseMonoBehaviour
{

    private void OnGUI()
    {
        if (GUILayout.Button("Test"))
        {
            EventHandler.Instance.TriggerEvent("Test");
            EventHandler.Instance.TriggerEvent("Test2","sadfsaf");
        }

    }

    private void Start()
    {
        EventHandler.Instance.RegisterEvent("Test", TestEvent);
        EventHandler.Instance.RegisterEvent<string>("Test2", TestEvent2);
    }

    public void TestEvent()
    {
        LogUtil.Log("1");
    }

    public void TestEvent2(string data)
    {
        LogUtil.Log("1"+data);
    }
}
