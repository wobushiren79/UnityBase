using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[InitializeOnLoad]
public class HierarchySelect
{
    static HierarchySelect()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyShowSelect;
    }

    //选择列表
    public static Dictionary<string, Component> dicSelectObj = new Dictionary<string, Component>();

    private static void OnHierarchyShowSelect(int instanceid, Rect selectionrect)
    {
        if (!EditorUtil.CheckIsPrefabMode(out var prefabStage))
        {
            dicSelectObj.Clear();
            return;
        }
        //获取当前obj
        var go = EditorUtility.InstanceIDToObject(instanceid) as GameObject;
        if (go == null)
            return;
        //控制开关
        var selectBox = new Rect(selectionrect);
        selectBox.x = selectBox.xMax;
        selectBox.width = 10;
        //检测是否选中
        bool hasGo = false;
        Component selectComonent = null;
        if (dicSelectObj.TryGetValue(go.name, out selectComonent))
        {
            hasGo = true;
        }
        hasGo = GUI.Toggle(selectBox, hasGo, string.Empty);
        if (hasGo)
        {
            if (!dicSelectObj.ContainsKey(go.name))
            {
                dicSelectObj.Add(go.name, null);
            }
        }
        else
        {
            if (dicSelectObj.ContainsKey(go.name))
            {
                dicSelectObj.Remove(go.name);
            }
        }
        //如果选中了
        if (hasGo)
        {
            //下拉选择
            var selectType = new Rect(selectionrect);
            selectType.x = selectBox.xMax - 170;
            selectType.width = 150;
            //获取该obj下所拥有的所有comnponent
            Component[] componentList = go.GetComponents<Component>();
            string[] listData = new string[componentList.Length];
            int selectComonentIndex = 0;
            //初始化所有可选component;
            for (int i = 0; i < componentList.Length; i++)
            {
                listData[i] = componentList[i].GetType().Name;
                if (selectComonent != null && selectComonent.GetType().Name.Equals(listData[i]))
                {
                    selectComonentIndex = i;
                }
            }
            //设置下拉数据
            selectComonentIndex = EditorGUI.Popup(selectType, selectComonentIndex, listData);
            //如果下拉数据改变
            dicSelectObj[go.name] = componentList[selectComonentIndex];
        }
    }
}