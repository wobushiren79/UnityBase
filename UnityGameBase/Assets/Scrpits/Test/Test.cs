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
    public Dictionary<Vector3, string> dicData;
    string[] listData;
    private void OnGUI()
    {
        if (GUILayout.Button("输出"))
        {
            Stopwatch stopwatch = TimeUtil.GetMethodTimeStart();
            for (int i = 0; i < 10000; i++)
            {
                string data1 = listData[50000];
            }
            TimeUtil.GetMethodTimeEnd("list", stopwatch);

            Stopwatch stopwatch2 = TimeUtil.GetMethodTimeStart();
            for (int i = 0; i < 10000; i++)
            {
                dicData.TryGetValue(new Vector3(55, 0, 0), out string data2);
            }
            TimeUtil.GetMethodTimeEnd("dicData", stopwatch2);
            LogUtil.Log("listData:" + listData.Length);
            LogUtil.Log("Count:" + dicData.Count);
        }
    }

    private void Start()
    {
        int cout = 9999999;
        dicData = new Dictionary<Vector3, string>();
        listData = new string[cout];
        for (int i = 0; i < cout; i++)
        {
            listData[i] = $"{i}";
            if (i < 100)
            {
                dicData.Add(new Vector3(i, 0, 0), $"{i}");
            }
        }
    }


}
